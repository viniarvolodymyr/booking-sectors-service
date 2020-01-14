using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/sectors")]
    [ApiController]
<<<<<<< HEAD
    [AllowAnonymous]
=======

>>>>>>> f24ba86fe815f7e36379f02bdb5c3fb84b66d800
    public class SectorController : ControllerBase
    {
        private readonly ISectorService sectorService;
        private readonly IBookingSectorService bookingSectorService;
        public SectorController(ISectorService sectorService, IBookingSectorService bookingSectorService)
        {
            this.sectorService = sectorService;
            this.bookingSectorService = bookingSectorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dtos = await sectorService.GetSectorsAsync();
            if (dtos.Any())
            {
                return Ok(dtos);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var dto = await sectorService.GetSectorByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dto);
            }
        }

        [HttpGet]
        [Route("free")]
        public async Task<IActionResult> Get([FromQuery]DateTime fromDate, [FromQuery]DateTime toDate)
        {
            var freeSectors = await bookingSectorService.FilterSectorsByDate(fromDate, toDate);
            if (!freeSectors.Any())
            {
                return NoContent();
            }

            return Ok(freeSectors);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SectorDTO sectorDTO)
        {
            var sector = await sectorService.InsertSectorAsync(sectorDTO);
            if (sector == null)
            {
                return BadRequest();
            }
            else
            {
                return Created($"api/sectors/{sector.Id}", sector);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] SectorDTO sectorDTO)
        {
            var sector = await sectorService.UpdateSectorAsync(id, sectorDTO);
            if (sector == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(sector);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var sector = await sectorService.DeleteSectorByIdAsync(id);
            if (sector == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(sector);
            }
        }
    }
}
