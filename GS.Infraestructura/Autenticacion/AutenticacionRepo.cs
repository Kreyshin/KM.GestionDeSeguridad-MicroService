using Dapper;
using GS.Dominio.Autenticacion;
using GS.Infraestructura.Comunes;
using Npgsql;

namespace GS.Infraestructura.Autenticacion
{
    /// <summary>
    /// Acceso a datos de autenticación contra PostgreSQL.
    /// </summary>
    public sealed class AutenticacionRepo(NpgsqlDataSource dataSource, DatosOpciones opciones) : IAutenticacionRepo
    {
        private readonly NpgsqlDataSource _dataSource = dataSource;
        private readonly DatosOpciones _opciones = opciones;

        /// <inheritdoc />
        public async Task<AutenticacionDB?> Autenticar(string usuario, string clave, string codigoSistema,
            int? idRol, int? idOrganizacion, int? idEmpresa, int? idSucursal, CancellationToken ct)
        {
            // El esquema viene de configuración y está validado como identificador
            // en DatosOpciones; los valores siempre viajan como parámetros.
            var sql = $"""
                SELECT *
                FROM {_opciones.Esquema}.fn_autenticar_usuario(
                    @ic_usuario, @ic_clave, @ic_codigo_sistema,
                    @iid_rol, @iid_organizacion, @iid_empresa, @iid_sucursal);
                """;

            var parametros = new
            {
                ic_usuario = usuario,
                ic_clave = clave,
                ic_codigo_sistema = codigoSistema,
                iid_rol = idRol,
                iid_organizacion = idOrganizacion,
                iid_empresa = idEmpresa,
                iid_sucursal = idSucursal
            };

            await using var conexion = await _dataSource.OpenConnectionAsync(ct);

            return await conexion.QuerySingleOrDefaultAsync<AutenticacionDB>(
                new CommandDefinition(sql, parametros, cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<ContextoDB>> ObtenerContextos(string usuario, string clave, string codigoSistema, CancellationToken ct)
        {
            var sql = $"""
                SELECT *
                FROM {_opciones.Esquema}.fn_obtener_contextos(@ic_usuario, @ic_clave, @ic_codigo_sistema);
                """;

            var parametros = new
            {
                ic_usuario = usuario,
                ic_clave = clave,
                ic_codigo_sistema = codigoSistema
            };

            await using var conexion = await _dataSource.OpenConnectionAsync(ct);

            var filas = await conexion.QueryAsync<ContextoDB>(
                new CommandDefinition(sql, parametros, cancellationToken: ct));

            return filas.AsList();
        }
    }
}
