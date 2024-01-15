using FluentValidation;
using RoleplayingSchemaBackend.Exceptions;
using System.Text.Json;

using ApplicationException = RoleplayingSchemaBackend.Exceptions.ApplicationException;

namespace RoleplayingSchemaBackend.Middleware
{
    internal sealed class MiddlewareExcepitonHandler : IMiddleware
    {
        private readonly ILogger<MiddlewareExcepitonHandler> _logger;

        public MiddlewareExcepitonHandler(ILogger<MiddlewareExcepitonHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            } 
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext http, Exception ex)
        {
            var statusCode = GetStatusCode(ex);

            var response = new
            {
                title = GetTitle(ex),
                status = statusCode,
                detail = ex.Message,
                errors = GetErrors(ex)
            };

            http.Response.ContentType = "application/json";

            http.Response.StatusCode = statusCode;

            await http.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static int GetStatusCode(Exception ex) =>
            ex switch
            {
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                EntryPointNotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

        private static string GetTitle(Exception ex) =>
            ex switch
            {
                ApplicationException exception => exception.Message,
                _ => "Server Error"
            };

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception ex)
        {
            IReadOnlyDictionary<string, string[]> errors = null;

            if (ex is ValidationExceptionPipeline validationException)
            {
                errors = validationException.ErrorsDict;
            }

            return errors;
        }
    }
}
