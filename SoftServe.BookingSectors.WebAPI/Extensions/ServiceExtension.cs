using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SoftServe.BookingSectors.WebAPI.BLL.Services;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;

namespace SoftServe.BookingSectors.WebAPI.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(mc =>{
                mc.AddProfile(new MappingProfile());
            }).CreateMapper());
        }

        public static void ConfigureDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<ISectorService, SectorService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITournamentSectorService, TournamentSectorService>();
            services.AddTransient<ITournamentService, TournamentService>();
            services.AddTransient<IBookingSectorService, BookingSectorService>();
            services.AddTransient<IUserService, UserService>();

        }

        public static void ConfigureModelRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }
    }
}
