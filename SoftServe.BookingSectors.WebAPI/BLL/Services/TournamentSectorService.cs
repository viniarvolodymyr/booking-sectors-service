﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;
using AutoMapper;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class TournamentSectorService : ITournamentSectorService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;
        public TournamentSectorService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TournamentSectorDTO>> GetAllTournamentSectorsAsync(int tournId)
        {
            var sectors = await Database.TournamentSectors.GetAllEntitiesAsync();
            var tournamentSectors = sectors.Where(x => x.TournamentId == tournId);

            var dtos = _mapper.Map<IEnumerable<TournamentSector>, List<TournamentSectorDTO>>(tournamentSectors);
            return dtos;
        }
        public async Task<int> DeleteSectorFromTournamentAsync(int tournId, int sectorId)
        {
            var sectors = await Database.TournamentSectors.GetAllEntitiesAsync();
            var tournSectors = sectors.Where(x => x.TournamentId == tournId);
            int result = 0;
            if (tournSectors != null)
            {
                foreach (TournamentSector sector in tournSectors)
                {
                    if (sector.SectorsId == sectorId)
                    {
                        await Database.TournamentSectors.DeleteEntityAsync(sector.Id);
                        result = 1;
                    }
                }
                await Database.SaveAsync();
            }
            return result;
        }

        public async Task AddSectorToTournamentAsync(int sectId, int tournId)
        {
            var sect =  await Database.Sectors.GetEntityAsync(sectId);
            //var tourn = await Database.Tournament.GetEntityAsync(tournId);
       
            if (sect != null)
            {
                TournamentSector sector = new TournamentSector();
                sector.SectorsId = sectId;
                sector.TournamentId = tournId;
                await Database.TournamentSectors.InsertEntityAsync(sector);
                await Database.SaveAsync();
            }

        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
