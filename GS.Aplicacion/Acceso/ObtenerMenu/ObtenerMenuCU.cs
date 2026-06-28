using AutoMapper;
using GS.Aplicacion.Acceso.ObtenerMenu.Dtos;
using GS.Aplicacion.Rol.Dtos.Response;
using GS.Dominio.Comunes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.ObtenerMenu
{
    public class ObtenerMenuCU(
         IMenuRepositoryQ menuQueryRepository,
         IMapper mapper,
         ILogger<ObtenerMenuCU> logger
        ): IObtenerMenuCU
    {

        private readonly IMenuRepositoryQ _menuQueryRepository = menuQueryRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<ObtenerMenuCU> _logger = logger;


        public async Task<SingleResponse<ObtenerMenuRE>> ObtenerMenuAsync(ObtenerMenuRQ param)
        {
           if( param == null )
             throw new ArgumentNullException(nameof(param));

            try
            {
                var query = _mapper.Map<ObtenerMenuQuery>(param);
                var oRes = await _menuQueryRepository.ObtenerMenuAsync(query);

                if (oRes.ErrorCode == 0)
                {
                    return new SingleResponse<ObtenerMenuRE>
                    {
                        StatusCode = 200,
                        Data = _mapper.Map<ObtenerMenuRE>(oRes.Data),
                        StatusType = "ÉXITO"
                    };
                }
                else if (oRes.ErrorCode == 50001)
                {
                    return new SingleResponse<ObtenerMenuRE>
                    {
                        StatusCode = 400,
                        Data = null,
                        StatusMessage = oRes.StatusMessage,
                        StatusType = oRes.StatusType
                    };
                }
                else
                {
                    return new SingleResponse<ObtenerMenuRE>
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
                _logger.LogError(ex, $"{50200}: Ocurrio un exepcion(c#) al intentar actualizar la Rol.");
                return new SingleResponse<ObtenerMenuRE>
                {
                    StatusCode = 500,
                    StatusType = "BACKEND-ERROR",
                    StatusMessage = "Error de BackEnd, comunicarse con el encargado de este microservicio."

                };
            }
        }
    }
}
