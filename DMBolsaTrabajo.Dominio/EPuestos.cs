
namespace DMBolsaTrabajo.Dominio
{
    public class EPuestosFiltro
    {
        public DateOnly? DAUDI_USR_INS { get; set; }
        public string CPUEST_TITULO { get; set; }
        public string UBICACION { get; set; }
        public int? NPUEST_ESTADO { get; set; }
        public int PAGE_SIZE { get; set; }
        public int PAGE_NUMBER { get; set; }
        public string P_ORDER_BY { get; set; }
        public string P_ORDER { get; set; }
    }

    public class EPuestosListaPaginado
    {
        public List<EPuestosLista> lstPuestosPaginado { get; set; }
        public int RECORDCOUNT { get; set; }
        public int PAGECOUNT { get; set; }
        public int CURRENTPAGE { get; set; }
    }

    public class EPuestosLista
    {
        public int? NUMERO { get; set; }
        public int NPUEST_ID { get; set; }
        public string CPUEST_TITULO { get; set; }
        public string CPUEST_DESCRIPCION { get; set; }
        public string CDIST_NOMBRE { get; set; }
        public string CDEPA_NOMBRE { get; set; }
        public string? CPUEST_IMAGEN { get; set; }
        public DateOnly DPUEST_FECHA_FIN { get; set; }
        public string UBICACION { get; set; }
        public DateTime FECHA_MODIFICACION { get; set; }
        public string USUARIO_RESPONSABLE { get; set; }
        public string ESTADO_TEXTO { get; set; }
    }

    public class EPostularInsUpd
    {
        public int? NPOST_ID { get; set; }
        public int? NUSUA_ID { get; set; }
        public string? CPOST_NOMBRES { get; set; }
        public int NCADE_TIPO_DOCUMENTO { get; set; }
        public string CPOST_NUMDOC { get; set; }
        public string? CPOST_PATERNO { get; set; }
        public string? CPOST_MATERNO { get; set; }
        public string CPOST_CELULAR { get; set; }
        public string CPOST_CORREO { get; set; }
        public int NPUEST_ID { get; set; }
        public string CPOST_ARCHIVO { get; set; }
    }
}
