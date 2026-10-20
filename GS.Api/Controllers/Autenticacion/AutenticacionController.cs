using GS.Aplicacion.Autenticacion;
using GS.Aplicacion.Autenticacion.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GS.Api.Controllers.Autenticacion
{
    /// <summary>Endpoints de inicio de sesión.</summary>
    [ApiController]
    [Route("v1/GestionSeguridad/[controller]")]
    public sealed class AutenticacionController(IAutenticacionCU autenticacionCU) : ControllerBase
    {
        private readonly IAutenticacionCU _autenticacionCU = autenticacionCU;

        /// <summary>
        /// Valida credenciales y devuelve los contextos donde el usuario puede
        /// operar. No emite ningún token.
        /// </summary>
        /// <param name="solicitud">Usuario, clave y código de sistema.</param>
        /// <param name="ct">Token de cancelación de la petición.</param>
        /// <response code="200">Devuelve la lista de contextos disponibles.</response>
        /// <response code="400">Faltan datos obligatorios en la solicitud.</response>
        /// <response code="401">Credenciales incorrectas o sin acceso al sistema.</response>
        /// <response code="500">Error interno.</response>
        [HttpPost("accesos")]
        public async Task<IActionResult> Accesos([FromBody] AutenticacionRQ solicitud, CancellationToken ct)
        {
            var resultado = await _autenticacionCU.ObtenerContextos(solicitud, ct);

            if (resultado.StatusCode == 200)
                return Ok(new { data = resultado.Data });

            return StatusCode(resultado.StatusCode, new { resultado.Codigo, resultado.StatusMessage });
        }

        /// <summary>
        /// Valida credenciales y devuelve un token de acceso.
        /// </summary>
        /// <param name="solicitud">Credenciales y, si hace falta, el contexto elegido.</param>
        /// <param name="ct">Token de cancelación de la petición.</param>
        /// <response code="200">Autenticado. Devuelve el token.</response>
        /// <response code="400">Faltan datos obligatorios en la solicitud.</response>
        /// <response code="401">Credenciales incorrectas o sin acceso al sistema.</response>
        /// <response code="409">Opera en varios contextos y debe elegir uno.</response>
        /// <response code="500">Error interno.</response>
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] AutenticacionRQ solicitud, CancellationToken ct)
        {
            var resultado = await _autenticacionCU.Autenticar(solicitud, ct);

            if (resultado.StatusCode == 200)
                return Ok(resultado.Data);

            return StatusCode(resultado.StatusCode, new { resultado.Codigo, resultado.StatusMessage });
        }
    }
}
