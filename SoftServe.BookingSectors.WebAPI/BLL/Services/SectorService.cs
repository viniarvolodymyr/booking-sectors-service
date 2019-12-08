using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;
using AutoMapper;

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
        public async Task InsertSectorAsync(SectorDTO sectorDTO)
        {
            var sectorToInsert = mapper.Map<SectorDTO, Sector>(sectorDTO);
            sectorToInsert.ModUserId = null;
            await database.SectorsRepository.InsertEntityAsync(sectorToInsert);
            await database.SaveAsync();
        }
        public async Task UpdateSector(SectorDTO sectorDTO)
        { 
            var tempSector = mapper.Map<SectorDTO, Sector>(sectorDTO);
            tempSector.Id = sectorDTO.Id;
            database.SectorsRepository.UpdateEntity(tempSector);
            await database.SaveAsync();
        }
        public async Task DeleteSectorByIdAsync(int id)
        {
            database.SectorsRepository.DeleteEntityByIdAsync(id);
            await database.SaveAsync();
        }
        public void Dispose()
        {
            database.Dispose();
        }
    }
}
