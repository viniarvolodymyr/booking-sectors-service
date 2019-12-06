using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDTO>> GetAllTournamentssAsync();
        Task<TournamentDTO> GetTournamentByIdAsync(int id);
        void Dispose();
    }
}
