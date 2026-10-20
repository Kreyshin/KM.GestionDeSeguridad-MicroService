using GS.Aplicacion.Autenticacion;
using GS.Aplicacion.Comunes;
using GS.Dominio.Autenticacion;
using GS.Dominio.Conexiones;
using GS.Infraestructura.Autenticacion;
using GS.Dominio.Permisos;
using GS.Infraestructura.Conexiones;
using GS.Infraestructura.Permisos;
using GS.Infraestructura.Comunes;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Configuración. Es el único proyecto que lee appsettings y variables de entorno.
var cadenaConexion = builder.Configuration.GetConnectionString("PostgreSql");
if (string.IsNullOrWhiteSpace(cadenaConexion))
    throw new InvalidOperationException("Falta ConnectionStrings:PostgreSql.");

var jwtClave = builder.Configuration["Jwt:Clave"];
var jwtEmisor = builder.Configuration["Jwt:Emisor"];
var jwtAudiencias = builder.Configuration.GetSection("Jwt:Audiencias").Get<string[]>() ?? [];
var jwtMinutos = builder.Configuration.GetValue("Jwt:Minutos", 15);

if (string.IsNullOrWhiteSpace(jwtClave) || System.Text.Encoding.UTF8.GetByteCount(jwtClave) < 32)
    throw new InvalidOperationException("Falta Jwt:Clave, o tiene menos de 32 bytes.");
if (string.IsNullOrWhiteSpace(jwtEmisor) || jwtAudiencias.Length == 0)
    throw new InvalidOperationException("Falta Jwt:Emisor o Jwt:Audiencias.");

var esquema = builder.Configuration["DataAccess:Esquema"] ?? "nuc_core";

builder.Services.AddSingleton(_ => NpgsqlDataSource.Create(cadenaConexion));
builder.Services.AddSingleton(new DatosOpciones(esquema));
builder.Services.AddSingleton(new TokenOpciones(jwtClave, jwtEmisor, jwtAudiencias, jwtMinutos));
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<IAutenticacionRepo, AutenticacionRepo>();
builder.Services.AddScoped<IConexionRepo, ConexionRepo>();
builder.Services.AddScoped<IPermisoRepo, PermisoRepo>();
builder.Services.AddScoped<IAutenticacionCU, AutenticacionCU>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
