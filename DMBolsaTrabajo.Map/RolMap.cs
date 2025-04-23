using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Rol;

namespace DMBolsaTrabajo.Map
{
    public class RolMap : Profile
    {
        public RolMap()
        {
            CreateMap<ERolCombo, RolComboResponseDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NROLE_ID))
                .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.CROLE_NOMBRE));

            CreateMap<RolFiltroRequestDto, ERolFiltro>()
                .ForMember(des => des.NTIRO_ID, opt => opt.MapFrom(src => src.IdTipoRol));

        }
    }
}
