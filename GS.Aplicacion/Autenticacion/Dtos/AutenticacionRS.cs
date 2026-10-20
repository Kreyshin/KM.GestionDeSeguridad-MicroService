namespace GS.Aplicacion.Autenticacion.Dtos
{
    /// <summary>Respuesta de un inicio de sesión correcto.</summary>
    public sealed record AutenticacionRS
    {
        /// <summary>Token de acceso.</summary>
        public string Token { get; init; } = string.Empty;

        /// <summary>Token de renovación. Se usa para obtener un token de acceso nuevo.</summary>
        public string RefreshToken { get; init; } = string.Empty;

        /// <summary>Momento en que el token deja de ser válido.</summary>
        public DateTime Expiracion { get; init; }

        /// <summary>Datos básicos del usuario autenticado.</summary>
        public UsuarioRS Usuario { get; init; } = new();
    }

    /// <summary>Datos básicos del usuario autenticado.</summary>
    public sealed record UsuarioRS
    {
        /// <summary>Identificador del usuario.</summary>
        public int IdUsuario { get; init; }

        /// <summary>Nombre de usuario.</summary>
        public string Usuario { get; init; } = string.Empty;

        /// <summary>Nombre completo.</summary>
        public string Nombre { get; init; } = string.Empty;

        /// <summary>Rol con el que entró al sistema.</summary>
        public int IdRol { get; init; }

        /// <summary>Sistema al que entró.</summary>
        public int IdSistema { get; init; }
    }
}
