using DMBolsaTrabajo.Dto.Rol;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.IAplicacion
{
    public interface IRolAplicacion
    {
        Task<Respuesta> ListarCmb(RolFiltroRequestDto request);
    }
}
