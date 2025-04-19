using GS.Api.Configuracion;
using GS.Aplicacion.Comunes.AuditoriaHelper;
using GS.Aplicacion.Configuracion;
using GS.Infraestructura.Configuracion;
using GS.Infraestructura.Persistencia;
using GS.Logging;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuracion Serilog
var logsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
SerilogConfigurator.ConfigureGlobalLogger(logsDirectory);
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
