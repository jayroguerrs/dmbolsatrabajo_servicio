using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Rol;

namespace DMBolsaTrabajo.Map
{
    public class EventoMap : Profile
    {
        public EventoMap()
        {
            CreateMap<EEventoCombo, EventoComboResponseDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NEVEN_ID))
                .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.CEVEN_NOMBRE));

            CreateMap<EventoFiltroRequestDto, EEventoFiltro>()
                .ForMember(des => des.NEVEN_ESTADO, opt => opt.MapFrom(src => src.Estado));

        }
    }
}
