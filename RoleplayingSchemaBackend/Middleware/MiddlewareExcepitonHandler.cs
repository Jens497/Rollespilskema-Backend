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
                UnautorizedException => StatusCodes.Status401Unauthorized,
                EntryPointNotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                IdentityModelExceptions => StatusCodes.Status406NotAcceptable, // Should maybe be a 400?
                _ => StatusCodes.Status500InternalServerError
            };

        private static string GetTitle(Exception ex) =>
            ex switch
            {
                ApplicationException exception => exception.Message,
                _ => "Server Error"
            };

        private static IDictionary<string, IReadOnlyDictionary<string, string>> GetErrors(Exception ex)
        {
            //Create a dictionary and fetch the validation errors that there are, if any.
            IDictionary<string, IReadOnlyDictionary<string, string>> errors = new Dictionary<string, IReadOnlyDictionary<string, string>>(); 

            if (ex is ValidationExceptionPipeline validationException)
            {
                errors.Add("ValidationError", validationException.ErrorsDict);
            }

            else if (ex is IdentityModelExceptions identityModelExceptions)
            {
                errors.Add("IdentityError", identityModelExceptions.ErrorsDict);
            }

            else if (ex is UnautorizedException unautorizedException)
            {
                errors.Add("UnautorizedError", unautorizedException.ErrorsDict);
            }

            return errors;
        }
    }
}
