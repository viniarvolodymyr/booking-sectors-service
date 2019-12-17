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
            var sector = await database.SectorRepository.GetAllEntitiesAsync();
            return sector.Where(x => (x.Number == number)).Select(x => x.Id).FirstOrDefault();
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
                return mapper.Map<Sector, SectorDTO>(insertedSector.Entity);
            }
        }

        public async Task<Sector> UpdateSectorAsync(int id, SectorDTO sectorDTO)
        {
            var existedSector = await database.SectorRepository.GetEntityByIdAsync(id);
            if (existedSector == null)
            {
                return null;
            }
            var sector = mapper.Map<SectorDTO, Sector>(sectorDTO);
            sector.Id = id;
            sector.CreateUserId = existedSector.CreateUserId;
            sector.CreateDate = existedSector.CreateDate;
            sector.ModDate = DateTime.Now;
            database.SectorRepository.UpdateEntity(sector);
            bool isSaved = await database.SaveAsync();

            return (isSaved == true) ? sector : null;
        }

        public async Task<Sector> DeleteSectorByIdAsync(int id)
        {
            var sector = await database.SectorRepository.DeleteEntityByIdAsync(id);           
            if (sector == null)
            {
                return null;
            }
            bool isSaved = await database.SaveAsync();

            return (isSaved == true) ? sector.Entity : null;
        }
    }
}
