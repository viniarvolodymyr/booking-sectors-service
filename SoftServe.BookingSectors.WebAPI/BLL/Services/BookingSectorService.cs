using AttributeRouting.Helpers;
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



        public async Task<IEnumerable<BookingSectorDTO>> GetBookingTournamentSectorsAsync()
        {
            var bookings = await database.BookingSectorRepository.GetAllEntitiesAsync();
            var tourBookings = bookings.Where(b=>b.TournamentId != null ).OrderBy(x=> x.TournamentId & x.SectorId);

            var dtos = mapper.Map<IEnumerable<BookingSector>, IEnumerable<BookingSectorDTO>>(tourBookings);

            return dtos;
        }



        public async Task<BookingSectorDTO> GetBookingByIdAsync(int id)
        {
            var booking = await database.BookingSectorRepository.GetEntityByIdAsync(id);
            var dto = mapper.Map<BookingSector, BookingSectorDTO>(booking);

            return dto;
        }


        public async Task<IEnumerable<BookingSectorDTO>> GetBookingTournamentByIdAsync(int idTour)
        {
            var bookings = await database.BookingSectorRepository.GetAllEntitiesAsync();
            var tourBookings = bookings.Where(b => b.TournamentId==idTour).OrderBy(x => x.SectorId);
            var dto = mapper.Map<IEnumerable<BookingSector>,IEnumerable< BookingSectorDTO>>(tourBookings);
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
            if (isSaved == false)
            {
                return null;
            }
            else
            {
                return mapper.Map<BookingSector, BookingSectorDTO>(insertedSector);
            }
        }

        public async Task<BookingSector> UpdateBookingApprovedAsync(int id, bool isApproved)
        {
            var booking = await database.BookingSectorRepository.GetEntityByIdAsync(id);
            if (booking == null)
            {
                return null;
            }
            booking.IsApproved = isApproved;
            database.BookingSectorRepository.UpdateEntity(booking);
            bool isSaved = await database.SaveAsync();

            return (isSaved == true) ? booking : null;
        }

        public async Task<BookingSector> UpdateTournamentBooking(int id, BookingSectorDTO bookingSectorDTO)
        {
            var bookedTournament = await database.BookingSectorRepository.GetEntityByIdAsync(id);
            if (bookedTournament == null)
            {
                return null;
            }
            bookedTournament.Id = id;
            bookedTournament.BookingStart = bookingSectorDTO.BookingStart;
            bookedTournament.BookingEnd = bookingSectorDTO.BookingEnd;
            bookedTournament.IsApproved = bookingSectorDTO.IsApproved;
            bookedTournament.SectorId = bookingSectorDTO.SectorId;
            bookedTournament.TournamentId = bookingSectorDTO.TournamentId;
            database.BookingSectorRepository.UpdateEntity(bookedTournament);
            bool isSaved = await database.SaveAsync();
            return (isSaved == true) ? bookedTournament : null;
        }

        public async Task<BookingSector> DeleteBookingByIdAsync(int id)
        {
            var booking = await database.BookingSectorRepository.DeleteEntityByIdAsync(id);
            if (booking == null)
            {
                return null;
            }
            bool isSaved = await database.SaveAsync();

            return (isSaved == true) ? booking.Entity : null;
        }
    }
}
