using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;
using SoftServe.BookingSectors.WebAPI.BLL.Services;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;

namespace SoftServe.BookingSectors.WebAPI.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(mc =>
            {
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
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ITo>
        }

        public static void ConfigureModelRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
