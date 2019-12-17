using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
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
        Task<Sector> UpdateSectorAsync(int id, SectorDTO sectorDTO);
        Task<Sector> DeleteSectorByIdAsync(int id);
    }
}
