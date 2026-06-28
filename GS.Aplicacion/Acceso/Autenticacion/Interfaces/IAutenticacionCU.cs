using GS.Aplicacion.Acceso.Autenticacion.CasosUso;
using GS.Aplicacion.Acceso.Autenticacion.Dtos.Request;
using GS.Aplicacion.Acceso.Autenticacion.Dtos.Response;
using GS.Dominio.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.Autenticacion.Interfaces
{
    public interface IAutenticacionCU
    {
        public Task<SingleResponse<AutenticacionRS>> Auth(AutenticacionRQ authRQ);
    }
}
