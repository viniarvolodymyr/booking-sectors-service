using AutoMapper;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;

namespace SoftServe.BookingSectors.WebAPI.BLL.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingSector, BookingSectorDTO>()
                .ReverseMap();
            CreateMap<SectorDTO, Sector>()
                .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<User, UserDTO>()
                .ForMember(m => m.RoleName, x => x.MapFrom(src => src.Role.Role));
            CreateMap<UserDTO, User>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.Role, opt => opt.Ignore())
                .ForMember(m => m.Password, opt => opt.Ignore())
                .ForMember(m => m.Photo, opt => opt.Ignore());
            
            CreateMap<SectorDTO, Sector>()
                .ForMember(m => m.Id, opt => opt.Ignore());
            CreateMap<Sector, SectorDTO>();

            CreateMap<TournamentSector, TournamentSectorDTO>();
            CreateMap<TournamentSectorDTO, TournamentSector>()
                 .ForMember(m => m.Id, opt => opt.Ignore());
            CreateMap<SectorDTO, Sector>()
                .ForMember(m => m.Id, opt => opt.Ignore());
            CreateMap<Sector, SectorDTO>()
                .ReverseMap();
            CreateMap<Setting, SettingsDTO>()
                .ReverseMap();
        }
    }
}
