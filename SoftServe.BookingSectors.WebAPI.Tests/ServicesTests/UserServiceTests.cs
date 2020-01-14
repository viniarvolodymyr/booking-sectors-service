using AutoMapper;
using Moq;
using NUnit.Framework;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;
using SoftServe.BookingSectors.WebAPI.BLL.Services;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.Tests.Data;

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests
{
    [TestFixture]
    class UserServiceTests
    {
            readonly IUserService userService;
            readonly Mock<IUnitOfWork> unitOfWorkMock;
            readonly Mock<IBaseRepository<User>> userRepositoryMock;
            readonly Mock<ILoggerManager> logger;

        List<User> usersContext;
        UserDTO userDTO;
        string newPass;
        string phone;

            public UserServiceTests()
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
            }

            [SetUp]
            public void SetUp()
            {
                usersContext = UserData.CreateUsers();
                userDTO = UserData.CreateUserDTO();
                //newPass = userData.newPassword; //#TODO: Add this field as static methods to UserData static class
                //phone = userData.phone;         //#TODO: Add this field as static methods to UserData static class
            }

        [TearDown]
            public void TearDown()
            {
                usersContext.Clear();
            }

            [Test]
            public async Task GetAllUsers_InputIsUserData_AllUsersReturned()
            {
                //Arrange
                userRepositoryMock.Setup(r => r.GetAllEntitiesAsync()).ReturnsAsync(usersContext);
                //Act
                var results = await userService.GetAllUsersAsync() as List<UserDTO>;
                //Assert
                Assert.IsNotNull(results);
                Assert.AreEqual(usersContext.Count, results.Count);
            }

            [Test]
            [TestCase(1)]
            [TestCase(2)]
            [TestCase(3)]
            public async Task GetUserById_InputIsUserData_OneUserReturned(int id)
            {
                //Arrange
                userRepositoryMock.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((int id) => usersContext.Find(s => s.Id == id));
                //Act
                var result = await userService.GetUserByIdAsync(id);
                if (result == null)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                        $"User with id: {id} not found when trying to get user.");
                }
                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(usersContext[id - 1].Id, result.Id);
            }

            [Test]
            [TestCase(1)]
            [TestCase(2)]
            [TestCase(3)]
            public async Task GetUserByPhone_InputIsUserData_OneUserReturned(int id)
            {
                //Arrange
                userRepositoryMock.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((int id) => usersContext.Find(s => s.Id == id));
                //Act
                var result = await userService.GetUserByPhoneAsync(phone);
                if (result == null)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                        $"User with phone: {id} not found when trying to get user.");
                }
                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(usersContext[id - 1], result.Id);
            }

            [Test]
            public async Task InsertUser_InputIsUserData_OneUserInserted()
            {
                //Arrange
                userRepositoryMock.Setup(r => r.InsertEntityAsync(It.IsAny<User>()))
                    .ReturnsAsync((User u) =>
                    {
                        u.Id = userDTO.Id;
                        usersContext.Add(u);
                        return u;
                    });
                //Act
             //   var result = await userService.InsertUserAsync(userDTO);
                //Assert
              //  Assert.IsNotNull(result);
                //Assert.AreEqual(userDTO.Id, result.Id);
            }

            [Test]
            [TestCase(1)]
            [TestCase(2)]
            [TestCase(3)]
            public async Task UpdateUser_InputIsUserData_OneUserUpdated(int id)
            {
                //Arrange
                userRepositoryMock.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((int id) => usersContext.Find(u => u.Id == id));
                userRepositoryMock.Setup(r => r.UpdateEntity(It.IsAny<User>()))
                    .Returns((User u) =>
                    {
                        usersContext[usersContext.FindIndex(i => i.Id == u.Id)] = u;
                        return u;
                    });
                //Act
                 var result = await userService.UpdateUserById(id, userDTO);
                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(usersContext[id - 1].Id, result.Id);
            }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UpdateUserPass_InputIsUserData_OneUserUpdated(int id)
        {
            //Arrange
            userRepositoryMock.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => usersContext.Find(u => u.Id == id));
            userRepositoryMock.Setup(r => r.UpdateEntity(It.IsAny<User>()))
                .Returns((User u) =>
                {
                    usersContext[usersContext.FindIndex(i => i.Id == u.Id)] = u;
                    return u;
                });
            //Act
            var result = await userService.UpdateUserPassById(id, newPass);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(usersContext[id - 1].Id, result.Id);
        }

        [Test]
        public async Task CheckPassword_InputIsUserData(int id)
        {
            //Arrange
            userRepositoryMock.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => usersContext.Find(s => s.Id == id));
            //Act
            var result = await userService.CheckPasswords(newPass, id);
            if (result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                    $"User with id: {id} not found when trying to get user.");
            }
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }

        [Test]
            [TestCase(1)]
            [TestCase(2)]
            [TestCase(3)]
            public async Task DeleteUser_InputIsUserData_OneUserDeleted(int id)
            {
                //Arrange
                userRepositoryMock.Setup(r => r.DeleteEntityByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((int id) =>
                    {
                        var foundUser = usersContext.Find(s => s.Id == id);
                        if (foundUser == null)
                        {
                            throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                                $"User with id: {id} not found when trying to delete user. User wasn't deleted.");
                        }
                        usersContext.Remove(foundUser);
                        return foundUser;
                    });
                int userContextLength = usersContext.Count;
                //Act
                var result = await userService.DeleteUserByIdAsync(id);
                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(userContextLength - 1, usersContext.Count);
            }

            
        }
}
