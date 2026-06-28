using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Comunes.Dtos.Response
{
    public class TokenResultadoRS
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
