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
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests
{
    public sealed class BookingSectorServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IBaseRepository<BookingSector>> bookingSectorRepositoryMock;
        private Mock<IBaseRepository<Sector>> sectorRepositoryMock;
        private IBookingSectorService bookingSectorService;
        private MapperConfiguration mapperConfiguration;
        private List<BookingSector> bookingsContext;
        private List<Sector> sectorsContext;
        private BookingSectorDTO bookingSectorDTO;

        public BookingSectorServiceTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            bookingSectorRepositoryMock = new Mock<IBaseRepository<BookingSector>>();
            sectorRepositoryMock = new Mock<IBaseRepository<Sector>>();
            mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<BookingSectorProfile>();
                c.AddProfile<SectorProfile>();
            });
            bookingSectorService = new BookingSectorService(unitOfWorkMock.Object, mapperConfiguration.CreateMapper());
        }

        [SetUp]
        public void Setup()
        {
            sectorsContext = SectorData.CreateSectors();
            bookingsContext = BookingSectorData.CreateBookingSectorsList();
            bookingSectorDTO = BookingSectorData.CreateBookingSectorDTO();
            unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(true);
            unitOfWorkMock.Setup(u => u.BookingSectorRepository).Returns(bookingSectorRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.SectorRepository).Returns(sectorRepositoryMock.Object);
            bookingSectorRepositoryMock.Setup(b => b.GetAllEntitiesAsync()).ReturnsAsync(bookingsContext);
            bookingSectorRepositoryMock.Setup(b => b.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => bookingsContext
                    .Find(b => b.Id == id));
        }

        [Test]
        public async Task GetBookingSectorsAsync_InputIsBookingSectorData_AllReturnedAsync() 
        {
            var result = await bookingSectorService.GetBookingSectorsAsync() as List<BookingSectorDTO>;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<BookingSectorDTO>>(result);
            Assert.AreEqual(bookingsContext.Count, result.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task GetBookingByIdAsync_InputIsBookingSectorData_ReturnedFoundBookingSectorDTO(int id)
        {
            //Act
            var result = await bookingSectorService.GetBookingByIdAsync(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BookingSectorDTO>(result);
            Assert.AreEqual(id, result.Id);
        }

        //[Test]
        //[TestCase(1, true)]
        //[TestCase(2, false)]
        //[TestCase(3, true)]
        //[TestCase(4, false)]
        //public async Task GetBookingsByUserId_InputIsBookingSectorData_ReturnedFoundBookingsSectorDTO(int id, bool isActual)
        //{        
        //    //Act
        //    var result = await bookingSectorService.GetBookingsByUserId(id, isActual) as List<BookingSectorDTO>;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<IEnumerable<BookingSectorDTO>>(result);
        //    Assert.IsNotNull(result[0].UserId);
        //}

        [Test] 
        [TestCase("2020-1-13", "2020-1-16")]
        public async Task FilterSectorsByDate_InputIsBookingSectorData_ReturnedUpdatedBookingSectorDTO(DateTime fromDate, DateTime toDate)
        {
            //Arrange
            sectorRepositoryMock.Setup(s => s.GetAllEntitiesAsync()).ReturnsAsync(sectorsContext);

            //Act
            var result = await bookingSectorService.FilterSectorsByDate(fromDate, toDate) as List<SectorDTO>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<SectorDTO>>(result);
            Assert.AreEqual(false, result.Find(b => b.Id == 2).IsActive);
        }

        [Test]
        public async Task BookSectorAsync_BookingSectorToInsert_ReturnedInsertedBookingSectorDTO()
        {
            //Arrange
            bookingSectorRepositoryMock.Setup(b => b.InsertEntityAsync(It.IsAny<BookingSector>()))
                .ReturnsAsync((BookingSector b) =>
                {
                    b.Id = bookingSectorDTO.Id;
                    bookingsContext.Add(b);
                    return b;
                });

            //Act
            var result = await bookingSectorService.BookSector(bookingSectorDTO);

            //Assert
            Assert.AreEqual(bookingSectorDTO.Id, result.Id);
        }

        [Test]
        public async Task GetBookingTournamentSectorsAsync_InputIsBookingSectorData_AllReturnedAsync()
        {
            //Act
            var result = await bookingSectorService.GetBookingTournamentSectorsAsync() as List<BookingSectorDTO>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<BookingSectorDTO>>(result);
            Assert.AreEqual(true, result[0].TournamentId.HasValue);
        }

        [Test]
        [TestCase(2)]
        public async Task GetBookingTournamentByIdAsync_InputIsBookingSectorData__ReturnedFoundBookingsSectorDTO(int id)
        {
            //Act
            var result = await bookingSectorService.GetBookingTournamentByIdAsync(id) as List<BookingSectorDTO>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<BookingSectorDTO>>(result);
            Assert.AreEqual(1, result.Count);
        } 

        [Test]
        [TestCase(1, true)]
        [TestCase(2, false)]
        public async Task UpdateBookingApprovedAsync_InputIsBookingSectorData_ReturnUpdatedBookingSectorDTO(int id, bool isAproved)
        {
            //Arrange
            bookingSectorRepositoryMock.Setup(b => b.UpdateEntity(It.IsAny<BookingSector>()))
                .Returns((BookingSector booking) =>
                {
                    var bookingToUpdate = bookingsContext.Find(b => b.Id == booking.Id);
                    bookingToUpdate.IsApproved = booking.IsApproved;
                    return bookingToUpdate;
                });

            //Act
            var result = await bookingSectorService.UpdateBookingApprovedAsync(id, isAproved);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BookingSectorDTO>(result);
            Assert.AreEqual(bookingsContext.Find(b => b.Id == id).IsApproved, result.IsApproved);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task UpdateTournamentBooking_InputIsBookingSectorData_ReturnUpdatedBookingSectorDTO(int id)
        {
            //Arrange
            bookingSectorRepositoryMock.Setup(b => b.UpdateEntity(It.IsAny<BookingSector>()))
                .Returns((BookingSector booking) => 
                {
                    bookingsContext[bookingsContext.FindIndex(b => b.Id == booking.Id)] = booking;
                    return booking;
                });

            //Act
            var result = await bookingSectorService.UpdateTournamentBooking(id, bookingSectorDTO);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BookingSectorDTO>(result);
            Assert.AreEqual(bookingsContext[id - 1].Id, result.Id);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task DeleteBookingByIdAsync_InputIsBookingSectorData_ReturnDeletedBookingSectorDTO(int id)
        {
            //Arrange
            bookingSectorRepositoryMock.Setup(b => b.DeleteEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => 
                {
                    var bookingSectorToDelete = bookingsContext.Find(b => b.Id == id);
                    if (bookingsContext.Remove(bookingSectorToDelete))
                        return bookingSectorToDelete;
                    else
                        return null;
                });

            int bookingSectorPreviousCount = bookingsContext.Count;

            //Act
            var result = await bookingSectorService.DeleteBookingByIdAsync(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BookingSectorDTO>(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(bookingSectorPreviousCount - 1, bookingsContext.Count);
        }
    }
}
