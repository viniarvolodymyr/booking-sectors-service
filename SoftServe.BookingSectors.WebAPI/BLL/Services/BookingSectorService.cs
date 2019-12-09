using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;

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
            var bookings = await database.BookingSectorsRepository.GetAllEntitiesAsync();
            var dtos = mapper.Map<IEnumerable<BookingSector>, IEnumerable<BookingSectorDTO>>(bookings);
            return dtos;
        }
        public async Task<BookingSectorDTO> GetBookingByIdAsync(int id)
        {
            var bookingById = await database.BookingSectorsRepository.GetEntityByIdAsync(id);
            var dtos = mapper.Map<BookingSector, BookingSectorDTO>(bookingById);
            return dtos;
        }

        public async Task<IEnumerable<SectorDTO>> GetFreeSectorsAsync(DateTime fromDate, DateTime toDate)
        {
            var bookings = await database.BookingSectorsRepository.GetAllEntitiesAsync();
            var group = bookings.GroupBy(x => x.SectorId).OrderBy(x => x.Key);
            var sectorsTasks = group.Select(async(temp) =>
                new
                {
                    Sector = await database.SectorsRepository.GetEntityByIdAsync(temp.Key),
                    IsFree = temp.All(b => (!(b.BookingStart >= fromDate && b.BookingStart <= toDate)
                                                    && !(b.BookingEnd >= fromDate && b.BookingEnd <= toDate)))
                });
            var sectors = await Task.WhenAll(sectorsTasks);
            var freeSectors = sectors.Where(s => s.IsFree).Select(s => s.Sector);
            var dtos = mapper.Map<IEnumerable<Sector>, IEnumerable<SectorDTO>>(freeSectors);
            return dtos;
        }

        public async Task UpdateBookingApprovedAsync(int id, bool isApproved)
        {
            var booking = await database.BookingSectorsRepository.GetEntityByIdAsync(id);
            booking.IsApproved = isApproved;
            if (booking == null)
            {
                throw new NullReferenceException();
            }
            database.BookingSectorsRepository.UpdateEntity(booking);
            await database.SaveAsync();
        }
        public async Task DeleteBookingByIdAsync(int id)
        {
            await database.BookingSectorsRepository.DeleteEntityByIdAsync(id);
            await database.SaveAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
