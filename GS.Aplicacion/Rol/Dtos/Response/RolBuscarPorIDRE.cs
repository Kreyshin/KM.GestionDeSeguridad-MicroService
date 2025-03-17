using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Rol.Dtos.Response
{
    public class RolBuscarPorIDRE
    {
        public string nombre { get; set; }
        public bool? activo { get; set; }
        public string estado { get; set; }
    }
}
