using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class TournamentSectorService : ITournamentSectorService
    {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;

        public TournamentSectorService(IUnitOfWork database, IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TournamentSectorDTO>> GetAllTournamentSectorsAsync(int id)
        {
            var sectors = await database.TournamentSectorRepository.GetAllEntitiesAsync();
            var tournamentSectors = sectors.Where(x => x.TournamentId == id);
            var dtos = mapper.Map<IEnumerable<TournamentSector>, List<TournamentSectorDTO>>(tournamentSectors);
            return dtos;
        }

        public async Task DeleteAllTournamentSectorsAsync(int tournId)
        {
            var sectors = await database.TournamentSectorRepository.GetAllEntitiesAsync();
            var tournamentSectors = sectors.Where(x => x.TournamentId == tournId);
            foreach (TournamentSector sector in tournamentSectors)
            {
                await database.TournamentSectorRepository.DeleteEntityByIdAsync(sector.Id);
            }
            await database.SaveAsync();
        }

        public async Task<int> DeleteSectorFromTournamentAsync(int tournamentId, int sectorId)
        {
            var sectors = await database.TournamentSectorRepository.GetAllEntitiesAsync();
            var tournSectors = sectors.Where(x => x.TournamentId == tournamentId);
            int result = 0;
            if (tournSectors != null)
            {
                foreach (TournamentSector sector in tournSectors)
                {
                    if (sector.SectorsId == sectorId)
                    {
                        await database.TournamentSectorRepository.DeleteEntityByIdAsync(sector.Id);
                        result = 1;
                    }
                }
                await database.SaveAsync();
            }
            return result;
        }

        public async Task AddSectorToTournamentAsync(int sectId, int tournId)
        {
            TournamentSector sector = new TournamentSector();
            sector.SectorsId = sectId;
            sector.TournamentId = tournId;
            sector.CreateDate = System.DateTime.Today;
            sector.ModDate = System.DateTime.Today;
            sector.CreateUserId = 5;
            await database.TournamentSectorRepository.InsertEntityAsync(sector);
            await database.SaveAsync();
        }
    }
}
