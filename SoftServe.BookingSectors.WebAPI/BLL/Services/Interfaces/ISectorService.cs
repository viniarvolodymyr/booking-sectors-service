using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
namespace SoftServe.BookingSectors.WebAPI.BLL.Interfaces
{
    public interface ISectorService
    {
        Task<IEnumerable<SectorDTO>> GetAllSectorsAsync();
        Task<SectorDTO> GetSectorByIdAsync(int id);
        void Dispose();
    }
}
