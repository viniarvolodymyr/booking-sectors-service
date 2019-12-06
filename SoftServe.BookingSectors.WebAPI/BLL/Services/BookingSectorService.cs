using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;
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
        public void DeleteBookingById(int id)
        {
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<BookingSectorDTO> GetBookingByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookingSectorDTO>> GetBookingSectorsAsync()
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
    }
}
