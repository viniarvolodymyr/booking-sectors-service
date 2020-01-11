using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using System;
using System.Net;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;

namespace SoftServe.BookingSectors.WebAPI.Middlewares
{
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILoggerManager  logger;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            this.next = next;
            this.logger = logger;
        }

        /// <summary>
        /// The invoke.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    logger.LogWarn(
                        "The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();

                if (ex is HttpStatusCodeException httpException)
                {
                    context.Response.StatusCode = (int)httpException.StatusCode;
                    context.Response.ContentType = httpException.ContentType;
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = @"application/json";
                    logger.LogError($"0, {ex}, An unhandled exception has occurred: {ex.Message}");
                }

                var result = JsonConvert.SerializeObject(new ErrorResponse(ex.Message));

                logger.LogError(result);
                await context.Response.WriteAsync(result);
            }
        }
    }
}
