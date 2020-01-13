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
    [TestFixture]
    class SectorControllerTests
    {
        private readonly Mock<ISectorService> sectorServiceMock;
        private readonly Mock<IBookingSectorService> bookingSectorServiceMock;
        private readonly SectorController sectorController;
        List<SectorDTO> sectorsContext;
        SectorDTO sectorDTO;

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
            sectorDTO = sectorData.SectorDTOToInsert;
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
            sectorServiceMock.Setup(sectorService => sectorService.GetSectorsAsync()).ReturnsAsync(sectorsContext);
            //Act
            var okResult = (await sectorController.Get()) as OkObjectResult;
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
            sectorServiceMock.Setup(sectorService => sectorService.GetSectorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => sectorsContext.Find(sector => sector.Id == id));
            //Act
            var okResult = (await sectorController.Get(id)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task InsertSector_InputIsSectorData_ReturnsCreated()
        {
            //Arrange
            sectorServiceMock.Setup(sectorService => sectorService.InsertSectorAsync(It.IsAny<SectorDTO>()))
                .ReturnsAsync((SectorDTO sectorDTO) =>
                {
                    sectorsContext.Add(sectorDTO);
                    return sectorDTO;
                });
            int sectorContextLength = sectorsContext.Count;
            //Act
            var createdResult = (await sectorController.Post(sectorDTO)) as CreatedResult;
            //Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(sectorContextLength + 1, sectorsContext.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UpdateSector_InputIsSectorData_ReturnsOk(int id)
        {
            //Arrange
            sectorServiceMock.Setup(sectorService => sectorService.UpdateSectorAsync(It.IsAny<int>(), It.IsAny<SectorDTO>()))
                .ReturnsAsync((int id, SectorDTO sectorDTO) =>
                {
                    sectorDTO.Id = id;
                    sectorsContext[sectorsContext.FindIndex(i => i.Id == id)] = sectorDTO;
                    return sectorDTO;
                });
            //Act
            var okResult = (await sectorController.Put(id, sectorDTO)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteSector_InputIsSectorData_ReturnsOk(int id)
        {
            //Arrange
            sectorServiceMock.Setup(sectorService => sectorService.DeleteSectorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    var foundSector = sectorsContext.Find(sector => sector.Id == id);
                    sectorsContext.Remove(foundSector);
                    return foundSector;
                });
            int sectorContextLength = sectorsContext.Count;
            //Act
            var okResult = (await sectorController.Delete(id)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(sectorContextLength - 1, sectorsContext.Count);
        }
    }
}