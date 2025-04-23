using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Puestos;
using DMBolsaTrabajo.Dto.Usuario;

namespace DMBolsaTrabajo.Map
{
    public class PuestosMap : Profile
    {
        public PuestosMap()
        {
            CreateMap<PuestosFilterRequestDto, EPuestosFiltro>()
                .ForMember(des => des.DAUDI_USR_INS, opt => opt.MapFrom(src => src.FechaRegistro))
                .ForMember(des => des.CPUEST_TITULO, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(des => des.UBICACION, opt => opt.MapFrom(src => src.Ubicacion))
                .ForMember(des => des.NPUEST_ESTADO, opt => opt.MapFrom(src => src.Estado))
                .ForMember(des => des.PAGE_NUMBER, opt => opt.MapFrom(src => src.NumeroPagina))
                .ForMember(des => des.PAGE_SIZE, opt => opt.MapFrom(src => src.TamanioPagina))
                .ForMember(des => des.P_ORDER_BY, opt => opt.MapFrom(src => src.SortColumn))
                .ForMember(des => des.P_ORDER, opt => opt.MapFrom(src => src.SortOrder));

            CreateMap<PuestosFilterNoCaptchaRequestDto, EPuestosFiltro>()
                .ForMember(des => des.DAUDI_USR_INS, opt => opt.MapFrom(src => src.FechaRegistro))
                .ForMember(des => des.CPUEST_TITULO, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(des => des.UBICACION, opt => opt.MapFrom(src => src.Ubicacion))
                .ForMember(des => des.NPUEST_ESTADO, opt => opt.MapFrom(src => src.Estado))
                .ForMember(des => des.PAGE_NUMBER, opt => opt.MapFrom(src => src.NumeroPagina))
                .ForMember(des => des.PAGE_SIZE, opt => opt.MapFrom(src => src.TamanioPagina))
                .ForMember(des => des.P_ORDER_BY, opt => opt.MapFrom(src => src.SortColumn))
                .ForMember(des => des.P_ORDER, opt => opt.MapFrom(src => src.SortOrder));

            CreateMap<EPuestosLista, PuestosResponseDto>()
                .ForMember(des => des.Numero, opt => opt.MapFrom(src => src.NUMERO))
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NPUEST_ID))
                .ForMember(des => des.Titulo, opt => opt.MapFrom(src => src.CPUEST_TITULO))
                .ForMember(des => des.Ubicacion, opt => opt.MapFrom(src => src.UBICACION))
                .ForMember(des => des.Distrito, opt => opt.MapFrom(src => src.CDIST_NOMBRE))
                .ForMember(des => des.Departamento, opt => opt.MapFrom(src => src.CDEPA_NOMBRE))
                .ForMember(des => des.Imagen, opt => opt.MapFrom(src => src.CPUEST_IMAGEN))
                .ForMember(des => des.FechaFin, opt => opt.MapFrom(src => src.DPUEST_FECHA_FIN))
                .ForMember(des => des.Descripcion, opt => opt.MapFrom(src => src.CPUEST_DESCRIPCION))
                .ForMember(des => des.EstadoTexto, opt => opt.MapFrom(src => src.ESTADO_TEXTO))
                .ForMember(des => des.FechaModificacion, opt => opt.MapFrom(src => src.FECHA_MODIFICACION))
                .ForMember(des => des.UsuarioResponsable, opt => opt.MapFrom(src => src.USUARIO_RESPONSABLE));

            CreateMap<PostularInsUpdDto, EPostularInsUpd>()
               .ForMember(des => des.NPOST_ID, opt => opt.MapFrom(src => src.PostulacionId))
               .ForMember(des => des.NUSUA_ID, opt => opt.MapFrom(src => src.UsuarioId))
               .ForMember(des => des.NCADE_TIPO_DOCUMENTO, opt => opt.MapFrom(src => src.TipoDocumento))
               .ForMember(des => des.CPOST_NUMDOC, opt => opt.MapFrom(src => src.NumeroDocumento))
               .ForMember(des => des.CPOST_NOMBRES, opt => opt.MapFrom(src => src.Nombres))
               .ForMember(des => des.CPOST_PATERNO, opt => opt.MapFrom(src => src.ApellidoPaterno))
               .ForMember(des => des.CPOST_MATERNO, opt => opt.MapFrom(src => src.ApellidoMaterno))
               .ForMember(des => des.CPOST_CELULAR, opt => opt.MapFrom(src => src.Celular))
               .ForMember(des => des.CPOST_CORREO, opt => opt.MapFrom(src => src.Correo))
               .ForMember(des => des.NPUEST_ID, opt => opt.MapFrom(src => src.PuestoId))
               .ForMember(des => des.CPOST_ARCHIVO, opt => opt.MapFrom(src => src.NombreArchivo));
        }
    }
}
