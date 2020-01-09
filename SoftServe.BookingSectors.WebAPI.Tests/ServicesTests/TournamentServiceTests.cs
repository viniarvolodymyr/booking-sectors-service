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

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests
{
    [TestFixture]
    class TournamentServiceTests
    {
        [OneTimeSetUp]
        [Obsolete]
        public void Setup()
        {
            /*    tournaments = SetUpTournaments();

                tournamentRepository= SetUpTournamentRepository();


                var config = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                });

                var mapper = config.CreateMapper();
                tournamentService = new TournamentService(unitOfWork, mapper);

                */
        }

        [SetUp]
        public void ReInitializeTest()
        {

        }

        /// ////////////////////////////////////////////////////////////
        private static List<Tournament> SetUpTournaments()
        {
            var tournaments = new List<Tournament>
                               {
                                   new Tournament()
                                   {
                                       Id = 1,
                                       Name = "Tour",
                                  Description="CNBJABC",
                                     PreparationTerm=0,
                                     CreateUserId=1,
                                     ModUserId = 0
                                   },
                                     new Tournament()
                                   {
                                       Id = 2,
                                       Name = "tournament6",
                                      Description="CNBJABC",
                                     PreparationTerm=3,
                                       CreateUserId=0,
                                       ModUserId = 0
                                   },
                                       new Tournament()
                                   {
                                       Id = 3,
                                       Name = "tiurnament6",
                                      Description="CNBJABC",
                                     PreparationTerm=2,
                                       CreateUserId=1,
                                   },
                                         new Tournament()
                                   {
                                       Id = 4,
                                       Name = "tournament7",
                               Description="CNBJABC",
                                     PreparationTerm=1,
                                       CreateUserId=3,
                                   },
                                           new Tournament()
                                   {
                                       Id = 5,
                                       Name = "Tournament9",
                                       Description="CNBJABC",
                                     PreparationTerm=4,
                                       CreateUserId=2,
                                   },
                                           new Tournament()
                                   {
                                       Id = 6,
                                       Name = "Tournament12",
                                    Description="CNBJABC",
                                     PreparationTerm=2,
                                       CreateUserId=4,
                                   },
                                                new Tournament()
                                   {
                                       Id = 7,
                                       Name = "Tournament12",
                                      Description="CNBJABC",
                                     PreparationTerm=2,
                                       CreateUserId=4,
                                   },
                               };
            return tournaments;
        }
        /*
                private IBaseRepository<Tournament> SetUpTournamentRepository()
                {
                    var mockRepo = new Mock<IBaseRepository<Tournament>>(MockBehavior.Default, unitOfWork);

                    mockRepo.Setup(p => p.GetAllEntitiesAsync()).ReturnsAsync(tournaments);

                    mockRepo.Setup(p => p.GetEntityByIdAsync(It.IsAny<int>()))
                        .ReturnsAsync(new Func<int, Tournament>(
                                     id => tournaments.Find(p => p.Id.Equals(id))));
                    /*
                    mockRepo.Setup(p => p.InsertEntityAsync((It.IsAny<Tournament>())))
                        .Callback(new Action<Tournament>(newTournament =>
                        {
                            dynamic maxProductID = tournaments.Last().Id;
                            dynamic nextProductID = maxProductID + 1;
                            newTournament.Id = nextProductID;
                            tournaments.Add(newTournament);
                        }));

                    mockRepo.Setup(p => p.UpdateEntity(It.IsAny<Tournament>()))
                        .Callback(new Action<Tournament>(prod =>
                        {
                            var oldProduct = tournaments.Find(a => a.Id == prod.Id);
                            oldProduct = prod;
                        }));

                    mockRepo.Setup(p => p.DeleteEntityByIdAsync(It.IsAny<int>()))
                        .Callback(new Action<Tournament>(prod =>
                        {
                            var productToRemove =
                                tournaments.Find(a => a.Id == prod.Id);

                            if (productToRemove != null)
                                tournaments.Remove(productToRemove);
                        }));

                    return mockRepo.Object;
                }
                */
        /////////////////////////
        [Test]
        public void GetAllTestAsync()
        {
            List<Tournament> tournaments = SetUpTournaments();
            List<TournamentDTO> tournamentsDTO = new List<TournamentDTO>();
            Mock<IBaseRepository<Tournament>> repositoryMock = new Mock<IBaseRepository<Tournament>>();

            repositoryMock.Setup(r => r.GetAllEntitiesAsync()).ReturnsAsync(tournaments);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.TournamentRepository).Returns(repositoryMock.Object);

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<TournamentProfile>();
            });
            var mapper = config.CreateMapper();

            var sut = new TournamentService(mockUnitOfWork.Object, mapper);

            //Act

            var result = sut.GetAllTournamentsAsync().Result;
            Console.WriteLine(result);
            Assert.IsNotNull(result);

            /*
            var result = sut.GetAllTournamentsAsync().Result;
            tournamentsDTO =  mapper.Map<List<Tournament>, List<TournamentDTO>>(tournaments);
            //  Assert.AreNotEqual(tournamentsDTO.Count(),result.Count());
            */

            /*
           if (model.Count() > 0)

               Assert.NotNull(model);

               var expected = model?.FirstOrDefault().Title;
               var actual = schedules?.FirstOrDefault().Title;

               Assert.Equal(expected: expected, actual: actual);
           }
           &+*/

        }



    }
}
