using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/settings")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService settingService;

        public SettingsController(ISettingsService settingService)
        {
            this.settingService = settingService;
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
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody]SettingsDTO settingsDTO)
        {
            var setting = await settingService.UpdateSettingsAsync(id, settingsDTO);
            if (setting == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(setting);
            }
        }
    }
}
