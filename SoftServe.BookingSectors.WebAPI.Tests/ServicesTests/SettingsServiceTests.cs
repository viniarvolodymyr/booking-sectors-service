using AutoMapper;
using Moq;
using NUnit.Framework;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;
using SoftServe.BookingSectors.WebAPI.BLL.Services;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.Tests.ServicesTests.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests
{
    [TestFixture]
    class SettingsServiceTests
    {
        private readonly ISettingsService settingsService;
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IBaseRepository<Setting>> settingsRepositoryMock;
        List<Setting> settingsContext;
        SettingsDTO settingsDTO;
        public SettingsServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SettingProfile>();
            });
            settingsRepositoryMock = new Mock<IBaseRepository<Setting>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.SaveAsync()).ReturnsAsync(true);
            unitOfWorkMock.Setup(uow => uow.SettingRepository).Returns(settingsRepositoryMock.Object);
            settingsService = new SettingsService(unitOfWorkMock.Object, config.CreateMapper());
        }
        [SetUp]
        public void Setup()
        {
            SettingsData settingsData = new SettingsData();
            settingsContext = settingsData.CreateSettings();
            settingsDTO = settingsData.CreateSettingDTO();
        }
        [TearDown]
        public void TearDown()
        {
            settingsContext.Clear();
        }
        [Test]
        public async Task GetAllSettings_InputIsSettingsData_AllSettingsReturned()
        {
            //Arrange
            settingsRepositoryMock.Setup(settingsRepository => settingsRepository.GetAllEntitiesAsync())
                    .ReturnsAsync(settingsContext);
            //Act
            var resultSettingsDTOs = (await settingsService.GetSettingsAsync()) as List<SectorDTO>;
            //Assert
            Assert.IsNotNull(resultSettingsDTOs);
            Assert.AreEqual(settingsContext.Count, resultSettingsDTOs.Count);
        }
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetSettigById_InputIsSettingData_OneSettingReturned(int id)
        {
            //Arrange
            settingsRepositoryMock.Setup(settingsRepository => settingsRepository
                .GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => settingsContext.Find(setting => setting.Id == id));
            //Act
            var resultSettingsDTO = (await settingsService.GetSettingByIdAsync(id)) as SettingsDTO;
            //Assert
            Assert.IsNotNull(resultSettingsDTO);
            Assert.AreEqual(settingsContext[id - 1].Id, resultSettingsDTO.Id);
        }
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task UpdateSetting_InputIsSettingData_OneSettingUpdated(int id)
        {
            //Arrange
            settingsRepositoryMock.Setup(sectorRepository => sectorRepository.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => settingsContext.Find(setting => setting.Id == id));
            settingsRepositoryMock.Setup(sectorRepository => sectorRepository.UpdateEntity(It.IsAny<Setting>()))
                .Returns((Setting setting) =>
                {
                    settingsContext[settingsContext.FindIndex(i => i.Id == setting.Id)] = setting;
                    return setting;
                });
            //Act
            var resultSettingDTO = (await settingsService.UpdateSettingsAsync(id, settingsDTO)) as SettingsDTO;
            //Assert
            Assert.IsNotNull(resultSettingDTO);
            Assert.AreEqual(settingsContext[id - 1].Id, resultSettingDTO.Id);
        }
    }
}
