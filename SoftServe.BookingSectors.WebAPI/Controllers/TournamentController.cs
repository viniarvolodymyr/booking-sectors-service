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
//    public class TournamentController : ControllerBase
//    {
//        GenericRepository<Tournament> tournament = null;
//        GenericRepository<TournamentSector> sectors = null;
//        TournamentSectorController tournamentSectorController = null;
//        public TournamentController()
//        {
//            tournament = new GenericRepository<Tournament>();
//            sectors = new GenericRepository<TournamentSector>();
//            tournamentSectorController = new TournamentSectorController();
//        }

//        // GET: api/Tournament
//        [HttpGet(Name = "GetTournamentsSectors")]
//        public IActionResult GetAll()
//        {
//            var allTour = tournament.GetAll();
//            var allSectors = sectors.GetAll();
//            var result = allTour.GroupJoin(allSectors,
//               p => p.Id,
//               t => t.IdTournament,
//               (p, t) => new { ID = p.Id, Name = p.Name, DateStart = p.DateStart, DateEnd = p.DateEnd, 
//                   PreparationTerm = p.PreparationTerm, Sectors = tournamentSectorController.GetSectors(p.Id).Select(x => x.IdSectors) }
//                );
//            return Ok(result);
//        }

//        [HttpGet("{tour_id}", Name = "GetTournament")]
//        public Tournament GetTournament(int tour_id)
//        {
//            return tournament.GetById(tour_id);
//        }
   
//        [HttpPost("{name}/{start}/{end}/{prepTerm}")]
//        public void Post( string name, DateTime start, DateTime end, int prepTerm )
//        {
//            Tournament tour = new Tournament() {
//                Name = name,
//                DateStart=start,
//                DateEnd=end,
//                PreparationTerm=prepTerm
//            };

//            tournament.Insert(tour);
//            tournament.Save();

//        }

//        // PUT: api/Tournament/5
//        [HttpPut("{id}")]
//        public void Put(int id, string name, DateTime start, DateTime end, int prepTerm)
//        {
//            Tournament tour = GetTournament(id);
//            tour.Name = name;
//            tour.DateStart = start;
//            tour.DateEnd = end;
//            tour.PreparationTerm = prepTerm;

//            tournament.Update(tour);
//            tournament.Save();
//        }


//        [HttpDelete("{tourId}")]
//        public void Delete(int tourId)
//        {
//            var tourSectors = tournamentSectorController.GetSectors(tourId);
//            if (tourSectors != null)
//            {
//                foreach (TournamentSector sector in tourSectors)
//                {
//                    sectors.Delete(sector.Id);
//                }
//                sectors.Save();
//                tournament.Delete(tourId);
//                tournament.Save();
//            }
//        }

      
      

      

       
//    }
//}
