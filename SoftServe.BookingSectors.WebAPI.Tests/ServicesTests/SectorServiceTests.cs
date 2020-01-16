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
using SoftServe.BookingSectors.WebAPI.Tests.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests
{
    [TestFixture]
    class SectorServiceTests
    {
        private readonly ISectorService sectorService;
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IBaseRepository<Sector>> sectorRepositoryMock;
        List<Sector> sectorsContext;
        SectorDTO sectorDTO;

        public SectorServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SectorProfile>();
            });
            sectorRepositoryMock = new Mock<IBaseRepository<Sector>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.SaveAsync()).ReturnsAsync(true);
            unitOfWorkMock.Setup(uow => uow.SectorRepository).Returns(sectorRepositoryMock.Object);
            sectorService = new SectorService(unitOfWorkMock.Object, config.CreateMapper());
        }

        [SetUp]
        public void SetUp()
        {
            sectorsContext = SectorData.CreateSectors();
            sectorDTO = SectorData.CreateSectorDTO();
        }

        [TearDown]
        public void TearDown()
        {
            sectorsContext.Clear();
        }

        [Test]
        public async Task GetAllSectors_InputIsSectorData_AllSectorsReturned()
        {
            //Arrange
            sectorRepositoryMock.Setup(sectorRepository => sectorRepository.GetAllEntitiesAsync()).ReturnsAsync(sectorsContext);
            //Act
            var resultSectorDTOs = (await sectorService.GetSectorsAsync()) as List<SectorDTO>;
            //Assert
            Assert.IsNotNull(resultSectorDTOs);
            Assert.AreEqual(sectorsContext.Count, resultSectorDTOs.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetSectorById_InputIsSectorData_OneSectorReturned(int id)
        {
            //Arrange
            sectorRepositoryMock.Setup(sectorRepository => sectorRepository.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => sectorsContext.Find(sector => sector.Id == id));
            //Act
            var resultSectorDTO = (await sectorService.GetSectorByIdAsync(id)) as SectorDTO;
            //Assert
            Assert.IsNotNull(resultSectorDTO);
            Assert.AreEqual(sectorsContext[id - 1].Id, resultSectorDTO.Id);
        }

        [Test]
        public async Task InsertSector_InputIsSectorData_OneSectorInserted()
        {
            //Arrange
            sectorRepositoryMock.Setup(sectorRepository => sectorRepository.InsertEntityAsync(It.IsAny<Sector>()))
                .ReturnsAsync((Sector sector) =>
                {
                    sector.Id = sectorDTO.Id;
                    sectorsContext.Add(sector);
                    return sector;
                });
            int sectorContextLength = sectorsContext.Count;
            //Act
            var resultSectorDTO = (await sectorService.InsertSectorAsync(sectorDTO)) as SectorDTO;
            //Assert
            Assert.IsNotNull(resultSectorDTO);
            Assert.AreEqual(sectorDTO.Id, resultSectorDTO.Id);
            Assert.AreEqual(sectorContextLength + 1, sectorsContext.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UpdateSector_InputIsSectorData_OneSectorUpdated(int id)
        {
            //Arrange
            sectorRepositoryMock.Setup(sectorRepository => sectorRepository.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => sectorsContext.Find(sector => sector.Id == id));
            sectorRepositoryMock.Setup(sectorRepository => sectorRepository.UpdateEntity(It.IsAny<Sector>()))
                .Returns((Sector sector) =>
                {
                    sectorsContext[sectorsContext.FindIndex(i => i.Id == sector.Id)] = sector;
                    return sector;
                });
            //Act
            var resultSectorDTO = (await sectorService.UpdateSectorAsync(id, sectorDTO)) as SectorDTO;
            //Assert
            Assert.IsNotNull(resultSectorDTO);
            Assert.AreEqual(sectorsContext[id - 1].Id, resultSectorDTO.Id);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteSector_InputIsSectorData_OneSectorDeleted(int id)
        {
            //Arrange
            sectorRepositoryMock.Setup(sectorRepository => sectorRepository.DeleteEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    var foundSector = sectorsContext.Find(sector => sector.Id == id);
                    sectorsContext.Remove(foundSector);
                    return foundSector;
                });
            int sectorContextLength = sectorsContext.Count;
            //Act
            var resultSectorDTO = (await sectorService.DeleteSectorByIdAsync(id)) as SectorDTO;
            //Assert
            Assert.IsNotNull(resultSectorDTO);
            Assert.AreEqual(sectorContextLength - 1, sectorsContext.Count);
        }
    }
}