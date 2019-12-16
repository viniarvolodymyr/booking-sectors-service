using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;

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
        [Route("all")]
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


        //GET: api/User/Phone/3212321
        [HttpGet]
        [Route("Phone/{phone}")]
        public async Task<ActionResult<UserDTO>> GetByPhone([FromRoute]string phone)
        {
            var dto = await userService.GetUserByPhoneAsync(phone);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);

        }

        // PUT: api/User/5
        [HttpPut]
        [Route("{id}")]
        public async Task UpdateUser([FromRoute]int id, [FromBody] UserDTO userDTO)
        {
           await userService.UpdateUserById(id,userDTO);
        }
    }
}
