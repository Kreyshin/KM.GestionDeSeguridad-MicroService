using GS.Dominio.Interfaces.Commands;
using GS.Dominio.Interfaces.Querys;
using GS.Infraestructura.Persistencia;
using GS.Infraestructura.Repositorios.Commands;
using GS.Infraestructura.Repositorios.Querys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Infraestructura.Configuracion
{
    public static class InyeccionInfraestructuraEX
    {
        public static IServiceCollection InyeccionInfraestructura(this IServiceCollection services)
        {
            services.AddScoped<DbConexion>();
            services.AddScoped<IRolRepositoryC, RolRepositoryC>();
            services.AddScoped<IRolRepositoryQ, RolRepositoryQ>();
            services.AddScoped<IUsuarioRepositoryC, UsuarioRepositoryC>();
            return services;
        }
    }
}
