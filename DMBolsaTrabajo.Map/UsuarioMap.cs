using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Usuario;
using DMBolsaTrabajo.ServiciosExt.EnviarCorreo;

namespace DMBolsaTrabajo.Map
{
    public class UsuarioMap : Profile
    {
        public UsuarioMap()
        {

            CreateMap<EUsuarioInicial, UsuarioInicialDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NUSUA_ID))
                .ForMember(des => des.IdRol, opt => opt.MapFrom(src => src.NROLE_ID))
                .ForMember(des => des.CodigoRol, opt => opt.MapFrom(src => src.CROLE_CODIGO))
                .ForMember(des => des.lstUsuarioMenuDto, opt => opt.MapFrom(src => src.lstUsuarioMenu))
                .ForMember(des => des.lstUsuarioRolInicialDto, opt => opt.MapFrom(src => src.lstUsuarioRolInicial));

            CreateMap<EUsuario, UsuarioResponseDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NUSUA_ID))
                .ForMember(des => des.Codigo, opt => opt.MapFrom(src => src.CUSUA_USERNAME))
                .ForMember(des => des.TipoDocumento, opt => opt.MapFrom(src => src.NCADE_ID_TIPO_DOCUMENTO))
                .ForMember(des => des.FechaNacimiento, opt => opt.MapFrom(src => src.DPERS_FEC_NACIMIENTO))
                .ForMember(des => des.NumeroDocumento, opt => opt.MapFrom(src => src.CPERS_NRO_DOCUMENTO))
                .ForMember(des => des.ApellidoPaterno, opt => opt.MapFrom(src => src.CPERS_APE_PATERNO))
                .ForMember(des => des.ApellidoMaterno, opt => opt.MapFrom(src => src.CPERS_APE_MATERNO))
                .ForMember(des => des.Nombres, opt => opt.MapFrom(src => src.CPERS_NOMBRES))
                .ForMember(des => des.Email, opt => opt.MapFrom(src => src.CPERS_CORREO))
                .ForMember(des => des.Telefono, opt => opt.MapFrom(src => src.CPERS_TELEFONO))
                .ForMember(des => des.Sexo, opt => opt.MapFrom(src => src.NCADE_ID_SEXO))
                .ForMember(des => des.Bio, opt => opt.MapFrom(src => src.CPERS_BIO))
                .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.NUSUA_ESTADO));

            CreateMap<UsuarioRequestDto, EUsuarioAct>()
                .ForMember(des => des.NUSUA_ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.CUSUA_USERNAME, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(des => des.NCADE_ID_TIPO_DOCUMENTO, opt => opt.MapFrom(src => src.TipoDocumento))
                .ForMember(des => des.NCADE_ID_SEXO, opt => opt.MapFrom(src => src.Sexo))
                .ForMember(des => des.DPERS_FEC_NACIMIENTO, opt => opt.MapFrom(src => src.FechaNacimiento))
                .ForMember(des => des.CPERS_NRO_DOCUMENTO, opt => opt.MapFrom(src => src.NumeroDocumento))
                .ForMember(des => des.CPERS_APE_PATERNO, opt => opt.MapFrom(src => src.ApellidoPaterno))
                .ForMember(des => des.CPERS_APE_MATERNO, opt => opt.MapFrom(src => src.ApellidoMaterno))
                .ForMember(des => des.CPERS_NOMBRES, opt => opt.MapFrom(src => src.Nombres))
                .ForMember(des => des.CPERS_CORREO, opt => opt.MapFrom(src => src.Email))
                .ForMember(des => des.CPERS_BIO, opt => opt.MapFrom(src => src.Bio))
                .ForMember(des => des.CPERS_TELEFONO, opt => opt.MapFrom(src => src.Telefono));

            CreateMap<EUsuarioRolInicial, UsuarioRolInicialDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NROLE_ID))
                .ForMember(des => des.Codigo, opt => opt.MapFrom(src => src.CROLE_CODIGO))
                .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.CROLE_NOMBRE));

            CreateMap<EUsuarioMenu, UsuarioMenuDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.CMENU_ID))
                .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.CMENU_NOMBRE))
                .ForMember(des => des.IdOrigen, opt => opt.MapFrom(src => src.CMENU_ID_ORIGEN))
                .ForMember(des => des.Ordenamiento, opt => opt.MapFrom(src => src.NMENU_ORDENAMIENTO))
                .ForMember(des => des.Ruta, opt => opt.MapFrom(src => src.CMENU_RUTA))
                .ForMember(des => des.Icono, opt => opt.MapFrom(src => src.CMENU_ICONO))
                .ForMember(des => des.Visible, opt => opt.MapFrom(src => src.NROME_VISIBLE));

            CreateMap<EUsuarioRolMenuInicial, UsuarioRolMenuInicialDto>()
                .ForMember(des => des.lstUsuarioMenuDto, opt => opt.MapFrom(src => src.lstUsuarioMenu))
                .ForMember(des => des.lstUsuarioRolInicialDto, opt => opt.MapFrom(src => src.lstUsuarioRolInicial));

            CreateMap<UsuarioFilterRequestDto, EUsuarioFiltro>()
                .ForMember(des => des.USUARIO, opt => opt.MapFrom(src => src.Usuario))
                .ForMember(des => des.NTIRO_ID, opt => opt.MapFrom(src => src.TipoRolId))
                .ForMember(des => des.NROLE_ID, opt => opt.MapFrom(src => src.RolId))
                .ForMember(des => des.NUARO_ESTADO, opt => opt.MapFrom(src => src.Estado))
                .ForMember(des => des.CPERS_CORREO, opt => opt.MapFrom(src => src.Email))
                .ForMember(des => des.NCADE_ID_SEXO, opt => opt.MapFrom(src => src.Sexo))
                .ForMember(des => des.PAGE_NUMBER, opt => opt.MapFrom(src => src.NumeroPagina))
                .ForMember(des => des.PAGE_SIZE, opt => opt.MapFrom(src => src.TamanioPagina))
                .ForMember(des => des.P_ORDER_BY, opt => opt.MapFrom(src => src.SortColumn))
                .ForMember(des => des.P_ORDER, opt => opt.MapFrom(src => src.SortOrder));

            CreateMap<EUsuarioRolLista, UsuarioResponseDto>()
                .ForMember(des => des.Numero, opt => opt.MapFrom(src => src.NUMERO))
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.NUARO_ID))
                .ForMember(des => des.Codigo, opt => opt.MapFrom(src => src.CUSUA_USERNAME))
                .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.NUSUA_ESTADO))
                .ForMember(des => des.EstadoTexto, opt => opt.MapFrom(src => src.ESTADO_TEXTO))
                .ForMember(des => des.NombreRol, opt => opt.MapFrom(src => src.CROLE_NOMBRE))
                .ForMember(des => des.NombreTipoRol, opt => opt.MapFrom(src => src.CTIRO_NOMBRE))
                .ForMember(des => des.EstadoRol, opt => opt.MapFrom(src => src.NROLE_ESTADO))
                .ForMember(des => des.EstadoUsuarioRol, opt => opt.MapFrom(src => src.NUARO_ESTADO))
                .ForMember(des => des.ApellidoPaterno, opt => opt.MapFrom(src => src.CPERS_APE_PATERNO))
                .ForMember(des => des.ApellidoMaterno, opt => opt.MapFrom(src => src.CPERS_APE_MATERNO))
                .ForMember(des => des.Nombres, opt => opt.MapFrom(src => src.CPERS_NOMBRES))
                .ForMember(des => des.Bio, opt => opt.MapFrom(src => src.CPERS_BIO))
                .ForMember(des => des.Email, opt => opt.MapFrom(src => src.CPERS_CORREO))
                .ForMember(des => des.Avatar, opt => opt.MapFrom(src => src.CPERS_AVATAR))
                .ForMember(des => des.FechaCreacion, opt => opt.MapFrom(src => src.DAUDI_REG_INS))
                .ForMember(des => des.FechaModificacion, opt => opt.MapFrom(src => src.DAUDI_REG_UPD))
                .ForMember(des => des.FechaCreacion, opt => opt.MapFrom(src => src.FECHA_CREACION_TEXT))
                .ForMember(des => des.FechaModificacionText, opt => opt.MapFrom(src => src.FECHA_MODIFICACION_TEXT))
                .ForMember(des => des.UsuarioResponsable, opt => opt.MapFrom(src => src.USUARIO_RESPONSABLE));

            CreateMap<EUsuarioRol, UsuarioRolResponseDto>()
                 .ForMember(des => des.IdUsuarioRol, opt => opt.MapFrom(src => src.NUARO_ID))
                 .ForMember(des => des.Codigo, opt => opt.MapFrom(src => src.CUSUA_USERNAME))
                 .ForMember(des => des.ApellidoPaterno, opt => opt.MapFrom(src => src.CPERS_APE_PATERNO))
                 .ForMember(des => des.ApellidoMaterno, opt => opt.MapFrom(src => src.CPERS_APE_MATERNO))
                 .ForMember(des => des.Nombres, opt => opt.MapFrom(src => src.CPERS_NOMBRES))
                 .ForMember(des => des.Estado, opt => opt.MapFrom(src => src.NUARO_ESTADO))
                 .ForMember(des => des.Correo, opt => opt.MapFrom(src => src.CPERS_CORREO))
                 .ForMember(des => des.RolId, opt => opt.MapFrom(src => src.NROLE_ID));

            CreateMap<UsuarioInsUpdDto, EUsuarioInsUpd>()
                .ForMember(des => des.NUARO_ID, opt => opt.MapFrom(src => src.IdRolUsuario))
                .ForMember(des => des.CPERS_NOMBRES, opt => opt.MapFrom(src => src.Nombres))
                .ForMember(des => des.CPERS_APE_PATERNO, opt => opt.MapFrom(src => src.ApellidoPaterno))
                .ForMember(des => des.CPERS_APE_MATERNO, opt => opt.MapFrom(src => src.ApellidoMaterno))
                .ForMember(des => des.CUSUA_USERNAME, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(des => des.CPERS_CORREO, opt => opt.MapFrom(src => src.Correo))
                .ForMember(des => des.NROLE_ID, opt => opt.MapFrom(src => src.RolId))
                .ForMember(des => des.NUARO_ESTADO, opt => opt.MapFrom(src => src.Estado))
                .ForMember(des => des.NAUDI_USR_INS, opt => opt.MapFrom(src => src.Usuario));

            CreateMap<UsuarioDelDto, EUsuarioDel>()
                .ForMember(des => des.NUARO_ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.NAUDI_USR_UPD, opt => opt.MapFrom(src => src.Usuario));

            CreateMap<UsuarioAsociarRolDto, EAsociarRol>()
                .ForMember(des => des.NUARO_ID, opt => opt.MapFrom(src => src.IdRolUsuario))
                .ForMember(des => des.NROLE_ID, opt => opt.MapFrom(src => src.RolId))
                .ForMember(des => des.NAUDI_USR_INS, opt => opt.MapFrom(src => src.Usuario));

            CreateMap<ECorreoElectronico, MessageSendGridDto>()
                .ForMember(des => des.subject, opt => opt.MapFrom(src => src.ASUNTO))
                .ForMember(des => des.destinationEmail, opt => opt.MapFrom(src => src.EMAIL_DESTINO))
                .ForMember(des => des.destinationName, opt => opt.MapFrom(src => src.NOMBRE_DESTINO))
                .ForMember(des => des.ccdestinationEmail, opt => opt.MapFrom(src => src.EMAIL_COPIA))
                .ForMember(des => des.ccdestinationName, opt => opt.MapFrom(src => src.NOMBRE_COPIA))
                .ForMember(des => des.plainTextContent, opt => opt.MapFrom(src => src.CONTENIDO_TEXTO))
                .ForMember(des => des.htmlContent, opt => opt.MapFrom(src => src.CONTENIDO_HTML));
        }
    }
}
