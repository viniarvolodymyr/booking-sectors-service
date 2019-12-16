using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService tournamentService;
        private readonly ITournamentSectorService tournamentSectorService;

        public TournamentController(ITournamentService tournamentService, ITournamentSectorService tournamentSectorService)
        {
            this.tournamentService = tournamentService;
            this.tournamentSectorService = tournamentSectorService;
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

        [HttpGet]
        [Route("{id}")]
        public Task<TournamentDTO> GetTournament([FromRoute]int id)
        {
            return tournamentService.GetTournamentByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TournamentDTO tournamentDTO)
        {
            await tournamentService.InsertTournamentAsync(tournamentDTO);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task Put([FromRoute]int tournamentId, [FromBody]TournamentDTO tournamentDTO)
        {
            await tournamentService.UpdateTournament(tournamentId, tournamentDTO);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await tournamentSectorService.DeleteAllTournamentSectorsAsync(id);
            await tournamentService.DeleteTournamentByIdAsync(id);
        }
    }
}
