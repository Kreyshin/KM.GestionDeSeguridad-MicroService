using GS.Aplicacion.Comunes.AuditoriaHelper;
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

            services.AddAutoMapper(
                    typeof(RolCrudProfileAM).Assembly
                );
            return services;
    }
}
}
