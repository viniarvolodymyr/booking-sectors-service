using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;

        public TournamentService(IUnitOfWork database, IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }
        private bool tournamentIsBooked(int id, IEnumerable<BookingSector> bookings, DateTime fromDate)
        {
           return  bookings.Any(bookings => bookings.TournamentId==id);
        }
        public async Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync()
        {
            var entities = await database.TournamentRepository.GetAllEntitiesAsync();
            return mapper.Map<IEnumerable<Tournament>, IEnumerable<TournamentDTO>>(entities);
        }

        public async Task<TournamentDTO> GetTournamentByIdAsync(int id)
        {
            var entity = await database.TournamentRepository.GetEntityByIdAsync(id);        
            return mapper.Map<Tournament, TournamentDTO>(entity);
        }

        public async Task<TournamentDTO> InsertTournamentAsync(TournamentDTO tournamentDTO)
        {
            var tournamentToInsert = mapper.Map<TournamentDTO, Tournament>(tournamentDTO);
            var insertedTournament = await database.TournamentRepository.InsertEntityAsync(tournamentToInsert);

            bool isSaved = await database.SaveAsync();
            if (isSaved == false)
            {
                return null;
            }
            else
            {
                return mapper.Map<Tournament, TournamentDTO>(insertedTournament);
            }
        }

        public async Task<TournamentDTO> UpdateTournamentAsync(int id, TournamentDTO tournamentDTO)
        {
            var existedTournament = await database.TournamentRepository.GetEntityByIdAsync(id);
            var tournament = mapper.Map<TournamentDTO, Tournament>(tournamentDTO);
            tournament.Id = id;
            tournament.CreateUserId = existedTournament.CreateUserId;
            tournament.CreateDate = existedTournament.CreateDate;
            tournament.ModDate = DateTime.Now;
            var updatedTournament = database.TournamentRepository.UpdateEntity(tournament);
            var updatedTournamentDTO = mapper.Map<Tournament, TournamentDTO>(updatedTournament);
            bool isSaved = await database.SaveAsync();

            return (isSaved == true) ? updatedTournamentDTO : null;
        }

        public async Task<TournamentDTO> DeleteTournamentByIdAsync(int id)
        {
            var deletedTournament = await database.TournamentRepository.DeleteEntityByIdAsync(id);
            bool isSaved = await database.SaveAsync();
            var tournamentDTO = mapper.Map<Tournament, TournamentDTO>(deletedTournament);
          
            return (isSaved == true) ? tournamentDTO : null;
        }
    }
}
