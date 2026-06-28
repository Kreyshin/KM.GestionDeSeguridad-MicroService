using GS.Dominio.Comunes;
using GS.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Dominio.Interfaces.Commands
{
    public interface IConexionRepositoryC
    {
        public Task<SingleResponse<int>> GuardarConexion(ConexionEN conexiones);
    }
}
