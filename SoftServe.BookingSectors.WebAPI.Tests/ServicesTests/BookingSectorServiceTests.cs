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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SoftServe.BookingSectors.WebAPI.Tests.ServicesTests.Data;

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
   
        private List<BookingSector> bookings = new List<BookingSector>() // #TODO: Move to diff file
        {
            new BookingSector()
            {
                Id = 1,
                UserId = 1,
                SectorId = 1,
                BookingStart = new DateTime(2020, 1, 9),
                BookingEnd = new DateTime(2020, 1, 10),
                IsApproved = false,
                CreateDate = new DateTime(2020, 1, 9),
                CreateUserId = 1,
                ModDate = new DateTime(2020, 1, 9)
            },
            new BookingSector()
            {
                Id = 2,
                UserId = 2,
                SectorId = 2,
                BookingStart = new DateTime(2020, 1, 13),
                BookingEnd = new DateTime(2020, 1, 16),
                IsApproved = true,
                TournamentId = 2,
                CreateDate = new DateTime(2020, 1, 13),
                CreateUserId = 2,
                ModDate = new DateTime(2020, 1, 13)
            },
            new BookingSector()
            {
                Id = 3,
                UserId = 3,
                SectorId = 3,
                BookingStart = new DateTime(2020, 1, 18),
                BookingEnd = new DateTime(2020, 1, 21),
                IsApproved = false,
                CreateDate = new DateTime(2020, 1, 18),
                CreateUserId = 3,
                ModDate = new DateTime(2020, 1, 18)
            },
            new BookingSector()
            {
                Id = 4,
                UserId = 4,
                SectorId = 4,
                BookingStart = new DateTime(2020, 1, 26),
                BookingEnd = new DateTime(2020, 1, 30),
                IsApproved = true,
                CreateDate = new DateTime(2020, 1, 26),
                CreateUserId = 3,
                ModDate = new DateTime(2020, 1, 26)
            }
        };

        private BookingSectorDTO bookingSector { get; } = new BookingSectorDTO() // #TODO: Move to diff file
        {
            Id = 10,
            UserId = 2,
            SectorId = 2,
            BookingStart = new DateTime(2020, 1, 9),
            BookingEnd = new DateTime(2020, 1, 10),
            IsApproved = false,      
            CreateUserId = 2         
        };

        [SetUp]
        public void Setup()
        {
            SectorData sectorData = new SectorData();
            sectorsContext = sectorData.Sectors;
            bookingsContext = bookings;
            bookingSectorDTO = bookingSector;
            unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(true);
            unitOfWorkMock.Setup(u => u.BookingSectorRepository).Returns(bookingSectorRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.SectorRepository).Returns(sectorRepositoryMock.Object);
            bookingSectorRepositoryMock.Setup(b => b.GetAllEntitiesAsync()).ReturnsAsync(bookingsContext);
            bookingSectorRepositoryMock.Setup(b => b.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => bookingsContext
                    .Find(b => b.Id == id));
        }

        // #TODO: Change method name when booking data property will be moved to diff file
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

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task GetBookingsByUserId_InputIsBookingSectorData_ReturnedFoundBookingsSectorDTO(int id)
        {        
            //Act
            var result = await bookingSectorService.GetBookingsByUserId(id) as List<BookingSectorDTO>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<BookingSectorDTO>>(result);
            Assert.IsNotNull(result[0].UserId);
        }

        [Test]
        [TestCase("2020-1-9", "2020-1-10")]
        [TestCase("2020-1-13", "2020-1-16")]
        [TestCase("2020-1-18", "2020-1-21")]
        [TestCase("2020-1-26", "2020-1-30")]
        public async Task FilterSectorsByDate_InputIsBookingSectorData_ReturnedUpdatedBookingSectorDTO(DateTime fromDate, DateTime toDate)
        {
            //Arrange
            sectorRepositoryMock.Setup(s => s.GetAllEntitiesAsync()).ReturnsAsync(sectorsContext);

            //Act
            var result = await bookingSectorService.FilterSectorsByDate(fromDate, toDate);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<SectorDTO>>(result);
            Assert.AreEqual(true, result.All(s => s.IsActive == false));
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

            int bookingSectorCount = bookingsContext.Count;

            //Act
            var result = await bookingSectorService.DeleteBookingByIdAsync(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BookingSectorDTO>(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(bookingSectorCount - 1, bookingsContext.Count);
        }
    }
}
