using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.BLL.Filters;
using Microsoft.AspNetCore.Authorization;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            var dtos = await userService.GetAllUsersAsync();
            if (!dtos.Any())
            {
                return NotFound();
            }
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<UserDTO>> GetById([FromRoute]int id)
        {
            var dto = await userService.GetUserByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpGet]
        [Route("phone/{phone}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<UserDTO>> GetByPhone([FromRoute]string phone)
        {
            var dto = await userService.GetUserByPhoneAsync(phone);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpGet]
        [Route("{id}/{password}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<bool> PasswordCheck([FromRoute]string password, [FromRoute]int id)
        {
            bool result = await userService.CheckPasswords(password, id);
            return result;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelState))]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] UserDTO userDTO)
        {
            var dto = await userService.InsertUserAsync(userDTO);

            if (dto == null)		
             {		
                 return BadRequest();		
             }		
             else		
             {		
                 return Created($"api/users/{dto.Id}", dto);		            
             }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateUser([FromRoute]int id, [FromBody]UserDTO userDTO)
        {
            var user = await userService.UpdateUserById(id, userDTO);
            if (user== null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
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

