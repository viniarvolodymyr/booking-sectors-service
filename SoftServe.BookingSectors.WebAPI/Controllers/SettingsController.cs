using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/settings")]
    [ApiController]
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
            return Ok(await settingService.GetSettingByIdAsync(id));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]SettingsDTO settingsDTO)
        {
            await settingService.UpdateSettingsAsync(id, settingsDTO);
            return Ok();
        }
    }
}
