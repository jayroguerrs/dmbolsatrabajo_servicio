namespace DMBolsaTrabajo.Dominio
{
    public class EFormulario
    {
        public int NUMERO { get; set; }
        public int NFORM_ID { get; set; }
        public string CFORM_TITULO { get; set; }
        public string CFORM_SUBTITULO { get; set; }
        public string? CEVEN_NOMBRE { get; set; }
        public string? USUARIO_RESPONSABLE { get; set; }
        public DateTime FECHA_MODIFICACION { get; set; }
        public int NFORM_ESTADO { get; set; }
        public string? ESTADO_TEXTO { get; set; }
    }

    public class EFormularioInicial
    {
        public int NFORM_ID { get; set; }
        public string CFORM_TITULO { get; set; }
        public string CFORM_SUBTITULO { get; set; }
        public string CFORM_IMAGEN { get; set; }
        public string CEVEN_NOMBRE { get; set; }
        public List<EPreguntas> lstPreguntas { get; set; }
        public EFormularioInicial()
        {
            lstPreguntas = new List<EPreguntas>();
        }
    }

    public class EFormularioFiltro
    {
        public DateOnly? DAUDI_USR_INS { get; set; }
        public string CFORM_TITULO { get; set; }
        public int NEVEN_ID { get; set; }
        public int? NFORM_ESTADO { get; set; }
        public int PAGE_SIZE { get; set; }
        public int PAGE_NUMBER { get; set; }
        public string P_ORDER_BY { get; set; }
        public string P_ORDER { get; set; }
    }

    public class EFormularioListaPaginado
    {
        public List<EFormulario> lstFormularioPaginado { get; set; }
        public int RECORDCOUNT { get; set; }
        public int PAGECOUNT { get; set; }
        public int CURRENTPAGE { get; set; }
    }

    public class EFormularioRespuesta
    {
        public int NPREG_ID { get; set; }
        public string CPREG_RESPUESTA { get; set; }
    }

    public class EListaFormularioRespuesta
    {
        public string CFORM_LINK { get; set; }
        public List<EFormularioRespuesta> lstFormularioRespuesta { get; set; }
    }

}
