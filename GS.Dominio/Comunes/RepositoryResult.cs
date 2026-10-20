namespace GS.Dominio.Comunes
{
    /// <summary>
    /// Base de las respuestas de la solución. Lleva el estado de la operación
    /// para que la capa de API no tenga que interpretar el dato devuelto.
    /// </summary>
    public abstract class RepositoryResult
    {
        /// <summary>Estado HTTP que corresponde a la operación.</summary>
        public int StatusCode { set; get; } = 0;

        /// <summary>Código simbólico con el formato AREA.CAUSA.</summary>
        public string Codigo { set; get; } = "";

        /// <summary>Código de error de la base o del backend. Cero si no hubo error.</summary>
        public int ErrorCode { set; get; } = 0;

        /// <summary>Detalle técnico del error. Va al log, no al consumidor.</summary>
        public string ErrorMessage { set; get; } = "";

        /// <summary>Mensaje de estado de la operación.</summary>
        public string StatusMessage { set; get; } = "Correcto";

        /// <summary>Tipo de estado: ÉXITO, VALIDACIÓN, SQL-ERROR, BACKEND-ERROR.</summary>
        public string StatusType { set; get; } = "";
    }
}
