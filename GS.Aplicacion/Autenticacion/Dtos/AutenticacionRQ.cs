namespace GS.Aplicacion.Autenticacion.Dtos
{
    /// <summary>Credenciales y contexto que envía el consumidor para iniciar sesión.</summary>
    public sealed record AutenticacionRQ
    {
        /// <summary>Nombre de usuario.</summary>
        public string Usuario { get; init; } = string.Empty;

        /// <summary>Clave en claro.</summary>
        public string Clave { get; init; } = string.Empty;

        /// <summary>Código del sistema al que desea entrar.</summary>
        public string CodSistema { get; init; } = string.Empty;

        /// <summary>Rol elegido. Se omite cuando el usuario opera en un solo contexto.</summary>
        public int? RolId { get; init; }

        /// <summary>Organización elegida.</summary>
        public int? OrganizacionId { get; init; }

        /// <summary>Empresa elegida.</summary>
        public int? EmpresaId { get; init; }

        /// <summary>Sucursal elegida.</summary>
        public int? SucursalId { get; init; }
    }
}
