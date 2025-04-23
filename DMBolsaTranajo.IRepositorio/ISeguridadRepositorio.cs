using DMBolsaTrabajo.Dominio;

namespace DMBolsaTrabajo.IRepositorio
{
    public interface ISeguridadRepositorio
    {
        Task<EUsuarioLogin> IniciarSesion(EUsuarioLoginFiltro pSeguridad);
        Task<ECorreoElectronico> EnviarCorreoRecuperacion(string email, string url);
        Task<(int, string)> ValidarCredencialesRestablecer(int IdUsuario, string Token);
        Task<(int, string)> CambiarClave(EClave request);
        Task<(int, string)> CambiarClaveNoAuth(EClave request);
    }
}
