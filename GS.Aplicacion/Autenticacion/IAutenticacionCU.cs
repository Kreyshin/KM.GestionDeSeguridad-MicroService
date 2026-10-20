using GS.Aplicacion.Autenticacion.Dtos;
using GS.Dominio.Comunes;

namespace GS.Aplicacion.Autenticacion
{
    /// <summary>Casos de uso de inicio de sesión.</summary>
    public interface IAutenticacionCU
    {
        /// <summary>
        /// Valida las credenciales y, si son correctas, emite un token de acceso.
        /// </summary>
        /// <param name="solicitud">Credenciales y contexto elegido.</param>
        /// <param name="ct">Token de cancelación de la petición.</param>
        Task<SingleResponse<AutenticacionRS>> Autenticar(AutenticacionRQ solicitud, CancellationToken ct);

        /// <summary>
        /// Valida las credenciales y devuelve los contextos donde el usuario
        /// puede operar. No emite ningún token.
        /// </summary>
        /// <param name="solicitud">Credenciales y código de sistema.</param>
        /// <param name="ct">Token de cancelación de la petición.</param>
        Task<ListResponse<ContextoRS>> ObtenerContextos(AutenticacionRQ solicitud, CancellationToken ct);
    }
}
