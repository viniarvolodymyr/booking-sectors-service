using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/tournamentSectors")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TournamentSectorController : ControllerBase
    {
        readonly ITournamentSectorService tournamentSectorService;
        public TournamentSectorController(ITournamentSectorService tournamentSectorService)
        {
           this.tournamentSectorService = tournamentSectorService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> GetAll()
        {
            var dtos = await tournamentSectorService.GetAll();
            if (!dtos.Any())
            {
                return NotFound();
            }

            return Ok(dtos);
        }

        [HttpGet]
        [Route("{tourId}")]
        public async Task<ActionResult> Get(int tourId)
        {
            var dtos = await tournamentSectorService.GetAllTournamentSectorsAsync(tourId);
            if (!dtos.Any())
            {
                return NotFound();
            }
            return Ok(dtos);
        }

        [HttpDelete]
        [Route("{tourId}/{sectorId}")]
        public async Task<IActionResult> Delete([FromRoute]int tourId, [FromRoute]int sectorId)
        {
            var result = await tournamentSectorService.DeleteSectorFromTournamentAsync(tourId, sectorId);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddSector([FromBody] TournamentSectorDTO tournamentSectorDTO)
        {
            var dto = await tournamentSectorService.AddSectorToTournamentAsync(tournamentSectorDTO);
            if (dto == null)
            {
                return BadRequest();
            }
            else
            {
                return Created($"api/tournamentSectors/{dto.Id}", dto);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TournamentSectorDTO tournamentSectorDTO)
        {
            var tournament = await tournamentSectorService.UpdateTournamentSector(id, tournamentSectorDTO);
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
