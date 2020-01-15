using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.BLL.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;


namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IRegistrationService registrationService;

        public UserController(IUserService userService, IRegistrationService registrationService)
        {
            this.userService = userService;
            this.registrationService = registrationService;
        }



        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var dtos = await userService.GetAllUsersAsync();

            if (dtos.Any())
            {
                return Ok(dtos);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById([FromRoute]int id)
        {
            var dto = await userService.GetUserByIdAsync(id);

            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dto);
            }
        }

        [HttpGet]
        [Route("phone/{phone}")]
        public async Task<ActionResult> GetByPhone([FromRoute]string phone)
        {
            var dto = await userService.GetUserByPhoneAsync(phone);

            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dto);
            }
        }

        [HttpGet]
        [Route("email/{email}")]
        public async Task<ActionResult<UserDTO>> GetByEmail([FromRoute]string email)
        {
            var dto = await registrationService.GetUserByEmailAsync(email);

            if (dto == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                   $"User with email: {email} not found when trying to get entity.");
            }

            return Ok(dto);
        }

        //[HttpGet]
        //[Route("UserPhoto/{id}")]
        //public async Task<string> GetPhotoById([FromRoute]int id)
        //{
        //    var photo = await userService.GetUserPhotoById(id);
        //    if (photo == null)
        //    {
        //        return null;
        //    }
        //    else return photo;
        //}

        [HttpGet]
        [Route("reset/{email}")]
        public async Task<IActionResult> ResetPassword([FromRoute]string email)
        {
            var selectedUser = await registrationService.GetUserByEmailAsync(email);

            if (selectedUser == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                    $"User with email: {email} not found when trying to get entity.");
            }

            bool isReset = await userService.ResetPassword(selectedUser);

            return isReset ?
                (IActionResult)Ok() :
                Conflict();
        }

        [HttpGet]
        [Route("{id}/{password}")]
        public async Task<IActionResult> PasswordCheck([FromRoute]string password, [FromRoute]int id)
        {
            var result = await userService.CheckPasswords(password, id);
            if (result == false)
            {
                return NotFound();
            }
            else return Ok(result);
        }




        [HttpPost]
        [ServiceFilter(typeof(ValidateModelState))]
        public async Task<IActionResult> Post([FromBody] UserDTO userDTO)
        {
            var dto = await registrationService.InsertUserAsync(userDTO);

            return dto == null ?
                (IActionResult)BadRequest() :
                Created($"api/users/{dto.Id}", dto);
        }



        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute]int id, [FromBody]UserDTO userDTO)
        {
            var dto = await userService.UpdateUserById(id, userDTO);

            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dto);
            }
        }

        [HttpPut]
        [Route("password/{id}")]
        public async Task<IActionResult> UpdateUserPass([FromRoute]int id, [BindRequired, FromQuery] string password)
        {
            var dto = await userService.UpdateUserPassById(id, password);

            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dto);
            }
        }

        [HttpPut]
        [Route("photo/{id}")]
        public async Task<IActionResult> UpdateUserPhoto([FromRoute]int id, [FromForm] IFormFile file)
        {
            var dto = await userService.UpdateUserPhotoById(id, file);

            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dto);
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var user = await userService.DeleteUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

    }
}

