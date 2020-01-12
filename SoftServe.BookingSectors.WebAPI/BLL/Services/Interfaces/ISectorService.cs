using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface ISectorService
    {
        Task<IEnumerable<SectorDTO>> GetSectorsAsync();
        Task<int> GetSectorIdByNumberAsync(int number);
        Task<SectorDTO> GetSectorByIdAsync(int id);
        Task<SectorDTO> InsertSectorAsync(SectorDTO sectorDTO);
        Task<SectorDTO> UpdateSectorAsync(int id, SectorDTO sectorDTO);
        Task<SectorDTO> DeleteSectorByIdAsync(int id);
    }
}
