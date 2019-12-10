using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SoftServe.BookingSectors.WebAPI.Extensions
{
    public static class SwaggerExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
             services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v2", new OpenApiInfo { Title = "Api", Version = "v2" });
            });  
        }
    }
}
