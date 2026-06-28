using GS.Aplicacion.Acceso.Autenticacion.CasosUso;
using GS.Aplicacion.Acceso.Autenticacion.Interfaces;
using GS.Aplicacion.Acceso.Conexiones.Interfaces;
using GS.Aplicacion.Acceso.Conexiones.Servicios;
using GS.Aplicacion.Acceso.ObtenerMenu;
using GS.Aplicacion.Comunes.AuditoriaHelper;
using GS.Aplicacion.Comunes.Interfaces;
using GS.Aplicacion.Rol.CasosUso;
using GS.Aplicacion.Rol.Interfaces;
using GS.Aplicacion.Rol.Mappers;
using GS.Dominio.Interfaces.Commands;
using GS.Dominio.Interfaces.Querys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Configuracion
{
    public static class  InyeccionAplicacionEX
    {
     public static IServiceCollection InyeccionAplicacion(this IServiceCollection services)
    {
        services.AddScoped<IAuditoriaHelp, AuditoriaHelp>();
        services.AddSingleton<IJwtService, JwtService>();
        services.AddScoped<IRolCrudCU, RolCrudCU>();
        services.AddScoped<IObtenerMenuCU, ObtenerMenuCU>();
            services.AddScoped<IAutenticacionCU, AutenticacionCU>();
            services.AddScoped<IGestionConexionesSV, GestionConexionesSV>();

            services.AddAutoMapper(
                    typeof(RolCrudProfileAM).Assembly,
                    typeof(ObtenerMenuAM).Assembly
                    
                ); 
            return services;
    }
}
}
