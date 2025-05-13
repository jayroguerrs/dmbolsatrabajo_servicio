using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Ubicacion;

namespace DMBolsaTrabajo.Map
{
    public class UbicacionMap : Profile
    {
        public UbicacionMap()
        {
            CreateMap<EDepartamentoCombo, DepartamentoResponseDto>()
                .ForMember(des => des.DepaId, opt => opt.MapFrom(src => src.NDEPA_ID))
                .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.CDEPA_NOMBRE));

            CreateMap<DepartamentoFiltroRequestDto, EDepartamentoFiltro>()
                .ForMember(des => des.NDEPA_ESTADO, opt => opt.MapFrom(src => src.Estado));

            CreateMap<EDistritoCombo, DistritoResponseDto>()
                .ForMember(des => des.DistritoId, opt => opt.MapFrom(src => src.NDIST_ID))
                .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.CDIST_NOMBRE));

            CreateMap<DistritoFiltroRequestDto, EDistritoFiltro>()
                .ForMember(des => des.NDEPA_ID, opt => opt.MapFrom(src => src.DepaId))
                .ForMember(des => des.NDIST_ESTADO, opt => opt.MapFrom(src => src.Estado));
        }
    }
}
