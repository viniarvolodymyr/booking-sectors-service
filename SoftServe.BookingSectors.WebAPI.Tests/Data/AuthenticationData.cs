using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.Tests.Data
{
    class AuthenticationData
    {
        private static MapperConfiguration mapperConfiguration;
        private static IMapper mapper;
        static AuthenticationData()
        {
            mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<UserProfile>();
            });
            mapper = mapperConfiguration.CreateMapper();
        }

        public static List<User> CreateUsers()
        {
            return new List<User>()
            {
                new User
                {
                    Id = 1,
                    Firstname = "User 1",
                    Lastname = "testUserSurname",
                    Phone = "8888888888",
                    RoleId = 2,
                    Password = SHA256Hash.Compute("12345"),
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
                    Phone = "7777777777",
                    RoleId = 2,
                    Password = SHA256Hash.Compute("qwe123"),
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
                    Password = SHA256Hash.Compute("blabla228"),
                    Email = "test@google.com",
                    Photo = null,
                    CreateDate = new DateTime(2019, 12, 28, 10, 20, 0),
                    CreateUserId = 1,
                    ModDate = new DateTime(2019, 12, 28, 10, 30, 0),
                    ModUserId = 2
                },

                 new User
                {
                    Id = 4,
                    Firstname = "User 4",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                    Password = SHA256Hash.Compute("dhsrtjsh5yae"),
                    Email = "test@google.com",
                    Photo = null,
                    CreateDate = new DateTime(2019, 12, 28, 10, 20, 0),
                    CreateUserId = 1,
                    ModDate = new DateTime(2019, 12, 28, 10, 30, 0),
                    ModUserId = 2
                },

                  new User
                {
                    Id = 5,
                    Firstname = "User 5",
                    Lastname = "testUserSurname123",
                    Phone = "6666666666",
                    RoleId = 2,
                    Password = SHA256Hash.Compute("1345123"),
                    Email = "test@google.com",
                    Photo = null,
                    CreateDate = new DateTime(2019, 12, 28, 10, 20, 0),
                    CreateUserId = 1,
                    ModDate = new DateTime(2019, 12, 28, 10, 30, 0),
                    ModUserId = 2
                },

                   new User
                {
                    Id = 6,
                    Firstname = "User 6",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                    Password = SHA256Hash.Compute("dxhsh34"),
                    Email = "test@google.com",
                    Photo = null,
                    CreateDate = new DateTime(2019, 12, 28, 10, 20, 0),
                    CreateUserId = 1,
                    ModDate = new DateTime(2019, 12, 28, 10, 30, 0),
                    ModUserId = 2
                },

                    new User
                {
                    Id = 7,
                    Firstname = "User 7",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                    Password = SHA256Hash.Compute("ztfjrxh5rhrfxhxth"),
                    Email = "test@google.com",
                    Photo = null,
                    CreateDate = new DateTime(2019, 12, 28, 10, 20, 0),
                    CreateUserId = 1,
                    ModDate = new DateTime(2019, 12, 28, 10, 30, 0),
                    ModUserId = 2
                }
            };
        }
    }
}
