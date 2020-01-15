using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;

namespace SoftServe.BookingSectors.WebAPI.BLL.Mapping
{
    public sealed class BookingSectorProfile : Profile
    {
        public BookingSectorProfile()
        {
            CreateMap<BookingSector, BookingSectorDTO>();
            CreateMap<BookingSectorDTO, BookingSector>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}
