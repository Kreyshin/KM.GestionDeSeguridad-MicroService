using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Dominio.Entidades
{
    public class PermisoEN
    {
        public int ID_Permiso { get; set; }
        public string C_Codigo { get; set; }
        public string C_Descripcion { get; set; }
        public bool B_Permite { get; set; }
    }
}
