//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SoftServe.BookingSectors.WebAPI.Repositories;
//using SoftServe.BookingSectors.WebAPI.Models;

//namespace SoftServe.BookingSectors.WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TournamentSectorController : ControllerBase
//    {

//        GenericRepository<TournamentSector> sectors = null;
//        public TournamentSectorController()
//        {
//            sectors = new GenericRepository<TournamentSector>();
//        }

//        [HttpGet("{tourId}", Name = "GetSectors")]
//        public IEnumerable<TournamentSector> GetSectors(int tourId)
//        {
//            return sectors.GetAll().Where(x => x.IdTournament == tourId);
//        }


//        [HttpDelete("{tourId}/{sectorId}")]
//        public void Delete(int tourId, int sectorId)
//        {
//            var tourSectors = GetSectors(tourId);
//            if (tourSectors != null)
//            {
//                foreach (TournamentSector sector in tourSectors)
//                {
//                    if (sector.IdSectors == sectorId)
//                        sectors.Delete(sector.Id);
//                }
//                sectors.Save();

//            }
//        }


//        [HttpPost(Name = "AddSectorToTournaments")]
//        public void AddSectorToTournament(int tournId, int sectId)
//        {
//            TournamentSector tournamentSector = new TournamentSector();
//            tournamentSector.IdSectors = sectId;
//            tournamentSector.IdTournament = tournId;
//            sectors.Insert(tournamentSector);
//            sectors.Save();
//        }

//    }
//}
