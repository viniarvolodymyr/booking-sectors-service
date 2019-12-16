using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<UserDTO>> GetByPhone([FromRoute]string phone)
        {
            var dto = await userService.GetUserByPhoneAsync(phone);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPost]
        public async Task Post([FromBody] UserDTO userDTO)
        {
            await userService.InsertUserAsync(userDTO);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task UpdateUser([FromRoute]int id, [FromBody]UserDTO userDTO)
        {
            await userService.UpdateUserById(id, userDTO);
        }
    }
}
