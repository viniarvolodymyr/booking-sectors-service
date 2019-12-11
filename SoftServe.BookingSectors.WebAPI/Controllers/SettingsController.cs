using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;


namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService settings;
        public SettingsController(ISettingsService settings)
        {
            this.settings = settings;
        }
        [HttpGet("{name}", Name = "GetSetting")]
        public Task<SettingsDTO> Get(string name)
        {
            return settings.GetSettingByIdAsync(name);
        }
        [HttpPut("{name1}", Name = "Update setting")]
        public async Task Update(string name1, [FromBody] SettingsDTO settingsDTO)
        {
           await settings.UpdateSettingsAsync(name1, settingsDTO);
        }

    }
}
