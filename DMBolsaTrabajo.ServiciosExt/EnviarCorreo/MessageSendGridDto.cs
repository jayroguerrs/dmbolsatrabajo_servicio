namespace DMBolsaTrabajo.ServiciosExt.EnviarCorreo
{
    public class MessageSendGridDto
    {
        public string subject { get; set; }
        public string destinationEmail { get; set; }
        public string destinationName { get; set; }
        public string ccdestinationEmail { get; set; }
        public string ccdestinationName { get; set; }
        public string plainTextContent { get; set; }
        public string htmlContent { get; set; }

    }
}
