using GS.Dominio.Comunes;
using GS.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Dominio.Interfaces.Querys
{
    public interface IPermisosRepositoryQ
    {
        Task<ListResponse<PermisoEN>> ObtenerPermisos(int idUsuario, int idSistema);
    }
}
