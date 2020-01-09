using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.BLL.Services;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Moq;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories;
using SoftServe.BookingSectors.WebAPI.Controllers;
namespace SoftServe.BookingSectors.WebAPI.Tests.ControllersTests
{
    class UserControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<IRegistrationService> _regServiceMock;
        UserController objController;
        Task<IEnumerable<UserDTO>> listCountry;

        [SetUp]
        public void SetUp()
        {
            _userServiceMock = new Mock<IUserService>();
            _regServiceMock = new Mock<IRegistrationService>();
            objController = new UserController(_userServiceMock.Object, _regServiceMock.Object);
        }
      
     

        [Test]
        public void Country_Get_All()
        {
            //Arrange
            _userServiceMock.Setup(x => x.GetAllUsersAsync()).Returns(listCountry);

            //Act
            var result = objController.Get();
            Assert.IsNotNull(result);
            //IEnumerable<UserDTO> userDTOs = result.Result;
            ////Assert
            //Assert.AreEqual(result.Count, 3);
            //Assert.AreEqual("US", result[0].Name);
            //Assert.AreEqual("India", result[1].Name);
            //Assert.AreEqual("Russia", result[2].Name);

        }
      

    }
}
