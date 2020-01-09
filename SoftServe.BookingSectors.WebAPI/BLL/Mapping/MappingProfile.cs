using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;

namespace SoftServe.BookingSectors.WebAPI.BLL.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingSector, BookingSectorDTO>();
            CreateMap<BookingSectorDTO, BookingSector>()
                .ForMember(m => m.IsApproved, opt => opt.Ignore())
                .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<Sector, SectorDTO>();
            CreateMap<SectorDTO, Sector>()
                .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<Setting, SettingsDTO>()
                .ReverseMap();

            CreateMap<Tournament, TournamentDTO>();
            CreateMap<TournamentDTO, Tournament>()
                .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<User, UserDTO>()
                .ForMember(m => m.RoleName, x => x.MapFrom(src => src.Role.Role));
            CreateMap<UserDTO, User>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.Role, opt => opt.Ignore())
                .ForMember(m => m.Password, opt => opt.Ignore())
                .ForMember(m => m.Photo, opt => opt.Ignore());             
        }
    }
}