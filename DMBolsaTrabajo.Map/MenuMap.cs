using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Menu;

namespace DMBolsaTrabajo.Map
{
    public class MenuMap : Profile
    {
        public MenuMap()
        {

            CreateMap<EMenu, MenuResponseDto>()
                 .ForMember(des => des.Id, opt => opt.MapFrom(src => src.CMENU_ID))
                 .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.CMENU_NOMBRE))
                 .ForMember(des => des.IdOrigen, opt => opt.MapFrom(src => src.NMENU_ID_ORIGEN))
                 .ForMember(des => des.Ordenamiento, opt => opt.MapFrom(src => src.NMENU_ORDENAMIENTO))
                 .ForMember(des => des.UsuarioRegistro, opt => opt.MapFrom(src => src.CAUDI_USR_INS))
                 .ForMember(des => des.FechaRegistro, opt => opt.MapFrom(src => src.DAUDI_REG_INS))
                 .ForMember(des => des.UsuarioModificacion, opt => opt.MapFrom(src => src.CAUDI_USR_UPD))
                 .ForMember(des => des.FechaModificacion, opt => opt.MapFrom(src => src.DAUDI_REG_UPD))
                 .ForMember(des => des.EstadoRegistro, opt => opt.MapFrom(src => src.CAUDI_EST_REG))
                 .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.NMENU_ESTADO))
                 .ForMember(des => des.Ruta, opt => opt.MapFrom(src => src.CMENU_RUTA))
                 .ForMember(des => des.Icono, opt => opt.MapFrom(src => src.CMENU_ICONO));

            CreateMap<ERolMenuPermisos, MenuPermisosDto>()
                 .ForMember(des => des.Id, opt => opt.MapFrom(src => src.CMENU_ID))
                 .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.CMENU_NOMBRE))
                 .ForMember(des => des.IdOrigen, opt => opt.MapFrom(src => src.NMENU_ID_ORIGEN))
                 .ForMember(des => des.Ordenamiento, opt => opt.MapFrom(src => src.NMENU_ORDENAMIENTO))
                 .ForMember(des => des.IdRolMenu, opt => opt.MapFrom(src => src.NROME_ID))
                 .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.NROME_ESTADO))
                 .ForMember(des => des.Ruta, opt => opt.MapFrom(src => src.CMENU_RUTA))
                 .ForMember(des => des.Icono, opt => opt.MapFrom(src => src.CMENU_ICONO));

            CreateMap<FiltroPermisosDto, EFiltroPermisos>()
                .ForMember(des => des.NSUPU_ID, opt => opt.MapFrom(src => src.IdSupuesto))
                .ForMember(des => des.MENU_ID, opt => opt.MapFrom(src => src.IdMenu));
        }
    }
}
