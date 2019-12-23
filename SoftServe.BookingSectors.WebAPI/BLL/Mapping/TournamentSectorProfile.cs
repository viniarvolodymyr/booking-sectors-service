using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;

namespace SoftServe.BookingSectors.WebAPI.BLL.Mapping
{
    public sealed class TournamentSectorProfile : Profile
    {
        public TournamentSectorProfile()
        {
            CreateMap<TournamentSector, TournamentSectorDTO>();
            CreateMap<TournamentSectorDTO, TournamentSector>()
                 .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}
