namespace GS.Dominio.Permisos
{
    /// <summary>Fila que devuelve nuc_core.fn_obtener_permisos.</summary>
    public sealed class PermisoDB
    {
        /// <summary>Identificador del permiso.</summary>
        public int id_permiso { get; set; }

        /// <summary>Código del permiso.</summary>
        public string c_codigo { get; set; } = string.Empty;

        /// <summary>Tipo de permiso.</summary>
        public string c_tipo { get; set; } = string.Empty;

        /// <summary>Si está concedido. Una denegación del usuario lo pone en falso.</summary>
        public bool b_permite { get; set; }
    }
}
