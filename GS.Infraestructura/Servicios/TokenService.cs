using GS.Aplicacion.Comunes.Dtos.Response;
using GS.Aplicacion.Comunes.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GS.Infraestructura.Servicios
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        public TokenResultadoRS GenerarToken(int idUsuario, string nombreUsuario, int idRol)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, idUsuario.ToString()),
            new Claim("uid", idUsuario.ToString()),
            new Claim("uname", nombreUsuario),
            new Claim("sid", nombreUsuario),
            new Claim("rol", idRol.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            token.Payload["aud"] = new[] { "https://localhost:9001", "https://localhost:9002" };

            return new TokenResultadoRS
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = DateTime.UtcNow.AddHours(1)
            };
        }

        public string GenerarRefreshToken()
        {
            var bytes = new byte[32];
            RandomNumberGenerator.Create().GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
