using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;


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
            var sectors = await _database.Sectors.GetAllEntitiesAsync();
            var dtos = _mapper.Map<IEnumerable<Sector>, List<SectorDTO>>(sectors);

            return dtos;
        }
        public async Task<SectorDTO> GetSectorByIdAsync(int id)
        {
            var entity = await _database.Sectors.GetEntityAsync(id);
            if (entity == null)
            {
                return null;
            }

            var dto = _mapper.Map<Sector, SectorDTO>(entity);

            return dto;
        }
        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
