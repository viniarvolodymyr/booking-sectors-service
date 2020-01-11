using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Configuration;
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
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            var dtos = await userService.GetAllUsersAsync();

            return !dtos.Any() ?
                (ActionResult<IEnumerable<UserDTO>>)NotFound() :
                Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserDTO>> GetById([FromRoute]int id)
        {
            var dto = await userService.GetUserByIdAsync(id);

            return dto == null ?
                (ActionResult<UserDTO>)NotFound() :
                Ok(dto);
        }

        [HttpGet]
        [Route("phone/{phone}")]
        public async Task<ActionResult<UserDTO>> GetByPhone([FromRoute]string phone)
        {
            var dto = await userService.GetUserByPhoneAsync(phone);

            return dto == null ?
                (ActionResult<UserDTO>)NotFound() :
                Ok(dto);
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

        [HttpGet]
        [Route("UserPhoto/{id}")]
        public async Task<string> GetPhotoById([FromRoute]int id)
        {
            return await userService.GetUserPhotoById(id);
        }

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
        public async Task<bool> PasswordCheck([FromRoute]string password, [FromRoute]int id)
        {
            return await userService.CheckPasswords(password, id);
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
            var user = await userService.UpdateUserById(id, userDTO);

            return user == null ?
                (IActionResult)NotFound() :
                Ok(user);
        }

        [HttpPut]
        [Route("password/{id}")]
        public async Task<IActionResult> UpdateUserPass([FromRoute]int id, [BindRequired, FromQuery] string password)
        {
            var user = await userService.UpdateUserPassById(id, password);

            return user == null ?
                (IActionResult)NotFound() :
                Ok(user);
        }

        [HttpPut]
        [Route("photo/{id}")]
        public async Task<IActionResult> UpdateUserPhoto([FromRoute]int id, [FromForm] IFormFile file)
        {
            var user = await userService.UpdateUserPhotoById(id, file);

            return user == null ?
                (IActionResult)NotFound() :
                Ok(user);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var user = await userService.DeleteUserByIdAsync(id);

            return user == null ?
                (IActionResult)NotFound() :
                Ok(user);
        }

    }
}

