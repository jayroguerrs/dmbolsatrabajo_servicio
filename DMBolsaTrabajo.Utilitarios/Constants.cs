namespace DMBolsaTrabajo.Utilitarios
{
    public class Constants
    {
        public const string RouteTemplate = "api/v1/[controller]";
        public const string RouteTemplateSecure = "api/v1/[controller]/[action]";
        public const string V1 = "1.0";

        public const int Ok = 200;
        public const int ErrorCodigo = 500;
        public const int Created = 201;//un código de estado 201 significa que una solicitud se procesó correctamente y devolvió,o creó, un recurso o resources en el proceso
        public const int Accepted = 202; //El código HTTP 202 significa «Aceptado» . Indica que el servidor ha recibido y entendido la solicitud del cliente, pero aún no ha sido procesada.
        public const int NotFound = 404;//El error 404, también conocido como HTTP 404 Not Found o HTTP 404 no encontrado, indica que una página que buscas no puede ser encontrada
        public const int BadRequest = 400; //El mensaje de error HTTP 400 indica que hay algo que no ha funcionado bien en la petición del cliente
        public const int NoContent = 204;//	Successful request without body content. La petición se ha completado con éxito pero su respuesta no tiene ningún contenido (la respuesta puede incluir información en sus cabeceras HTTP)

        public const string Aceptado = "Aceptado";
        public const string Creado = "Creado";
        public const string Listo = "Ok";
        public const string NoEncontrado = "No Encontrado";
        public const string NoValido = "No Valido";
        public const string NoContentText = "NoContent";

        public const string DateFormat = "yyyy-MM-dd";
        public const string DatePattern = "^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$";


        public const string Error = "Sucedió un error en el servidor";
    }
}
