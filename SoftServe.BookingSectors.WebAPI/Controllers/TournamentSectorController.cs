using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentSectorController : ControllerBase
    {
        readonly ITournamentSectorService tournamentSectorService;
        public TournamentSectorController(ITournamentSectorService service)
        {
            tournamentSectorService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentSectorDTO>>> Get(int tournId)
        {
            var dtos = await tournamentSectorService.GetAllTournamentSectorsAsync(tournId);
            if (!dtos.Any())
            {
                return NotFound();
            }
   
            return Ok(dtos);
        }
        [HttpDelete("{tourId}")]
        public async Task<IActionResult> DeleteAllSectors(int tourId)
        {
            await tournamentSectorService.DeleteAllTournamentSectorsAsync(tourId);
            return Ok();
        }

        [HttpDelete("{tourId}/{sectorId}")]
        public async Task<IActionResult> Delete(int tourId, int sectorId)
        {
            var result = await tournamentSectorService.DeleteSectorFromTournamentAsync(tourId, sectorId);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok();
        }


        [HttpPost(Name = "AddSectorToTournament")]
        public async Task AddSector(int tourId, int sectorId)
        {
             await tournamentSectorService.AddSectorToTournamentAsync(sectorId, tourId);
        }
        

    }
}
