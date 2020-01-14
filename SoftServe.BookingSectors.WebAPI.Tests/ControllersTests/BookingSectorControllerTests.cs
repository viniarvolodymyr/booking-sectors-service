using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.Controllers;
using SoftServe.BookingSectors.WebAPI.Tests.ServicesTests.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Tests.ControllersTests
{
    [TestFixture]
    class BookingSectorControllerTests
    {
        private BookingSectorController bookingSectorController;
        private Mock<IBookingSectorService> bookingSectorServiceMock;

        private List<BookingSectorDTO> bookingSectorContext;
        private BookingSectorDTO bookingSectorDTO;
        public BookingSectorControllerTests()
        {
            bookingSectorServiceMock = new Mock<IBookingSectorService>();
            bookingSectorController = new BookingSectorController(bookingSectorServiceMock.Object);
        }

        [SetUp]
        public void Setup()
        {
            bookingSectorContext = BookingSectorData.CreateBookingSectorDTOs();
            bookingSectorDTO = BookingSectorData.CreateBookingSectorDTO();
        }

        [Test]
        public async Task GetAllBookingSectorsAsync_InputIsBookingSectorData_ReturnsOk()
        {
            //Arrange
            bookingSectorServiceMock.Setup(b => b.GetBookingSectorsAsync())
                .ReturnsAsync(bookingSectorContext);

            //Act 
            var result = await bookingSectorController.Get() as OkObjectResult;
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task GetBookingSectorsByIdAsync_InputIsBookingSectorData_ReturnsOk(int id)
        {
            //Arrange
            bookingSectorServiceMock.Setup(b => b.GetBookingByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => 
                {
                    return bookingSectorContext.Find(b => b.Id == id);
                });

            //Act
            var result = await bookingSectorController.Get(id) as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task InsertBookingSectorDTO_InputIsBookingSectorData_ReturnsCreatedBookingSectorDTO()
        {
            //Arrange 
            bookingSectorServiceMock.Setup(b => b.BookSector(It.IsAny<BookingSectorDTO>()))
                .ReturnsAsync((BookingSectorDTO bookingDTO) => 
                {
                    bookingSectorContext.Add(bookingDTO);
                    return bookingDTO;
                });

            var bookingSectorPreviousCount = bookingSectorContext.Count;

            //Act 
            var result = await bookingSectorController.Post(bookingSectorDTO) as CreatedResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(bookingSectorPreviousCount + 1, bookingSectorContext.Count);
        }
    }
}
