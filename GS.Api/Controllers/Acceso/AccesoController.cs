using GS.Aplicacion.Acceso.ObtenerMenu;
using GS.Aplicacion.Rol.Dtos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GS.Api.Controllers.Acceso
{
    [Route("v1/GestionSeguridad/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly IObtenerMenuCU _obtenerMenuUC;


        public AccesoController(IObtenerMenuCU obtenerMenuUC)
        {
            _obtenerMenuUC = obtenerMenuUC;
        }

        #region Querys
        [HttpPost("ObtenerMenu")]
        public async Task<IActionResult> ObtenerMenu(ObtenerMenuRQ oFiltro)
        {

            var oResult = await _obtenerMenuUC.ObtenerMenuAsync(oFiltro);

            return oResult.StatusCode switch
            {
                200 => Ok(new { oResult.Data, oResult.StatusType, oResult.StatusMessage }),
                204 => NoContent(),
                _ => StatusCode(500, new { oResult.StatusMessage })
            };
        }
        #endregion


    }
}
