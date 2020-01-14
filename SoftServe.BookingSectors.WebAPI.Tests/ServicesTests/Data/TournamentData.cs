using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests.Data
{
    class TournamentData
    {
        public TournamentData() { }

        public List<Tournament> Tournaments { get; } = new List<Tournament>()
        {
            new Tournament
            {
                Id = 1,
                Name="Tournament 1",
                Description = "Description of tournament 1",
                PreparationTerm=1,
                CreateDate = new DateTime(2019, 12, 28, 10, 20, 0),
                CreateUserId = 1,
                ModDate = new DateTime(2019, 12, 28, 10, 30, 0),
                ModUserId = 2
            },
            new Tournament
            {
                Id = 2,
                Name="Tournament 2",
                Description = "Description of tournament 2",
                PreparationTerm=2,
                CreateDate = new DateTime(2019, 12, 29, 10, 20, 0),
                CreateUserId = 2,
                ModDate = new DateTime(2019, 12, 30, 10, 30, 0),
                ModUserId = 1
            },
            new Tournament
            {
                 Id = 3,
                Name="Tournament 3",
                Description = "Description of tournament 3",
                PreparationTerm=3,
                CreateDate = new DateTime(2020, 01, 01, 11, 22, 0),
                CreateUserId = 1,
                ModDate = new DateTime(2020, 01, 02, 10, 30, 0),
                ModUserId = 2
            }
        };

        public TournamentDTO TournamentDTOToInsert { get; } = new TournamentDTO()
        {
            Id = 4,
            Name = "Tournament 4",
            Description = "Description of tournament 4",
            PreparationTerm = 4,
        };
    }
}