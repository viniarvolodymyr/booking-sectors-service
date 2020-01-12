using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;
using SoftServe.BookingSectors.WebAPI.BLL.Services;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.Controllers;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.Tests.ControllersTests.Data;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
namespace SoftServe.BookingSectors.WebAPI.Tests.ControllersTests
{
    [TestFixture]
    class SectorControllerTests
    {
        private Mock<ISectorService> sectorServiceMock;
        private Mock<IBookingSectorService> bookingSectorServiceMock;
        SectorController sectorController;
        List<SectorDTO> sectorsContext;

        public SectorControllerTests()
        {
            sectorServiceMock = new Mock<ISectorService>();
            bookingSectorServiceMock = new Mock<IBookingSectorService>();
            sectorController = new SectorController(sectorServiceMock.Object, bookingSectorServiceMock.Object);
        }
        [SetUp]
        public void SetUp()
        {
            SectorData sectorData = new SectorData();
            sectorsContext = sectorData.Sectors;
        }

        [TearDown]
        public void TearDown()
        {
            sectorsContext.Clear();
        }

        [Test]
        public async Task GetAllSectors_InputIsSectorData_ReturnsOk()
        {
            //Arrange
            sectorServiceMock.Setup(x => x.GetSectorsAsync()).ReturnsAsync(sectorsContext);
            //Act
            var result = await sectorController.Get();
            var okResult = result as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetSectorById_InputIsSectorData_ReturnsOk(int id)
        {
            //Arrange
            sectorServiceMock.Setup(x => x.GetSectorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => sectorsContext.Find(s => s.Id == id));
            //Act
            var result = await sectorController.Get(id);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
