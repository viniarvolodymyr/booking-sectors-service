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
        public async Task GetAllSectors_SectorDataSectors_AllReturned()
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
        public async Task InsertSector_SectorData_OneInserted()
        {
            repository.Setup(r => r.InsertEntityAsync(It.IsAny<Sector>()))
                .Callback<Sector>(s =>
                {
                    s.Id = ++sectorsContext[sectorsContext.Count - 1].Id;
                    sectorsContext.Add(s);
                }).ReturnsAsync((Sector s) => s);

            //Act
            var result = await sectorService.InsertSectorAsync(sectorDTO);
            //Assert
            Assert.AreEqual(4, result.Id);
            //Assert.IsNotNull(result);
            //Assert.AreEqual(sectorDTO.Description, sectorsContext[sectorsContext.Count - 1].Description);

            //Assert.AreEqual(4, sectorsContext.Count);
        }
    }
}
