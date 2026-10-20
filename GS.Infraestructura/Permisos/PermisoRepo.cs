using Dapper;
using GS.Dominio.Permisos;
using GS.Infraestructura.Comunes;
using Npgsql;

namespace GS.Infraestructura.Permisos
{
    /// <summary>Acceso a datos de permisos contra PostgreSQL.</summary>
    public sealed class PermisoRepo(NpgsqlDataSource dataSource, DatosOpciones opciones) : IPermisoRepo
    {
        private readonly NpgsqlDataSource _dataSource = dataSource;
        private readonly DatosOpciones _opciones = opciones;

        /// <inheritdoc />
        public async Task<IReadOnlyList<PermisoDB>> ObtenerEfectivos(int idUsuario, int idSistema, int idRol,
            int? idOrganizacion, int? idEmpresa, int? idSucursal, CancellationToken ct)
        {
            var sql = $"""
                SELECT *
                FROM {_opciones.Esquema}.fn_obtener_permisos(
                    @p_id_usuario, @p_id_sistema, @p_id_rol,
                    @p_id_organizacion, @p_id_empresa, @p_id_sucursal);
                """;

            var parametros = new
            {
                p_id_usuario = idUsuario,
                p_id_sistema = idSistema,
                p_id_rol = idRol,
                p_id_organizacion = idOrganizacion,
                p_id_empresa = idEmpresa,
                p_id_sucursal = idSucursal
            };

            await using var conexion = await _dataSource.OpenConnectionAsync(ct);

            var filas = await conexion.QueryAsync<PermisoDB>(
                new CommandDefinition(sql, parametros, cancellationToken: ct));

            return filas.AsList();
        }
    }
}
