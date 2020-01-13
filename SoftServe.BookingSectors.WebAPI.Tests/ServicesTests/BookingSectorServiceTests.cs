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

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests
{
    public sealed class BookingSectorServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IBaseRepository<BookingSector>> bookingSectorRepositoryMock;
        private IBookingSectorService bookingSectorService;
        private MapperConfiguration mapperConfiguration;
        private List<BookingSector> bookingsContext;
        private BookingSectorDTO bookingSectorDTOToInsert;

        public BookingSectorServiceTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            bookingSectorRepositoryMock = new Mock<IBaseRepository<BookingSector>>();
            mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<BookingSectorProfile>();
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
                BookingStart = new DateTime(2020, 1, 9),
                BookingEnd = new DateTime(2020, 1, 10),
                IsApproved = true,
                CreateDate = new DateTime(2020, 1, 9),
                CreateUserId = 2,
                ModDate = new DateTime(2020, 1, 9)
            },
            new BookingSector()
            {
                Id = 3,
                UserId = 3,
                SectorId = 3,
                BookingStart = new DateTime(2020, 1, 9),
                BookingEnd = new DateTime(2020, 1, 10),
                IsApproved = false,
                CreateDate = new DateTime(2020, 1, 9),
                CreateUserId = 3,
                ModDate = new DateTime(2020, 1, 9)
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
            bookingsContext = bookings;
            bookingSectorDTOToInsert = bookingSector;
            unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(true);
            unitOfWorkMock.Setup(u => u.BookingSectorRepository).Returns(bookingSectorRepositoryMock.Object);
            bookingSectorRepositoryMock.Setup(b => b.GetAllEntitiesAsync()).ReturnsAsync(bookingsContext);        
        }

        // #TODO: Change method name when booking data property will be moved to diff file
        [Test]
        public async Task GetAllBookingsAsync_Bookings_AllReturnedAsync() 
        {
            var result = await bookingSectorService.GetBookingSectorsAsync();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<BookingSectorDTO>>(result);
            Assert.AreEqual(bookingsContext.Count, (result as List<BookingSectorDTO>).Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetBookingByIdAsync_InputIsBookingSectorData_GetFoundBookingSectorDTO(int id)
        {
            //Arrange
            bookingSectorRepositoryMock.Setup(b => b.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => bookingsContext
                    .Find(b => b.Id == id));
            //Act
            var result = await bookingSectorService.GetBookingByIdAsync(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BookingSectorDTO>(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public async Task BookSectorAsync_BookingSectorToInsert_GetInsertedBooking()
        {
            bookingSectorRepositoryMock.Setup(b => b.InsertEntityAsync(It.IsAny<BookingSector>()))
                .ReturnsAsync((BookingSector b) =>
                {
                    b.Id = bookingSectorDTOToInsert.Id;
                    bookingsContext.Add(b);
                    return b;
                });

            var result = await bookingSectorService.BookSector(bookingSectorDTOToInsert);

            Assert.AreEqual(bookingSectorDTOToInsert.Id, result.Id);
        }
    }
}
