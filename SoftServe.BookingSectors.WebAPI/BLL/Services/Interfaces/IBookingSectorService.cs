using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface IBookingSectorService
    {
        Task<IEnumerable<BookingSectorDTO>> GetBookingSectorsAsync();
        Task<BookingSectorDTO> GetBookingByIdAsync(int id);
        Task<IEnumerable<SectorDTO>> GetFreeSectorsAsync(DateTime fromDate, DateTime toDate);
        Task UpdateBookingApprovedAsync(int id, bool isApproved);
        Task DeleteBookingByIdAsync(int id);
        void Dispose();
    }
}
