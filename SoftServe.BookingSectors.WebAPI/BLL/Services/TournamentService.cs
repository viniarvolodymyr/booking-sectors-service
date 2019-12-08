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
            var tours = await _database.tournamentRepositoty.GetAllEntitiesAsync();
            var sectors = await _database.TournamentSectors.GetAllEntitiesAsync();
            if (tours == null || sectors == null)
            {
                return null;
            }

            var entity = tours.GroupJoin(
                sectors,
               p => p.Id,
               t => t.IdTournament,
                (p, t) => new Tournament()
                {
                    Id = p.Id,
                    Name = p.Name,
                    DateStart = p.DateStart,
                    DateEnd = p.DateEnd,
                    PreparationTerm = p.PreparationTerm,
                    TournamentSector = t.Where(x => x.IdTournament == p.Id).ToList()
                }

            );
            var dtos = _mapper.Map<IEnumerable<Tournament>, IEnumerable<TournamentDTO>>(entity);
            return dtos;

        }

        public async Task<TournamentDTO> GetTournamentByIdAsync(int id)
        {
            var tour = await _database.tournamentRepositoty.GetEntityByIdAsync(id);
            var sectors = await _database.TournamentSectors.GetAllEntitiesAsync();
            if (tour == null || sectors == null)
            {
                return null;
            };
            tour.TournamentSector = sectors.Where(x => x.IdTournament == id).Select(x => x).ToList();
            var dto = _mapper.Map<Tournament, TournamentDTO>(tour);
            return dto;

        }

        //change
        public async Task InsertTournamentAsync(TournamentDTO tournamentDTO)
        {
            var tournamentToInsert = _mapper.Map<TournamentDTO, Tournament>(tournamentDTO);
            tournamentToInsert.ModUserId = null;
            await _database.tournamentRepositoty.InsertEntityAsync(tournamentToInsert);

            foreach (var sector in tournamentToInsert.TournamentSector)
            {
                await _database.TournamentSectors.InsertEntityAsync(sector);
            }
            await _database.SaveAsync();
        }
        //modify
        public async Task UpdateTournament(int id, TournamentDTO tournamentDTO)
        {
            var entity = await _database.tournamentRepositoty.GetEntityByIdAsync(id);
            var tournament = _mapper.Map<TournamentDTO, Tournament>(tournamentDTO);
            tournament.Id = id;
            tournament.CreateUserId = entity.CreateUserId;
            tournament.CreateDate = entity.CreateDate;
            tournament.ModDate = DateTime.Now;
            _database.tournamentRepositoty.UpdateEntity(tournament);
           
          //    _database.TournamentSectors.UpdateEntity();        
            await _database.SaveAsync();
        }

       
            public async Task DeleteTournamentByIdAsync(int id)
        {
            var tour = await _database.tournamentRepositoty.GetEntityByIdAsync(id);
            if (tour == null)
            {
                return;
            };
            var tempSectors = await _database.TournamentSectors.GetAllEntitiesAsync();
            var sectors = tempSectors.Where(x => x.IdTournament == id);
            if (sectors != null)
            {
                foreach (var sector in sectors)
                {
                    await _database.TournamentSectors.DeleteEntityByIdAsync(sector.Id);
                }
            }
            await _database.tournamentRepositoty.DeleteEntityByIdAsync(id);
            await _database.SaveAsync();
        }

        public void Dispose()
        {
            _database.Dispose();
        }

    }
}
