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
        Task<IEnumerable<BookingSectorDTO>> GetBookingsByUserId(int id, bool isActual);
        Task<BookingSectorDTO> BookSector(BookingSectorDTO bookingSectorDTO);
        Task<BookingSectorDTO> UpdateBookingIsApprovedAsync(int id, bool? isApproved);
        Task<IEnumerable<SectorDTO>> FilterSectorsByDate(DateTime fromDate, DateTime toDate);
        Task<BookingSectorDTO> DeleteBookingByIdAsync(int id);
          
        Task<IEnumerable<BookingSectorDTO>> GetBookingTournamentSectorsAsync();
        Task<IEnumerable<BookingSectorDTO>> GetBookingTournamentByIdAsync(int idTour);
        Task<BookingSectorDTO> UpdateBookingTournament(int id, BookingSectorDTO bookingSectorDTO);
    }
}
