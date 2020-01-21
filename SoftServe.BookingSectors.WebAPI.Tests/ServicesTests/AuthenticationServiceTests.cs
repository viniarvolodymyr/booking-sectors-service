using AutoMapper;
using Moq;
using NUnit.Framework;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.Jwt;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;
using SoftServe.BookingSectors.WebAPI.BLL.Services;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.Tests.Data;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests
{
    class AuthenticationServiceTests
    {
        readonly IUserService userService;
        readonly Mock<IUnitOfWork> unitOfWorkMock;
        readonly Mock<IBaseRepository<User>> userRepositoryMock;
        readonly Mock<ILoggerManager> logger;
        AuthenticationService authService;
        JwtFactory jwtFac;


        List<User> usersContext;

        public AuthenticationServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });
            logger = new Mock<ILoggerManager>();
            userRepositoryMock = new Mock<IBaseRepository<User>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(true);
            unitOfWorkMock.Setup(u => u.UserRepository).Returns(userRepositoryMock.Object);
            userService = new UserService(unitOfWorkMock.Object, config.CreateMapper(), logger.Object);
            authService = new AuthenticationService(unitOfWorkMock.Object, config.CreateMapper(), jwtFac);
        }

        [SetUp]
        public void SetUp()
        {
            usersContext = AuthenticationData.CreateUsers();
        }

        [TearDown]
        public void TearDown()
        {
            usersContext.Clear();
        }

        [Test]
        [TestCase(1, "12345")]
        [TestCase(2, "qwe123")]
        [TestCase(3, "blabla228")]
        [TestCase(4, "dhsrtjsh5yae")]
        [TestCase(5, "1345123")]
        [TestCase(6, "dxhsh34")]
        [TestCase(7, "ztfjrxh5rhrfxhxth")]
        public void IsSamePass(int id, string pass)
        {
            //Arrange
            User user = usersContext[id - 1];
            
            //Act
            var result = authService.IsPasswordTheSame(user, pass);
            //Assert
            Assert.IsTrue(result);
        }
    }
}
