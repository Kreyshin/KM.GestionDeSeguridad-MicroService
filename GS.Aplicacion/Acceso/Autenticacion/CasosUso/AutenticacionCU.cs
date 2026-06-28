using AutoMapper;
using GS.Aplicacion.Acceso.Autenticacion.Dtos.Request;
using GS.Aplicacion.Acceso.Autenticacion.Dtos.Response;
using GS.Aplicacion.Acceso.Autenticacion.Interfaces;
using GS.Aplicacion.Acceso.Conexiones.Dtos;
using GS.Aplicacion.Acceso.Conexiones.Interfaces;
using GS.Aplicacion.Comunes.Interfaces;
using GS.Dominio.Comunes;
using GS.Dominio.Entidades;
using GS.Dominio.Interfaces.Commands;
using GS.Dominio.Interfaces.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.Autenticacion.CasosUso
{
    public sealed class AutenticacionCU(
        IUsuariosAuthRepositoryQ authRepository,
        ITokenService tokenService,
        IMapper imapper,
        IGestionConexionesSV gestionConexiones) : IAutenticacionCU
    {
        private readonly IMapper _imapper = imapper;


        public async Task<SingleResponse<AutenticacionRS>> Auth(AutenticacionRQ authRQ)
        {
            if (string.IsNullOrWhiteSpace(authRQ?.Usuario) ||
            string.IsNullOrWhiteSpace(authRQ?.Clave))
                return Unauthorized("Usuario o Clave incorrecta.");

            try
            {
                var oRes = await authRepository.ObtenerPorCodigo(authRQ.Usuario);

                if (oRes.ErrorCode == 50001 || oRes.Data == null)
                    return Unauthorized("Usuario o Clave incorrecta.");

                if (!oRes.Data.VerificarClave(authRQ.Clave))
                    return Unauthorized("Usuario o Clave incorrecta.");

                if (!oRes.Data.B_Activo)
                    return Unauthorized("Usuario inactivo. Contacte al administrador.");

                var token = tokenService.GenerarToken(
                oRes.Data.ID_Usuario, authRQ.Usuario, oRes.Data.ID_Rol);
                var refreshToken = tokenService.GenerarRefreshToken();

                await gestionConexiones.GuardarConexion(new NuevaConexionRQ
                {
                    idUsuario = oRes.Data.ID_Usuario,
                    idRol = oRes.Data.ID_Rol,
                    codSistema = authRQ.CodSistema,
                    refreshToken = refreshToken
                });

                return new SingleResponse<AutenticacionRS>
                {
                    StatusCode = 200,
                    StatusMessage = "OK",
                    Data = new AutenticacionRS
                    {
                        Usuario = oRes.StatusCode == 0 ? _imapper.Map<UsuarioInfoRS>(oRes.Data) : null,
                        Token = token.AccessToken,
                        RefreshToken = refreshToken,
                        Expiracion = token.Expiracion
                    }
                };
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error en Auth para {User}", authRQ.usuario);
                return new SingleResponse<AutenticacionRS>
                {
                    StatusCode = 500,
                    StatusMessage = "Error interno. Intente nuevamente."
                };
            }
        }

        private static SingleResponse<AutenticacionRS> Unauthorized(string mensaje) =>
        new() { StatusCode = 401, StatusMessage = mensaje };

       
    }
}
