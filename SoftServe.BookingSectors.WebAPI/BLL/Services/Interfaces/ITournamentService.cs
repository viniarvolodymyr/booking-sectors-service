using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync();
  //      Task<TournamentDTO> GetTournamentByIdAsync(int id);
   //     Task DeleteTournamentByIdAsync(int id);
        void Dispose();
    }
}
