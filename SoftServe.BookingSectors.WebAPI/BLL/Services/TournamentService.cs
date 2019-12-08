using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class TournamentService:ITournamentService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;
        public TournamentService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync()
        {
            var tours = await Database.Tournaments.GetAllEntitiesAsync();
            var sectors = await Database.TournamentSectors.GetAllEntitiesAsync();
            if (tours == null || sectors == null)
            {
                return null;
            }

            var entity =  tours.GroupJoin(
                sectors,
               p => p.Id,
               t => t.IdTournament,
                (p, t) => new Tournament {
                    Id=p.Id,
                    Name = p.Name,
                    DateStart = p.DateStart,
                    DateEnd = p.DateEnd,
                    PreparationTerm = p.PreparationTerm,
                    TournamentSector = t.Where(x => x.IdTournament == p.Id).Select(x=>x).ToList()
                }
            );
            var dtos = _mapper.Map<IEnumerable<Tournament>, List<TournamentDTO>>(entity);
            return dtos;

        }
        /*
        public async Task<TournamentDTO> GetTournamentByIdAsync(int id)
        {
            var tour =await  Database.Tournaments.GetEntityAsync(id);
            var sectors =await  Database.TournamentSectors.GetAllEntitiesAsync();
            if (tour == null|| sectors==null)
            {
                return null;
            };
            tour.TournamentSector = sectors.Where(x=>x.IdTournament == id).Select(x=>x);
            var dto = _mapper.Map<Tournament, TournamentDTO>(tour);
            return dto;

        }
        /*
        public async Task DeleteTournamentByIdAsync(int id) {
            var tour = await Database.Tournaments.GetEntityAsync(id);
            var sectors = await Database.TournamentSectors.GetAllEntitiesAsync();
            if (tour == null || sectors == null)
            {
                return;
            };

            foreach (var sector in sectors) {
               await  Database.TournamentSectors.DeleteEntityAsync(sector.Id);
            }
            await Database.Tournaments.DeleteEntityAsync(id);
        }

        public async Task UpdateTournamentByIdAsync(int id, string name, DateTime dateStart, DateTime dateEnd, int preparationTerm, int [] sectors)
        {
            var tour = await Database.Tournaments.GetEntityAsync(id);
          
            if (tour == null)
            {
                return;
            };

            await Database.Tournaments.DeleteEntityAsync(id);
        }
        */
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
