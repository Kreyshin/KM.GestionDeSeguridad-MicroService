namespace GS.Dominio.Autenticacion
{
    /// <summary>
    /// Fila que devuelve nuc_core.fn_autenticar_usuario.
    /// Conserva la nomenclatura de la base; no se expone a la API.
    /// </summary>
    public sealed class AutenticacionDB
    {
        /// <summary>Resultado de la autenticación. AUTH.OK si entró.</summary>
        public string oc_codigo { get; set; } = string.Empty;

        /// <summary>Identificador del usuario. Nulo si no autenticó.</summary>
        public int? oid_usuario { get; set; }

        /// <summary>Nombre de usuario. Nulo si no autenticó.</summary>
        public string? oc_usuario { get; set; }

        /// <summary>Nombre completo. Nulo si no autenticó.</summary>
        public string? oc_nombre { get; set; }

        /// <summary>Rol con el que entra al sistema.</summary>
        public int? oid_rol { get; set; }

        /// <summary>Sistema al que entra.</summary>
        public int? oid_sistema { get; set; }

        /// <summary>Organización del contexto. Nula si la asignación no la tiene.</summary>
        public int? oid_organizacion { get; set; }

        /// <summary>Empresa del contexto. Nula si la asignación no la tiene.</summary>
        public int? oid_empresa { get; set; }

        /// <summary>Sucursal del contexto. Nula si la asignación no la tiene.</summary>
        public int? oid_sucursal { get; set; }
    }

    /// <summary>Fila que devuelve nuc_core.fn_obtener_contextos.</summary>
    public sealed class ContextoDB
    {
        /// <summary>Resultado de la consulta. AUTH.OK si la fila es un contexto.</summary>
        public string oc_codigo { get; set; } = string.Empty;

        /// <summary>Rol de este contexto.</summary>
        public int? oid_rol { get; set; }

        /// <summary>Nombre del rol.</summary>
        public string? oc_rol { get; set; }

        /// <summary>Organización, si la asignación la tiene.</summary>
        public int? oid_organizacion { get; set; }

        /// <summary>Nombre de la organización.</summary>
        public string? oc_organizacion { get; set; }

        /// <summary>Empresa, si la asignación la tiene.</summary>
        public int? oid_empresa { get; set; }

        /// <summary>Razón social de la empresa.</summary>
        public string? oc_empresa { get; set; }

        /// <summary>Sucursal, si la asignación la tiene.</summary>
        public int? oid_sucursal { get; set; }

        /// <summary>Nombre de la sucursal.</summary>
        public string? oc_sucursal { get; set; }
    }
}
