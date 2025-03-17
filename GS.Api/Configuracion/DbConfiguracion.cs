using GS.Infraestructura.Persistencia;

namespace GS.Api.Configuracion
{
    public class DbConfiguracion(IConfiguration configuration) : IDbConfiguracion
    {
        public string? ConnectionString { get; } = configuration.GetConnectionString("cnSql");
    }
}
