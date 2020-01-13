using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.Controllers;
using SoftServe.BookingSectors.WebAPI.Tests.ControllersTests.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Tests.ControllersTests
{
    [TestFixture]
    class TournamentControllerTests
    {
        private readonly Mock<ITournamentService> tournamentServiceMock;
        private readonly TournamentController tournamentController;
        List<TournamentDTO> tournamentsContext;
        TournamentDTO tournamentDTO;


        public TournamentControllerTests()
        {
            tournamentServiceMock = new Mock<ITournamentService>();
            tournamentController = new TournamentController(tournamentServiceMock.Object);
        }

        [SetUp]
        public void SetUp()
        {
            TournamentDTOData tournamentData = new TournamentDTOData();
            tournamentsContext = tournamentData.Tournaments;
            tournamentDTO = tournamentData.TournamentDTOToInsert;
        }

        [TearDown]
        public void TearDown()
        {
            tournamentsContext.Clear();
        }


        [Test]
        public async Task GetAllTournaments_InputIsTournamentData_ReturnsOk()
        {
            //Arrange
            tournamentServiceMock.Setup(tournamentService => tournamentService.GetAllTournamentsAsync()).ReturnsAsync(tournamentsContext);
            //Act
            var okResult = (await tournamentController.GetAll()) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetTournamentById_InputIsTournamentData_ReturnsOk(int id)
        {
            //Arrange
            tournamentServiceMock.Setup(tournamentService => tournamentService.GetTournamentByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => tournamentsContext.Find(tournament => tournament.Id == id));
            //Act
            var okResult = (await tournamentController.GetTournament(id)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }


        [Test]
        public async Task InsertTournament_InputIsTournamentData_ReturnsCreated()
        {
            //Arrange
            tournamentServiceMock.Setup(tournamentService => tournamentService.InsertTournamentAsync(It.IsAny<TournamentDTO>()))
                .ReturnsAsync((TournamentDTO tournamentDTO) =>
                {
                    tournamentsContext.Add(tournamentDTO);
                    return tournamentDTO;
                });
            int tournamentsContextLength = tournamentsContext.Count;
            //Act
            var createdResult = (await tournamentController.Post(tournamentDTO)) as CreatedResult;
            //Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(tournamentsContextLength + 1, tournamentsContext.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UpdateTournament_InputIsTournamentData_ReturnsOk(int id)
        {
            //Arrange
            tournamentServiceMock.Setup(tournamentService => tournamentService.UpdateTournamentAsync(It.IsAny<int>(), It.IsAny<TournamentDTO>()))
                .ReturnsAsync((int id, TournamentDTO tournamentDTO) =>
                {
                    tournamentDTO.Id = id;
                    tournamentsContext[tournamentsContext.FindIndex(i => i.Id == id)] = tournamentDTO;
                    return tournamentDTO;
                });
            //Act
            var okResult = (await tournamentController.Put(id, tournamentDTO)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteTournament_InputIsTournamentData_ReturnsOk(int id)
        {
            //Arrange
            tournamentServiceMock.Setup(tournamentService => tournamentService.DeleteTournamentByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    var foundTournament = tournamentsContext.Find(tournament => tournament.Id == id);
                    tournamentsContext.Remove(foundTournament);
                    return foundTournament;
                });
            int tournamentContextLength = tournamentsContext.Count;
            //Act
            var okResult = (await tournamentController.Delete(id)) as OkObjectResult;
            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(tournamentContextLength - 1, tournamentsContext.Count);
        }
    }
}
