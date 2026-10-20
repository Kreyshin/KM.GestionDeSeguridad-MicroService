namespace GS.Dominio.Comunes
{
    /// <summary>Valores admitidos en <see cref="RepositoryResult.StatusType"/>.</summary>
    public static class TiposEstado
    {
        /// <summary>La operación terminó correctamente.</summary>
        public const string Exito = "ÉXITO";

        /// <summary>La operación no procedió por una regla de negocio.</summary>
        public const string Validacion = "VALIDACIÓN";

        /// <summary>Error devuelto por la base de datos.</summary>
        public const string ErrorSql = "SQL-ERROR";

        /// <summary>Error no previsto en el backend.</summary>
        public const string ErrorBackend = "BACKEND-ERROR";
    }
}
