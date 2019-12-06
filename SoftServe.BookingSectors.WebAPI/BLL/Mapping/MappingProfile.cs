using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            CreateMap<Sector, SectorDTO>()
                .ReverseMap();

            CreateMap<User, UserDTO>()
               .ReverseMap();

            CreateMap<TournamentSector, TournamentSectorDTO>()
               .ReverseMap();
        }
    }
}
