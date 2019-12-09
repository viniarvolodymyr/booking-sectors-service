using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
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
