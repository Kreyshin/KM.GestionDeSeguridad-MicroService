using Dapper;
using GS.Dominio.Comunes;
using GS.Dominio.Entidades;
using GS.Dominio.Interfaces.Commands;
using GS.Infraestructura.Comunes;
using GS.Infraestructura.Persistencia;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                IID_Rol = rol.ID_Rol,
                IC_Nombre = rol.C_Nombre,
                IB_Estado = rol.B_Activo,
                IC_Usuario_Modifiacion = rol.C_Usuario_Modificacion
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
                _logger.LogError(exsql, "Ocurrio un exepcion(Sql) al intentar actualizar el ROl con ID: {ID_Rol}", rol.ID_Rol);
                oResp.StatusCode = exsql.Number;
                oResp.StatusMessage = "Error  de base de datos, comunicarse con el Administrador de Base de datos.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrio un exepcion(c#) al intentar actualizar el ROl con ID: {ID_Rol}", rol.ID_Rol);
                oResp.StatusCode = 959899;
                oResp.StatusMessage = "Error de BackEnd, comunicarse con el encargado de esta Api.";
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
                if (exsql.Number != 50001)
                {
                    _logger.LogError(exsql, "Ocurrio un exepcion(Sql) al intentar crear el rol.");
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

        public async Task<SingleResponse<int>> Eliminar(int id)
        {
            var oResp = new SingleResponse<int>();
            DynamicParameters objParam = Utilitarios.GenerarParametros(new
            {
                IID_Rol = id
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
                _logger.LogError(exsql, "Ocurrio un exepcion(Sql) al intentar eliminar el ROl con ID: {id}", id);
                oResp.StatusCode = exsql.Number;
                oResp.StatusMessage = "Error  de base de datos, comunicarse con el Administrador de Base de datos.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrio un exepcion(c#) al intentar eliminar el ROl con ID: {id}", id);
                oResp.StatusCode = 959899;
                oResp.StatusMessage = "Error de BackEnd, comunicarse con el encargado de esta Api.";
            }

            return oResp;
        }
    }
}
