namespace DMBolsaTrabajo.Dominio
{
    public class EPersona
    {
        public int NPERS_ID { get; set; }
        public int NCADE_ID_TIPO_DOCUMENTO { get; set; }
        public string CPERS_NRO_DOCUMENTO { get; set; }
        public string CPERS_APE_PATERNO { get; set; }
        public string CPERS_APE_MATERNO { get; set; }
        public string CPERS_NOMBRES { get; set; }
        public DateTime? DPERS_FEC_NACIMIENTO { get; set; }
        public int? NCADE_ID_SEXO { get; set; }
        public DateTime? DPERS_FEC_EMISION { get; set; }
        public int? NDIST_ID { get; set; }
        public string CPERS_CORREO { get; set; }
        public int NROLE_ID { get; set; }
        public int EXISTE_ROL { get; set; }


        public string CPERS_TELEFONO { get; set; }
        public string CAUDI_USR_INS { get; set; }
        public DateTime DAUDI_REG_INS { get; set; }
        public string? CAUDI_USR_UPD { get; set; }
        public DateTime? DAUDI_REG_UPD { get; set; }
        public string CAUDI_EST_REG { get; set; }
        public int NPERS_ESTADO { get; set; }
        public string ESTADO_TEXTO { get; set; }
    }
}
