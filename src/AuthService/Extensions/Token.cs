using AuthService.Domain.Models;
using AuthService.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Extensions
{
    public static class Token
    {
        public static string Generate(AuthSetting authSetting, UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(authSetting.SecretKey);

            var desc = new SecurityTokenDescriptor
            {
                Subject = user.ToClaim(),
                Expires = DateTime.UtcNow.AddHours(authSetting.ExpireInHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(desc);
            return tokenHandler.WriteToken(token);
        }

        public static string EncodeTokenMail(AuthSetting authSetting, UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(authSetting.SecretKey);

            var desc = new SecurityTokenDescriptor
            {
                Subject = user.ToClaim(),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(desc);
            return tokenHandler.WriteToken(token);
        }

        public static UserModel DecodeTokenMail(AuthSetting authSetting, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(authSetting.SecretKey);

            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claimsPrincipal = tokenHandler.ValidateToken(token, validations, out var validatedToken);
            //var token = tokenHandler.CreateToken(desc);
            //return tokenHandler.WriteToken(token);
        }

        public static ClaimsIdentity ToClaim(this UserModel user)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Email}"),
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Authenticated");

            return claimsIdentity;
        }

        public static UserModel ToUser(this ClaimsPrincipal claimsPrincipal)
        {
            
        }
    }
}
