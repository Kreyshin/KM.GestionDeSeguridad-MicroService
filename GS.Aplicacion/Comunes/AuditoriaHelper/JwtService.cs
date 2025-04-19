using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GS.Aplicacion.Comunes.AuditoriaHelper
{
    public class JwtService : IJwtService
    {
        public string GetUserFromToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
                return "Unknown";

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            // Buscar el claim con el nombre del usuario
            var userClaim = token.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.Name || c.Type == "UName");

            return userClaim?.Value ?? "Unknown";
        }
    }
}
