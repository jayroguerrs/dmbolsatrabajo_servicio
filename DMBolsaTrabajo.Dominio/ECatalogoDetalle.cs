namespace DMBolsaTrabajo.Dominio
{
    public class ECatalogoDetalle
    {
        public int NUMERO { get; set; }
        public int NCADE_ID { get; set; }
        public string? CCATA_NOMBRE { get; set; }
        public string? NCATA_ID { get; set; }
        public string CCADE_CODIGO { get; set; }
        public string CCADE_NOMBRE { get; set; }
        public string? CCADE_DESCRIPCION { get; set; }
        public string? CCADE_ABREVIATURA { get; set; }
        public int NCADE_ORDENAMIENTO { get; set; }
        public string USUARIO_RESPONSABLE { get; set; }
        public DateTime FECHA_MODIFICACION { get; set; }
        public int NCADE_ESTADO { get; set; }
        public string? ESTADO_TEXTO { get; set; }
    }

    public class ECatalogoDetalleResponsePorId
    {
        public Int32 NCADE_ID { get; set; }
        public string CCADE_CODIGO { get; set; }
        public string CCADE_NOMBRE { get; set; }
        public string? CCADE_ABREVIATURA { get; set; }
        public Int32 NCATA_ID { get; set; }
        public string? CCADE_DESCRIPCION { get; set; }
        public Int32? NCADE_ID_ORIGEN { get; set; }
        public Int32? NCADE_ORDENAMIENTO { get; set; }
        public Int32? NCADE_ESTADO { get; set; }
        public int NAUDI_USR_INS { get; set; }
    }

    public class ECatalogoDetalleFiltro
    {
        public string CCADE_NOMBRE { get; set; }
        public int NCATA_ID { get; set; }
        public int? NCADE_ESTADO { get; set; }
        public int PAGE_SIZE { get; set; }
        public int PAGE_NUMBER { get; set; }
        public string P_ORDER_BY { get; set; }
        public string P_ORDER { get; set; }
    }

    public class ECatalogoResponse
    {
        public Int32 NCATA_ID { get; set; }
        public string CCATA_NOMBRE { get; set; }
    }

    public class ECatalogoDetallePorIdCatalogo
    {
        public int NCATA_ID { get; set; }
        public int? NCADE_ID_ORIGEN { get; set; }
        public string? CCADE_GRUPO { get; set; }
    }

    public class ECatalogoDetalleListaPaginado
    {
        public List<ECatalogoDetalle> lstCatalogoDetallePaginado { get; set; }
        public int RECORDCOUNT { get; set; }
        public int PAGECOUNT { get; set; }
        public int CURRENTPAGE { get; set; }
    }
}
