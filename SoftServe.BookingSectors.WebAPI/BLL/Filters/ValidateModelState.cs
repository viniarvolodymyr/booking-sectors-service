using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;
using System;
using SoftServe.BookingSectors.WebAPI.Extensions;

namespace SoftServe.BookingSectors.WebAPI.BLL.Filters
{
    public class ValidateModelState : IAsyncActionFilter
    {
        private readonly ILoggerManager logger;

        public ValidateModelState(ILoggerManager logger)
        {
            this.logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                if (!context.ModelState.IsValid)
                {
                    var body = context.HttpContext?.Request?.BodyToString();
                    logger.LogError($"Invalid request body:{Environment.NewLine}{body}");

                    context.Result = new BadRequestObjectResult(context.ModelState);
                }
            }
            catch (Exception e)
            {
                logger?.LogError($"Exception: {e}, ModelStateValidation Filter failed.");
            }

            var result = await next();

            // execute any code after the action executes
        }
    }
}
