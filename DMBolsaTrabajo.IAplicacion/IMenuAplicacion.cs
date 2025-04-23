using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.IAplicacion
{
    public interface IMenuAplicacion
    {
        Task<Respuesta> ListarPorIdOrigen(int IdOrigen);
        Task<Respuesta> Listar(int idApp);
        Task<Respuesta> ListarMenuPermisos(int idRol, int idApp);
    }
}
