using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;

namespace SoftServe.BookingSectors.WebAPI.BLL.Mapping
{
    public sealed class SettingProfile : Profile
    {
        public SettingProfile()
        {
            CreateMap<Setting, SettingsDTO>()
              .ReverseMap();
        }
    }
}
