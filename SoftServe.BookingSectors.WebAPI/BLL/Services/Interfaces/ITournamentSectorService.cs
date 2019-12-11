using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface ITournamentSectorService
    {
        Task<IEnumerable<TournamentSectorDTO>> GetAllTournamentSectorsAsync(int id);
        Task DeleteAllTournamentSectorsAsync(int id);
        Task<int> DeleteSectorFromTournamentAsync(int sectId, int id);
        Task AddSectorToTournamentAsync(int sectorId, int tournamentId);
    }
}
