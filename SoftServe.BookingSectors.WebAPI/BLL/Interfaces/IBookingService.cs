using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingSector>> GetBookingSectorsAsync();
        Task<BookingSector> GetBookingByIdAsync();
        Task<IEnumerable<SectorDTO>> GetFreeSectorsAsync(DateTime fromDate, DateTime toDate);
        void UpdateBookingApproved(int id, bool isApproved);
        void DeleteBookingById(int id);
        void Dispose();
    }
}
