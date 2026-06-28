using AutoMapper;
using GS.Aplicacion.Acceso.Autenticacion.Dtos.Response;
using GS.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.Autenticacion.Mappers
{
    public class AuthProfileAMQ : Profile
    {
        public AuthProfileAMQ() {

            CreateMap<UsuariosEN, UsuarioInfoRS>()
                 .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.ID_Usuario))
                 .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.C_Usuario))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.C_Nombre));
        }
    }
}
