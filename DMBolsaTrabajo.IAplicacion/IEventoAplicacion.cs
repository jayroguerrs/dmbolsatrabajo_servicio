using DMBolsaTrabajo.Dto.Rol;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.IAplicacion
{
    public interface IEventoAplicacion
    {
        Task<Respuesta> ListarCmb(EventoFiltroRequestDto request);
    }
}
