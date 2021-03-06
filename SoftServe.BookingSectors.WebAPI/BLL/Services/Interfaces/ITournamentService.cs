﻿using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync();
        Task<TournamentDTO> GetTournamentByIdAsync(int id);
        Task<TournamentDTO> InsertTournamentAsync(TournamentDTO tournamentDTO);
        Task<TournamentDTO> UpdateTournamentAsync(int id, TournamentDTO tournamentDTO);
        Task<TournamentDTO> DeleteTournamentByIdAsync(int id);
    }
}
