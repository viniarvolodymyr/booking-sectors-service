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
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests
{
    [TestFixture]
    class UserServiceTests
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
        private static List<UserDTO> SetUpUsersDTO()
        {
            var usersDTO = new List<UserDTO>
                               {
                new UserDTO()
                {
                    Id = 1,
                    Firstname = "testUserName",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                    Password = "12345",
                },
                   new UserDTO()
                {
                    Id = 2,
                    Firstname = "testUserName",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                    Password = "12345",
                },
                      new UserDTO()
                {
                    Id = 3,
                    Firstname = "testUserName",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                    Password = "12345",
                },
                         new UserDTO()
                {
                    Id = 4,
                    Firstname = "testUserName",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                    Password = "12345",
                },
                            new UserDTO()
                {
                    Id = 5,
                    Firstname = "testUserName",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                    Password = "12345",
                },
        };
            return usersDTO;
        }


        private static List<User> SetUpUsers()
        {
            var users = new List<User>
                               {
                new User()
                {
                    Id = 1,
                    Firstname = "testUserName",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                   
                },
                   new User()
                {
                    Id = 2,
                    Firstname = "testUserName",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                
                },
                      new User()
                {
                    Id = 3,
                    Firstname = "testUserName",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                 
                },
                         new User()
                {
                    Id = 4,
                    Firstname = "testUserName",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                   
                },
                            new User()
                {
                    Id = 5,
                    Firstname = "testUserName",
                    Lastname = "testUserSurname",
                    Phone = "9999999999",
                    RoleId = 2,
                    
                },
        };
            return users;
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
            List<User> users = SetUpUsers();
            
          //  List<TournamentDTO> tournamentsDTO = new List<TournamentDTO>();
            Mock<IBaseRepository<User>> repositoryMock = new Mock<IBaseRepository<User>>();

            repositoryMock.Setup(r => r.GetAllEntitiesAsync()).ReturnsAsync(users);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.UserRepository).Returns(repositoryMock.Object);

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<UserProfile>();
            });
            var mapper = config.CreateMapper();
            ILoggerManager logger = new LoggerManager();
            var sut = new UserService(mockUnitOfWork.Object, mapper, logger);

            //Act

            var result = sut.GetAllUsersAsync().Result;
            Console.WriteLine(result);
            //Assert.IsNotNull(result);
            Assert.IsAssignableFrom<Task<List<UserDTO>>>(result);
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


//        [SetUp]
//        public void Setup()
//        {
//            _mockRepository = new Mock<UserRepository>();
//            _mockUnitWork = new Mock<IUnitOfWork>();
//            _service = new UserService(_mockUnitWork.Object, mapper, logger);

//            userDTO = new RegistrationDTO()
//            {
//                Firstname = "testUserName",
//                Lastname = "testUserSurname",
//                Phone = "9999999999",
//                RoleId = 2,
//                Password = "12345",
//            };
////            var dto =  registrationService.InsertUserAsync(user);
//            //database.SaveAsync();
//        }
//        [Test]
//        public void GetAllUsersTest()
//        {
//            var result = userService.GetAllUsersAsync();
//            Assert.IsAssignableFrom<Task<IEnumerable<UserDTO>>>(result);
//        }

//        [Test]
//        public void CheckPasswordsTest()
//        {
//            byte[] passToCheck = SHA256Hash.Compute(userDTO.Password);
//            //bool exp = user.Password.SequenceEqual(passToCheck);
//            var result =userService.CheckPasswords(userDTO.Password, userDTO.Id);
//           // Assert.IsFalse(result);
//        }
      

//        [Test]
//        public void GetUserByIdTest()
//        {

//            //Arrange
//            _mockRepository.Setup(x => x.GetAllEntitiesAsync()).Returns(listCountry);

//            //Act
//            // List<UserDTO> results = _service.GetAllUsersAsync() as List<UserDTO>;
//            var resultAll = _service.GetAllUsersAsync();

//            //Assert
//            Assert.IsNotNull(resultAll);
//            //var result = _service.GetUserByIdAsync(userDTO.Id);
//            //UserDTO dto = result.Result;

//           // Assert.IsAssignableFrom<Task<UserDTO>>(result);
//           // Assert.AreEqual(userDTO.Id, dto.Id);

//        }

//        [Test]
//        public void GetUserByPhoneTest()
//        {
//            Task<UserDTO> result = _service.GetUserByPhoneAsync(userDTO.Phone);
            
//            UserDTO dto = result.Result;
        
//            Assert.AreEqual(userDTO.Phone, dto.Phone);
//            //Assert.IsAssignableFrom<Task<UserDTO>>(result);
//        }
    }
}
