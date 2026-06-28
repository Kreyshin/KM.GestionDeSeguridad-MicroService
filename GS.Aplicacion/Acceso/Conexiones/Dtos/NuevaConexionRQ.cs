using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.Conexiones.Dtos
{
    public class NuevaConexionRQ
    {
        public int idUsuario { get; set; }
        public int idRol { get; set; }
        public string codSistema { get; set; }
        public string refreshToken { get; set; }
    }
}
