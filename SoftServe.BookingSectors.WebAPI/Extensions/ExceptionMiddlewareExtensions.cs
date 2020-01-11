using SoftServe.BookingSectors.WebAPI.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace SoftServe.BookingSectors.WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
        }
    }
}