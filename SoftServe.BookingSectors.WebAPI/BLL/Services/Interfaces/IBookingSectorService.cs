using Microsoft.EntityFrameworkCore.ChangeTracking;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface IBookingSectorService : IDisposable
    {
        Task<IEnumerable<BookingSectorDTO>> GetBookingSectorsAsync();
        Task<BookingSectorDTO> GetBookingByIdAsync(int id);
        Task<IEnumerable<SectorDTO>> GetFreeSectorsAsync(DateTime fromDate, DateTime toDate);
        Task<BookingSectorDTO> BookSector(BookingSectorInfo bookingSectorInfo);
        Task<BookingSector> UpdateBookingApprovedAsync(int id, bool isApproved);
        Task DeleteBookingByIdAsync(int id);
    }
}
