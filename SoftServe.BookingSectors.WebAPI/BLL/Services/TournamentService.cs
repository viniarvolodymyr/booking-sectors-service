using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
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
            return bookings.All(b => b.BookingStart >= fromDate && b.TournamentId == id);
        }
        public async Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync()
        {
            var tournaments = await database.TournamentRepository.GetAllEntitiesAsync();
            var bookings = await database.BookingSectorRepository.GetAllEntitiesAsync();
        
            var entities = tournaments.GroupJoin(bookings,
                tournament => tournament.Id,
                booking => booking.SectorId,
                (tournament, bookings) => new Tournament()
                {
                    Id = tournament.Id,
                    Name = tournament.Name,
                    Description = tournament.Description,
                    PreparationTerm = tournament.PreparationTerm,
                    IsBooked = tournamentIsBooked(tournament.Id, bookings, DateTime.Now),
                    CreateUserId = tournament.CreateUserId
                });
            return mapper.Map<IEnumerable<Sector>, IEnumerable<SectorDTO>>(entities);
        }

        public async Task<TournamentDTO> GetTournamentByIdAsync(int id)
        {
            var enity = await database.TournamentRepository.GetEntityByIdAsync(id);
            var dto = mapper.Map<Tournament, TournamentDTO>(enity);
            return dto;
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
            if (existedTournament == null)
            {
                return null;
            }
            var tournament = mapper.Map<TournamentDTO, Tournament>(tournamentDTO);
            tournament.Id = id;
            tournament.Name = tournamentDTO.Name;
            tournament.Description = tournamentDTO.Description;
            tournament.PreparationTerm = tournamentDTO.PreparationTerm;
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
