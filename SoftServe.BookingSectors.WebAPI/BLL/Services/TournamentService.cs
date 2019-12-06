using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class TournamentService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;
        public TournamentService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TournamentDTO>> GetAllTournamentssAsync() { 
        
        }
        public async Task<TournamentDTO> GetTournamentByIdAsync(int id) {

            var entity =  await Database.Tournaments.GetEntityAsync(id);
            if (entity == null)
            {
                return null;
            }
            var dto = _mapper.Map<Tournament, TournamentDTO>(entity);
            return dto;

        }



        void Dispose()
        {
            Database.Dispose();
        }
    }
}
