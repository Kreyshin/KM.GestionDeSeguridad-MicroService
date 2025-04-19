using Dapper;
using GS.Dominio.Comunes;
using GS.Dominio.Entidades;
using GS.Dominio.Interfaces.Querys;
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

namespace GS.Infraestructura.Repositorios.Querys
{
    public class RolRepositoryQ(DbConexion dbConexion, ILogger<RolRepositoryQ> logger) : IRolRepositoryQ
    {
        private readonly DbConexion _dbConexion = dbConexion;
        private readonly ILogger<RolRepositoryQ> _logger = logger;

        public async Task<SingleResponse<RolEN>> BuscarPorID(int id)
        {
            SingleResponse<RolEN> oResp = new();
            DynamicParameters parametros = Utilitarios.GenerarParametros(new
            {
                IID = id,
            });

            try
            {
                IDbConnection connection = _dbConexion.CrearConexion;
                oResp.Data = await connection.QueryFirstOrDefaultAsync<RolEN>(
                    sql: "Sp_RolQ_BuscarPorID",
                    commandType: CommandType.StoredProcedure,
                    param: parametros
                );
            }
            catch (SqlException exsql)
            {
                _logger.LogError(exsql, "Ocurrio un exepcion(Sql) al intentar eliminar el ROl con ID: {id}", id);
                oResp.ErrorCode = exsql.Number;
                oResp.ErrorMessage = "Error de base de datos, contactar con el administrador del sistema.";
                oResp.StatusType = "SQL-ERROR";
                return oResp;
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

        public async Task<ListResponse<RolEN>> Consultar(RolEN oFiltro)
        {
            ListResponse<RolEN> oResp = new();
            DynamicParameters parametros = Utilitarios.GenerarParametros(new
            {
                IC_Nombre = oFiltro.C_Nombre,
                IC_Estado = oFiltro.C_Estado,               
            });

            try
            {
                IDbConnection connection = _dbConexion.CrearConexion;
                oResp.Data = await connection.QueryAsync<RolEN>(
                   sql: "Sp_RolQ_Consultar",
                   commandType: CommandType.StoredProcedure,
                   param: parametros
                );
            }
            catch (SqlException exsql)
            {
                _logger.LogError(exsql, "SQL Error ({ErrorCode}): Ocurrió una excepción SQL al autenticar usuario.", exsql.Number);
                oResp.ErrorCode = exsql.Number;
                oResp.ErrorMessage = "Error de base de datos, contactar con el administrador del sistema.";
                oResp.StatusType = "SQL-ERROR";
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
