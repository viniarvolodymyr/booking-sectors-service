using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
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
        [Route("{settingName}")]
        public async Task<IActionResult> Get([FromRoute]string settingName)
        {
            return Ok(await settingService.GetSettingByIdAsync(settingName));
        }

        [HttpPut]
        [Route("{settingName}")]
        public async Task<IActionResult> Update([FromRoute]string settingName, [FromBody]SettingsDTO settingsDTO)
        {
            await settingService.UpdateSettingsAsync(settingName, settingsDTO);
            return Ok();
        }
    }
}
