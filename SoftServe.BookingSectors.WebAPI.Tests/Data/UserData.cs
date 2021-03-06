﻿using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.Tests.Data
{
    public static class UserData
    {
        private static MapperConfiguration mapperConfiguration;
        private static IMapper mapper;
        static UserData()
        {
            mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<UserProfile>();
            });
            mapper = mapperConfiguration.CreateMapper();
        }

        public static List<UserDTO> CreateUserDTOs()
        {
            return mapper.Map<List<User>, List<UserDTO>>(CreateUsers());
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
                    Phone = "111",
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
                    Phone = "222",
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
                    Phone = "333",
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
        }
        public static UserDTO CreateUserDTO()
        {
            return new UserDTO()
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
        }
    }
}
