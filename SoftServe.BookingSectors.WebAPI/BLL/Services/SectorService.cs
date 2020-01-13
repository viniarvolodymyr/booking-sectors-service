using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using System.Net;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class SectorService : ISectorService
    {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;

        public SectorService(IUnitOfWork database, IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SectorDTO>> GetSectorsAsync()
        {
            var sectors = await database.SectorRepository.GetAllEntitiesAsync();
            var dtos = mapper.Map<IEnumerable<Sector>, IEnumerable<SectorDTO>>(sectors);
           
            return dtos;
        }

        public async Task<SectorDTO> GetSectorByIdAsync(int id)
        {
            var sector = await database.SectorRepository.GetEntityByIdAsync(id);
            var dto = mapper.Map<Sector, SectorDTO>(sector);
            
            return dto;
        }

        public async Task<int> GetSectorIdByNumberAsync(int number)
        {
            int sectorId = await database.SectorRepository
                    .GetByCondition(x => x.Number == number)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();
            if (sectorId == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Sector with number: {number} not found when trying to get id.");
            }

            return sectorId;
        }

        public async Task<SectorDTO> InsertSectorAsync(SectorDTO sectorDTO)
        {
            var sector = mapper.Map<SectorDTO, Sector>(sectorDTO);
            var insertedSector = await database.SectorRepository.InsertEntityAsync(sector);
            
            
            bool isSaved = await database.SaveAsync();
            if (isSaved == false)
            {
                return null;
            }
            else
            {
                return mapper.Map<Sector, SectorDTO>(insertedSector);
            }
        }

        public async Task<SectorDTO> UpdateSectorAsync(int id, SectorDTO sectorDTO)
        {
            var existedSector = await database.SectorRepository.GetEntityByIdAsync(id);
            var sector = mapper.Map<SectorDTO, Sector>(sectorDTO);
            sector.Id = id;
            sector.CreateUserId = existedSector.CreateUserId;
            sector.CreateDate = existedSector.CreateDate;
            sector.ModDate = DateTime.Now;
            var updatedSector = database.SectorRepository.UpdateEntity(sector);
            var updatedSectorDTO = mapper.Map<Sector, SectorDTO>(updatedSector);
            bool isSaved = await database.SaveAsync();

            return (isSaved == true) ? updatedSectorDTO : null;
        }

        public async Task<SectorDTO> DeleteSectorByIdAsync(int id)
        {
            var deletedSector = await database.SectorRepository.DeleteEntityByIdAsync(id);
            bool isSaved = await database.SaveAsync();
            var sectorDTO = mapper.Map<Sector, SectorDTO>(deletedSector);

            return (isSaved == true) ? sectorDTO : null;
        }
    }
}
