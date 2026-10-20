namespace GS.Dominio.Conexiones
{
    /// <summary>Acceso a datos de las sesiones de acceso.</summary>
    public interface IConexionRepo
    {
        /// <summary>Registra la sesión que se abre al autenticar.</summary>
        /// <param name="idUsuario">Usuario que inicia sesión.</param>
        /// <param name="idRol">Rol con el que entra.</param>
        /// <param name="idSistema">Sistema al que entra.</param>
        /// <param name="resumenRenovacion">SHA-256 del token de renovación, en hexadecimal.</param>
        /// <param name="ct">Token de cancelación de la petición.</param>
        Task<ConexionDB?> Abrir(int idUsuario, int idRol, int idSistema, string resumenRenovacion, CancellationToken ct);
    }
}
