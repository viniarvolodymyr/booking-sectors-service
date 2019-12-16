using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/tournamentsSectors")]
    [ApiController]
    public class TournamentSectorController : ControllerBase
    {
        private readonly ITournamentSectorService tournamentSectorService;

        public TournamentSectorController(ITournamentSectorService tournamentSectorService)
        {
            this.tournamentSectorService = tournamentSectorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentSectorDTO>>> Get(int id)
        {
            var dtos = await tournamentSectorService.GetAllTournamentSectorsAsync(id);
            if (!dtos.Any())
            {
                return NotFound();
            }
            return Ok(dtos);
        }

        [HttpPost]
        public async Task AddSector(int tourId, int sectorId)
        {
            await tournamentSectorService.AddSectorToTournamentAsync(sectorId, tourId);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAllSectors([FromRoute]int id)
        {
            await tournamentSectorService.DeleteAllTournamentSectorsAsync(id);
            return Ok();
        }

        [HttpDelete]
        [Route("{tournamentId}/{sectorId}")]
        public async Task<IActionResult> Delete([FromRoute]int tournamentId, [FromRoute]int sectorId)
        {
            var result = await tournamentSectorService.DeleteSectorFromTournamentAsync(tournamentId, sectorId);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
