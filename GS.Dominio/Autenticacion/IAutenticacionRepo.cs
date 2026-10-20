namespace GS.Dominio.Autenticacion
{
    /// <summary>Acceso a datos de autenticación.</summary>
    public interface IAutenticacionRepo
    {
        /// <summary>
        /// Invoca la función de autenticación. La clave se compara en la base:
        /// su resumen no sale de ahí.
        /// </summary>
        /// <param name="usuario">Nombre de usuario.</param>
        /// <param name="clave">Clave en claro.</param>
        /// <param name="codigoSistema">Código del sistema.</param>
        /// <param name="idRol">Rol elegido, o nulo si no se eligió.</param>
        /// <param name="idOrganizacion">Organización elegida, o nula.</param>
        /// <param name="idEmpresa">Empresa elegida, o nula.</param>
        /// <param name="idSucursal">Sucursal elegida, o nula.</param>
        /// <param name="ct">Token de cancelación de la petición.</param>
        Task<AutenticacionDB?> Autenticar(string usuario, string clave, string codigoSistema,
            int? idRol, int? idOrganizacion, int? idEmpresa, int? idSucursal, CancellationToken ct);

        /// <summary>
        /// Valida credenciales y devuelve los contextos donde el usuario puede
        /// operar. No emite ningún token.
        /// </summary>
        /// <param name="usuario">Nombre de usuario.</param>
        /// <param name="clave">Clave en claro.</param>
        /// <param name="codigoSistema">Código del sistema.</param>
        /// <param name="ct">Token de cancelación de la petición.</param>
        Task<IReadOnlyList<ContextoDB>> ObtenerContextos(string usuario, string clave, string codigoSistema, CancellationToken ct);
    }
}
