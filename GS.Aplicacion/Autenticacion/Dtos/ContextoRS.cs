namespace GS.Aplicacion.Autenticacion.Dtos
{
    /// <summary>Un contexto en el que el usuario puede operar.</summary>
    public sealed record ContextoRS
    {
        /// <summary>Identificador del rol.</summary>
        public int RolId { get; init; }

        /// <summary>Nombre del rol.</summary>
        public string Rol { get; init; } = string.Empty;

        /// <summary>Identificador de la organización, si la hay.</summary>
        public int? OrganizacionId { get; init; }

        /// <summary>Nombre de la organización, si la hay.</summary>
        public string? Organizacion { get; init; }

        /// <summary>Identificador de la empresa, si la hay.</summary>
        public int? EmpresaId { get; init; }

        /// <summary>Razón social de la empresa, si la hay.</summary>
        public string? Empresa { get; init; }

        /// <summary>Identificador de la sucursal, si la hay.</summary>
        public int? SucursalId { get; init; }

        /// <summary>Nombre de la sucursal, si la hay.</summary>
        public string? Sucursal { get; init; }
    }
}
