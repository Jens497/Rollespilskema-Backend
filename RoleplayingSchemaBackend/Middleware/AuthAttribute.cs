using Microsoft.AspNetCore.Authorization;

namespace RoleplayingSchemaBackend.Middleware
{
    public abstract class AuthAttribute : Attribute
    {
    }

    public class AllowAnonymousAttribute : AuthAttribute, IAllowAnonymous
    {

    }

    public class AuthorizedAttribute : AuthAttribute, IAuthorizeData
    {
        public string? Policy { get; set; }
        public string? Roles { get; set; }
        public string? AuthenticationSchemes { get; set; }

        public AuthorizedAttribute(string? policy = null, string? roles = null, string? authenticationSchemes = null)
        {
            Policy = policy;
            Roles = roles;
            AuthenticationSchemes = authenticationSchemes;
        }
    }
}
