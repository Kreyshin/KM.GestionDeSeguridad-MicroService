using GS.Aplicacion.Autenticacion.Dtos;
using GS.Aplicacion.Comunes;
using GS.Dominio.Autenticacion;
using GS.Dominio.Comunes;
using GS.Dominio.Conexiones;
using GS.Dominio.Permisos;
using Microsoft.Extensions.Logging;

namespace GS.Aplicacion.Autenticacion
{
    /// <summary>
    /// Inicio de sesión. Las credenciales se validan en la base;
    /// aquí se traduce el resultado y se emite el token.
    /// </summary>
    public sealed class AutenticacionCU(
        IAutenticacionRepo repositorio,
        ITokenService tokenService,
        IConexionRepo conexionRepo,
        IPermisoRepo permisoRepo,
        ILogger<AutenticacionCU> logger) : IAutenticacionCU
    {
        private readonly IAutenticacionRepo _repositorio = repositorio;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IConexionRepo _conexionRepo = conexionRepo;
        private readonly IPermisoRepo _permisoRepo = permisoRepo;
        private readonly ILogger<AutenticacionCU> _logger = logger;

        /// <inheritdoc />
        public async Task<SingleResponse<AutenticacionRS>> Autenticar(AutenticacionRQ solicitud, CancellationToken ct)
        {
            if (!EsSolicitudCompleta(solicitud))
                return Fallo(Codigos.Auth.SolicitudIncompleta, TiposEstado.Validacion,
                    "Faltan datos obligatorios en la solicitud.");

            try
            {
                var fila = await _repositorio.Autenticar(solicitud.Usuario, solicitud.Clave, solicitud.CodSistema,
                    solicitud.RolId, solicitud.OrganizacionId, solicitud.EmpresaId, solicitud.SucursalId, ct);

                if (fila is null)
                {
                    _logger.LogError("fn_autenticar_usuario no devolvió ninguna fila para {Usuario}", solicitud.Usuario);
                    return Fallo(Codigos.Srv.RespuestaInesperada, TiposEstado.ErrorSql,
                        "Error interno. Intente nuevamente.");
                }

                if (fila.oc_codigo != Codigos.Auth.Ok)
                    return Fallo(fila.oc_codigo, TiposEstado.Validacion, "No autorizado.");

                // La función declara estas columnas como nulas porque vienen vacías
                // cuando no autentica. Si llegan vacías junto a AUTH.OK, el contrato
                // de la base cambió y no se puede emitir un token coherente.
                if (fila.oid_usuario is not { } idUsuario
                    || fila.oid_rol is not { } idRol
                    || fila.oid_sistema is not { } idSistema
                    || string.IsNullOrWhiteSpace(fila.oc_usuario))
                {
                    _logger.LogError("fn_autenticar_usuario devolvió {Codigo} con datos incompletos para {Usuario}",
                        fila.oc_codigo, solicitud.Usuario);
                    return Fallo(Codigos.Srv.RespuestaInesperada, TiposEstado.ErrorSql,
                        "Error interno. Intente nuevamente.");
                }

                var permisos = await _permisoRepo.ObtenerEfectivos(idUsuario, idSistema, idRol,
                    fila.oid_organizacion, fila.oid_empresa, fila.oid_sucursal, ct);

                var codigosPermitidos = permisos
                    .Where(p => p.b_permite)
                    .Select(p => p.c_codigo)
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .ToList();

                var token = _tokenService.Generar(idUsuario, fila.oc_usuario, idRol, idSistema, codigosPermitidos);
                var renovacion = _tokenService.GenerarRenovacion();

                var conexion = await _conexionRepo.Abrir(idUsuario, idRol, idSistema, renovacion.Resumen, ct);

                if (conexion?.oc_codigo != Codigos.Conexion.Ok)
                {
                    _logger.LogError("No se pudo registrar la sesión de {Usuario}: {Codigo}",
                        solicitud.Usuario, conexion?.oc_codigo ?? "sin respuesta");
                    return Fallo(Codigos.Srv.ErrorInterno, TiposEstado.ErrorSql,
                        "Error interno. Intente nuevamente.");
                }

                return new SingleResponse<AutenticacionRS>
                {
                    StatusCode = MapeoEstado.EstadoDe(Codigos.Auth.Ok),
                    Codigo = Codigos.Auth.Ok,
                    StatusType = TiposEstado.Exito,
                    Data = new AutenticacionRS
                    {
                        Token = token.Token,
                        RefreshToken = renovacion.Token,
                        Expiracion = token.Expiracion,
                        Usuario = new UsuarioRS
                        {
                            IdUsuario = idUsuario,
                            Usuario = fila.oc_usuario,
                            Nombre = fila.oc_nombre ?? string.Empty,
                            IdRol = idRol,
                            IdSistema = idSistema
                        }
                    }
                };
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al autenticar al usuario {Usuario}", solicitud.Usuario);

                return new SingleResponse<AutenticacionRS>
                {
                    StatusCode = MapeoEstado.EstadoDe(Codigos.Srv.ErrorInterno),
                    Codigo = Codigos.Srv.ErrorInterno,
                    ErrorCode = 50100,
                    ErrorMessage = ex.Message,
                    StatusMessage = "Error interno. Intente nuevamente.",
                    StatusType = TiposEstado.ErrorBackend
                };
            }
        }

        /// <inheritdoc />
        public async Task<ListResponse<ContextoRS>> ObtenerContextos(AutenticacionRQ solicitud, CancellationToken ct)
        {
            if (!EsSolicitudCompleta(solicitud))
                return FalloLista(Codigos.Auth.SolicitudIncompleta, TiposEstado.Validacion,
                    "Faltan datos obligatorios en la solicitud.");

            try
            {
                var filas = await _repositorio.ObtenerContextos(
                    solicitud.Usuario, solicitud.Clave, solicitud.CodSistema, ct);

                var codigo = filas.Count > 0 ? filas[0].oc_codigo : Codigos.Srv.RespuestaInesperada;

                if (codigo != Codigos.Auth.Ok)
                    return FalloLista(codigo, TiposEstado.Validacion, "No autorizado.");

                return new ListResponse<ContextoRS>
                {
                    StatusCode = MapeoEstado.EstadoDe(Codigos.Auth.Ok),
                    Codigo = Codigos.Auth.Ok,
                    StatusType = TiposEstado.Exito,
                    Data = filas.Select(f => new ContextoRS
                    {
                        RolId = f.oid_rol ?? 0,
                        Rol = f.oc_rol ?? string.Empty,
                        OrganizacionId = f.oid_organizacion,
                        Organizacion = f.oc_organizacion,
                        EmpresaId = f.oid_empresa,
                        Empresa = f.oc_empresa,
                        SucursalId = f.oid_sucursal,
                        Sucursal = f.oc_sucursal
                    }).ToList()
                };
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los contextos de {Usuario}", solicitud.Usuario);

                return new ListResponse<ContextoRS>
                {
                    StatusCode = MapeoEstado.EstadoDe(Codigos.Srv.ErrorInterno),
                    Codigo = Codigos.Srv.ErrorInterno,
                    ErrorCode = 50100,
                    ErrorMessage = ex.Message,
                    StatusMessage = "Error interno. Intente nuevamente.",
                    StatusType = TiposEstado.ErrorBackend
                };
            }
        }

        /// <summary>Comprueba que vengan usuario, clave y código de sistema.</summary>
        private static bool EsSolicitudCompleta(AutenticacionRQ solicitud) =>
            solicitud is not null
            && !string.IsNullOrWhiteSpace(solicitud.Usuario)
            && !string.IsNullOrWhiteSpace(solicitud.Clave)
            && !string.IsNullOrWhiteSpace(solicitud.CodSistema);

        /// <summary>Arma una respuesta fallida con el estado HTTP que le toca al código.</summary>
        private static SingleResponse<AutenticacionRS> Fallo(string codigo, string tipoEstado, string mensaje) => new()
        {
            StatusCode = MapeoEstado.EstadoDe(codigo),
            Codigo = codigo,
            StatusMessage = mensaje,
            StatusType = tipoEstado
        };

        /// <summary>Arma una respuesta de lista fallida.</summary>
        private static ListResponse<ContextoRS> FalloLista(string codigo, string tipoEstado, string mensaje) => new()
        {
            StatusCode = MapeoEstado.EstadoDe(codigo),
            Codigo = codigo,
            StatusMessage = mensaje,
            StatusType = tipoEstado
        };
    }
}
