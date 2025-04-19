using Dapper;
using GS.Dominio.Comunes;
using GS.Dominio.Entidades;
using GS.Dominio.Interfaces.Commands;
using GS.Infraestructura.Comunes;
using GS.Infraestructura.Persistencia;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;

namespace GS.Infraestructura.Repositorios.Commands
{
    public class RolRepositoryC(DbConexion dbConexion, ILogger<RolRepositoryC> logger) : IRolRepositoryC
    {

        public readonly DbConexion _dbConexion = dbConexion;
        private readonly ILogger<RolRepositoryC> _logger = logger;

        public async Task<SingleResponse<RolEN>> Actualizar(RolEN rol)
        {
            if (rol == null)
            {
                throw new ArgumentNullException(nameof(rol));
            }

            var oResp = new SingleResponse<RolEN>();

            DynamicParameters objParam = Utilitarios.GenerarParametros(new
            {
                IID = rol.ID,
                IC_Nombre = rol.C_Nombre,
                IB_Activo = rol.B_Activo,
                IC_Usuario_Modificacion = rol.C_Usuario_Modificacion
            });

            try
            {
                using var connection = _dbConexion.CrearConexion;
                oResp.Data = await connection.QuerySingleOrDefaultAsync<RolEN>(
                    sql: "Sp_RolC_Actualizar",
                    commandType: CommandType.StoredProcedure,
                    param: objParam
                );
            }
            catch (SqlException exsql)
            {
                if (exsql.Number != 50001)
                {
                    _logger.LogError(exsql, "Ocurrio un exepcion(Sql) al intentar actualizar el ROl con ID: {id}", rol.ID);
                    oResp.ErrorCode = exsql.Number;
                    oResp.ErrorMessage = "Error de base de datos, contactar con el administrador del sistema.";
                    oResp.StatusType = "SQL-ERROR";
                    return oResp;
                }

                oResp.StatusMessage = exsql.Message;
            }
            catch (Exception ex)
            {              
                _logger.LogError(ex, "Ocurrio un exepcion(c#) al intentar actualizar el ROl con ID: {ID_Rol}", rol.ID);
                oResp.ErrorCode = 50100;
                oResp.ErrorMessage = "Error de BackEnd, comunicarse con el encargado de este microservicio.";
                oResp.StatusType = "BACKEND-ERROR";
                return oResp;
            }

            return oResp;
        }

        public async Task<SingleResponse<RolEN>> Crear(RolEN rol)
        {
            if (rol == null)
            {
                throw new ArgumentNullException(nameof(rol));
            }

            var oResp = new SingleResponse<RolEN>();

            DynamicParameters objParam = Utilitarios.GenerarParametros(new
            {
                IC_Nombre = rol.C_Nombre,
                IC_Usuario_Creacion = rol.C_Usuario_Creacion
            });

            try
            {
                using var connection = _dbConexion.CrearConexion;
                oResp.Data = await connection.QuerySingleAsync<RolEN>(
                         sql: "Sp_RolC_Crear",
                         commandType: CommandType.StoredProcedure,
                         param: objParam
                       );
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

        public async Task<SingleResponse<int>> Eliminar(int id)
        {
            var oResp = new SingleResponse<int>();
            DynamicParameters objParam = Utilitarios.GenerarParametros(new
            {
                IID = id
            });
            try
            {
                using var connection = _dbConexion.CrearConexion;
                oResp.Data = await connection.ExecuteAsync(
                    sql: "Sp_RolC_Eliminar",
                    commandType: CommandType.StoredProcedure,
                    param: objParam
                );
            }
            catch (SqlException exsql)
            {
                if (exsql.Number != 50001)
                {
                    _logger.LogError(exsql, "Ocurrio un exepcion(Sql) al intentar eliminar el ROl con ID: {id}", id);
                    oResp.ErrorCode = exsql.Number;
                    oResp.ErrorMessage = "Error de base de datos, contactar con el administrador del sistema.";
                    oResp.StatusType = "SQL-ERROR";
                    return oResp;
                }

                oResp.StatusMessage = exsql.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrio un exepcion(c#) al intentar crear el rol.");
                oResp.ErrorCode = 50100;
                oResp.ErrorMessage = "Error de BackEnd, comunicarse con el encargado de este microservicio.";
                oResp.StatusType = "BACKEND-ERROR";
                return oResp;
            }

            return oResp;
        }
    }
}
