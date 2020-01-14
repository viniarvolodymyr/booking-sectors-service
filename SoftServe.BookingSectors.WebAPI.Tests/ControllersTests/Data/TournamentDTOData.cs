using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftServe.BookingSectors.WebAPI.Tests.ControllersTests.Data
{
    class TournamentDTOData
    {
        public List<TournamentDTO> Tournaments { get; } = new List<TournamentDTO>()
        {
            new TournamentDTO
            {
                Id = 1,
                Name="Tournament 1",
                Description = "Description of tournament 1",
                PreparationTerm=1,
            },
            new TournamentDTO
            {
                Id = 2,
                Name="Tournament 2",
                Description = "Description of tournament 2",
                PreparationTerm=2,
            },
            new TournamentDTO
            {
                 Id = 3,
                Name="Tournament 3",
                Description = "Description of tournament 3",
                PreparationTerm=3,          
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
