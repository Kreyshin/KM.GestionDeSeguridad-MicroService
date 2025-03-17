using AutoMapper;
using GS.Aplicacion.Rol.Dtos.Request;
using GS.Aplicacion.Rol.Dtos.Response;
using GS.Aplicacion.Rol.Interfaces;
using GS.Dominio.Comunes;
using GS.Dominio.Entidades;
using GS.Dominio.Interfaces.Commands;
using GS.Dominio.Interfaces.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Rol.CasosUso
{
    public class RolCrudCU(IRolRepositoryQ rolRepositoryQ, IRolRepositoryC rolRepositoryC, IMapper mapper) : IRolCrudCU
    {

        private readonly IRolRepositoryQ _rolRepoQ = rolRepositoryQ;
        private readonly IRolRepositoryC _rolRepoC = rolRepositoryC;
        private readonly IMapper _mapper = mapper;

        public async Task<SingleResponse<RolActualizarRE>> Actualizar(int id, RolActualizarRQ oRegistro)
        {
            if (oRegistro == null)
            {
                throw new ArgumentNullException(nameof(oRegistro));
            }

            try
            {
                var rolEn = _mapper.Map<RolEN>(oRegistro);
                rolEn.ID_Rol = id;

                var oRes = await _rolRepoC.Actualizar(rolEn);
                var oResp = new SingleResponse<RolActualizarRE>
                {
                    StatusCode = oRes.StatusCode,
                    Data = oRes.StatusCode == 0 ? _mapper.Map<RolActualizarRE>(oRes.Data) : null
                };

                return oResp;
            }
            catch (Exception ex)
            {
                return new SingleResponse<RolActualizarRE>
                {
                    StatusCode = -1,
                    //type = "Excepcion(c#)",
                    StatusMessage = ex.Message
                };
            }
        }

        public Task<SingleResponse<RolBuscarPorIDRE>> BuscarPorID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ListResponse<RolConsultarRE>> Consultar(RolConsultarRQ oFiltro)
        {
            throw new NotImplementedException();
        }

        public async Task<SingleResponse<RolCrearRE>> Crear(RolCrearRQ oRegistro)
        {
            if (oRegistro == null)
            {
                throw new ArgumentNullException(nameof(oRegistro));
            }

            try
            {
                var rolEn = _mapper.Map<RolEN>(oRegistro);


                var oRes = await _rolRepoC.Crear(rolEn);
                var oResp = new SingleResponse<RolCrearRE>
                {
                    StatusCode = oRes.StatusCode,
                    //type = oRes.Result.code == 0 ? "Correcto" : "Excepcion(Sql)",
                    Data = oRes.StatusCode == 0 ? _mapper.Map<RolCrearRE>(oRes.Data) : null
                };

                return oResp;
            }
            catch (Exception ex)
            {
                return new SingleResponse<RolCrearRE>
                {
                    StatusCode = -1,
                    //type = "Excepcion(c#)",
                    StatusMessage = ex.Message
                };
            }
        }

        public Task<SingleResponse<bool>> Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
