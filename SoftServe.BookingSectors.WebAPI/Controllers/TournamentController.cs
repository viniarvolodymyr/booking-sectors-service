using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/tournament")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        readonly ITournamentService tournamentService;
        public TournamentController(ITournamentService service)
        {
            tournamentService = service;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDTO>>> GetAll()
        {
            var dtos = await tournamentService.GetAllTournamentsAsync();
            if (!dtos.Any())
            {
                return NotFound();
            }
            return Ok(dtos);
}
        
        /*
        [HttpGet("{tour_id}")]
        public async Task<TournamentDTO> GetTournament(int tourId)
        {
            return await tournamentService.GetTournamentByIdAsync(tourId);
        }
        /*
        [HttpPost("{name}/{start}/{end}/{prepTerm}")]
        public void Post( string name, DateTime start, DateTime end, int prepTerm )
        {
            Tournament tour = new Tournament() {
                Name = name,
                DateStart=start,
                DateEnd=end,
                PreparationTerm=prepTerm
            };

            tournament.Insert(tour);
            tournament.Save();

        }
                              *
        // PUT: api/Tournament/5
        [HttpPut("{id}")]
        public async Task Put(int id, string name, DateTime start, DateTime end, int prepTerm)
        {
           
        }
    

        [HttpDelete("{tourId}")]
        public async Task Delete(int tourId)
        {
            await tournamentService.DeleteTournamentByIdAsync(tourId);
           
        }
      */
    }
}
