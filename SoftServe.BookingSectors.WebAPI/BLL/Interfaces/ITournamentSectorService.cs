using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;

namespace SoftServe.BookingSectors.WebAPI.BLL.Interfaces
{
    public interface ITournamentSectorService
    {
        Task<IEnumerable<TournamentSectorDTO>> GetAllTournamentSectorsAsync(int tournId);
        Task DeleteAllTournamentSectorsAsync(int tournId);
        Task<int> DeleteSectorFromTournamentAsync(int sectId, int tournId);
        Task AddSectorToTournamentAsync(int sectId, int tournId);
        void Dispose();
    }
}
