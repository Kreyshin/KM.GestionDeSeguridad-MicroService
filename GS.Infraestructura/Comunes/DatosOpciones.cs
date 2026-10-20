using System.Text.RegularExpressions;

namespace GS.Infraestructura.Comunes
{
    /// <summary>
    /// Opciones de acceso a datos. El esquema es configurable porque puede
    /// cambiar entre entornos sin que cambie el código.
    /// </summary>
    public sealed partial class DatosOpciones
    {
        /// <summary>Esquema de PostgreSQL donde viven los objetos de la solución.</summary>
        public string Esquema { get; }

        /// <summary>
        /// Crea las opciones validando el nombre del esquema. El valor se
        /// concatena en las sentencias SQL, así que solo se admiten
        /// identificadores en minúsculas; cualquier otra cosa se rechaza.
        /// </summary>
        /// <param name="esquema">Nombre del esquema.</param>
        /// <exception cref="ArgumentException">Si el nombre no es un identificador válido.</exception>
        public DatosOpciones(string esquema)
        {
            if (string.IsNullOrWhiteSpace(esquema) || !EsquemaValido().IsMatch(esquema))
                throw new ArgumentException($"El esquema '{esquema}' no es un identificador válido.", nameof(esquema));

            Esquema = esquema;
        }

        [GeneratedRegex("^[a-z_][a-z0-9_]*$")]
        private static partial Regex EsquemaValido();
    }
}
