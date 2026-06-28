using AutoMapper;
using GS.Aplicacion.Acceso.ObtenerMenu.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Acceso.ObtenerMenu
{
    public class ObtenerMenuAM : Profile
    {
        public ObtenerMenuAM() {
            CreateMap<ObtenerMenuRQ, ObtenerMenuQuery>()
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.IdUsuario))
                .ForMember(dest => dest.IdSistema, opt => opt.MapFrom(src => src.IdSistema));

            // Mapeo de MenuSpResultDto a ObtenerMenuRE
            CreateMap<MenuSpResultDto, ObtenerMenuRE>()
                .ForMember(dest => dest.Modulos, opt => opt.MapFrom(src => src.Modulos))
                .ForMember(dest => dest.Secciones, opt => opt.MapFrom(src => src.Secciones))
                .ForMember(dest => dest.Opciones, opt => opt.MapFrom(src => src.Opciones));

            // Mapeo de MenuModuloQDto a MenuModuloDto
            CreateMap<MenuModuloQDto, MenuModuloDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdModulo, opt => opt.MapFrom(src => src.ID_Modulo))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.C_Nombre))
                .ForMember(dest => dest.RutaIcono, opt => opt.MapFrom(src => src.C_RutaIcono));

            // Mapeo de MenuSeccionQDto a MenuSeccionDto
            CreateMap<MenuSeccionQDto, MenuSeccionDto>()
                .ForMember(dest => dest.IdModulo, opt => opt.MapFrom(src => src.Id_Modulo))
                .ForMember(dest => dest.IdSeccion, opt => opt.MapFrom(src => src.Id_Seccion))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.C_Nombre));

            // Mapeo de MenuOpcionQDto a MenuOpcionDto
            CreateMap<MenuOpcionQDto, MenuOpcionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdModulo, opt => opt.MapFrom(src => src.Id_Modulo))
                .ForMember(dest => dest.IdSeccion, opt => opt.MapFrom(src => src.Id_Seccion))
                .ForMember(dest => dest.IdOpcion, opt => opt.MapFrom(src => src.Id_Opcion))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.C_Nombre))
                .ForMember(dest => dest.RutaIcono, opt => opt.MapFrom(src => src.C_RutaIcono))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.C_Url));


        }
    }
}
