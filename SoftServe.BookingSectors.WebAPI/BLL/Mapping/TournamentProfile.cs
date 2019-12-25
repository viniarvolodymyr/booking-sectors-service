using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;

namespace SoftServe.BookingSectors.WebAPI.BLL.Mapping
{
    public sealed class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<Tournament, TournamentDTO>();
            CreateMap<TournamentDTO, Tournament>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}
