using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;
namespace SoftServe.BookingSectors.WebAPI.Tests.ControllersTests.Data
{
    class UserData
    {
        public UserData() { }
        public List<UserDTO> Users { get; } = new List<UserDTO>()
        {
            new UserDTO
            {
                Id = 1,
                Firstname = "User 1",
                Lastname = "testUserSurname",
                Phone = "111",
                RoleId = 2,
                RoleName = "admin",
                Password = "12345",
                Email = "lilybekirova@google.com",
                Photo = null
            },
             new UserDTO
            {
                Id = 2,
                Firstname = "User 2",
                Lastname = "testUserSurname",
                Phone = "222",
                RoleId = 2,
                RoleName = "admin",
                Password = "12345",
                Email = "test@google.com",
                Photo = null
            },
              new UserDTO
            {
                Id = 3,
                Firstname = "User 3",
                Lastname = "testUserSurname",
                Phone = "333",
                RoleId = 2,
                RoleName = "admin",
                Password = "12345",
                Email = "test@google.com",
                Photo = null
            },
        };
        public UserDTO UserDTOToInsert { get; } = new UserDTO()
        {
            Id = 4,
            Firstname = "User 4",
            Lastname = "testUserSurname",
            Phone = "9999999999",
            RoleId = 2,
            RoleName = "admin",
            Password = "12345",
            Email = "test@google.com",
            Photo = null
        };

        public string newPassword = "123456";
        public string[] phones = { "111", "222", "333" };
        public string phone = "9999999999";
    }
}
