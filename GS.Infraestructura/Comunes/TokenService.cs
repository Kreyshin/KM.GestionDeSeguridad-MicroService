using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GS.Aplicacion.Comunes;
using Microsoft.IdentityModel.Tokens;

namespace GS.Infraestructura.Comunes
{
    /// <summary>Opciones de firma y vigencia del token de acceso.</summary>
    /// <param name="Clave">Secreto de firma. Mínimo 32 bytes.</param>
    /// <param name="Emisor">Emisor que se escribe y se valida en el token.</param>
    /// <param name="Audiencias">Destinatarios válidos del token.</param>
    /// <param name="Minutos">Minutos de vigencia.</param>
    public sealed record TokenOpciones(string Clave, string Emisor, string[] Audiencias, int Minutos);

    /// <summary>Emisión de tokens JWT firmados con HMAC SHA-256.</summary>
    public sealed class TokenService(TokenOpciones opciones) : ITokenService
    {
        private readonly TokenOpciones _opciones = opciones;

        /// <inheritdoc />
        public TokenGenerado Generar(int idUsuario, string usuario, int idRol, int idSistema, IReadOnlyCollection<string> permisos)
        {
            var credenciales = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opciones.Clave)),
                SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddMinutes(_opciones.Minutos);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, idUsuario.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("uid", idUsuario.ToString()),
                new("uname", usuario),
                new("rol", idRol.ToString()),
                new("sys", idSistema.ToString())
            };

            claims.AddRange(permisos.Distinct(StringComparer.OrdinalIgnoreCase)
                .Select(codigo => new Claim("permission", codigo)));

            var token = new JwtSecurityToken(
                issuer: _opciones.Emisor,
                claims: claims,
                expires: expiracion,
                signingCredentials: credenciales);

            token.Payload["aud"] = _opciones.Audiencias;

            return new TokenGenerado(new JwtSecurityTokenHandler().WriteToken(token), expiracion);
        }

        /// <inheritdoc />
        public RenovacionGenerada GenerarRenovacion()
        {
            var valor = Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(32));
            var resumen = Convert.ToHexString(
                System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(valor))).ToLowerInvariant();

            return new RenovacionGenerada(valor, resumen);
        }
    }
}
