using ARMS.Application.Entities.LoginMaster.Services.Interfaces;
using ARMS.Application.Entities.LoginMaster.ViewModels;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Core.Entities;
using ARMS.WebAPI.Models;
using ARMS.WebAPI.Services.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ARMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILdapAuhenticationService _ldapAuthenticationService;
        private readonly ILoginService _loginService;

        public LoginController(ILdapAuhenticationService ldapAuthenticationService,
            ILoginService loginService)
        {
            _ldapAuthenticationService = ldapAuthenticationService;
            _loginService = loginService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AdUser model)
        {
            var user = await _loginService.GetAsync(model.Username, model.Password);

            if (user != null)
            {
                var newAccesstoken = await _ldapAuthenticationService.GetJwtTokenAsync(model.Username);
                var NewRefreshtoken = _ldapAuthenticationService.CreateRefreshToken();
                
                HttpContext.Response.Cookies.Append("access", newAccesstoken, new CookieOptions
                {
                    
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None

                });

                HttpContext.Response.Cookies.Append("Refresh", NewRefreshtoken, new CookieOptions
                {

                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None

                });


                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            TokenApiDto tokendto = new TokenApiDto();

            string accessToken = Request.Cookies["access"];
            string refreshToken = Request.Cookies["Refresh"];

            
            var refreshTokenParts = refreshToken.Split('|');
            if (refreshTokenParts.Length != 2)
            {
                return BadRequest("Invalid Refresh Token");
            }

            var principal = _ldapAuthenticationService.GetPrincipleFromExpiredToken(accessToken);
            var username = principal.FindFirstValue(JwtClaimTypes.PreferredUserName);

            
            if (DateTime.TryParseExact(refreshTokenParts[1], "yyyy-MM-ddTHH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime expirationTime))
            {
                if (expirationTime < DateTime.Now)
                {
                    return BadRequest("Refresh Token has expired Please Login Again");
                }
            }
            else
            {
                return BadRequest("Invalid Refresh Token");
            }
            
            tokendto.AccessToken =  await _ldapAuthenticationService.GetJwtTokenAsync(username);
            tokendto.RefreshToken = _ldapAuthenticationService.CreateRefreshToken();
            HttpContext.Response.Cookies.Append("access", tokendto.AccessToken, new CookieOptions
            {

                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None

            });

            HttpContext.Response.Cookies.Append("Refresh", tokendto.RefreshToken, new CookieOptions
            {

                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None

            });


            return Ok();
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            
            Response.Cookies.Delete("access");
            Response.Cookies.Delete("Refresh");

            return Ok();
        }
    }
}
