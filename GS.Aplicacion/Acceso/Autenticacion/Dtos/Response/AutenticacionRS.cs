using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.Autenticacion.Dtos.Response
{
    public class AutenticacionRS
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiracion { get; set; }
        public UsuarioInfoRS? Usuario { get; set; }   // datos básicos del usuario
    }

    public class UsuarioInfoRS
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
       
    }
}
