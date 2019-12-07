using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/sector")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ISectorService _sectorService;
        public SectorController(ISectorService service)
        {
            _sectorService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SectorDTO>>> Get()
        {
            var dtos = await _sectorService.GetAllSectorsAsync();
            if (!dtos.Any())
            {
                return NotFound();
            }
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SectorDTO>> GetById(int id)
        {
            var dto = await _sectorService.GetSectorByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPost]
        public async Task Post([FromBody] SectorDTO sectorDTO)
        {
            await _sectorService.InsertSectorAsync(sectorDTO);
        }
        
        [HttpPut]
        public async Task Put([FromBody] SectorDTO sectorDTO)
        {
            await _sectorService.UpdateSector(sectorDTO);
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _sectorService.DeleteSectorByIdAsync(id);
        }
    }
}
