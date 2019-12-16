using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/tournaments")]
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

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> GetAll()
        {
            var dtos = await tournamentService.GetAllTournamentsAsync();
            if (!dtos.Any())
            {
                return NotFound();
            }
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{tourId}")]
        public async Task<ActionResult> GetTournament(int tourId)
        {
            var dto = await tournamentService.GetTournamentByIdAsync(tourId);
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
        [Route("{tourId}")]
        public async Task<IActionResult> Put([FromRoute]int tourId, [FromBody] TournamentDTO tournamentDTO)
        {
            var tournament = await tournamentService.UpdateTournament(tourId, tournamentDTO);
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
        [Route("{tourId}")]
        public async Task<ActionResult> Delete(int tourId)
        {
            var sectors = await tournamentSectorService.DeleteAllTournamentSectorsAsync(tourId);
            if (sectors == null)
            {
                return NotFound();
            }
            var tournament = await tournamentService.DeleteTournamentByIdAsync(tourId);
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
