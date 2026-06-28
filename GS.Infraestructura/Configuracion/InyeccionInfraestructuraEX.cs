using GS.Aplicacion.Acceso.ObtenerMenu;
using GS.Aplicacion.Comunes.Interfaces;
using GS.Dominio.Interfaces.Commands;
using GS.Dominio.Interfaces.Querys;
using GS.Infraestructura.Acceso.Menu;
using GS.Infraestructura.Persistencia;
using GS.Infraestructura.Repositorios.Commands;
using GS.Infraestructura.Repositorios.Querys;
using GS.Infraestructura.Servicios;
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
            services.AddScoped<IMenuRepositoryQ, MenuRepositoryQ>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUsuariosAuthRepositoryQ, UsuarioRepositoryQ>();
            services.AddScoped<IPermisosRepositoryQ, PermisosRepositoryQ>();
            services.AddScoped<IConexionRepositoryC, ConexionRepositoryC>();

            return services;
        }
    }
}
