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

        private List<BookingSectorDTO> bookingContext;

        public BookingSectorControllerTests()
        {
            bookingSectorServiceMock = new Mock<IBookingSectorService>();
            bookingSectorController = new BookingSectorController(bookingSectorServiceMock.Object);
        }

        [SetUp]
        public void Setup()
        {
            bookingContext = BookingSectorData.CreateBookingSectorDTOs();
        }

        [Test]
        public async Task GetAllBookingSectors_InputIsBookingSectorData_ReturnsOk()
        {
            //Arrange
            bookingSectorServiceMock.Setup(b => b.GetBookingSectorsAsync())
                .ReturnsAsync(bookingContext);

            //Act 
            var result = await bookingSectorController.Get() as OkObjectResult;
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}
