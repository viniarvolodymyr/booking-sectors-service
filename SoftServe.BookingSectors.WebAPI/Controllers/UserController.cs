//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SoftServe.BookingSectors.WebAPI.Repositories;
//using SoftServe.BookingSectors.WebAPI.Models;
//using System.Text;

//namespace SoftServe.BookingSectors.WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {

//        GenericRepository<User> repository = null;

//        public UserController()
//        {
//            repository = new GenericRepository<User>();
//        }

//        // GET: api/User
//        [HttpGet]
//        public IEnumerable<User> Get()
//        {
//            return repository.GetAll();
//        }

//        // GET: api/User/5
//        [HttpGet("{id}")]
//        public User GetByID(int id)
//        {
//            return repository.GetById(id);
//        }

//        //GET: api/User/Phone/3212321
//        [HttpGet("Phone/{phone}", Name = "GetBookingByPhone")]
//        public int Get(string phone)
//        {
//            return repository.GetAll()
//                            .Where(u => u.Phone == phone)
//                                .Select(u => u.Id)
//                                    .FirstOrDefault();
//        }

//        // PUT: api/User/5
//        [HttpPut("{id}")]
//        public void PutUser(int id, string firstName, string lastName, string phone, string password)
//        {
//            User user = repository.GetById(id);
//            user.Firstname = firstName;
//            user.Lastname = lastName;
//            user.Phone = phone;
//            user.Password = Encoding.ASCII.GetBytes(password);
//            repository.Update(user);
//            repository.Save();
//        }
//    }
//}
