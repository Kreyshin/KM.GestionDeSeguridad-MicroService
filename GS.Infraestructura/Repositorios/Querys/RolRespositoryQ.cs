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
    public class RolRespositoryQ(DbConexion dbConexion, ILogger<RolRespositoryQ> logger) : IRolRepositoryQ
    {
        private readonly DbConexion _dbConexion = dbConexion;
        private readonly ILogger<RolRespositoryQ> _logger = logger;

        public async Task<SingleResponse<RolEN>> BuscarPorID(int id)
        {
            SingleResponse<RolEN> oResp = new();
            DynamicParameters parametros = Utilitarios.GenerarParametros(new
            {
                IID_Rol = id,
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
                oResp.StatusCode = exsql.Number;
                oResp.StatusMessage = exsql.Message;
            }
            catch (Exception ex)
            {
                oResp.StatusCode = 959899;
                oResp.StatusMessage = ex.Message;
            }

            return oResp;
        }

        public async Task<ListResponse<RolEN>> Consultar(RolEN oFiltro)
        {
            ListResponse<RolEN> oResp = new();
            DynamicParameters parametros = Utilitarios.GenerarParametros(new
            {
                IC_Estado = oFiltro.C_Estado,
                IC_Nombre = oFiltro.C_Nombre,

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
                oResp.StatusCode = exsql.Number;
                oResp.StatusMessage = exsql.Message;
            }
            catch (Exception exc)
            {
                oResp.StatusCode = 959899;
                oResp.StatusMessage = exc.Message;
            }

            return oResp;
        }
    }
}
