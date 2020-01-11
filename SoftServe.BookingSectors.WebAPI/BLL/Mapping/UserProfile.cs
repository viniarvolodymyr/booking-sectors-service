using System;
using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;

namespace SoftServe.BookingSectors.WebAPI.BLL.Mapping
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(m => m.RoleName, x => x.MapFrom(src => src.Role.Role))
               // .ForMember(m=>m.Photo, x =>x.MapFrom(src=> Convert.ToBase64String(src.Photo)))
                .ForMember(m => m.Photo, opt => opt.Ignore());
                
            CreateMap<UserDTO, User>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.Role, opt => opt.Ignore())
                .ForMember(m => m.Password, opt => opt.Ignore())
                .ForMember(m => m.Photo, opt => opt.Ignore()); 
            
        }
    }
}
