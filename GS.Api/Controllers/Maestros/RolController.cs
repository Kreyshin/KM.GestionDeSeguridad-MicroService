using GS.Aplicacion.Rol.Dtos.Request;
using GS.Aplicacion.Rol.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GS.Api.Controllers.Maestros
{
    [Route("v1/GestionSeguridad/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {

        private readonly IRolCrudCU _rolCrudCU;


        public RolController(IRolCrudCU rolCrudCU)
        {
            _rolCrudCU = rolCrudCU;
          
        }

        #region Querys
        [HttpGet("")]
        public async Task<IActionResult> Consultar(RolConsultarRQ oFiltro)
        {

            var oResult = await _rolCrudCU.Consultar(oFiltro);

            return oResult.StatusCode switch
            {
                200 => Ok(new { oResult.Data, oResult.StatusType, oResult.StatusMessage }),
                204 => NoContent(),
                _ => StatusCode(500, new { oResult.StatusMessage })
            };
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> BuscarPorID(int id)
        {
            var oResult = await _rolCrudCU.BuscarPorID(id);

            return oResult.StatusCode switch
            {
                200 => Ok(new { oResult.Data, oResult.StatusType, oResult.StatusMessage }),
                204 => NoContent(),
                _ => StatusCode(500, new { oResult.StatusMessage })
            };
        }
        #endregion


        #region Commands

        [HttpPost("")]
        public async Task<IActionResult> Crear(RolCrearRQ oRegistro)
        {
            if (oRegistro == null)
            {
                return BadRequest(new { StatusType = "InvalidInput", StatusMessage = "El registro no puede ser nulo." });
            }

            var oResult = await _rolCrudCU.Crear(oRegistro);

            return oResult.StatusCode switch
            {
                200 => Ok(new { oResult.Data, oResult.StatusType, oResult.StatusMessage }),
                400 => BadRequest(new { oResult.StatusType, oResult.StatusMessage }),
                _ => StatusCode(500, new { oResult.StatusType, oResult.StatusMessage })
            };
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, RolActualizarRQ oRegistro)
        {
            if (oRegistro == null)
            {
                return BadRequest(new { StatusType = "InvalidInput", StatusMessage = "El registro no puede ser nulo." });
            }

            var oResult = await _rolCrudCU.Actualizar(id, oRegistro);

            return oResult.StatusCode switch
            {
                200 => Ok(new { oResult.Data, oResult.StatusType, oResult.StatusMessage }),
                400 => BadRequest(new { oResult.StatusType, oResult.StatusMessage }),
                _ => StatusCode(500, new { oResult.StatusMessage })
            };
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {

            var oResult = await _rolCrudCU.Eliminar(id);

            return oResult.StatusCode switch
            {
                200 => Ok(new { oResult.Data, oResult.StatusType, oResult.StatusMessage }),
                400 => BadRequest(new { oResult.StatusType, oResult.StatusMessage }),
                _ => StatusCode(500, new { oResult.StatusMessage })
            };

        }

        #endregion

    }
}
