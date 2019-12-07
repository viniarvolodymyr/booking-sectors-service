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
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        public SectorService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SectorDTO>> GetAllSectorsAsync()
        {  
            var sectors = await _database.SectorsRepository.GetAllEntitiesAsync();
            var dtos = _mapper.Map<IEnumerable<Sector>, List<SectorDTO>>(sectors);
            return dtos;
        }
        public async Task<SectorDTO> GetSectorByIdAsync(int id)
        {
            var entity = await _database.SectorsRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            var dto = _mapper.Map<Sector, SectorDTO>(entity);
            return dto;
        }
        public async Task InsertSectorAsync(SectorDTO sectorDTO)
        {
            var sectorToInsert = _mapper.Map<SectorDTO, Sector>(sectorDTO);
            sectorToInsert.ModUserId = null;
            await _database.SectorsRepository.InsertEntityAsync(sectorToInsert);
            await _database.SaveAsync();
        }
        public async Task UpdateSector(SectorDTO sectorDTO)
        { 
            var tempSector = _mapper.Map<SectorDTO, Sector>(sectorDTO);
            tempSector.Id = sectorDTO.Id;
            _database.SectorsRepository.UpdateEntity(tempSector);
            await _database.SaveAsync();
        }
        public async Task DeleteSectorByIdAsync(int id)
        {
            _database.SectorsRepository.DeleteEntityByIdAsync(id);
            await _database.SaveAsync();
        }
        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
