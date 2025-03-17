using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Rol.Dtos.Request
{
    public class RolActualizarRQ
    {
        public string? nombre { get; set; }
        public bool? estado { set; get; }
    }
}
