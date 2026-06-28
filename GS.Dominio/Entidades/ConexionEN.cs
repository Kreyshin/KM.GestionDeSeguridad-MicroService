using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Dominio.Entidades
{
    public class ConexionEN
    {
        public int ID { set; get; }
        public int ID_Usuario { set; get; }
        public int ID_Rol { set; get; }
        public string C_Cod_Sistema { set; get; }
        public string C_RefreshToken { set; get; }
        public DateTime F_FechaConexion { set; get; }
        public string C_Usuario_Creacion { set; get; }
        public string C_Usuario { set; get; }
    }
}
