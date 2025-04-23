using DMBolsaTrabajo.Dto.Seguridad;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.IAplicacion
{
    public interface ISeguridadAplicacion
    {
        Task<Respuesta> IniciarSesion(SeguridadRequestDto seguridadBusDto);
        Task<Respuesta> EnviarCorreoRecuperacion(string email, string url);
        Task<Respuesta> ValidarCredencialesRestablecer(int IdUsuario, string Token);
        Task<Respuesta> CambiarClave(ClaveRequestDto request);
        Task<Respuesta> CambiarClaveNoAuth(ClaveRequestDto request);

    }
}
