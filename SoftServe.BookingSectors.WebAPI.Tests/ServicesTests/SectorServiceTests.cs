﻿using AutoMapper;
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
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests
{
    [TestFixture]
    class SectorServiceTests
    {
        readonly ISectorService sectorService;
        readonly Mock<IUnitOfWork> unitOfWork;
        readonly Mock<IBaseRepository<Sector>> repository;
        List<Sector> sectorsContext;
        SectorDTO sectorDTO;
        public SectorServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SectorProfile>();
            });
            repository = new Mock<IBaseRepository<Sector>>();
            unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(true);
            unitOfWork.Setup(u => u.SectorRepository).Returns(repository.Object);
            sectorService = new SectorService(unitOfWork.Object, config.CreateMapper());
        }

        [SetUp]
        public void SetUp()
        {
            sectorsContext = SectorData.Sectors;
            sectorDTO = SectorData.SectorDTOToInsert;
        }

        [Test]
        public async Task GetAllSectors_SectorDataSectors_AllSectorsReturned()
        {
            //Arrange
            repository.Setup(r => r.GetAllEntitiesAsync()).ReturnsAsync(sectorsContext);
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
            repository.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => sectorsContext.Find(s => s.Id == id));
            //Act
            var result = await sectorService.GetSectorByIdAsync(id);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(sectorsContext[id - 1].Id, result.Id);
        }

        [Test]
        public async Task InsertSector_SectorData_SectorInserted()
        {
            //Arrange
            repository.Setup(r => r.InsertEntityAsync(It.IsAny<Sector>()))
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
        public async Task DeleteSector_SectorData_SectorDeleted(int id)
        {
            repository.Setup(r => r.DeleteEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    var founded = sectorsContext.Find(s => s.Id == id);
                    if (founded != null)
                    {
                        throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Sector with id: {id} not found when trying to delete sector. Sector wasn't deleted.");
                    }
                    sectorsContext.Remove(founded);
                    return founded;
                });
        }
    }
}
