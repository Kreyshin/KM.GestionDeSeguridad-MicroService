using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Dominio.Entidades
{
    public class RolEN
    {
        public int ID { set; get; }
        public string C_Nombre { set; get; } = string.Empty;
        public bool? B_Activo { set; get; }
        public string? C_Usuario_Creacion { set; get; }
        public string? C_Usuario_Modificacion { set; get; }

        #region DatosRelacionados
        public string? C_Estado { set; get; }
        #endregion
    }
}
