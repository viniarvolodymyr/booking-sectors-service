using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.Controllers;
using SoftServe.BookingSectors.WebAPI.Tests.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var resultDTOs = result.Value as List<BookingSectorDTO>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.IsNotNull(resultDTOs);
            Assert.AreEqual(bookingSectorContext.Count, resultDTOs.Count);
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
            var resultDTO = result.Value as BookingSectorDTO;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.IsNotNull(resultDTO);
            Assert.AreEqual(id, resultDTO.Id);
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
            Assert.AreEqual(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
            Assert.AreEqual(bookingSectorPreviousCount + 1, bookingSectorContext.Count);
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, false)]
        public async Task UpdateBookingIsApprovedAsync_InputIsBookingSectorData_ReturnsOk(int id, bool isAproved)
        {
            //Arrange
            bookingSectorServiceMock.Setup(b => b.UpdateBookingIsApprovedAsync(id, isAproved))
                .ReturnsAsync((int id, bool isAproved) => 
                {
                    var bookingToUpdate = bookingSectorContext.Find(b => b.Id == id);
                    bookingToUpdate.IsApproved = isAproved;
                    return bookingToUpdate;
                });

            //Act
            var result = await bookingSectorController.Put(id, isAproved) as OkObjectResult;
            var resultDTO = result.Value as BookingSectorDTO;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.IsNotNull(resultDTO);
            Assert.AreEqual(id, resultDTO.Id);
            Assert.AreEqual(isAproved, resultDTO.IsApproved);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task DeleteBookingSectorByIdAsync_InputIsBookingSectorData_ReturnsOk(int id)
        {
            //Arrange
            bookingSectorServiceMock.Setup(b => b.DeleteBookingByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => 
                {
                    var bookingSectorToDelete = bookingSectorContext.Find(b => b.Id == id);
                    if (bookingSectorContext.Remove(bookingSectorToDelete))
                        return bookingSectorToDelete;
                    else
                        return null;
                });

            var bookingSectorPreviousCount = bookingSectorContext.Count;

            //Act
            var result = await bookingSectorController.Delete(id) as OkObjectResult;
            var resultDTO = result.Value as BookingSectorDTO;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.IsNotNull(resultDTO);
            Assert.AreEqual(id, resultDTO.Id);
            Assert.AreEqual(bookingSectorPreviousCount - 1, bookingSectorContext.Count);
        }

        [Test]
        public async Task GetAllBookingTournamentsAsync_InputIsBookingSectorData_ReturnsOk()
        {
            //Arrange
            bookingSectorServiceMock.Setup(b => b.GetBookingTournamentSectorsAsync())
                .ReturnsAsync(bookingSectorContext);

            //Act
            var result = await bookingSectorController.GetTournaments() as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetBookingTournamentByIdAsync_InputIsBookingSectorData_ReturnsOk(int id)
        {
            //Arrange
            bookingSectorServiceMock.Setup(b => b.GetBookingTournamentByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => 
                {
                    return bookingSectorContext.Where(b => b.TournamentId == id).ToList();
                });

            //Act
            var result = await bookingSectorController.GetTournament(id) as OkObjectResult;
            var resultDTOs = result.Value as List<BookingSectorDTO>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.IsNotNull(resultDTOs);
            Assert.IsTrue(resultDTOs.All(b => b.TournamentId.HasValue));
        }
    }
}
