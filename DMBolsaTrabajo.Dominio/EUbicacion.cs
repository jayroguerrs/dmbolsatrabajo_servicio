namespace DMBolsaTrabajo.Dominio
{
    public class EDepartamentoFiltro
    {
        public int NDEPA_ESTADO { get; set; }
    }

    public class EDistritoFiltro
    {
        public int NDEPA_ID { get; set; }
        public int NDIST_ESTADO { get; set; }
    }

    public class EDistritoCombo
    {
        public int NDIST_ID { get; set; }
        public string CDIST_NOMBRE { get; set; }
    }
    public class EDepartamentoCombo
    {
        public int NDEPA_ID { get; set; }
        public string CDEPA_NOMBRE { get; set; }
    }
}
