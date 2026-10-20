namespace GS.Dominio.Permisos
{
    /// <summary>Acceso a datos de permisos.</summary>
    public interface IPermisoRepo
    {
        /// <summary>
        /// Permisos efectivos del usuario en ese rol y contexto: los del rol,
        /// con las excepciones del usuario aplicadas encima.
        /// </summary>
        /// <param name="idUsuario">Usuario.</param>
        /// <param name="idSistema">Sistema por el que entra.</param>
        /// <param name="idRol">Rol con el que entra.</param>
        /// <param name="idOrganizacion">Organización del contexto, o nula.</param>
        /// <param name="idEmpresa">Empresa del contexto, o nula.</param>
        /// <param name="idSucursal">Sucursal del contexto, o nula.</param>
        /// <param name="ct">Token de cancelación de la petición.</param>
        Task<IReadOnlyList<PermisoDB>> ObtenerEfectivos(int idUsuario, int idSistema, int idRol,
            int? idOrganizacion, int? idEmpresa, int? idSucursal, CancellationToken ct);
    }
}
