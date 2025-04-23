using DMBolsaTrabajo.Dto.Puestos;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using Microsoft.AspNetCore.Http;

namespace DMBolsaTrabajo.IAplicacion
{
    public interface IPuestosAplicacion
    {
        Task<Respuesta> ListarPaginado(PuestosFilterRequestDto request);
        Task<Respuesta> ListarPaginadoNoCaptcha(PuestosFilterNoCaptchaRequestDto request);
        Task<Respuesta> ObtenerPorId(int idRolUsuario); 
        Task<Respuesta> Postular(IFormFile archivo, PostularInsUpdDto request);
    }
}
