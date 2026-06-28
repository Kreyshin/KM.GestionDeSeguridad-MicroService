using GS.Dominio.Comunes;
using GS.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Dominio.Interfaces.Querys
{
    public interface IUsuariosAuthRepositoryQ
    {
        Task<SingleResponse<UsuariosEN>> ObtenerPorCodigo(string usuario);
    }
}
