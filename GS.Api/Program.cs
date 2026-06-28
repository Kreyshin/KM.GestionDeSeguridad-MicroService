using GS.Api.Configuracion;
using GS.Aplicacion.Comunes.AuditoriaHelper;
using GS.Aplicacion.Configuracion;
using GS.Infraestructura.Configuracion;
using GS.Infraestructura.Persistencia;
using GS.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:4200",
                "https://localhost:9001"  // el gateway también debe poder llamarlo
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuracion Serilog
//var logsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
//SerilogConfigurator.ConfigureGlobalLogger(logsDirectory);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.GrafanaLoki(
    "http://loki:3100",
    labels: new List<LokiLabel>
    {
        new() {Key = "app", Value = "ms-seguridad"},
        new() {Key = "env", Value = builder.Environment.EnvironmentName.ToLower()}
    }
    ).CreateLogger();

builder.Host.UseSerilog(); // Configurar Serilog como proveedor de logging

builder.Services.AddLogging(); // Agregar ILogger al contenedor

builder.Services.AddSingleton<IDbConfiguracion, DbConfiguracion>();
builder.Services.InyeccionInfraestructura();
builder.Services.InyeccionAplicacion();


var app = builder.Build();
app.UseMiddleware<AuditoriaMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Assets")),
        RequestPath = "/swagger-assets"
    });


    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.InjectStylesheet("/swagger-assets/swagger-dark.css"); // Aplica el modo oscuro
    });
}

app.UseCors("CorsPolicy");
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
