namespace GS.Dominio.Comunes
{
    /// <summary>
    /// Catálogo de códigos de respuesta, con el formato AREA.CAUSA.
    /// Un código publicado no se renombra: puede haber clientes reaccionando a él.
    /// </summary>
    public static class Codigos
    {
        /// <summary>Códigos del área de autenticación. Los emite la base.</summary>
        public static class Auth
        {
            /// <summary>Autenticado correctamente.</summary>
            public const string Ok = "AUTH.OK";

            /// <summary>Usuario o clave incorrectos.</summary>
            public const string CredencialInvalida = "AUTH.CREDENCIAL_INVALIDA";

            /// <summary>El usuario existe pero está desactivado.</summary>
            public const string UsuarioInactivo = "AUTH.USUARIO_INACTIVO";

            /// <summary>El código de sistema no existe o está inactivo.</summary>
            public const string SistemaNoDisponible = "AUTH.SISTEMA_NO_DISPONIBLE";

            /// <summary>El usuario no tiene rol activo en ese sistema.</summary>
            public const string SinAccesoAlSistema = "AUTH.SIN_ACCESO_AL_SISTEMA";

            /// <summary>Opera en varios contextos y debe elegir empresa o sucursal.</summary>
            public const string ContextoRequerido = "AUTH.CONTEXTO_REQUERIDO";

            /// <summary>Faltan datos obligatorios en la solicitud.</summary>
            public const string SolicitudIncompleta = "AUTH.SOLICITUD_INCOMPLETA";
        }

        /// <summary>Códigos del área de sesiones.</summary>
        public static class Conexion
        {
            /// <summary>Sesión registrada correctamente.</summary>
            public const string Ok = "CONEXION.OK";

            /// <summary>El resumen del token de renovación no es válido.</summary>
            public const string ResumenInvalido = "CONEXION.RESUMEN_INVALIDO";
        }

        /// <summary>Códigos transversales de servidor.</summary>
        public static class Srv
        {
            /// <summary>Fallo no previsto. El detalle queda en el log.</summary>
            public const string ErrorInterno = "SRV.ERROR_INTERNO";

            /// <summary>La base de datos devolvió algo que no se esperaba.</summary>
            public const string RespuestaInesperada = "SRV.RESPUESTA_INESPERADA";
        }
    }
}
