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
    public class ConexionRepositoryC(DbConexion dbConexion, ILogger<ConexionRepositoryC> logger) : IConexionRepositoryC
    {
        private readonly DbConexion _dbConexion = dbConexion;
        private readonly ILogger<ConexionRepositoryC> _logger = logger;

        public async Task<SingleResponse<int>> GuardarConexion(ConexionEN conexiones)
        {
            if (conexiones == null)
            {
                throw new ArgumentNullException(nameof(conexiones));
            }

            var oResp = new SingleResponse<int>();

            DynamicParameters objParam = Utilitarios.GenerarParametros(new
            {
                IID_Usuario = conexiones.ID_Usuario,
                IID_Rol = conexiones.ID_Rol,
                IC_CodSistema = conexiones.C_Cod_Sistema,
                IC_RefreshToken = conexiones.C_RefreshToken,
                IC_Usuario_Creacion = conexiones.C_Usuario_Creacion
            });

            try
            {
                using var connection = _dbConexion.CrearConexion;
                oResp.Data = await connection.ExecuteAsync(
                     sql: "Sp_ConexionesC_Crear",
                     commandType: CommandType.StoredProcedure,
                     param: objParam
                    );
            }
            catch (SqlException exsql)
            {
                if (exsql.Number != 50001)
                {
                    _logger.LogError(exsql, "{ErrorCode}: Ocurrio un exepcion(Sql) al intentar crear el rol.", oResp.ErrorCode);
                    oResp.ErrorCode = exsql.Number;
                    oResp.ErrorMessage = "Error de base de datos, contactar con el administrador del sistema.";
                    oResp.StatusType = "SQL-ERROR";
                    return oResp;
                }

                oResp.StatusMessage = exsql.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ErrorCode}: Ocurrio un exepcion(c#) al intentar crear el rol.", oResp.ErrorCode);
                oResp.ErrorCode = 50100;
                oResp.ErrorMessage = "Error de BackEnd, comunicarse con el encargado de este microservicio.";
                oResp.StatusType = "BACKEND-ERROR";
                return oResp;
            }

            return oResp;
        }
    }
}
