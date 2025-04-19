using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Comunes.AuditoriaHelper
{
    public interface IJwtService
    {
        string GetUserFromToken(string jwtToken);
    }
}
