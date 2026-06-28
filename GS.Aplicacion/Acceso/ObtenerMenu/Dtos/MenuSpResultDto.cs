using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.ObtenerMenu.Dtos
{
    public class MenuSpResultDto
    {
        public List<MenuModuloQDto> Modulos { get; set; } = [];
        public List<MenuSeccionQDto> Secciones { get; set; } = [];
        public List<MenuOpcionQDto> Opciones { get; set; } = [];
    }
}
