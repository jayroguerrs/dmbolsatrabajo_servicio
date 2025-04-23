using DMBolsaTrabajo.Dto.Rol;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.IAplicacion
{
    public interface IUbicacionAplicacion
    {
        Task<Respuesta> ListarDepartamentoCmb(DepartamentoFiltroRequestDto request);
        Task<Respuesta> ListarDistritoCmb(DistritoFiltroRequestDto request);
    }
}
