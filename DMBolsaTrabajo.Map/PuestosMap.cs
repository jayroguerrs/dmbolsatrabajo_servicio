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

            CreateMap<PostulantesFilterRequestDto, EPostulantesFiltro>()
                .ForMember(des => des.CPOST_NUMDOC, opt => opt.MapFrom(src => src.NumeroDocumento))
                .ForMember(des => des.NOMBRES, opt => opt.MapFrom(src => src.Nombres))
                .ForMember(des => des.NPUEST_ID, opt => opt.MapFrom(src => src.PuestoId))
                .ForMember(des => des.NUARO_ESTADO, opt => opt.MapFrom(src => src.Estado))
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
                .ForMember(des => des.FechaIni, opt => opt.MapFrom(src => src.DPUEST_FECHA_INI))
                .ForMember(des => des.FechaFin, opt => opt.MapFrom(src => src.DPUEST_FECHA_FIN))
                .ForMember(des => des.Descripcion, opt => opt.MapFrom(src => src.CPUEST_DESCRIPCION))
                .ForMember(des => des.EstadoTexto, opt => opt.MapFrom(src => src.ESTADO_TEXTO))
                .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.NPUEST_ESTADO))
                .ForMember(des => des.FechaModificacion, opt => opt.MapFrom(src => src.FECHA_MODIFICACION))
                .ForMember(des => des.UsuarioResponsable, opt => opt.MapFrom(src => src.USUARIO_RESPONSABLE));

            CreateMap<EPostulantesLista, PostulantesResponseDto>()
                .ForMember(des => des.Numero, opt => opt.MapFrom(src => src.NUMERO))
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NPOST_ID))
                .ForMember(des => des.Nombres, opt => opt.MapFrom(src => src.NOMBRES))
                .ForMember(des => des.Correo, opt => opt.MapFrom(src => src.CPOST_CORREO))
                .ForMember(des => des.Archivo, opt => opt.MapFrom(src => src.CPOST_ARCHIVO))
                .ForMember(des => des.EstadoTexto, opt => opt.MapFrom(src => src.ESTADO_TEXTO))
                .ForMember(des => des.FechaModificacion, opt => opt.MapFrom(src => src.FECHA_MODIFICACION))
                .ForMember(des => des.UsuarioResponsable, opt => opt.MapFrom(src => src.USUARIO_RESPONSABLE));

            CreateMap<EPuestosResponseId, PuestosResponsePorIdDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NPUEST_ID))
                .ForMember(des => des.Titulo, opt => opt.MapFrom(src => src.CPUEST_TITULO))
                .ForMember(des => des.Descripcion, opt => opt.MapFrom(src => src.CPUEST_DESCRIPCION))
                .ForMember(des => des.DistritoId, opt => opt.MapFrom(src => src.NDIST_ID))
                .ForMember(des => des.DepartamentoId, opt => opt.MapFrom(src => src.NDEPA_ID))
                .ForMember(des => des.Imagen, opt => opt.MapFrom(src => src.CPUEST_IMAGEN))
                .ForMember(des => des.FechaIni, opt => opt.MapFrom(src => src.DPUEST_FECHA_INI))
                .ForMember(des => des.FechaFin, opt => opt.MapFrom(src => src.DPUEST_FECHA_FIN))
                .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.NPUEST_ESTADO));

            CreateMap<PuestosDelDto, EPuestosDel>()
                .ForMember(des => des.NPUEST_ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.NAUDI_USR_UPD, opt => opt.MapFrom(src => src.Usuario));

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

            CreateMap<PuestosInsUpdDto, EPuestosInsUpd >()
                .ForMember(des => des.NPUEST_ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.CPUEST_TITULO, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(des => des.CPUEST_DESCRIPCION, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(des => des.NDIST_ID, opt => opt.MapFrom(src => src.DistritoId))
                .ForMember(des => des.DPUEST_FECHA_INI, opt => opt.MapFrom(src => src.FechaInicial))
                .ForMember(des => des.DPUEST_FECHA_FIN, opt => opt.MapFrom(src => src.FechaFinal))
                .ForMember(des => des.NPUEST_ESTADO, opt => opt.MapFrom(src => src.Estado))
                .ForMember(des => des.NAUDI_USR_INS, opt => opt.MapFrom(src => src.Usuario));
            
            CreateMap<PuestosEstadoDto, EEstadoCambio>()
               .ForMember(dest => dest.NPUEST_ID, opt => opt.MapFrom(src => src.PuestoId))
               .ForMember(dest => dest.NAUDI_USR_UPD, opt => opt.MapFrom(src => src.Usuario));
        }
    }
}
