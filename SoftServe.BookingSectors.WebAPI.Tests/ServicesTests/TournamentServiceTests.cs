using NUnit.Framework;
using SoftServe.BookingSectors.WebAPI.BLL.Services;
using System;
using System.Collections.Generic;
using Moq;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories;
using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.Tests.ServicesTests.Data;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests
{
    [TestFixture]
    class TournamentServiceTests
    {
        private readonly ITournamentService tournamentService;
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IBaseRepository<Tournament>> tournamentRepositoryMock;
        List<Tournament> tournamentsContext;
        TournamentDTO tournamentDTO;


        public TournamentServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TournamentProfile>();
            });
            tournamentRepositoryMock = new Mock<IBaseRepository<Tournament>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.SaveAsync()).ReturnsAsync(true);
            unitOfWorkMock.Setup(uow => uow.TournamentRepository).Returns(tournamentRepositoryMock.Object);
            tournamentService = new TournamentService(unitOfWorkMock.Object, config.CreateMapper());
        }


        [SetUp]
        public void SetUp()
        {
            TournamentData tournamentData = new TournamentData();
            tournamentsContext = tournamentData.Tournaments;
            tournamentDTO = tournamentData.TournamentDTOToInsert;
        }

        [TearDown]
        public void TearDown()
        {
            tournamentsContext.Clear();
        }

        [Test]
        public async Task GetAllTournaments_InputIsTournamentData_AllTournamentsReturned()
        {
            //Arrange
            tournamentRepositoryMock.Setup(tournamentRepository => tournamentRepository.GetAllEntitiesAsync()).ReturnsAsync(tournamentsContext);
            //Act
            var resultTournamentDTOs = (await tournamentService.GetAllTournamentsAsync()) as List<TournamentDTO>;
            //Assert
            Assert.IsNotNull(resultTournamentDTOs);
            Assert.AreEqual(tournamentsContext.Count, resultTournamentDTOs.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetTournamentById_InputIsTournamentData_OneTournamentReturned(int id)
        {
            //Arrange
            tournamentRepositoryMock.Setup(tournamentRepository => tournamentRepository.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => tournamentsContext.Find(tournament => tournament.Id == id));
            //Act
            var resultTournamentDTO = (await tournamentService.GetTournamentByIdAsync(id)) as TournamentDTO;
            //Assert
            Assert.IsNotNull(resultTournamentDTO);
            Assert.AreEqual(tournamentsContext[id - 1].Id, resultTournamentDTO.Id);
        }


        [Test]
        public async Task InsertTournament_InputIsTournamentData_OneTournamentInserted()
        {
            //Arrange
            tournamentRepositoryMock.Setup(tournamentRepository => tournamentRepository.InsertEntityAsync(It.IsAny<Tournament>()))
                .ReturnsAsync((Tournament tournament) =>
                {
                    tournament.Id = tournamentDTO.Id;
                    tournamentsContext.Add(tournament);
                    return tournament;
                });
            int tournamentContextLength = tournamentsContext.Count;
            //Act
            var resultTournamentDTO = (await tournamentService.InsertTournamentAsync(tournamentDTO)) as TournamentDTO;
            //Assert
            Assert.IsNotNull(resultTournamentDTO);
            Assert.AreEqual(tournamentDTO.Id, resultTournamentDTO.Id);
            Assert.AreEqual(tournamentContextLength + 1, tournamentsContext.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UpdateTournament_InputIsTournamentData_OneTournamentUpdated(int id)
        {
            //Arrange
            tournamentRepositoryMock.Setup(tournamentRepository => tournamentRepository.GetEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => tournamentsContext.Find(tournament => tournament.Id == id));
            tournamentRepositoryMock.Setup(tournamentRepository => tournamentRepository.UpdateEntity(It.IsAny<Tournament>()))
                .Returns((Tournament tournament) =>
                {
                    tournamentsContext[tournamentsContext.FindIndex(i => i.Id == tournament.Id)] = tournament;
                    return tournament;
                });
            //Act
            var resultTournamentDTO = (await tournamentService.UpdateTournamentAsync(id, tournamentDTO)) as TournamentDTO;
            //Assert
            Assert.IsNotNull(resultTournamentDTO);
            Assert.AreEqual(tournamentsContext[id - 1].Id, resultTournamentDTO.Id);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteTournament_InputIsTournamentData_OneTournamentDeleted(int id)
        {
            //Arrange
            tournamentRepositoryMock.Setup(tournamentRepository => tournamentRepository.DeleteEntityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    var foundTournament = tournamentsContext.Find(tournament => tournament.Id == id);
                    tournamentsContext.Remove(foundTournament);
                    return foundTournament;
                });
            int tournamentContextLength = tournamentsContext.Count;
            //Act
            var resultTournamentDTO = (await tournamentService.DeleteTournamentByIdAsync(id)) as TournamentDTO;
            //Assert
            Assert.IsNotNull(resultTournamentDTO);
            Assert.AreEqual(tournamentContextLength - 1, tournamentsContext.Count);
        }
    }
}
