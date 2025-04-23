namespace DMBolsaTrabajo.Dominio
{
    public class EMenu
    {
        public string CMENU_ID { get; set; }
        public string CMENU_NOMBRE { get; set; }
        public int? NMENU_ID_ORIGEN { get; set; }
        public int NMENU_ORDENAMIENTO { get; set; }
        public string? CAUDI_USR_INS { get; set; }
        public DateTime? DAUDI_REG_INS { get; set; }
        public string? CAUDI_USR_UPD { get; set; }
        public DateTime? DAUDI_REG_UPD { get; set; }
        public string? CAUDI_EST_REG { get; set; }
        public int? NMENU_ESTADO { get; set; }
        public string? CMENU_RUTA { get; set; }
        public string? CMENU_ICONO { get; set; }
    }

    public class ERolMenuPermisos
    {
        public string CMENU_ID { get; set; }
        public string? CMENU_NOMBRE { get; set; }
        public int NMENU_ID_ORIGEN { get; set; }
        public int NMENU_ORDENAMIENTO { get; set; }
        public string? CMENU_RUTA { get; set; }
        public string? CMENU_ICONO { get; set; }
        public int NROME_ID { get; set; }
        public int NROME_ESTADO { get; set; }

    }

    public class EFiltroPermisos
    {
        public int NSUPU_ID { get; set; }
        public int NROLE_ID { get; set; }
        public int MENU_ID { get; set; }
    }
}
