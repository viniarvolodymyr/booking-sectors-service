using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.Controllers;
using SoftServe.BookingSectors.WebAPI.Tests.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Tests.ControllersTests
{
    [TestFixture]
    class UserControllerTests
    {
        private readonly Mock<IUserService> userServiceMock;
        private readonly Mock<IRegistrationService> registrationServiceMock;
        private readonly UserController userController;
        List<UserDTO> usersContext;
        UserDTO userDTO;
        string newPass;
        string[] phones;

        public UserControllerTests()
        {
            userServiceMock = new Mock<IUserService>();
            registrationServiceMock = new Mock<IRegistrationService>();
            userController = new UserController(userServiceMock.Object, registrationServiceMock.Object);
        }

        [SetUp]
        public void SetUp()
        {
            usersContext = UserData.CreateUserDTOs();
            userDTO = UserData.CreateUserDTO();
        }

        [TearDown]
        public void TearDown()
        {
            usersContext.Clear();
        }

        [Test]
        public async Task GetAllUsers_InputIsUserData_ReturnsOk()
        {
            //Arrange
            userServiceMock.Setup(userService => userService.GetAllUsersAsync()).ReturnsAsync(usersContext);
            //Act
            var okResult = (await userController.Get()) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetUserById_InputIsUserData_ReturnsOk(int id)
        {
            //Arrange
            userServiceMock.Setup(userService => userService.GetUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => usersContext.Find(user => user.Id == id));
            //Act
            var okResult = (await userController.GetById(id)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        [TestCase("111")]
        [TestCase("222")]
        [TestCase("333")]
        public async Task GetUserByPhone_InputIsUserData_ReturnsOk(string phone)
        {
            //Arrange
            userServiceMock.Setup(userService => userService.GetUserByPhoneAsync(It.IsAny<string>()))
                .ReturnsAsync((string phone) => usersContext.Find(user => user.Phone == phone));
            //Act
            var okResult = (await userController.GetByPhone(phone)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
      

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UpdateUser_InputIsUserData_ReturnsOk(int id)
        {
            //Arrange
            userServiceMock.Setup(userService => userService.UpdateUserById(It.IsAny<int>(), It.IsAny<UserDTO>()))
                .ReturnsAsync((int id, UserDTO userDTO) =>
                {
                    userDTO.Id = id;
                    usersContext[usersContext.FindIndex(i => i.Id == id)] = userDTO;
                    return userDTO;
                });
            //Act
            var okResult = (await userController.UpdateUser(id, userDTO)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }


        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task CheckPass_InputIsUserData_ReturnsOk(int id)
        {
            //Arrange
            userServiceMock.Setup(userService => userService.CheckPasswords(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(( string pass, int id) =>
                {
                   pass = usersContext[usersContext.FindIndex(i => i.Id == id)].Password;
                    if (pass == "12435") { return true; }
                    else return false;
                   
                });
            //Act
            var okResult = (await userController.PasswordCheck("12345",id)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteUser_InputIsUserData_ReturnsOk(int id)
        {
            //Arrange
            userServiceMock.Setup(userService => userService.DeleteUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    var foundUser = usersContext.Find(user => user.Id == id);
                    usersContext.Remove(foundUser);
                    return foundUser;
                });
            int userContextLength = usersContext.Count;
            //Act
            var okResult = (await userController.Delete(id)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(userContextLength - 1, usersContext.Count);
        }


    }
}
