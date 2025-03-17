using GS.Aplicacion.Rol.Dtos.Request;
using GS.Aplicacion.Rol.Dtos.Response;
using GS.Dominio.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Rol.Interfaces
{
    public interface IRolCrudCU
    {
        public Task<SingleResponse<RolCrearRE>> Crear(RolCrearRQ oRegistro);
        public Task<SingleResponse<RolActualizarRE>> Actualizar(int id, RolActualizarRQ oRegistro);
        public Task<SingleResponse<bool>> Eliminar(int id);
        public Task<ListResponse<RolConsultarRE>> Consultar(RolConsultarRQ oFiltro);
        public Task<SingleResponse<RolBuscarPorIDRE>> BuscarPorID(int id);
    }
}
