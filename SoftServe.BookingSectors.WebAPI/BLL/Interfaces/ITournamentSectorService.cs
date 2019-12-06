using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;

namespace SoftServe.BookingSectors.WebAPI.BLL.Interfaces
{
    interface ITournamentSectorService
    {
        Task<IEnumerable<TournamentSectorDTO>> GetAllTournamentSectorsAsync();
        Task<TournamentSectorDTO> GetTournamentSectorByIdAsync(int id);
        void Dispose();
    }
}
