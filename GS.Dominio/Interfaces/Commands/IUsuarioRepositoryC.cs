using GS.Dominio.Comunes;
using GS.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Dominio.Interfaces.Commands
{
    public interface IUsuarioRepositoryC
    {
        public Task<SingleResponse<UsuariosEN>> Crear(UsuariosEN usuario);
        public Task<SingleResponse<UsuariosEN>> Actualizar(UsuariosEN usuario);
        public Task<SingleResponse<int>> Eliminar(int id);
    }
}
