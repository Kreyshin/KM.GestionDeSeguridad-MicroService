using GS.Dominio.Comunes;
using GS.Dominio.Entidades;

namespace GS.Dominio.Interfaces.Querys
{
    public interface IUsuarioRepositoryQ
    {
        public Task<SingleResponse<UsuariosEN>> BuscarPorID(int id);
        public Task<ListResponse<UsuariosEN>> Consultar(UsuariosEN oFiltro);
    }
}
