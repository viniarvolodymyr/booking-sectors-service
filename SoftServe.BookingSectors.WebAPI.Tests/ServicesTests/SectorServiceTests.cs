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
using SoftServe.BookingSectors.WebAPI.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests
{
    [TestFixture]
    class SectorServiceTests
    {
        readonly ISectorService sectorService;
        readonly Mock<IUnitOfWork> unitOfWorkMock;
        readonly Mock<IBaseRepository<Sector>> sectorRepositoryMock;
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
            unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(true);
            unitOfWorkMock.Setup(u => u.SectorRepository).Returns(sectorRepositoryMock.Object);
            sectorService = new SectorService(unitOfWorkMock.Object, config.CreateMapper());
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
        public async Task GetAllSectors_SectorDataSectors_AllSectorsReturned()
        {
            //Arrange
            sectorRepositoryMock.Setup(r => r.GetAllEntitiesAsync()).ReturnsAsync(sectorsContext);
            //Act
            var results = await sectorService.GetSectorsAsync() as List<SectorDTO>;
            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(sectorsContext.Count, results.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetSectorById_SectorDataSectors_OneSectorReturned(int id)
        {
            //Arrange
            sectorRepositoryMock.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => sectorsContext.Find(s => s.Id == id));
            //Act
            var result = await sectorService.GetSectorByIdAsync(id);
            if (result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Sector with id: {id} not found when trying to get sector.");
            }
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(sectorsContext[id - 1].Id, result.Id);
        }

        [Test]
        public async Task InsertSector_SectorData_SectorInserted()
        {
            //Arrange
            sectorRepositoryMock.Setup(r => r.InsertEntityAsync(It.IsAny<Sector>()))
                .ReturnsAsync((Sector s) =>
                {
                    s.Id = sectorDTO.Id;
                    sectorsContext.Add(s);
                    return s;
                });
            //Act
            var result = await sectorService.InsertSectorAsync(sectorDTO);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(sectorDTO.Id, result.Id);
        }

        [Test]
        [TestCase(1)]
        public async Task UpdateSector_SectorData_SectorUpdated(int id)
        {
            //Arrange
            sectorRepositoryMock.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => sectorsContext.Find(s => s.Id == id));
            sectorRepositoryMock.Setup(r => r.UpdateEntity(It.IsAny<Sector>()));
            //Act
            var result = await sectorService.UpdateSectorAsync(id, sectorDTO); 
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteSector_SectorData_SectorDeleted(int id)
        {
            //Arrange
            sectorRepositoryMock.Setup(r => r.DeleteEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    var founded = sectorsContext.Find(s => s.Id == id);
                    if (founded == null)
                    {
                        throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Sector with id: {id} not found when trying to delete sector. Sector wasn't deleted.");
                    }
                    sectorsContext.Remove(founded);
                    return founded;
                });
            int contextLength = sectorsContext.Count;
            //Act
            var result = await sectorService.DeleteSectorByIdAsync(id);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(contextLength - 1, sectorsContext.Count);
        }
    }
}
