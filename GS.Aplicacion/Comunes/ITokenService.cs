namespace GS.Aplicacion.Comunes
{
    /// <summary>Generación de tokens de acceso y de renovación.</summary>
    public interface ITokenService
    {
        /// <summary>Genera un token de acceso para un usuario ya autenticado.</summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <param name="usuario">Nombre de usuario.</param>
        /// <param name="idRol">Rol con el que entra.</param>
        /// <param name="idSistema">Sistema al que entra.</param>
        /// <param name="permisos">Códigos de permiso que lleva el token.</param>
        TokenGenerado Generar(int idUsuario, string usuario, int idRol, int idSistema, IReadOnlyCollection<string> permisos);

        /// <summary>
        /// Genera un token de renovación aleatorio y su resumen SHA-256.
        /// El token va al cliente; a la base solo viaja el resumen.
        /// </summary>
        RenovacionGenerada GenerarRenovacion();
    }

    /// <summary>Token de acceso emitido y su vencimiento.</summary>
    /// <param name="Token">Token de acceso firmado.</param>
    /// <param name="Expiracion">Momento en que deja de ser válido.</param>
    public sealed record TokenGenerado(string Token, DateTime Expiracion);

    /// <summary>Token de renovación y su resumen.</summary>
    /// <param name="Token">Valor que se entrega al cliente.</param>
    /// <param name="Resumen">SHA-256 en hexadecimal que se guarda en la base.</param>
    public sealed record RenovacionGenerada(string Token, string Resumen);
}
