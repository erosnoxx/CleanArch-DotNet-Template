using Application.Contracts.Services;
using Application.Contracts.Settings;
using Application.Models.Dtos.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IAppSettings _appSettings;

        public TokenService(
            IAppSettings appSettings
        )
        {
            _appSettings = appSettings;
        }

        public TokenOutputDto GenerateToken(string id, string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, id),
                new Claim(JwtRegisteredClaimNames.UniqueName, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresInMinutes = DateTime.Now.AddMinutes(_appSettings.JwtSettings.ExpiresInMinutes);
            var token = new JwtSecurityToken(
                issuer: _appSettings.JwtSettings.Issuer,
                audience: _appSettings.JwtSettings.Audience,
                claims: claims,
                expires: expiresInMinutes,
                signingCredentials: creds);

            var tokenWritten = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenOutputDto
            {
                AccessToken = tokenWritten,
                ExpiresInMinutes = _appSettings.JwtSettings.ExpiresInMinutes
            };
        }
    }
}
