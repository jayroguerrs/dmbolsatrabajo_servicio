namespace DMBolsaTrabajo.Utilitarios.EstadoRespuesta
{
    public class RespuestaBD
    {
        public int NSUCCESS { get; set; }
        public string CMESSAGE { get; set; }
        public int NREGISTRO { get; set; }
        public RespuestaBD()
        {
            this.NSUCCESS = 0;
            this.CMESSAGE = String.Empty;
            this.NREGISTRO = 0;
        }
    }
}
