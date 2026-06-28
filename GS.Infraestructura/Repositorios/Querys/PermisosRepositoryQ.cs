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
    public class PermisosRepositoryQ(DbConexion dbConexion, ILogger<PermisosRepositoryQ> logger) : IPermisosRepositoryQ
    {
        private readonly DbConexion _dbConexion = dbConexion;
        private readonly ILogger<PermisosRepositoryQ> _logger = logger;

        public async Task<ListResponse<PermisoEN>> ObtenerPermisos(int idUsuario, int idSistema)
        {
            if (idUsuario <= 0)
            {
                throw new ArgumentNullException(nameof(idUsuario));
            }
            if (idSistema <= 0)
            {
                throw new ArgumentNullException(nameof(idSistema));
            }

            var oResp = new ListResponse<PermisoEN>();

            var objParam = Utilitarios.GenerarParametros(new
            {
                IC_Usuario = idUsuario,
                IC_Sistema = idSistema
            });

            try
            {
                using var connection = _dbConexion.CrearConexion;
                oResp.Data = await connection.QueryAsync<PermisoEN>(
                         sql: "Seg_GetPermisos",
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
                    oResp.StatusMessage = exsql.Message;
                    oResp.StatusType = "VALIDATION";
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
