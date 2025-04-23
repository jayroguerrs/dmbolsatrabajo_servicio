namespace DMBolsaTrabajo.Utilitarios.EstadoRespuesta
{
    public class Respuesta
    {
        public Object data { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
        public List<GenericMessage> validations { get; set; }

        public Respuesta()
        {
            validations = new List<GenericMessage>();
        }

    }

    public class GenericMessage
    {
        public GenericMessage(string _type, string _message, string _code = null)
        {
            type = _type;
            message = _message;
            code = _code;
        }

        public string type { get; set; }

        public string code { get; set; }

        public string message { get; set; }
    }
    public class RespuestaGen<T>
    {
        public T data { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
        public List<GenericMessage> validations { get; set; }

        public RespuestaGen()
        {
            validations = new List<GenericMessage>();
        }

    }

}
