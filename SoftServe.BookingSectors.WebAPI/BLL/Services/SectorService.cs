using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;


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
            var sectors = await database.Sectors.GetAllEntitiesAsync();
            var dtos = mapper.Map<IEnumerable<Sector>, List<SectorDTO>>(sectors);

            return dtos;
        }
        public async Task<SectorDTO> GetSectorByIdAsync(int id)
        {
            var entity = await database.Sectors.GetEntityAsync(id);
            if (entity == null)
            {
                return null;
            }

            var dto = mapper.Map<Sector, SectorDTO>(entity);

            return dto;
        }
        public void Dispose()
        {
            database.Dispose();
        }
    }
}
