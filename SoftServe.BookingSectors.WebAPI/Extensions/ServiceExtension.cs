using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;
using SoftServe.BookingSectors.WebAPI.BLL.Services;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;
using SoftServe.BookingSectors.WebAPI.BLL.Filters;
using SoftServe.BookingSectors.WebAPI.DAL.EF;
using Microsoft.Extensions.Configuration;

namespace SoftServe.BookingSectors.WebAPI.Extensions
{
    public static class ServiceExtension
    {
       
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration config)
        {
           
            services.AddDbContext<BookingSectorContext>(options =>
                options.UseSqlServer(@config["AzureConnectionString"]));
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new SectorProfile());
                mc.AddProfile(new BookingSectorProfile());
                mc.AddProfile(new SettingProfile());
                mc.AddProfile(new TournamentProfile());
            }).CreateMapper());
        }

        public static void ConfigureModelRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISectorService, SectorService>();
            services.AddScoped<IBookingSectorService, BookingSectorService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(options =>
                {
                    options.Filters.Add<ValidateModelState>();
                });

            services.AddSingleton<ValidateModelState>();
        }
    }
}


