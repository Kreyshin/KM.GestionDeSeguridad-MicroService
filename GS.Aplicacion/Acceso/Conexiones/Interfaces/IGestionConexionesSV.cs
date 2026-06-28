using GS.Aplicacion.Acceso.Conexiones.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.Conexiones.Interfaces
{
    public interface IGestionConexionesSV
    {
        Task<bool> GuardarConexion(NuevaConexionRQ conexionRQ);
    }
}
