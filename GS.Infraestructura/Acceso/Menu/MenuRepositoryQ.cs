using Dapper;
using GS.Aplicacion.Acceso.ObtenerMenu;
using GS.Aplicacion.Acceso.ObtenerMenu.Dtos;
using GS.Dominio.Comunes;
using GS.Infraestructura.Comunes;
using GS.Infraestructura.Persistencia;
using GS.Infraestructura.Repositorios.Commands;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;

namespace GS.Infraestructura.Acceso.Menu
{
    public class MenuRepositoryQ(DbConexion dbConexion, ILogger<RolRepositoryC> logger) : IMenuRepositoryQ
    {

        public readonly DbConexion _dbConexion = dbConexion;
        private readonly ILogger<RolRepositoryC> _logger = logger;
   
        public async Task<SingleResponse<MenuSpResultDto>> ObtenerMenuAsync(ObtenerMenuQuery query)
        {
            if(query == null)
            throw new ArgumentNullException(nameof(query));

            var oResp = new SingleResponse<MenuSpResultDto>();

            DynamicParameters objParam = Utilitarios.GenerarParametros(new
            {
                IID_Usuario = query.IdUsuario,
                IID_Sistema = query.IdSistema
            });

            try
            {
                using var connection = _dbConexion.CrearConexion;
                using var multi = await connection.QueryMultipleAsync(
                     sql: "Seg_GetMenuUsuario",
                     commandType: CommandType.StoredProcedure,
                     param: objParam);

                 oResp.Data = new MenuSpResultDto
                {
                    Modulos = multi.Read<MenuModuloQDto>().ToList(),
                    Secciones = multi.Read<MenuSeccionQDto>().ToList(),
                    Opciones = multi.Read<MenuOpcionQDto>().ToList()
                };
            }
            catch (SqlException exsql)
            {
                if (exsql.Number == 50001)
                {
                    // Es un mensaje de validación del SP, no un error crítico
                    _logger.LogWarning("Validación de negocio: {Mensaje}", exsql.Message);
                    oResp.ErrorCode = 50001;
                    oResp.StatusMessage = exsql.Message;
                    oResp.StatusType = "VALIDACION";
                }
                else
                {
                    // Es un error real de SQL
                    _logger.LogError(exsql, "SQL Error ({ErrorCode}): Ocurrió una excepción SQL al autenticar usuario.", exsql.Number);
                    oResp.ErrorCode = exsql.Number;
                    oResp.ErrorMessage = "Error de base de datos, contactar con el administrador del sistema.";
                    oResp.StatusType = "SQL-ERROR";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Backend Error (50100): Ocurrió una excepción en C# al autenticar usuario.");
                oResp.ErrorCode = 50100;
                oResp.ErrorMessage = "Error de BackEnd, comunicarse con el encargado de este microservicio.";
                oResp.StatusType = "BACKEND-ERROR";
            }

            return oResp;
        }
    }
}
