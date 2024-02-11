using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RoleplayingSchemaBackend.Data;
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

            var user = _contextAccessor.HttpContext.User;
            var isSignedIn = _signInManager.IsSignedIn(user);

            if (!isSignedIn)
            {
                //TBD maybe make it to our own exception so that it goes through the middleware created for exceptions
                throw new ApplicationException("Unauthorized: has to be signed in");
            }

            var authorizedAttributes = authAttributes.Where(a => a is AuthorizedAttribute && ((AuthorizedAttribute)a).Roles is not null).Select(a => a as AuthorizedAttribute);
            var errorsDict = authorizedAttributes
                .Select(attribute =>
                    attribute?.Roles?.Split(",").Where(role => !user.IsInRole(role)))
                .Where(es => !es.Where(a => a is not null).IsNullOrEmpty());


            if (errorsDict.Any())
            {
                throw new ApplicationException("Unauthorized: lacking roles");
            }

            return await next(); //After the errors has been thrown return and await for the next command coming.
        }
    }
}
