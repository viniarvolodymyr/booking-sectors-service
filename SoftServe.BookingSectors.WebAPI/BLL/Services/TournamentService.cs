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

        public async Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync()
        {
            var entities = await database.TournamentRepository.GetAllEntitiesAsync();
            if (entities == null)
            {
                return null;
            }
            var dtos = mapper.Map<IEnumerable<Tournament>, IEnumerable<TournamentDTO>>(entities);
            return dtos;

        }

        public async Task<TournamentDTO> GetTournamentByIdAsync(int id)
        {
            var enity = await database.TournamentRepository.GetEntityByIdAsync(id);
            if (enity == null)
            {
                return null;
            };
            var dto = mapper.Map<Tournament, TournamentDTO>(enity);
            return dto;

        }

        public async Task InsertTournamentAsync(TournamentDTO tournamentDTO)
        {
            var tournamentToInsert = mapper.Map<TournamentDTO, Tournament>(tournamentDTO);
            tournamentToInsert.ModUserId = null;
            await database.TournamentRepository.InsertEntityAsync(tournamentToInsert);
            await database.SaveAsync();
        }

        public async Task UpdateTournament(int id, TournamentDTO tournamentDTO)
        {
            var entity = await database.TournamentRepository.GetEntityByIdAsync(id);
            var tournament = mapper.Map<TournamentDTO, Tournament>(tournamentDTO);
            tournament.Id = id;
            tournament.CreateUserId = entity.CreateUserId;
            tournament.CreateDate = entity.CreateDate;
            tournament.ModDate = DateTime.Now;
            database.TournamentRepository.UpdateEntity(tournament);
            await database.SaveAsync();
        }

        public async Task DeleteTournamentByIdAsync(int id)
        {
            var entity = await database.TournamentRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return;
            };
            await database.TournamentRepository.DeleteEntityByIdAsync(id);
            await database.SaveAsync();
        }
    }
}
