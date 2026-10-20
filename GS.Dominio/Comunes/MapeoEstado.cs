namespace GS.Dominio.Comunes
{
    /// <summary>
    /// Traducción de un código AREA.CAUSA al estado HTTP que le corresponde.
    /// Es el único sitio donde vive esa decisión: si un código nuevo no se
    /// registra aquí, se responde 500 en vez de adivinar.
    /// </summary>
    public static class MapeoEstado
    {
        private static readonly Dictionary<string, int> Estados = new(StringComparer.Ordinal)
        {
            [Codigos.Auth.Ok] = 200,
            [Codigos.Auth.SolicitudIncompleta] = 400,
            [Codigos.Auth.CredencialInvalida] = 401,
            [Codigos.Auth.UsuarioInactivo] = 401,
            [Codigos.Auth.SistemaNoDisponible] = 401,
            [Codigos.Auth.SinAccesoAlSistema] = 401,
            [Codigos.Auth.ContextoRequerido] = 409,
            [Codigos.Conexion.Ok] = 200,
            [Codigos.Conexion.ResumenInvalido] = 500,
            [Codigos.Srv.ErrorInterno] = 500,
            [Codigos.Srv.RespuestaInesperada] = 500
        };

        /// <summary>
        /// Devuelve el estado HTTP del código. Si el código no está registrado
        /// devuelve 500, porque responder otra cosa sería inventar.
        /// </summary>
        /// <param name="codigo">Código con formato AREA.CAUSA.</param>
        public static int EstadoDe(string codigo) =>
            Estados.TryGetValue(codigo, out var estado) ? estado : 500;

        /// <summary>Indica si el código está registrado en el catálogo.</summary>
        /// <param name="codigo">Código con formato AREA.CAUSA.</param>
        public static bool EsConocido(string codigo) => Estados.ContainsKey(codigo);
    }
}
