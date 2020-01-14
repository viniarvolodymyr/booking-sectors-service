using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftServe.BookingSectors.WebAPI.Extensions;
using NLog;
using System.IO;

namespace SoftServe.BookingSectors.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(configFile: $"{Directory.GetCurrentDirectory()}/nlog.config");
         
            Configuration = configuration;
            EmailConfiguration = configuration;
        }

        public static IConfiguration EmailConfiguration { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabase(Configuration);
            services.ConfigureLoggerService();
            services.AddControllers();
            services.ConfigureSwagger();
            services.ConfigureAuthentication(Configuration);
            services.ConfigureAutoMapper();
            services.ConfigureModelRepositories();
            services.ConfigureDataAccessServices();
            services.ConfigureCors();
            services.ConfigureFilters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpStatusCodeExceptionMiddleware();
             
            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
           
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}