using ARMS.Application.Authorization;
using ARMS.Application.Entities.Permissions.Services.Interfaces;
using ARMS.Application.Exceptions;
using ARMS.Application.Utilities;
using ARMS.Core.Models;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Reflection;
using System.Security.Claims;

namespace ARMS.WebAPI.Filters
{
    public class AppAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPermissionService _permissionService;

        public AppAuthorizationFilter(IHttpContextAccessor httpContextAccessor, IPermissionService permissionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _permissionService = permissionService;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }

            // Do not check permissions for Middleware
            //Boolean.TryParse(context.HttpContext.User.FindFirstValue("is_import_client"), out bool isImportClient);
            //if (isImportClient)
            //{
            //    return;
            //}

            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }
            try
            {
                await CheckPermissions(
                        context.ActionDescriptor.GetMethodInfo(),
                        context.ActionDescriptor.GetMethodInfo().DeclaringType
                );

            }
            catch (AppAuthorizationException ex)
            {
                context.Result = new ObjectResult(new ErrorInfo
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Title = ex.Message,
                    Errors = ex?.InnerException?.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
            }
            catch (System.Exception ex)
            {
                context.Result = new ObjectResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        private async Task CheckPermissions(MethodInfo methodInfo, Type type)
        {
            if (AllowAnonymous(methodInfo, type))
            {
                return;
            }

            var authorizeAttributes =
                ReflectionHelper
                    .GetAttributesOfMemberAndType(methodInfo, type)
                    .OfType<AppAuthorizeAttribute>()
                    .ToArray();

            if (!authorizeAttributes.Any())
            {
                return;
            }

            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new AppAuthorizationException("msg.userNotLogin");
            }

            // check for specific scope
            //var clientScopes = _httpContextAccessor.HttpContext.User.Claims
            //    .Where(x => x.Type == "scope")
            //    .Select(x => x.Value)
            //    .ToList();

            //if (clientScopes.Any(scope => scope.StartsWith("spapi.")))
            //{
            //    foreach (var authorizeAttribute in authorizeAttributes)
            //    {
            //        if (!clientScopes.Contains(authorizeAttribute.Scope?.ToLower() ?? string.Empty))
            //        {
            //            throw new AppAuthorizationException("msg.notAllowScope");
            //        }
            //    }
            //}

            var currentUser = _httpContextAccessor.HttpContext.User.FindFirstValue(JwtClaimTypes.PreferredUserName);

            ////check for common api
            //if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("AllowPermissions", out var headerPermissions) && _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("RequiredAllPermissions", out var headerRequiredAllPermissions))
            //{
            //    var permissions = headerPermissions.ToString().Split(',');
            //    if (permissions.Count() > 0)
            //    {
            //        await AuthorizeAsync(bool.Parse(headerRequiredAllPermissions.ToString()), currentUser, permissions);
            //    }
            //}
            //else
            //{
            foreach (var authorizeAttribute in authorizeAttributes)
            {
                await AuthorizeAsync(authorizeAttribute.RequireAllPermissions, currentUser, authorizeAttribute.Permissions);
            }
            //}
        }
        //helper
        private bool AllowAnonymous(MemberInfo memberInfo, Type type)
        {
            return ReflectionHelper
                .GetAttributesOfMemberAndType(memberInfo, type)
                .OfType<AppAllowAnonymousAttribute>()
                .Any();
        }

        private async Task AuthorizeAsync(bool requireAll, string userName, params string[] permissionNames)
        {

            if (await _permissionService.IsUserGrantedAsync(requireAll, userName, permissionNames))
            {
                return;
            }

            if (requireAll)
            {
                throw new AppAuthorizationException("msg.allPermissionsRequired");
            }
            else
            {
                throw new AppAuthorizationException("msg.oneOfThesePermissionsRequired");
            }
        }

    }

}
