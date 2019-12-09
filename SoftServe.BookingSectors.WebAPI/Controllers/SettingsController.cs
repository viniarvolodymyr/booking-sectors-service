using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.Interfaces;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settings;
        public SettingsController(ISettingsService settings)
        {
            _settings = settings;
        }
        [HttpGet("{name}", Name = "GetSetting")]
        public Task<SettingsDTO> Get(string name)
        {
            return _settings.GetSettingByIdAsync(name);
        }
        [HttpPut("{name1}", Name = "Update setting")]
        public void Update(string name1, SettingsDTO settingsDTO)
        {
            _settings.UpdateSettingsAsync(name1, settingsDTO);
        }

    }
}
