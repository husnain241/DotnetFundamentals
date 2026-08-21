using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace mini_project_SMS.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger, IModelMetadataProvider modelMetadataProvider)
        {
            _logger = logger;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            // 1. Log the unhandled exception
            _logger.LogError(context.Exception, "An unhandled exception occurred: {Message}", context.Exception.Message);

            // 2. Return friendly Error view
            var result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml",
                ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
            };

            context.Result = result;
            context.ExceptionHandled = true; // Mark exception as handled
        }
    }
}