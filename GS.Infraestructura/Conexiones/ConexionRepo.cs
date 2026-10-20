using Dapper;
using GS.Dominio.Conexiones;
using GS.Infraestructura.Comunes;
using Npgsql;

namespace GS.Infraestructura.Conexiones
{
    /// <summary>Acceso a datos de sesiones contra PostgreSQL.</summary>
    public sealed class ConexionRepo(NpgsqlDataSource dataSource, DatosOpciones opciones) : IConexionRepo
    {
        private readonly NpgsqlDataSource _dataSource = dataSource;
        private readonly DatosOpciones _opciones = opciones;

        /// <inheritdoc />
        public async Task<ConexionDB?> Abrir(int idUsuario, int idRol, int idSistema, string resumenRenovacion, CancellationToken ct)
        {
            var sql = $"""
                SELECT *
                FROM {_opciones.Esquema}.fn_abrir_conexion(@iid_usuario, @iid_rol, @iid_sistema, @ic_resumen_renovacion);
                """;

            var parametros = new
            {
                iid_usuario = idUsuario,
                iid_rol = idRol,
                iid_sistema = idSistema,
                ic_resumen_renovacion = resumenRenovacion
            };

            await using var conexion = await _dataSource.OpenConnectionAsync(ct);

            return await conexion.QuerySingleOrDefaultAsync<ConexionDB>(
                new CommandDefinition(sql, parametros, cancellationToken: ct));
        }
    }
}
