using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.ObtenerMenu.Dtos
{
    public class MenuOpcionQDto
    {
        public int Id { get; set; }
        public int Id_Modulo { get; set; }
        public int Id_Seccion { get; set; }
        public int Id_Opcion { get; set; }
        public string C_Nombre { get; set; }
        public string C_RutaIcono { get; set; }
        public string C_Url { get; set; }
    }
}
