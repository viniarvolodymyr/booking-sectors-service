using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        readonly ITournamentService tournamentService;
        public TournamentController(ITournamentService tournamentService)
        {
            this.tournamentService = tournamentService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> GetAll()
        {
            var dtos = await tournamentService.GetAllTournamentsAsync();
            if (!dtos.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(dtos);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetTournament(int id)
        {
            var dto = await tournamentService.GetTournamentByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dto);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([FromBody] TournamentDTO tournamentDTO)
        {
            var dto = await tournamentService.InsertTournamentAsync(tournamentDTO);
            if (dto == null)
            {
                return BadRequest();
            }
            else
            {
                return Created($"api/tournaments/{dto.Id}", dto);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] TournamentDTO tournamentDTO)
        {
            var tournament = await tournamentService.UpdateTournamentAsync(id, tournamentDTO);
            if (tournament == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tournament);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var tournament = await tournamentService.DeleteTournamentByIdAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tournament);
            }
        }
    }
}
