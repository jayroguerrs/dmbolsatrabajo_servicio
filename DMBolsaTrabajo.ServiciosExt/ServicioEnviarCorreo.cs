using DMBolsaTrabajo.ServiciosExt.EnviarCorreo;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace DMBolsaTrabajo.ServiciosExt
{
    public class ServicioEnviarCorreo: IServicioEnviarCorreo
    {
        IConfiguration _configuration;
        public ServicioEnviarCorreo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Respuesta> EnviarCorreo(MessageSendGridDto message)
        {
            var respuesta = new Respuesta();

            try
            {
                // Leer configuración del archivo appsettings.json
                var smtpConfig = _configuration.GetSection("SmtpSettings");
                if (smtpConfig == null) throw new Exception("No se encontró la configuración SMTP en appsettings.json");

                string _host = smtpConfig.GetValue<string>("host") ?? throw new Exception("El valor de SMTP:host no puede ser nulo o vacío.");
                int _port = smtpConfig.GetValue<int>("port");
                string _user = smtpConfig.GetValue<string>("user") ?? throw new Exception("El valor de SMTP:user no puede ser nulo o vacío.");
                string _pass = smtpConfig.GetValue<string>("password") ?? throw new Exception("El valor de SMTP:password no puede ser nulo o vacío.");
                bool _enableSsl = smtpConfig.GetValue<bool>("enableSsl");

                using (var smtpClient = new SmtpClient(_host, _port))
                {
                    smtpClient.Credentials = new NetworkCredential(_user, _pass);
                    smtpClient.EnableSsl = _enableSsl;

                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(_user, "DM Desarrollo");
                        mailMessage.Subject = message.subject;
                        mailMessage.Body = message.htmlContent ?? message.plainTextContent;
                        mailMessage.IsBodyHtml = !string.IsNullOrEmpty(message.htmlContent);
                        mailMessage.To.Add(new MailAddress(message.destinationEmail, message.destinationName));

                        if (!string.IsNullOrEmpty(message.ccdestinationEmail))
                        {
                            mailMessage.CC.Add(new MailAddress(message.ccdestinationEmail, message.ccdestinationName));
                        }

                        await smtpClient.SendMailAsync(mailMessage);
                    }

                    respuesta.success = true;
                    respuesta.status = 200;
                }
            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.status = 500;
                respuesta.validations.Add(new GenericMessage("error", ex.Message));
            }

            return respuesta;
        }
    }
}
