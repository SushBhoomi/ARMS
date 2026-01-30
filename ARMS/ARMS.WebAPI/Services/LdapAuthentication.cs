using ARMS.Application.Entities.LoginMaster.Services.Interfaces;
using ARMS.Application.Entities.LoginMaster.ViewModels;
using ARMS.Core.Entities;
using ARMS.Infrastructure.Contexts;
using ARMS.WebAPI.Extensions;
using ARMS.WebAPI.Models;
using ARMS.WebAPI.Services.Interfaces;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Novell.Directory.Ldap;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LdapConnection = Novell.Directory.Ldap.LdapConnection;
using LdapException = Novell.Directory.Ldap.LdapException;


namespace ARMS.WebAPI.Services
{

    public class LdapAuthentication : ILdapAuhenticationService
    {
        private readonly ILdapConnection _ldapConnection;
        private readonly LdapConfig _ldapConfig;
        private readonly IConfiguration _configuration;
        private readonly string _accessTokenSecret;
        private readonly string _refreshTokenSecret;
        private readonly ILoginService _loginService;
        private readonly ErrorDBContext _dbcontext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LdapAuthentication(
            IOptions<LdapConfig> ldapConfig,
            ILdapConnection ldapConnection,
            IConfiguration configuration, ILoginService service, ErrorDBContext dbcontext, IHttpContextAccessor httpContextAccessor)
        {
            _ldapConfig = ldapConfig.Value;
            _ldapConnection = ldapConnection;
            _configuration = configuration;
            _accessTokenSecret = configuration.GetSection("TokenSettings:AccessTokenSecret").Value;
            _refreshTokenSecret = configuration.GetSection("TokenSettings:RefreshTokenSecret").Value;
            _loginService = service;
            _dbcontext = dbcontext;
            _httpContextAccessor = httpContextAccessor;
        }

#pragma warning disable CS1998
        public async Task<bool> LoginAsync(string username, string password)
#pragma warning restore CS1998 
        {

            try
            {

                var ldapServerIp = _ldapConfig.Path;
                var ldapServerPort = _ldapConfig.Port;


                _ldapConnection.Connect(ldapServerIp, ldapServerPort);

                string dn = "uid=" + username + ",dc=example,dc=com";
                _ldapConnection.Bind(LdapConnection.LdapV3, dn, password);
                if (_ldapConnection.Bound)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (LdapException)
            {
                return false;
            }
            finally
            {
                _ldapConnection.Disconnect();
            }
        }

        public async Task<string> GetJwtTokenAsync(string username)
        {
            var user = await _loginService.GetUserAsync(username);
            var claims = new List<Claim>();
            try
            {
                claims.Add(new Claim(JwtClaimTypes.PreferredUserName, username));
                claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
                claims.Add(new Claim(JwtClaimTypes.Id, user.Id.ToString()));

            }
            catch (LdapException)
            {

            }
            finally
            {
                _ldapConnection.Disconnect();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



        public string CreateRefreshToken()
        {
            var tokenBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }

            var expirationTime = DateTime.Now.AddMinutes(30);
            var refreshToken = Convert.ToBase64String(tokenBytes) + "|" + expirationTime.ToString("yyyy-MM-ddTHH:mm:ss");

            return refreshToken;
        }




        ClaimsPrincipal ILdapAuhenticationService.GetPrincipleFromExpiredToken(string token)
        {

            var key = Encoding.ASCII.GetBytes(_accessTokenSecret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false  // Update this to validate the token's expiration as needed
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;



            try
            {
                // Validate the token
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);


                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("This is an Invalid Token");

                return principal;
            }
            catch (SecurityTokenExpiredException)
            {

                var tokenDescriptor = tokenHandler.ReadJwtToken(token);
                return new ClaimsPrincipal(new ClaimsIdentity(tokenDescriptor.Claims, "jwt"));
            }
            catch (Exception ex)
            {

                throw new SecurityTokenException("Invalid Token", ex);
            }
        }

        public async Task<ActivityLogModel> CreateActivityLog(string Activity, string username, int id)
        {
            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            if (ipAddress == "::1")
            {
                ipAddress = "127.0.0.1";
            }
            var log = new ActivityLogModel
            {
                UserId = id,
                UserName = username,
                //ActivityType = Activity,
                Timestamp = DateTime.UtcNow.ToLocalTime(),
                IpAddress = ipAddress
            };

            await _dbcontext.ActivityLog.AddAsync(log);
            await _dbcontext.SaveChangesAsync();
            return log;
        }


       
    }
}

