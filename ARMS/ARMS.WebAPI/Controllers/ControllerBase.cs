using ARMS.Application.Common;
using IdentityModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ARMS.WebAPI.Controllers
{
    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase, ICurrentUserAccessor
    {

        public IdentityInfo CurrentUser
        {
            get
            {
                // Retrieve HTTP context
                var httpContext = HttpContext;
                if (HttpContext == null)
                {
                    return new IdentityInfo();
                }
                // Retrieve JWT token from cookie
                var token = httpContext.Request.Cookies["access"];

                if (string.IsNullOrEmpty(token))
                {
                    return new IdentityInfo();
                }

                // Decode JWT token to extract claims
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken == null)
                {
                    return new IdentityInfo(); // Return empty identity info if token is invalid
                }

                // Extract claims from JWT token
                var id = jsonToken.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Id)?.Value;
                var username = jsonToken.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.PreferredUserName)?.Value;
                var email = jsonToken.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Email)?.Value;
                var name = jsonToken.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value;
                var phone = jsonToken.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.PhoneNumber)?.Value;

                return new IdentityInfo
                {
                    Id = Convert.ToInt32(id),
                    Username = username ?? string.Empty,
                    Email = email ?? string.Empty,
                    Name = name ?? string.Empty,
                    Phone = phone ?? string.Empty
                };
            }
        }
    }
}





