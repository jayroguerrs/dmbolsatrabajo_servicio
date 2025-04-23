using AutoMapper;
using DMBolsaTrabajo.Dto.Seguridad;
using DMBolsaTrabajo.Dominio;

namespace DMBolsaTrabajo.Map
{
    public class SeguridadMap : Profile
    {
        public SeguridadMap()
        {
            CreateMap<SeguridadRequestDto, EUsuarioLoginFiltro>()
                .ForMember(des => des.CUSUA_USERNAME, opt => opt.MapFrom(src => src.Usuario))
                .ForMember(des => des.CUSUA_PASSWORD, opt => opt.MapFrom(src => src.Password));

            CreateMap<EUsuarioLogin, SeguridadResponseDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NUSUA_ID))
                .ForMember(des => des.Documento, opt => opt.MapFrom(src => src.CPERS_NRO_DOCUMENTO))
                .ForMember(des => des.ApellidoPaterno, opt => opt.MapFrom(src => src.CPERS_APE_PATERNO))
                .ForMember(des => des.ApellidoMaterno, opt => opt.MapFrom(src => src.CPERS_APE_MATERNO))
                .ForMember(des => des.Nombres, opt => opt.MapFrom(src => src.CPERS_NOMBRES))
                .ForMember(des => des.IdRol, opt => opt.MapFrom(src => src.NROLE_ID))
                .ForMember(des => des.Email, opt => opt.MapFrom(src => src.CPERS_CORREO));

            CreateMap<ClaveRequestDto, EClave>()
                .ForMember(des => des.NUSUA_ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.CUSUA_PASSWORD1, opt => opt.MapFrom(src => src.Password1))
                .ForMember(des => des.CUSUA_PASSWORD2, opt => opt.MapFrom(src => src.Password2));

        }
    }
}
