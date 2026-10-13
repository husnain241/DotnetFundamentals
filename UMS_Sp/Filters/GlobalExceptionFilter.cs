using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using UMS_Sp.DTOs.Common;

namespace UMS_Sp.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred: {Message}", context.Exception.Message);

            var errorResponse = new ErrorResponse
            {
                StatusCode = 500,
                Message = "An unexpected error occurred. Please try again later.",
                Details = context.Exception.Message 
            };

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}