using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;

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
                return NotFound();
            }
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SectorDTO>> GetById(int id)
        {
            var dto = await sectorService.GetSectorByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpGet("free", Name = "GetFreeSectors")]
        public async Task<ActionResult<IEnumerable<SectorDTO>>> Get([FromQuery]DateTime fromDate, [FromQuery]DateTime toDate)
        {
            var freeSectors = await bookingSectorService.GetFreeSectorsAsync(fromDate, toDate);
            if (!freeSectors.Any())
            {
                return NotFound();
            }
            return Ok(freeSectors);
        }

        [HttpPost]
        public async Task Post([FromBody] SectorDTO sectorDTO)
        {
            await sectorService.InsertSectorAsync(sectorDTO);
        }
        
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] SectorDTO sectorDTO)
        {
            await sectorService.UpdateSector(id, sectorDTO);
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await sectorService.DeleteSectorByIdAsync(id);
        }
    }
}
