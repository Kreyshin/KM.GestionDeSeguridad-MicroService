using GS.Dominio.Comunes;
using GS.Dominio.Entidades;

namespace GS.Dominio.Interfaces.Querys
{

    public interface IRolRepositoryQ
    {
        public Task<SingleResponse<RolEN>> BuscarPorID(int id);
        public Task<ListResponse<RolEN>> Consultar(RolEN oFiltro);
    }
}
