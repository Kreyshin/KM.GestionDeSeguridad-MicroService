using GS.Aplicacion.Acceso.Conexiones.Dtos;
using GS.Aplicacion.Acceso.Conexiones.Interfaces;
using GS.Dominio.Entidades;
using GS.Dominio.Interfaces.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.Conexiones.Servicios
{
    public class GestionConexionesSV(
    IConexionRepositoryC conexionRepoC,
    ILogger<IGestionConexionesSV> logger) : IGestionConexionesSV
    {
        // Constante en lugar de hardcodear el string
        private const string USUARIO_SISTEMA = "SISTEMAS";


        public async Task<bool> GuardarConexion(NuevaConexionRQ conexionRQ)
        {
            // Validación → retorna false, no lanza excepción
            if (conexionRQ is null)
            {
                logger.LogWarning("GuardarConexion recibió un request nulo.");
                return false;
            }

            try
            {
                var conexionEN = new ConexionEN
                {
                    ID_Usuario = conexionRQ.idUsuario,
                    ID_Rol = conexionRQ.idRol,
                    C_Cod_Sistema = conexionRQ.codSistema,
                    C_RefreshToken = conexionRQ.refreshToken,
                    C_Usuario_Creacion = USUARIO_SISTEMA
                };

                var oRes = await conexionRepoC.GuardarConexion(conexionEN);

                // Éxito: solo ErrorCode == 0
                if (oRes.ErrorCode == 0)
                    return true;

                // SP devolvió un error de negocio conocido
                logger.LogWarning(
                    "SP GuardarConexion retornó error {Code}: {Message}",
                    oRes.ErrorCode, oRes.ErrorMessage);

                return false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar conexión para usuario {IdUsuario}",
                    conexionRQ.idUsuario);
                return false;
            }
        }
    }
}
