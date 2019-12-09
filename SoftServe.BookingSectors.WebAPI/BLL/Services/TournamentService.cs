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
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        public TournamentService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync()
        {
            var entities = await _database.TournamentRepository.GetAllEntitiesAsync();
            if (entities == null)
            {
                return null;
            }
            var dtos = _mapper.Map<IEnumerable<Tournament>, IEnumerable<TournamentDTO>>(entities);
            return dtos;

        }

        public async Task<TournamentDTO> GetTournamentByIdAsync(int id)
        {
            var enity = await _database.TournamentRepository.GetEntityByIdAsync(id);
            if (enity == null)
            {
                return null;
            };
            var dto = _mapper.Map<Tournament, TournamentDTO>(enity);
            return dto;

        }

        //change
        public async Task InsertTournamentAsync(TournamentDTO tournamentDTO)
        {
            var tournamentToInsert = _mapper.Map<TournamentDTO, Tournament>(tournamentDTO);
            tournamentToInsert.ModUserId = null;
            await _database.TournamentRepository.InsertEntityAsync(tournamentToInsert);
            await _database.SaveAsync();
        }
        //modify
        public async Task UpdateTournament(int id, TournamentDTO tournamentDTO)
        {
            var entity = await _database.TournamentRepository.GetEntityByIdAsync(id);
            var tournament = _mapper.Map<TournamentDTO, Tournament>(tournamentDTO);
            tournament.Id = id;
            tournament.CreateUserId = entity.CreateUserId;
            tournament.CreateDate = entity.CreateDate;
            tournament.ModDate = DateTime.Now;
            _database.TournamentRepository.UpdateEntity(tournament);
            await _database.SaveAsync();
        }

       
            public async Task DeleteTournamentByIdAsync(int id)
        {
            var entity = await _database.TournamentRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return;
            };
            await _database.TournamentRepository.DeleteEntityByIdAsync(id);
            await _database.SaveAsync();
        }

        public void Dispose()
        {
            _database.Dispose();
        }

    }
}
