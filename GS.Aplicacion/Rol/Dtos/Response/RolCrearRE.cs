using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Rol.Dtos.Response
{
    public class RolCrearRE
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
        public string estado { get; set; }
    }
}
