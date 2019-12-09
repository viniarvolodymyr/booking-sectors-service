using AutoMapper;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
namespace SoftServe.BookingSectors.WebAPI.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Sector, SectorDTO>();
            CreateMap<SectorDTO, Sector>()
                .ForMember(m => m.Id, opt => opt.Ignore());
            
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>()
                .ForMember(m => m.Id, opt => opt.Ignore());
           
            CreateMap<TournamentSector, TournamentSectorDTO>();
            CreateMap<TournamentSectorDTO, TournamentSector>()
                .ForMember(m => m.Id, opt => opt.Ignore());

        }
    }
}
