using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.ObtenerMenu
{
    public class ObtenerMenuRE
    {
        public List<MenuModuloDto> Modulos { get; set; } = [];
        public List<MenuSeccionDto> Secciones { get; set; } = [];
        public List<MenuOpcionDto> Opciones { get; set; } = [];

    }

    public class  MenuModuloDto
    {
        public int Id { get; set; }
        public int IdModulo { get; set; }
        public string Nombre { get; set; }
        public string RutaIcono { get; set; }
    }

    public class MenuSeccionDto
    {
        public int IdModulo { get; set; }
        public int IdSeccion { get; set; }
        public string Nombre { get; set; }
    }

    public class MenuOpcionDto
    {
        public int Id { get; set; }
        public int IdModulo { get; set; }
        public int IdSeccion { get; set; }
        public int IdOpcion { get; set; }
        public string Nombre { get; set; }
        public string RutaIcono { get; set; }
        public string Url { get; set; }
    }

}
