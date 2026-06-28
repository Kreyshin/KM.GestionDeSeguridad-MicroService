using GS.Aplicacion.Acceso.ObtenerMenu.Dtos;
using GS.Dominio.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.ObtenerMenu
{
    public interface IMenuRepositoryQ
    {
        public Task<SingleResponse<MenuSpResultDto>> ObtenerMenuAsync(ObtenerMenuQuery query);
    }
}
