using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface ITournamentSectorService
    {
        Task<IEnumerable<TournamentSectorDTO>> GetAll();
        Task<IEnumerable<TournamentSectorDTO>> GetAllTournamentSectorsAsync(int tourId);
        Task<IEnumerable<TournamentSector>> DeleteAllTournamentSectorsAsync(int tourId);
        Task<TournamentSector> DeleteSectorFromTournamentAsync(int sectId, int tourId);
        Task<TournamentSector> UpdateTournamentSector(int id, TournamentSectorDTO tournamentSectorDTO);
        Task<TournamentSectorDTO> AddSectorToTournamentAsync(TournamentSectorDTO tournamentSectorDTO);
        void Dispose();
    }
}
