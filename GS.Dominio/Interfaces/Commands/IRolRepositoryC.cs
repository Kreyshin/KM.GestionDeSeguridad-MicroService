using GS.Dominio.Comunes;
using GS.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Dominio.Interfaces.Commands
{
    public interface IRolRepositoryC
    {
        public Task<SingleResponse<RolEN>> Crear(RolEN rol);
        public Task<SingleResponse<int>> Eliminar(int id);
        public Task<SingleResponse<RolEN>> Actualizar(RolEN rol);
    }
}
