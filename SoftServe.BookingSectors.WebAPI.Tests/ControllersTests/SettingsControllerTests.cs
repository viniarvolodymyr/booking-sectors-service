using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.Controllers;
using SoftServe.BookingSectors.WebAPI.Tests.ControllersTests.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Tests.ControllersTests
{
    class SettingsControllerTests
    {
        private readonly Mock<ISettingsService> settingsServiceMock;
        private readonly SettingsController settingsController;
        List<SettingsDTO> settingsContext;
        SettingsDTO settingsDTO;
        SettingsControllerTests()
        {
            settingsServiceMock = new Mock<ISettingsService>();
            settingsController = new SettingsController(settingsServiceMock.Object);
        }
        [SetUp]
        public void SetUp()
        {
            SettingsData settingsData = new SettingsData();
            settingsContext = settingsData.SettingsDTO;
            settingsDTO = settingsData.settingToInsert;
        }
        [TearDown]
        public void TearDown()
        {
            settingsContext.Clear();
        }
        [Test]
        public async Task GetAllSectors_InputIsSettingsData_ReturnsOk()
        {
            //Arrange
            settingsServiceMock.Setup(settingsService => settingsService.GetSettingsAsync())
                .ReturnsAsync(settingsContext);
            //Act
            var okResult = (await settingsController.Get()) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetSettingById_InputIsSettingsData_ReturnsOk(int id)
        {
            //Arrange
            settingsServiceMock.Setup(settingsService => settingsService
                .GetSettingByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => settingsContext.Find(setting => setting.Id == id));
            //Act
            var okResult = (await settingsController.Get(id)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task UpdateSetting_InputIsSettingData_ReturnsOk(int id)
        {
            //Arrange
            settingsServiceMock.Setup(settingsService => settingsService
                .UpdateSettingsAsync(It.IsAny<int>(), It.IsAny<SettingsDTO>()))
                .ReturnsAsync((int id, SettingsDTO settingsDTO) =>
                {
                    settingsDTO.Id = id;
                    settingsContext[settingsContext.FindIndex(i => i.Id == id)] = settingsDTO;
                    return settingsDTO;
                });
            //Act
            var okResult = (await settingsController.Put(id, settingsDTO)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
