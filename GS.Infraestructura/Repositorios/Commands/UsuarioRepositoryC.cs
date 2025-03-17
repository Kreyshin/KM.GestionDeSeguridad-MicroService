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
    public class UsuarioRepositoryC(DbConexion dbConexion, ILogger<UsuarioRepositoryC> logger) : IUsuarioRepositoryC
    {
        private readonly DbConexion _dbConexion = dbConexion;
        private readonly ILogger<UsuarioRepositoryC> _logger = logger;

        public Task<SingleResponse<UsuarioEN>> Actualizar(UsuarioEN usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<SingleResponse<UsuarioEN>> Crear(UsuarioEN usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            var oResp = new SingleResponse<UsuarioEN>();

            DynamicParameters objParam = Utilitarios.GenerarParametros(new
            {
                IC_Usuario = usuario.C_Usuario,
                IC_Nombre = usuario.C_Nombre,
                IC_Clave = usuario.C_Clave,
                IC_Salt = usuario.C_Salt,
                IID_Rol = usuario.ID_Rol,
                IID_Empleado = usuario.ID_Empleado,
                IC_Usuario_Creacion = usuario.C_Usuario_Creacion
            });


            try
            {
                using var connection = _dbConexion.CrearConexion;
                oResp.Data = await connection.QuerySingleAsync<UsuarioEN>(
                         sql: "Sp_UsuarioC_Crear",
                         commandType: CommandType.StoredProcedure,
                         param: objParam
                       );
            }
            catch (SqlException exsql)
            {
                _logger.LogError(exsql, "Ocurrio un exepcion(Sql) al intentar crear el Usuario.");
                oResp.StatusCode = exsql.Number;
                oResp.StatusMessage = "Error  de base de datos, comunicarse con el Administrador de Base de datos.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrio un exepcion(c#) al intentar crear el Usuario.");
                oResp.StatusCode = 959899;
                oResp.StatusMessage = "Error de BackEnd, comunicarse con el encargado de esta Api.";
            }

            return oResp;
        }

        public async Task<SingleResponse<int>> Eliminar(int id)
        {
            var oResp = new SingleResponse<int>();
            DynamicParameters objParam = Utilitarios.GenerarParametros(new
            {
                IID_Usuario = id
            });

            try
            {
                using var connection = _dbConexion.CrearConexion;
                oResp.Data = await connection.ExecuteAsync(
                    sql: "Sp_UsuarioC_Eliminar",
                    commandType: CommandType.StoredProcedure,
                    param: objParam
                );
            }
            catch (SqlException exsql)
            {
                _logger.LogError(exsql, "Ocurrio un exepcion(Sql) al intentar eliminar el Usuario con ID: {id}", id);
                oResp.StatusCode = exsql.Number;
                oResp.StatusMessage = "Error  de base de datos, comunicarse con el Administrador de Base de datos.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrio un exepcion(c#) al intentar eliminar el Usuario con ID: {id}", id);
                oResp.StatusCode = 959899;
                oResp.StatusMessage = "Error de BackEnd, comunicarse con el encargado de esta Api.";
            }

            return oResp;
        }
    }
}
