namespace GS.Dominio.Conexiones
{
    /// <summary>Fila que devuelve nuc_core.fn_abrir_conexion.</summary>
    public sealed class ConexionDB
    {
        /// <summary>Resultado de la operación. CONEXION.OK si se creó.</summary>
        public string oc_codigo { get; set; } = string.Empty;

        /// <summary>Identificador de la sesión creada.</summary>
        public int? oid_conexion { get; set; }

        /// <summary>Momento en que la sesión deja de ser válida.</summary>
        public DateTime? of_vencimiento { get; set; }
    }
}
