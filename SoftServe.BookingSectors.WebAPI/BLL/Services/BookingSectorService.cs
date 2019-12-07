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
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public BookingSectorService(IUnitOfWork database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingSectorDTO>> GetBookingSectorsAsync()
        {
            var bookings = await _database.BookingSectors.GetAllEntitiesAsync();
            var dtos = _mapper.Map<IEnumerable<BookingSector>, IEnumerable<BookingSectorDTO>>(bookings);
            return dtos;
        }
        public async Task<BookingSectorDTO> GetBookingByIdAsync(int id)
        {
            var bookingById = await _database.BookingSectors.GetEntityAsync(id);
            var dtos = _mapper.Map<BookingSector, BookingSectorDTO>(bookingById);
            return dtos;
        }

        public async Task<IEnumerable<SectorDTO>> GetFreeSectorsAsync(DateTime fromDate, DateTime toDate)
        {
            var bookings = await _database.BookingSectors.GetAllEntitiesAsync();
            var group = bookings.GroupBy(x => x.SectorId).OrderBy(x => x.Key);
            var sectors = group.Select(temp =>
                new
                {
                    Sector = _database.Sectors.GetEntityAsync(temp.Key),
                    IsFree = temp.All(b => (!(b.BookingStart >= fromDate && b.BookingStart <= toDate)
                                                    && !(b.BookingEnd >= fromDate && b.BookingEnd <= toDate)))
                });
            var freeSectors = sectors.Where(s => s.IsFree).Select(s => s.Sector);
            var dtos = _mapper.Map<IEnumerable<Task<Sector>>, IEnumerable<SectorDTO>>(freeSectors);
            return dtos;
        }

        public void UpdateBookingApproved(int id, bool isApproved)
        {
            throw new NotImplementedException();
        }
        public void DeleteBookingById(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
