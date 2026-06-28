using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.Permisos.Dtos.Response
{
    public class PermisoRS
    {
        public int IdPermiso { get; set; }
        public string Codigo { get; set; }  // ej: "ERP_USUARIOS_CREAR"
        public string Descripcion { get; set; }
        public bool Permite { get; set; }
    }
}
