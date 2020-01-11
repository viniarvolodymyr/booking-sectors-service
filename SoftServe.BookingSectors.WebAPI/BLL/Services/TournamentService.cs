using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
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

        public async Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync()
        {
            var entities = await database.TournamentRepository.GetAllEntitiesAsync();
            var dtos = mapper.Map<IEnumerable<Tournament>, IEnumerable<TournamentDTO>>(entities);
            return dtos;
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
                return mapper.Map<Tournament, TournamentDTO>(insertedTournament.Entity);
            }
        }

        public async Task<Tournament> UpdateTournament(int id, TournamentDTO tournamentDTO)
        {
            var tournament = await database.TournamentRepository.GetEntityByIdAsync(id);
            if (tournament == null)
            {
                return null;
            }
            tournament.Id = id;
            tournament.Name = tournamentDTO.Name;
            tournament.PreparationTerm = tournamentDTO.PreparationTerm;
            database.TournamentRepository.UpdateEntity(tournament);
            bool isSaved = await database.SaveAsync();
            return (isSaved == true) ? tournament : null;
        }

        public async Task<Tournament> DeleteTournamentByIdAsync(int id)
        {
            var tournament = await database.TournamentRepository.DeleteEntityByIdAsync(id);
            if (tournament == null)
            {
                return  null;
            }
            bool isSaved = await database.SaveAsync();
            return (isSaved == true) ? tournament.Entity : null;
        }

        public void Dispose()
        {
            database.Dispose();
        }

    }
}
