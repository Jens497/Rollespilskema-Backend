using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Exceptions;
using System.Reflection;

namespace RoleplayingSchemaBackend.Middleware
{
    public sealed class AuthorizationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private IHttpContextAccessor _contextAccessor;
        private readonly SignInManager<Users> _signInManager;

        public AuthorizationPipeline(IHttpContextAccessor context, SignInManager<Users> signInManager)
        {
            _contextAccessor = context;
            _signInManager = signInManager;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authAttributes = request.GetType().GetCustomAttributes<AuthAttribute>();
            if (authAttributes.Any(a => a is AllowAnonymousAttribute))
            {
                return await next();
            }

            var user = _contextAccessor.HttpContext.User; // Current user that is logged in
            var isSignedIn = _signInManager.IsSignedIn(user); // Check if user is signed in

            if (!isSignedIn)
            {
                var errsDict = new Dictionary<string, string>
                {
                    {"Unauthorized", "has to be signed in"}
                };
                throw new UnautorizedException(errsDict);
            }
            //Check if roles are there and if the role that the current user has is sufficient to use the endpoint that is being used.
            var authorizedAttributes = authAttributes.Where(a => a is AuthorizedAttribute && ((AuthorizedAttribute)a).Roles is not null).Select(a => a as AuthorizedAttribute);
            var errorsDict = authorizedAttributes
                .Select(attribute =>
                    attribute?.Roles?.Split(",").Where(role => !user.IsInRole(role)))
                .Where(es => !es.Where(a => a is not null).IsNullOrEmpty());


            if (errorsDict.Any())
            {
                var errsDict = new Dictionary<string, string>
                {
                    {"Unauthorized", "lacking roles"}
                };
                throw new UnautorizedException(errsDict);
            }

            return await next(); //After the errors has been thrown return and await for the next command coming.
        }
    }
}
