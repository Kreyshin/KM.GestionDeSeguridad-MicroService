using GS.Dominio.Comunes;
using GS.Dominio.Entidades;

namespace GS.Dominio.Interfaces.Querys
{
    public interface IUsuarioRepositoryQ
    {
        public Task<SingleResponse<UsuarioEN>> BuscarPorID(int id);
        public Task<ListResponse<UsuarioEN>> Consultar(UsuarioEN oFiltro);
    }
}
