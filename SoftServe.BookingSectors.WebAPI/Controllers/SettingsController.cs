using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.Interfaces;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService settingService;
        public SettingsController(ISettingsService settings)
        {
            this.settingService = settings;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dtos = await settingService.GetSettingsAsync();
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
            var dto = await settingService.GetSettingByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dto);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] SettingsDTO sectorDTO)
        {
            var sector = await settingService.UpdateSettingsAsync(id, sectorDTO);
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
