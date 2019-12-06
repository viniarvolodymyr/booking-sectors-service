using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Repositories.Interfaces
{
    public interface ISettingsRepository
    {
            public Setting GetSetting(string name);
            public void UpdateSettings(string name1, string name2, int value1, int value2);
        }
}
