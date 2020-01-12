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

        [Test]
        public async Task InsertSector_InputIsSectorData_ReturnsCreated()
        {
            //Arrange
            sectorServiceMock.Setup(r => r.InsertSectorAsync(It.IsAny<SectorDTO>()))
                .ReturnsAsync((SectorDTO s) =>
                {
                    sectorsContext.Add(s);
                    return s;
                });
            int sectorContextLength = sectorsContext.Count;
            //Act
            var result = await sectorController.Post(sectorDTO);
            var createdResult = result as CreatedResult;
            //Assert
            Assert.IsNotNull(result);
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
            sectorServiceMock.Setup(r => r.UpdateSectorAsync(It.IsAny<int>(), It.IsAny<SectorDTO>()))
                .ReturnsAsync((int id, SectorDTO s) =>
                {
                    s.Id = id;
                    sectorsContext[sectorsContext.FindIndex(i => i.Id == id)] = s;
                    return s;
                });
            //Act
            var result = await sectorController.Put(id, sectorDTO);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
