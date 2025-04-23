using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Formularios;
using DMBolsaTrabajo.Dto.Preguntas;

namespace DMBolsaTrabajo.Map
{
    public class FormulariosMap : Profile
    {
        public FormulariosMap()
        {
            CreateMap<EFormularioInicial, FormularioInicialDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NFORM_ID))
                .ForMember(des => des.Titulo, opt => opt.MapFrom(src => src.CFORM_TITULO))
                .ForMember(des => des.Imagen, opt => opt.MapFrom(src => src.CFORM_IMAGEN))
                .ForMember(des => des.Subtitulo, opt => opt.MapFrom(src => src.CFORM_SUBTITULO))
                .ForMember(des => des.Encuesta, opt => opt.MapFrom(src => src.CEVEN_NOMBRE))
                .ForMember(des => des.lstPreguntasDto, opt => opt.MapFrom(src => src.lstPreguntas));

            CreateMap<EPreguntas, PreguntasDto>()
                .ForMember(des => des.Index, opt => opt.MapFrom(src => src.NUMERO))
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NPREG_ID))
                .ForMember(des => des.Pregunta, opt => opt.MapFrom(src => src.CPREG_DESCRIPCION))
                .ForMember(des => des.Obligatorio, opt => opt.MapFrom(src => src.NPREG_OBLIGATORIO))
                .ForMember(des => des.EsFecha, opt => opt.MapFrom(src => src.NPREG_ES_FECHA))
                .ForMember(des => des.EsNumero, opt => opt.MapFrom(src => src.NPREG_ES_NUMERO))
                .ForMember(des => des.EsRegExp, opt => opt.MapFrom(src => src.NPREG_ES_REGEXP))
                .ForMember(des => des.EsCorreo, opt => opt.MapFrom(src => src.NPREG_ES_CORREO))
                .ForMember(des => des.CantidadArchivos, opt => opt.MapFrom(src => src.NCANT_ARCHIVOS))
                .ForMember(des => des.Alternativa, opt => opt.MapFrom(src => src.CPREG_ALTERNATIVAS))
                .ForMember(des => des.TipoPregunta, opt => opt.MapFrom(src => src.NCADE_TIPO_PREG))
                .ForMember(des => des.lstAlternativasDto, opt => opt.MapFrom(src => src.lstAlternativas));

            CreateMap<EAlternativas, AlternativasDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NPREG_ID))
                .ForMember(des => des.Index, opt => opt.MapFrom(src => src.INDEX))
                .ForMember(des => des.Descripcion, opt => opt.MapFrom(src => src.DESCRIPCION));

            // Mapeo de EFormularios a FormulariosRequestDto
            CreateMap<EFormulario, FormularioResponseDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NFORM_ID))
                .ForMember(des => des.Titulo, opt => opt.MapFrom(src => src.CFORM_TITULO))
                .ForMember(des => des.Subtitulo, opt => opt.MapFrom(src => src.CFORM_SUBTITULO))
                .ForMember(des => des.Evento, opt => opt.MapFrom(src => src.CEVEN_NOMBRE))
                .ForMember(des => des.EstadoTexto, opt => opt.MapFrom(src => src.ESTADO_TEXTO))
                .ForMember(des => des.FechaModificacion, opt => opt.MapFrom(src => src.FECHA_MODIFICACION))
                .ForMember(des => des.UsuarioModificacion, opt => opt.MapFrom(src => src.USUARIO_RESPONSABLE))
                ;

            // Mapeo de FormulariosRequestDto a EFormularios
            CreateMap<FormularioRequestPorFiltroDto, EFormularioFiltro>()
                .ForMember(des => des.NEVEN_ID, opt => opt.MapFrom(src => src.EventoId))
                .ForMember(des => des.CFORM_TITULO, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(des => des.NFORM_ESTADO, opt => opt.MapFrom(src => src.Estado))
                .ForMember(des => des.DAUDI_USR_INS, opt => opt.MapFrom(src => src.Fecha))
                .ForMember(des => des.PAGE_NUMBER, opt => opt.MapFrom(src => src.NumeroPagina))
                .ForMember(des => des.PAGE_SIZE, opt => opt.MapFrom(src => src.TamanioPagina))
                .ForMember(des => des.P_ORDER_BY, opt => opt.MapFrom(src => src.SortColumn))
                .ForMember(des => des.P_ORDER, opt => opt.MapFrom(src => src.SortOrder));

            CreateMap<RespuestaRequestDto, EFormularioRespuesta>()
                .ForMember(des => des.NPREG_ID, opt => opt.MapFrom(src => src.PreguntaId))
                .ForMember(des => des.CPREG_RESPUESTA, opt => opt.MapFrom(src => src.Respuesta));

            CreateMap<ListaRespuestaRequestDto, EListaFormularioRespuesta>()
                .ForMember(des => des.CFORM_LINK, opt => opt.MapFrom(src => src.FormularioLink))
                .ForMember(des => des.lstFormularioRespuesta, opt => opt.MapFrom(src => src.ListaRespuesta));

        }
    }
}


