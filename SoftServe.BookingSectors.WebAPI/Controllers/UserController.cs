using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;


namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService userService;
        public UserController(IUserService service)
        {
            userService = service;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var dto = await userService.GetUserByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }
       

        //GET: api/User/Phone/3212321
        [HttpGet("Phone/{phone}", Name = "GetUserByPhone")]
        public async Task<ActionResult<UserDTO>> GetByPhone(string phone)
        {
            var dto = await userService.GetUserByPhoneAsync(phone);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);

         }

        // PUT: api/User/5
        //[HttpPut("{id}")]
        //public async void PutUser(int id, string firstName, string lastName, string phone, string password)
        //{
        //    User user = repository.GetById(id);
        //    user.Firstname = firstName;
        //    user.Lastname = lastName;
        //    user.Phone = phone;
        //    user.Password = Encoding.ASCII.GetBytes(password);
        //    repository.Update(user);
        //    repository.Save();
        //}
    }
}
