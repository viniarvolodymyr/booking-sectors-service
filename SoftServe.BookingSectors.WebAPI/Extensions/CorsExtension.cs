using Microsoft.Extensions.DependencyInjection;

namespace SoftServe.BookingSectors.WebAPI.Extensions
{
    public static class CorsExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        }
    }
}
