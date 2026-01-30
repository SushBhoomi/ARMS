using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
      
        public static void AddAuthenticationExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var accessTokenSecret = configuration.GetValue<string>("TokenSettings:AccessTokenSecret");

            if (string.IsNullOrEmpty(accessTokenSecret))
            {
                throw new InvalidOperationException("AccessTokenSecret is not configured in appsettings.json.");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer" ,options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "your_issuer",
                    ValidAudience = "your_audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenSecret)),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["access"] ?? context.Request.Cookies["Refresh"];
                        return Task.CompletedTask;
                    }
                };
            });
        }

        public static void AddAuthorizationPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();
        }
    }
}
