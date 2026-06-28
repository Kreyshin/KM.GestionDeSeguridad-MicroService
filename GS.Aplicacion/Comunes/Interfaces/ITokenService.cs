using GS.Aplicacion.Comunes.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Comunes.Interfaces
{
    public interface ITokenService
    {
        TokenResultadoRS GenerarToken(int idUsuario, string nombreUsuario, int idRol);
        string GenerarRefreshToken();
    }
}
