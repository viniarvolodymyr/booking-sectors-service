using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using AutoMapper;
using System;
using System.Linq;

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
        public async Task<IEnumerable<SectorDTO>> GetAllSectorsAsync()
        {  
            var sectors = await database.SectorsRepository.GetAllEntitiesAsync();
            var dtos = mapper.Map<IEnumerable<Sector>, List<SectorDTO>>(sectors);
            return dtos;
        }
        public async Task<SectorDTO> GetSectorByIdAsync(int id)
        {
            var entity = await database.SectorsRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            var dto = mapper.Map<Sector, SectorDTO>(entity);
            return dto;
        }
        public async Task<int> GetSectorIdByNumberAsync(int number)
        {
            var entity = await database.SectorsRepository.GetAllEntitiesAsync();
            return entity.Where(x => (x.Number == number)).Select(x => x.Id).FirstOrDefault();
        }
        public async Task InsertSectorAsync(SectorDTO sectorDTO)
        {
            var sectorToInsert = mapper.Map<SectorDTO, Sector>(sectorDTO);
            sectorToInsert.ModUserId = null;
            await database.SectorsRepository.InsertEntityAsync(sectorToInsert);
            await database.SaveAsync();
        }
        public async Task UpdateSector(int id, SectorDTO sectorDTO)
        {
            var entity = await database.SectorsRepository.GetEntityByIdAsync(id);

            var sector = mapper.Map<SectorDTO, Sector>(sectorDTO);
            sector.Id = id;
            sector.CreateUserId = entity.CreateUserId;
            sector.CreateDate = entity.CreateDate;
            sector.ModDate = DateTime.Now;

            database.SectorsRepository.UpdateEntity(sector);
            await database.SaveAsync();
        }
        public async Task DeleteSectorByIdAsync(int id)
        {
            await database.SectorsRepository.DeleteEntityByIdAsync(id);
            await database.SaveAsync();
        }
        public void Dispose()
        {
            database.Dispose();
        }
    }
}
