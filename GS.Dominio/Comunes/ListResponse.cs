namespace GS.Dominio.Comunes
{
    /// <summary>Respuesta con una colección de datos.</summary>
    /// <typeparam name="T">Tipo de los elementos devueltos.</typeparam>
    public class ListResponse<T> : RepositoryResult
    {
        /// <summary>Elementos devueltos. Vacía cuando no hay resultados.</summary>
        public IEnumerable<T> Data { set; get; } = new List<T>();
    }
}
