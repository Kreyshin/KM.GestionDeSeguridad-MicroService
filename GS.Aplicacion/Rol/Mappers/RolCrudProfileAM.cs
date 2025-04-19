using AutoMapper;
using GS.Aplicacion.Rol.Dtos.Request;
using GS.Aplicacion.Rol.Dtos.Response;
using GS.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Rol.Mappers
{
    public class RolCrudProfileAM : Profile
    {
        public RolCrudProfileAM() {
            CreateMap<RolActualizarRQ , RolEN>()
                .ForMember(dest => dest.C_Nombre, opt => opt.MapFrom(src => src.nombre))
                .ForMember(dest => dest.B_Activo, opt => opt.MapFrom(src => src.activo));

            CreateMap<RolCrearRQ, RolEN>()
             .ForMember(dest => dest.C_Nombre, opt => opt.MapFrom(src => src.nombre));

            CreateMap<RolEN, RolCrearRE>()
             .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
             .ForMember(dest => dest.nombre, opt => opt.MapFrom(src => src.C_Nombre))
             .ForMember(dest => dest.activo, opt => opt.MapFrom(src => src.B_Activo))
             .ForMember(dest => dest.estado, opt => opt.MapFrom(src => src.C_Estado));


            CreateMap<RolEN , RolActualizarRE>()
              .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
              .ForMember(dest => dest.nombre, opt => opt.MapFrom(src => src.C_Nombre))
              .ForMember(dest => dest.activo, opt => opt.MapFrom(src => src.B_Activo))
              .ForMember(dest => dest.estado, opt => opt.MapFrom(src => src.C_Estado));



            CreateMap<RolConsultarRQ, RolEN>()
               .ForMember(dest => dest.C_Nombre, opt => opt.MapFrom(src => src.nombre))
               .ForMember(dest => dest.C_Estado, opt => opt.MapFrom(src => src.estado));

            CreateMap<RolEN, RolBuscarPorIDRE>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.nombre, opt => opt.MapFrom(src => src.C_Nombre))
                .ForMember(dest => dest.activo, opt => opt.MapFrom(src => src.B_Activo))
                .ForMember(dest => dest.estado, opt => opt.MapFrom(src => src.C_Estado))
                .ReverseMap();

            CreateMap<RolEN, RolConsultarRE>()
               .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
               .ForMember(dest => dest.nombre, opt => opt.MapFrom(src => src.C_Nombre))
               .ForMember(dest => dest.activo, opt => opt.MapFrom(src => src.B_Activo))
               .ForMember(dest => dest.estado, opt => opt.MapFrom(src => src.C_Estado))
               ;


        }

    }
}
