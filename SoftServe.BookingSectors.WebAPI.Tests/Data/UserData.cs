using System;
using System.Collections.Generic;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;

namespace SoftServe.BookingSectors.WebAPI.Tests.Data
{
    class UserData
    {
        public UserData() { }
        public List<User> Users { get; } = new List<User>()
        {
            new User
            {
                Id = 1,
                Firstname = "User 1",
                Lastname = "testUserSurname",
                Phone = "9999999999",
                RoleId = 2,
                Password = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                Email = "test@google.com",
                Photo = null,
                CreateDate = new DateTime(2019, 12, 28, 10, 20, 0),
                CreateUserId = 1,
                ModDate = new DateTime(2019, 12, 28, 10, 30, 0),
                ModUserId = 2
            },
             new User
            {
                Id = 2,
                Firstname = "User 2",
                Lastname = "testUserSurname",
                Phone = "9999999999",
                RoleId = 2,
                Password = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                Email = "test@google.com",
                Photo = null,
                CreateDate = new DateTime(2019, 12, 28, 10, 20, 0),
                CreateUserId = 1,
                ModDate = new DateTime(2019, 12, 28, 10, 30, 0),
                ModUserId = 2
            },
          new User
            {
                Id = 3,
                Firstname = "User 3",
                Lastname = "testUserSurname",
                Phone = "9999999999",
                RoleId = 2,
                Password = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                Email = "test@google.com",
                Photo = null,
                CreateDate = new DateTime(2019, 12, 28, 10, 20, 0),
                CreateUserId = 1,
                ModDate = new DateTime(2019, 12, 28, 10, 30, 0),
                ModUserId = 2
            }
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

        public string newPassword = "12345";
        public string phone = "9999999999";
    
}
}
