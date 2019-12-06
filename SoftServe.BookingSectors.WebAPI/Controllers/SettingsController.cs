using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.Interfaces;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsRepository _settings;
        public SettingsController(ISettingsRepository settings)
        {
            _settings = settings;
        }
        [HttpGet("{name}", Name = "GetSetting")]
        public Setting Get(string name)
        {
            return _settings.GetSetting(name);
        }
        [HttpPut("{name1}, {name2}, {value1}, {value2}", Name = "Update setting")]
        public void Update(string name1, string name2, int value1, int value2)
        {
            _settings.UpdateSettings(name1, name2, value1, value2);
        }

    }
}
