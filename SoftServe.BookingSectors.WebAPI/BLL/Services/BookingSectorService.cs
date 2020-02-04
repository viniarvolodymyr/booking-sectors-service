using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class BookingSectorService : IBookingSectorService
    {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;

        public BookingSectorService(IUnitOfWork database, IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookingSectorDTO>> GetBookingSectorsAsync()
        {
            var bookings = await database.BookingSectorRepository.GetAllEntitiesAsync();
            var dtos = mapper.Map<IEnumerable<BookingSector>, IEnumerable<BookingSectorDTO>>(bookings);

            return dtos;
        }

        public async Task<IEnumerable<BookingSectorDTO>> GetBookingsByUserId(int id, bool isActual)
        {
            IEnumerable<BookingSectorDTO> dtos;
            var bookings = await database.BookingSectorRepository.GetAllEntitiesAsync();
            var bookingsByUserId = bookings.Where(b => b.UserId == id);
            if (isActual)
            {
                var actualBookings = bookingsByUserId.Where(b => DateTime.Compare(b.BookingEnd, DateTime.Now.AddDays(-1)) > 0);
                dtos = mapper.Map<IEnumerable<BookingSector>, IEnumerable<BookingSectorDTO>>(actualBookings);
            }
            else
            {
                var historyBookings = bookingsByUserId.Where(b => DateTime.Compare(b.BookingEnd, DateTime.Now.AddDays(-1)) < 0);
                dtos = mapper.Map<IEnumerable<BookingSector>, IEnumerable<BookingSectorDTO>>(historyBookings);
            }
           
            return dtos;
        }

        public async Task<IEnumerable<BookingSectorDTO>> GetBookingTournamentSectorsAsync()
        {
            var bookings = await database.BookingSectorRepository.GetAllEntitiesAsync();
            var tournamentBookings = bookings.Where(b => b.TournamentId.HasValue).OrderBy(b => b.TournamentId & b.SectorId);

            var dtos = mapper.Map<IEnumerable<BookingSector>, IEnumerable<BookingSectorDTO>>(tournamentBookings);

            return dtos;
        }

        public async Task<BookingSectorDTO> GetBookingByIdAsync(int id)
        {
            var booking = await database.BookingSectorRepository.GetEntityByIdAsync(id);
            var dto = mapper.Map<BookingSector, BookingSectorDTO>(booking);

            return dto;
        }

        public async Task<IEnumerable<BookingSectorDTO>> GetBookingTournamentByIdAsync(int id)
        {
            var bookings = await database.BookingSectorRepository.GetAllEntitiesAsync();
            var tournamentBookings = bookings.Where(b => b.TournamentId == id).OrderBy(b => b.SectorId);
            var dto = mapper.Map<IEnumerable<BookingSector>, IEnumerable<BookingSectorDTO>>(tournamentBookings);

            return dto;
        }

        private bool? sectorIsFree(Sector sector, IEnumerable<BookingSector> bookings, DateTime fromDate, DateTime toDate)
        {
            if(sector.IsActive.HasValue)
            {
                return bookings.All(b => (!(b.BookingStart >= fromDate && b.BookingStart <= toDate)
                                                    && !(b.BookingEnd >= fromDate && b.BookingEnd <= toDate)));
            }
            return null;
        }

        public async Task<IEnumerable<SectorDTO>> FilterSectorsByDate(DateTime fromDate, DateTime toDate)
        {
            var bookings = await database.BookingSectorRepository.GetAllEntitiesAsync();
            var sectors = await database.SectorRepository.GetAllEntitiesAsync();

            var entities = sectors.GroupJoin(bookings,
                sector => sector.Id,
                booking => booking.SectorId,
                (sector, bookings) => new Sector()
                {
                    Id = sector.Id,
                    Number = sector.Number,
                    Description = sector.Description,
                    GpsLat = sector.GpsLat,
                    GpsLng = sector.GpsLng,
                    IsActive = sectorIsFree(sector, bookings, fromDate, toDate),
                    CreateUserId = sector.CreateUserId
                });
            return mapper.Map<IEnumerable<Sector>, IEnumerable<SectorDTO>>(entities);
        }

        public async Task<BookingSectorDTO> BookSector(BookingSectorDTO bookingSectorDTO)
        {
            var bookingSector = mapper.Map<BookingSectorDTO, BookingSector>(bookingSectorDTO);
            if(bookingSector.TournamentId.HasValue)
            {
                var tournament = await database.TournamentRepository.GetEntityByIdAsync(bookingSector.TournamentId.Value);

                bookingSector.BookingStart = bookingSector.BookingStart.AddDays(-tournament.PreparationTerm);
            }

            var insertedSector = await database.BookingSectorRepository.InsertEntityAsync(bookingSector);
            bool isSaved = await database.SaveAsync();
           
            return isSaved
                 ? mapper.Map<BookingSector, BookingSectorDTO>(insertedSector)
                 : null;
        }

        public async Task<BookingSectorDTO> UpdateBookingIsApprovedAsync(int id, bool? isApproved)
        {
            var bookingToUpdate = await database.BookingSectorRepository.GetEntityByIdAsync(id);
            if (bookingToUpdate == null)
            {
                return null;
            }
            bookingToUpdate.IsApproved = isApproved;
            database.BookingSectorRepository.UpdateEntity(bookingToUpdate);
            bool isSaved = await database.SaveAsync();

            return isSaved
                 ? mapper.Map<BookingSector, BookingSectorDTO>(bookingToUpdate)
                 : null;
        }

        public async Task<BookingSectorDTO> UpdateBookingTournament(int id, BookingSectorDTO bookingSectorDTO)
        {
            var bookingTournamentToUpdate = await database.BookingSectorRepository.GetEntityByIdAsync(id);
            if (bookingTournamentToUpdate == null)
            {
                return null;
            }
            bookingTournamentToUpdate.Id = id;
            bookingTournamentToUpdate.BookingStart = bookingSectorDTO.BookingStart;
            bookingTournamentToUpdate.BookingEnd = bookingSectorDTO.BookingEnd;
            bookingTournamentToUpdate.IsApproved = bookingSectorDTO.IsApproved;
            bookingTournamentToUpdate.SectorId = bookingSectorDTO.SectorId;
            bookingTournamentToUpdate.TournamentId = bookingSectorDTO.TournamentId;
            database.BookingSectorRepository.UpdateEntity(bookingTournamentToUpdate);

            bool isSaved = await database.SaveAsync();

            return isSaved
                 ? mapper.Map<BookingSector, BookingSectorDTO>(bookingTournamentToUpdate)
                 : null;
        }

        public async Task<BookingSectorDTO> DeleteBookingByIdAsync(int id)
        {
            var bookingToDelete = await database.BookingSectorRepository.DeleteEntityByIdAsync(id);
            if (bookingToDelete == null)
            {
                return null;
            }
            bool isSaved = await database.SaveAsync();

            return isSaved
                 ? mapper.Map<BookingSector, BookingSectorDTO>(bookingToDelete)
                 : null;
        }
    }
}
