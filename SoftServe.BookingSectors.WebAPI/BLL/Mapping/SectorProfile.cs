using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;

namespace SoftServe.BookingSectors.WebAPI.BLL.Mapping
{
    public sealed class SectorProfile : Profile
    {
        public SectorProfile()
        {
            CreateMap<Sector, SectorDTO>();
            CreateMap<SectorDTO, Sector>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}
