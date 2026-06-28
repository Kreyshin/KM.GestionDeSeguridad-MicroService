using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.Autenticacion.Dtos.Request
{
    public sealed record AutenticacionRQ
    {
        public string Usuario { get; init; } = string.Empty;
        public string Clave { get; init; } = string.Empty;
        public string CodSistema { get; init; } = string.Empty;
    }
}
