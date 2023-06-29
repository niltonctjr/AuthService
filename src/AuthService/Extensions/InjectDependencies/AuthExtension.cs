using AuthService.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthService.Extensions.InjectDependencies
{
    public static class AuthExtension
    {
        public static IServiceCollection AddJWTBearer(this IServiceCollection services, IConfiguration config)
        {

            var authSetting = config.GetSetting<AuthSetting>(); 
            var key = Encoding.UTF8.GetBytes(authSetting.SecretKey);
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(j => {
                j.RequireHttpsMetadata = false;
                j.SaveToken = true;
                j.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            return services;
        }        
    }
}
