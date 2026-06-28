using GS.Dominio.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.ObtenerMenu
{
    public interface IObtenerMenuCU
    {
        Task<SingleResponse<ObtenerMenuRE>> ObtenerMenuAsync(ObtenerMenuRQ param);
    }
}
