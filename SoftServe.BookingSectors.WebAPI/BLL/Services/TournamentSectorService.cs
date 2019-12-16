using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;

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

        public async Task<IEnumerable<TournamentSectorDTO>> GetAll()
        {
            var entities = await database.TournamentSectorRepository.GetAllEntitiesAsync();
            var dtos = mapper.Map<IEnumerable<TournamentSector>, IEnumerable<TournamentSectorDTO>>(entities);
            return dtos;
        }


        public async Task<IEnumerable<TournamentSectorDTO>> GetAllTournamentSectorsAsync(int tournId)
        {
            var sectors = await database.TournamentSectorRepository.GetAllEntitiesAsync();
            var tournamentSectors = sectors.Where(x => x.TournamentId == tournId);
            var dtos = mapper.Map<IEnumerable<TournamentSector>, IEnumerable<TournamentSectorDTO>>(tournamentSectors);
            return dtos;
        }
        public async Task<IEnumerable<TournamentSector>> DeleteAllTournamentSectorsAsync(int tournId)
        {
            var sectors = await database.TournamentSectorRepository.GetAllEntitiesAsync();
            var tournamentSectors = sectors.Where(x => x.TournamentId == tournId);
            if (tournamentSectors == null)
            {
                return null;
            }

            foreach (TournamentSector sector in tournamentSectors)
            {
                await database.TournamentSectorRepository.DeleteEntityByIdAsync(sector.Id);
            }
            bool isSaved = await database.SaveAsync();
            return (isSaved == true) ? tournamentSectors : null;
        }

        public async Task<TournamentSector> DeleteSectorFromTournamentAsync(int tournId, int sectorId)
        {
            var sectors = await database.TournamentSectorRepository.GetAllEntitiesAsync();
            var tournSectors = sectors.Where(x => x.TournamentId == tournId);
            if (tournSectors == null)
            {
                return null;
            }
            foreach (TournamentSector sector in tournSectors)
            {
                if (sector.SectorsId == sectorId)
                {
                    var deletedSector = await database.TournamentSectorRepository.DeleteEntityByIdAsync(sector.Id);
                    bool isSaved = await database.SaveAsync();
                    return (isSaved == true) ? deletedSector.Entity : null;
                }
            }
            return null;
        }

        public async Task<TournamentSectorDTO> AddSectorToTournamentAsync(TournamentSectorDTO tournamentSectorDTO)
        {
            var tournSector = mapper.Map<TournamentSectorDTO, TournamentSector>(tournamentSectorDTO);
            var insertedSector = await database.TournamentSectorRepository.InsertEntityAsync(tournSector);
            bool isSaved = await database.SaveAsync();
            if (isSaved == false)
            {
                return null;
            }
            else
            {
                return mapper.Map<TournamentSector, TournamentSectorDTO>(insertedSector.Entity);
            }
        }
        public async Task<TournamentSector> UpdateTournamentSector(int id, TournamentSectorDTO tournamentSectorDTO)
        {
            var tournamentSector = await database.TournamentSectorRepository.GetEntityByIdAsync(id);
            if (tournamentSector == null)
            {
                return null;
            }
            tournamentSector.Id = id;
            tournamentSector.TournamentId = tournamentSectorDTO.TournamentId;
            tournamentSector.SectorsId = tournamentSectorDTO.SectorsId;
            database.TournamentSectorRepository.UpdateEntity(tournamentSector);
            bool isSaved = await database.SaveAsync();
            return (isSaved == true) ? tournamentSector : null;
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
