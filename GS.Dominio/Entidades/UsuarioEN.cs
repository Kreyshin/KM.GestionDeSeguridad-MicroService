using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Dominio.Entidades
{
    public class UsuarioEN
    {
        public int ID_Usuario { set; get; }
        public string C_Usuario { set; get; }
        public string C_Nombre { set; get; }
        public string C_Clave { set; get; }
        public string C_Salt { set; get; }
        public int ID_Rol { set; get; }
        public int ID_Empleado { set; get; }
        public string C_Usuario_Creacion { set; get; }
        public string F_Fecha_Creacion { set; get; }
        public bool B_Activo { set; get; }

        #region DatosRelacionados
        public string C_Rol { set; get; }
        public string C_Empleado { set; get; }
        #endregion
    }
}
