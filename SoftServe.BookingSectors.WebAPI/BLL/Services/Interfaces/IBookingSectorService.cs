using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    public interface IBookingSectorService
    {
        Task<IEnumerable<BookingSectorDTO>> GetBookingSectorsAsync();
        Task<BookingSectorDTO> GetBookingByIdAsync(int id);
        Task<IEnumerable<SectorDTO>> FilterSectorsByDate(DateTime fromDate, DateTime toDate);
        Task<BookingSectorDTO> BookSector(BookingSectorDTO bookingSectorDTO);
        Task<BookingSector> UpdateBookingApprovedAsync(int id, bool isApproved);
        Task<BookingSector> DeleteBookingByIdAsync(int id);
        Task<IEnumerable<BookingSectorDTO>> GetBookingsByUserId(int id);

        Task<IEnumerable<BookingSectorDTO>> GetBookingTournamentSectorsAsync();
        Task<IEnumerable<BookingSectorDTO>> GetBookingTournamentByIdAsync(int idTour);
        Task<BookingSector> UpdateTournamentBooking(int id, BookingSectorDTO bookingSectorDTO);

    }
}
