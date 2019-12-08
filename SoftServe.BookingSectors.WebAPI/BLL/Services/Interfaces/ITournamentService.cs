using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync();
        Task<TournamentDTO> GetTournamentByIdAsync(int id);
        Task InsertTournamentAsync(TournamentDTO tournamentDTO);
        Task UpdateTournament(int tourId, TournamentDTO tournamentDTO);
        Task DeleteTournamentByIdAsync(int id);
        void Dispose();
    }
}
