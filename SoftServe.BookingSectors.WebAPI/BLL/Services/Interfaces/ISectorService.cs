using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface ISectorService
    {
        Task<IEnumerable<SectorDTO>> GetAllSectorsAsync();
        Task<SectorDTO> GetSectorByIdAsync(int id);
        Task InsertSectorAsync(SectorDTO sectorDTO);
        Task UpdateSector(int id, SectorDTO sectorDTO);
        Task DeleteSectorByIdAsync(int id);
        void Dispose();
    }
}
