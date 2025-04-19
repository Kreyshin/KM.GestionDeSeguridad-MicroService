using AutoMapper;
using GS.Aplicacion.Comunes.AuditoriaHelper;
using GS.Aplicacion.Rol.Dtos.Request;
using GS.Aplicacion.Rol.Dtos.Response;
using GS.Aplicacion.Rol.Interfaces;
using GS.Dominio.Comunes;
using GS.Dominio.Entidades;
using GS.Dominio.Interfaces.Commands;
using GS.Dominio.Interfaces.Querys;
using Microsoft.Extensions.Logging;

namespace GS.Aplicacion.Rol.CasosUso
{
    public class RolCrudCU(
        IRolRepositoryQ rolRepositoryQ, 
        IRolRepositoryC rolRepositoryC, 
        IMapper mapper, ILogger<RolCrudCU> logger,
        IAuditoriaHelp audiHelp) : IRolCrudCU
    {

        private readonly IRolRepositoryQ _rolRepoQ = rolRepositoryQ;
        private readonly IRolRepositoryC _rolRepoC = rolRepositoryC;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<RolCrudCU> _logger = logger;
        private readonly IAuditoriaHelp _audiHelp = audiHelp;

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

        public async Task<ListResponse<RolConsultarRE>> Consultar(RolConsultarRQ oFiltro)
        {
           if(oFiltro == null)
           {
               throw new ArgumentNullException(nameof(oFiltro));
           }

            try
            {
                var rolEN = _mapper.Map<RolEN>(oFiltro);

                var oRes = await _rolRepoQ.Consultar(rolEN);

                if(oRes.ErrorCode == 0 && oRes.Data.Count() > 0)
                {
                    return new ListResponse<RolConsultarRE>
                    {
                        StatusCode = 200,
                        Data = _mapper.Map<List<RolConsultarRE>>(oRes.Data),
                        StatusType = "ÉXITO"
                    };
                }
                else if (oRes.ErrorCode == 0 && oRes.Data.Count() > 0)
                {
                    return new ListResponse<RolConsultarRE>
                    {
                        StatusCode = 204,
                        Data = null,
                        StatusMessage = oRes.StatusMessage,
                        StatusType = oRes.StatusType
                    };
                }
                else
                {
                    return new ListResponse<RolConsultarRE>
                    {
                        StatusCode = 500,
                        Data = null,
                        StatusMessage = oRes.ErrorMessage,
                        StatusType = oRes.StatusType
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{50200}: Ocurrio un exepcion(c#) al intentar consultar los roles.");
                return new ListResponse<RolConsultarRE>
                {
                    StatusCode = 500,
                    StatusType = "BACKEND-ERROR",
                    StatusMessage = "Error de BackEnd, comunicarse con el encargado de este microservicio."
                };
            }
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
                rolEn.C_Usuario_Creacion = _audiHelp.UserName;

                var oRes = await _rolRepoC.Crear(rolEn);

                if(oRes.ErrorCode == 0)
                {   
                    return new SingleResponse<RolCrearRE>
                    {
                        StatusCode = 200,
                        Data = _mapper.Map<RolCrearRE>(oRes.Data),
                        StatusType = "ÉXITO"
                    };
                }
                else if (oRes.ErrorCode == 50001)
                {
                    return new SingleResponse<RolCrearRE>
                    {
                        StatusCode = 204,
                        Data =  null,
                        StatusMessage = oRes.StatusMessage,
                        StatusType = oRes.StatusType
                    };
                }
                else
                {
                    return new SingleResponse<RolCrearRE>
                    {
                        StatusCode = 500,
                        Data = null,
                        StatusMessage = oRes.ErrorMessage,
                        StatusType = oRes.StatusType
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{50200}: Ocurrio un exepcion(c#) al intentar crear el rol.");
                return new SingleResponse<RolCrearRE>
                {
                    StatusCode = 500,
                    StatusType = "BACKEND-ERROR",
                    StatusMessage = "Error de BackEnd, comunicarse con el encargado de este microservicio."
                };
            }
        }

        public Task<SingleResponse<bool>> Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
