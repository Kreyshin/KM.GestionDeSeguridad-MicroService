using GS.Aplicacion.Acceso.Autenticacion.Dtos.Request;
using GS.Aplicacion.Acceso.Autenticacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GS.Api.Controllers.Acceso
{
    [Route("v1/GestionSeguridad/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAutenticacionCU _authCU;

        public AutenticacionController(IAutenticacionCU authCU)
        {
            _authCU = authCU;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Login([FromBody] AutenticacionRQ usuario)
        {
            if (usuario is null)
                return BadRequest(new { StatusCode = 400, StatusMessage = "El cuerpo no puede estar vacío." });

            var oResult = await _authCU.Auth(usuario);

            return oResult.StatusCode switch
            {
                200 => Ok(oResult.Data),
                401 => Unauthorized(new { oResult.StatusMessage }),
                _ => StatusCode(500, new { oResult.StatusMessage })
            };
        }
    }
}
