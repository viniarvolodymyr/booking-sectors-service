using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/tournament")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        readonly ITournamentService tournamentService;
        readonly ITournamentSectorService tournamentSectorService;
        public TournamentController(ITournamentService tournamentService, ITournamentSectorService tournamentSectorService)
        {
            this.tournamentService = tournamentService;
            this.tournamentSectorService = tournamentSectorService;

        }


        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TournamentDTO>>> GetAll()
        {
            var dtos = await tournamentService.GetAllTournamentsAsync();
            if (!dtos.Any())
            {
                return NotFound();
            }
            return Ok(dtos);
        }


        [HttpGet("{tourId}")]
        public async Task<TournamentDTO> GetTournament(int tourId)
        {
            return await tournamentService.GetTournamentByIdAsync(tourId);
        }

        [HttpPost]
        public async Task Post([FromBody] TournamentDTO tournamentDTO)
        {
            await tournamentService.InsertTournamentAsync(tournamentDTO);
        }
        [HttpPut("{tourId}")]
        public async Task Put(int tourId, [FromBody] TournamentDTO tournamentDTO)
        {
            await tournamentService.UpdateTournament(tourId, tournamentDTO);
        }
        [HttpDelete("{tourId}")]
        public async Task Delete(int tourId)
        {
            await tournamentSectorService.DeleteAllTournamentSectorsAsync(tourId);
            await tournamentService.DeleteTournamentByIdAsync(tourId);
        }
    }
}
