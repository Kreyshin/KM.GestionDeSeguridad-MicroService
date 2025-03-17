using Serilog;

namespace GS.Logging
{
    public static class SerilogConfigurator
    {
        public static void ConfigureGlobalLogger(string logsDirectory)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                //.WriteTo.Console() // Logs en consola
                .WriteTo.File(
                    $"{logsDirectory}/general-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7 // Mantener logs por 7 días
                )
                .WriteTo.File(
                    $"{logsDirectory}/errors-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error // Solo errores
                )
                .CreateLogger();
        }
    }
}
