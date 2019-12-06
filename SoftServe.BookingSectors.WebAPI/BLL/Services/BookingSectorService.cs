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
    public class BookingSectorService : IBookingService
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
        public Task<BookingSectorDTO> GetBookingByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SectorDTO>> GetFreeSectorsAsync(DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
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
