namespace DMBolsaTrabajo.Dominio
{
    public class EPreguntas
    {
        public int NUMERO { get; set; }
        public int NPREG_ID { get; set; }
        public string CPREG_DESCRIPCION { get; set; }
        public string CPREG_ALTERNATIVAS { get; set; }
        public int NPREG_OBLIGATORIO { get; set; }
        public int NCANT_ARCHIVOS { get; set; }
        public string CPREG_RESPUESTA { get; set; }
        public int NCADE_TIPO_PREG { get; set; }
        public int NPREG_ES_FECHA { get; set; }
        public int NPREG_ES_NUMERO { get; set; }
        public int NPREG_ES_CORREO { get; set; }
        public int NPREG_ES_REGEXP { get; set; }
        public List<EAlternativas> lstAlternativas { get; set; }
        public EPreguntas()
        {
            lstAlternativas = new List<EAlternativas>();
        }

    }

    public class EAlternativas { 
        public int NPREG_ID { get; set; }
        public int INDEX { get; set; }
        public string DESCRIPCION { get; set; }
    }
}
