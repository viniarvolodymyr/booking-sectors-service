using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;
using AutoMapper;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class TournamentSectorService : ITournamentSectorService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;
        public TournamentSectorService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TournamentSectorDTO>> GetAllTournamentSectorsAsync()
        {
            var tournamentSectors = await Database.TournamentSectors.GetAllEntitiesAsync();
            var dtos = _mapper.Map<IEnumerable<TournamentSector>, List<TournamentSectorDTO>>(tournamentSectors);
            return dtos;
        }
        public async Task<TournamentSectorDTO> GetTournamentSectorByIdAsync(int id)
        {
            var entity = await Database.TournamentSectors.GetEntityAsync(id);
            if (entity == null)
            {
                return null;
            }
            var dto = _mapper.Map<TournamentSector, TournamentSectorDTO>(entity);
            return dto;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
