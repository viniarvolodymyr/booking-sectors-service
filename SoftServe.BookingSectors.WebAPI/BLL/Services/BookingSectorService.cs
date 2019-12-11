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

        public async Task<BookingSectorDTO> GetBookingByIdAsync(int id)
        {
            var booking = await database.BookingSectorRepository.GetEntityByIdAsync(id);
            var dtos = mapper.Map<BookingSector, BookingSectorDTO>(booking);
            return dtos;
        }

        public async Task<IEnumerable<SectorDTO>> GetFreeSectorsAsync(DateTime fromDate, DateTime toDate)
        {
            var bookings = await database.BookingSectorRepository.GetAllEntitiesAsync();
            List<Sector> freeSectors = new List<Sector>();
            var group = bookings.GroupBy(x => x.SectorId).OrderBy(x => x.Key);
            Sector sector = null;
            bool isFree;
            foreach (var item in group)
            {
                sector = await database.SectorRepository.GetEntityByIdAsync(item.Key);
                isFree = item.All(b => (!(b.BookingStart >= fromDate && b.BookingStart <= toDate)
                                                    && !(b.BookingEnd >= fromDate && b.BookingEnd <= toDate)));
                if (isFree)
                    freeSectors.Add(sector);
            }
            return mapper.Map<IEnumerable<Sector>, IEnumerable<SectorDTO>>(freeSectors);
        }

        public async Task<BookingSector> UpdateBookingApprovedAsync(int id, bool isApproved)
        {
            var booking = await database.BookingSectorRepository.GetEntityByIdAsync(id);
            if (booking == null)
                return null;

            booking.IsApproved = isApproved;
            database.BookingSectorRepository.UpdateEntity(booking);
            await database.SaveAsync();
            return booking;
        }

        public async Task DeleteBookingByIdAsync(int id)
        {
            await database.BookingSectorRepository.DeleteEntityByIdAsync(id);
            await database.SaveAsync();
        }

        public async Task<BookingSectorDTO> BookSector(BookingSectorDTO bookingSectorDTO)
        {
            var bookingSector = mapper.Map<BookingSectorDTO, BookingSector>(bookingSectorDTO);
            await database.BookingSectorRepository.InsertEntityAsync(bookingSector);
            await database.SaveAsync();
            return mapper.Map<BookingSector, BookingSectorDTO>(bookingSector);
        }
    }
}
