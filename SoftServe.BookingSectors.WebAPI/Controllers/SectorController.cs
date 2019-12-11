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
        public async Task<ActionResult<IEnumerable<SectorDTO>>> Get()
        {
            var dtos = await sectorService.GetAllSectorsAsync();
            if (!dtos.Any())
            {
                return NoContent();
            }
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var dto = await sectorService.GetSectorByIdAsync(id);
            if (dto == null)
            {
                return NoContent();
            }
            return Ok(dto);
        }

        [HttpGet]
        [Route("free")]
        public async Task<ActionResult<IEnumerable<SectorDTO>>> Get([FromQuery]DateTime fromDate, [FromQuery]DateTime toDate)
        {
            var freeSectors = await bookingSectorService.GetFreeSectorsAsync(fromDate, toDate);
            if (!freeSectors.Any())
            {
                return NoContent();
            }
            return Ok(freeSectors);
        }

        [HttpPost]
        public async Task Post([FromBody] SectorDTO sectorDTO)
        {
            await sectorService.InsertSectorAsync(sectorDTO);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task Put([FromRoute]int id, [FromBody] SectorDTO sectorDTO)
        {
            await sectorService.UpdateSector(id, sectorDTO);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await sectorService.DeleteSectorByIdAsync(id);
        }
    }
}
