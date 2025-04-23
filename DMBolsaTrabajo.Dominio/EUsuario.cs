namespace DMBolsaTrabajo.Dominio
{
    public class EUsuarioLogin : EPersona
    {
        public int NUSUA_ID;
    }

    public class EUsuarioLoginFiltro
    {
        public string CUSUA_USERNAME;
        public string CUSUA_PASSWORD;
    }

    public class EUsuarioInicial
    {
        public int NUSUA_ID { get; set; }
        public string CROLE_CODIGO { get; set; }
        public int NROLE_ID { get; set; }
        public List<EUsuarioRolInicial> lstUsuarioRolInicial { get; set; }
        public List<EUsuarioMenu> lstUsuarioMenu { get; set; }
        public EUsuarioInicial()
        {
            lstUsuarioRolInicial = new List<EUsuarioRolInicial>();
            lstUsuarioMenu = new List<EUsuarioMenu>();
        }
    }

    public class EUsuarioRolInicial
    {
        public int NROLE_ID { get; set; }
        public string CROLE_CODIGO { get; set; }
        public string CROLE_NOMBRE { get; set; }
    }

    public class EUsuarioMenu
    {
        public string CMENU_ID { get; set; }
        public string CMENU_NOMBRE { get; set; }
        public string? CMENU_ID_ORIGEN { get; set; }
        public int NMENU_ORDENAMIENTO { get; set; }
        public string CMENU_RUTA { get; set; }
        public string CMENU_ICONO { get; set; }
        public int NROME_VISIBLE { get; set; }
    }

    public class EUsuario
    {
        public int NUSUA_ID { get; set; }
        public int NCADE_ID_TIPO_DOCUMENTO { get; set; }
        public string? CPERS_NRO_DOCUMENTO { get; set; }
        public DateTime DPERS_FEC_NACIMIENTO { get; set; }
        public string CUSUA_USERNAME { get; set; }
        public string CPERS_APE_PATERNO { get; set; }
        public string CPERS_APE_MATERNO { get; set; }
        public string CPERS_NOMBRES { get; set; }
        public string CPERS_CORREO { get; set; }
        public string CPERS_TELEFONO { get; set; }
        public int? NUSUA_ESTADO { get; set; }
        public int? NCADE_ID_SEXO { get; set; }
        public string? CPERS_BIO { get; set; }
    }

    public class EUsuarioRol
    {
        public int NUARO_ID { get; set; }
        public string CUSUA_USERNAME { get; set; }
        public string CPERS_APE_PATERNO { get; set; }
        public string CPERS_APE_MATERNO { get; set; }
        public string CPERS_NOMBRES { get; set; }
        public string CPERS_CORREO { get; set; }
        public string CPERS_TELEFONO { get; set; }
        public int NUARO_ESTADO { get; set; }
        public int NROLE_ID { get; set; }
    }

    public class EUsuarioAct
    {
        public int NUSUA_ID { get; set; }
        public string CROLE_CODIGO { get; set; }
        public int NROLE_ID { get; set; }
        public int NCADE_ID_TIPO_DOCUMENTO { get; set; }
        public int? NCADE_ID_SEXO { get; set; }
        public string CPERS_NRO_DOCUMENTO { get; set; }
        public DateTime DPERS_FEC_NACIMIENTO { get; set; }
        public string CUSUA_USERNAME { get; set; }
        public string CPERS_APE_PATERNO { get; set; }
        public string CPERS_APE_MATERNO { get; set; }
        public string CPERS_NOMBRES { get; set; }
        public string CPERS_CORREO { get; set; }
        public string CPERS_TELEFONO { get; set; }
        public string? CPERS_BIO { get; set; }
    }

    public class EUsuarioInsUpd
    {
        public int NUARO_ID { get; set; }
        public string CUSUA_USERNAME { get; set; }
        public string CPERS_NOMBRES { get; set; }
        public int NROLE_ID { get; set; }
        public string CPERS_APE_PATERNO { get; set; }
        public string CPERS_APE_MATERNO { get; set; }
        public string CPERS_CORREO { get; set; }
        public int NUARO_ESTADO { get; set; }
        public int NAUDI_USR_INS { get; set; }
    }

    public class EUsuarioDel
    {
        public int NUARO_ID;
        public string NAUDI_USR_UPD;
    }
    public class EUsuarioRolMenuInicial
    {
        public List<EUsuarioRolInicial> lstUsuarioRolInicial { get; set; }
        public List<EUsuarioMenu> lstUsuarioMenu { get; set; }
        public EUsuarioRolMenuInicial()
        {
            lstUsuarioRolInicial = new List<EUsuarioRolInicial>();
            lstUsuarioMenu = new List<EUsuarioMenu>();
        }
    }

    public class EUsuarioListaPaginado
    {
        public List<EUsuarioRolLista> lstUsuarioPaginado { get; set; }
        public int RECORDCOUNT { get; set; }
        public int PAGECOUNT { get; set; }
        public int CURRENTPAGE { get; set; }
    }

    public class EUsuarioRolLista
    {
        public int? NUMERO { get; set; }
        public int NUARO_ID { get; set; }
        public DateTime DUARO_FEC_INICIO { get; set; }
        public DateTime DUARO_FEC_FIN { get; set; }
        public int NUSUA_ID { get; set; }
        public string CUSUA_USERNAME { get; set; }
        public int NUSUA_ESTADO { get; set; }
        public string CENTI_NOMBRE { get; set; }
        public string CROLE_NOMBRE { get; set; }
        public string CTIRO_NOMBRE { get; set; }
        public string? CPERS_BIO { get; set; }
        public int NROLE_ESTADO { get; set; }
        public int NUARO_ESTADO { get; set; }
        public string CCADE_NOMBRE { get; set; }
        public int NCADE_ID_TIPO_DOCUMENTO { get; set; }
        public string CPERS_NRO_DOCUMENTO { get; set; }
        public string CPERS_APE_PATERNO { get; set; }
        public string CPERS_APE_MATERNO { get; set; }
        public string CPERS_NOMBRES { get; set; }
        public string CPERS_CORREO { get; set; }
        public string CPERS_AVATAR { get; set; }
        public string CSUPU_NOMBRE { get; set; }
        public DateTime DAUDI_REG_INS { get; set; }
        public DateTime? DAUDI_REG_UPD { get; set; }
        public string FECHA_CREACION_TEXT { get; set; }
        public string FECHA_MODIFICACION_TEXT { get; set; }
        public string USUARIO_RESPONSABLE { get; set; }
        public string ESTADO_TEXTO { get; set; }
    }

    public class EUsuarioFiltro
    {
        public int TIPO_DOCUMENTO { get; set; }
        public string NRO_DOCUMENTO { get; set; }

        public string USUARIO;
        public int? NUARO_ESTADO;
        public int NTIRO_ID { get; set; }
        public string CPERS_CORREO { get; set; }
        public int NCADE_ID_SEXO { get; set; }
        public int PAGE_SIZE { get; set; }
        public int PAGE_NUMBER { get; set; }
        public string P_ORDER_BY { get; set; }
        public string P_ORDER { get; set; }
        public int RolId { get; set; }
        public int NROLE_ID { get; set; }
    }

    public class EAsociarRol
    {
        public int NUARO_ID { get; set; }
        public int NROLE_ID { get; set; }
        public int NAUDI_USR_INS { get; set; }
    }

    public class ECorreoElectronico
    {

        public string ASUNTO { get; set; }
        public string EMAIL_DESTINO { get; set; }
        public string NOMBRE_DESTINO { get; set; }
        public string EMAIL_COPIA { get; set; }
        public string NOMBRE_COPIA { get; set; }
        public string CONTENIDO_TEXTO { get; set; }
        public string CONTENIDO_HTML { get; set; }
        public int ESTADO { get; set; }
        public string MSG { get; set; }
        public int? NUSUA_ID { get; set; }
    }
}
