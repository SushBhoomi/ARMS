using Microsoft.AspNetCore.Authorization;

namespace ARMS.Application.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AppAuthorizeAttribute : AuthorizeAttribute
    {
        /// <inheritdoc/>
        public string[] Permissions { get; set; }

        /// <inheritdoc/>
        public bool RequireAllPermissions { get; set; }

        public string Scope { get; set; }

        public AppAuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }
    }
}
