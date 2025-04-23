using DMBolsaTrabajo.ServiciosExt.EnviarCorreo;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.ServiciosExt
{
    public interface IServicioEnviarCorreo
    {
        Task<Respuesta> EnviarCorreo(MessageSendGridDto message);
    }
}
