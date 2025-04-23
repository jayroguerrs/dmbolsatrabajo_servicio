using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.CatalogoDetalle;

namespace DMBolsaTrabajo.Map
{
    public class CatalogoDetalleMap : Profile
    {
        public CatalogoDetalleMap()
        {
            CreateMap<ECatalogoDetalle, CatalogoDetalleResponseDto>()
                .ForMember(des => des.Index, opt => opt.MapFrom(src => src.NUMERO))
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NCADE_ID))
                .ForMember(des => des.Catalogo, opt => opt.MapFrom(src => src.CCATA_NOMBRE))
                .ForMember(des => des.IdCatalogo, opt => opt.MapFrom(src => src.NCATA_ID))
                .ForMember(des => des.Codigo, opt => opt.MapFrom(src => src.CCADE_CODIGO))
                .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.CCADE_NOMBRE))
                .ForMember(des => des.Abreviatura, opt => opt.MapFrom(src => src.CCADE_ABREVIATURA))
                .ForMember(des => des.Ordenamiento, opt => opt.MapFrom(src => src.NCADE_ORDENAMIENTO))
                .ForMember(des => des.UsuarioModificacion, opt => opt.MapFrom(src => src.USUARIO_RESPONSABLE))
                .ForMember(des => des.FechaModificacion, opt => opt.MapFrom(src => src.FECHA_MODIFICACION))
                .ForMember(des => des.EstadoTexto, opt => opt.MapFrom(src => src.ESTADO_TEXTO))
                .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.NCADE_ESTADO));

            CreateMap<ECatalogoDetalleResponsePorId, CatalogoDetalleResponsePorIdDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NCADE_ID))
                .ForMember(des => des.Codigo, opt => opt.MapFrom(src => src.CCADE_CODIGO))
                .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.CCADE_NOMBRE))
                .ForMember(des => des.IdCatalogo, opt => opt.MapFrom(src => src.NCATA_ID))
                .ForMember(des => des.Abreviatura, opt => opt.MapFrom(src => src.CCADE_ABREVIATURA))
                .ForMember(des => des.Descripcion, opt => opt.MapFrom(src => src.CCADE_DESCRIPCION))
                .ForMember(des => des.IdOrigen, opt => opt.MapFrom(src => src.NCADE_ID_ORIGEN))
                .ForMember(des => des.Ordenamiento, opt => opt.MapFrom(src => src.NCADE_ORDENAMIENTO))
                .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.NCADE_ESTADO))
                .ForMember(des => des.Usuario, opt => opt.MapFrom(src => src.NAUDI_USR_INS));

            CreateMap<CatalogoDetalleInsUpdDto, ECatalogoDetalleResponsePorId>()
                .ForMember(des => des.NCADE_ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.NCATA_ID, opt => opt.MapFrom(src => src.IdCatalogo))
                .ForMember(des => des.CCADE_CODIGO, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(des => des.CCADE_NOMBRE, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(des => des.CCADE_ABREVIATURA, opt => opt.MapFrom(src => src.Abreviatura))
                .ForMember(des => des.CCADE_DESCRIPCION, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(des => des.NCADE_ID_ORIGEN, opt => opt.MapFrom(src => src.IdOrigen))
                .ForMember(des => des.NCADE_ORDENAMIENTO, opt => opt.MapFrom(src => src.Ordenamiento))
                .ForMember(des => des.NCADE_ESTADO, opt => opt.MapFrom(src => src.Estado))
                .ForMember(des => des.NAUDI_USR_INS, opt => opt.MapFrom(src => src.Usuario));



            CreateMap<CatalogoDetalleRequestPorFiltroDto, ECatalogoDetalleFiltro>()
                .ForMember(des => des.CCADE_NOMBRE, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(des => des.NCATA_ID, opt => opt.MapFrom(src => src.IdCatalogo))
                .ForMember(des => des.NCADE_ESTADO, opt => opt.MapFrom(src => src.Estado))
                .ForMember(des => des.PAGE_NUMBER, opt => opt.MapFrom(src => src.NumeroPagina))
                .ForMember(des => des.PAGE_SIZE, opt => opt.MapFrom(src => src.TamanioPagina))
                .ForMember(des => des.P_ORDER_BY, opt => opt.MapFrom(src => src.SortColumn))
                .ForMember(des => des.P_ORDER, opt => opt.MapFrom(src => src.SortOrder));

            CreateMap<ECatalogoResponse, CatalogoResponseDto>()
                .ForMember(des => des.IdCatalogo, opt => opt.MapFrom(src => src.NCATA_ID))
                .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.CCATA_NOMBRE));

        }
    }

}
