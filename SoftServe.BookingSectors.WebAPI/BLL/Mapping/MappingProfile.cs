using AutoMapper;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
namespace SoftServe.BookingSectors.WebAPI.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
             CreateMap<SectorDTO, Sector>()
                .ForMember(m => m.Id, opt => opt.Ignore());
            CreateMap<Sector, SectorDTO>()
                .ReverseMap();
           
        }
    }
}
