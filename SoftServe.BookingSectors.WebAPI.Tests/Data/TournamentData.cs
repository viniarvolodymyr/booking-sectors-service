using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.Tests.Data
{
    public static class TournamentData
    {
        private static MapperConfiguration mapperConfiguration;
        private static IMapper mapper;
        static TournamentData()
        {
            mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<TournamentProfile>();
            });
            mapper = mapperConfiguration.CreateMapper();
        }

        public static List<TournamentDTO> CreateTournamentDTOs()
        {
            return mapper.Map<List<Tournament>, List<TournamentDTO>>(CreateTournaments());
        }
        public static List<Tournament> CreateTournaments()
        {
            return new List<Tournament>()
            {
                new Tournament
                {
                    Id = 1,
                    Name = "Tournament 1",
                    Description = "Description of tournament 1",
                    PreparationTerm = 1,
                    TournamentStart=Convert.ToDateTime("2020-04-04"),
                    TournamentEnd=Convert.ToDateTime("2020-05-04"),
                    CreateDate = new DateTime(2019, 12, 28, 10, 20, 0),
                    CreateUserId = 1,
                    ModDate = new DateTime(2019, 12, 28, 10, 30, 0),
                    ModUserId = 2
                },
                new Tournament
                {
                    Id = 2,
                    Name = "Tournament 2",
                    Description = "Description of tournament 2",
                    PreparationTerm = 2,
                    TournamentStart=Convert.ToDateTime("2020-04-04"),
                    TournamentEnd=Convert.ToDateTime("2020-05-04"),
                    CreateDate = new DateTime(2019, 12, 29, 10, 20, 0),
                    CreateUserId = 2,
                    ModDate = new DateTime(2019, 12, 30, 10, 30, 0),
                    ModUserId = 1
                },
                new Tournament
                {
                    Id = 3,
                    Name = "Tournament 3",
                    Description = "Description of tournament 3",
                    PreparationTerm = 3,
                    TournamentStart=Convert.ToDateTime("2020-04-04"),
                    TournamentEnd=Convert.ToDateTime("2020-05-04"),
                    CreateDate = new DateTime(2020, 01, 01, 11, 22, 0),
                    CreateUserId = 1,
                    ModDate = new DateTime(2020, 01, 02, 10, 30, 0),
                    ModUserId = 2
                }
            };
        }

        public static TournamentDTO CreateTournamentDTO()
        {
            return new TournamentDTO()
            {
                Id = 4,
                Name = "Tournament 4",
                Description = "Description of tournament 4",
                PreparationTerm = 4,
                TournamentStart = Convert.ToDateTime("2020-04-04"),
                TournamentEnd = Convert.ToDateTime("2020-05-04"),
            };
        }
    }
}