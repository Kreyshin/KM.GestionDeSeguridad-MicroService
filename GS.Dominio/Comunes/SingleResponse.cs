namespace GS.Dominio.Comunes
{
    /// <summary>Respuesta con un único dato.</summary>
    /// <typeparam name="T">Tipo del dato devuelto.</typeparam>
    public class SingleResponse<T> : RepositoryResult
    {
        /// <summary>Dato devuelto. Nulo cuando la operación no fue correcta.</summary>
        public T? Data { set; get; }
    }
}
