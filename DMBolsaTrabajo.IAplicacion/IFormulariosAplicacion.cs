using DMBolsaTrabajo.Dto.Formularios;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using Microsoft.AspNetCore.Http;

namespace DMBolsaTrabajo.IAplicacion
{
    public interface IFormulariosAplicacion
    {
        Task<Respuesta> EnviarFormularios(FormulariosRequestDto request);
        Task<Respuesta> ValidarFormularios(int id);
        Task<Respuesta> ListarPaginado(FormularioRequestPorFiltroDto request);
        Task<Respuesta> ObtenerDatos(string url);
        Task<Respuesta> EnviarFormulario(List<IFormFile> archivos, ListaRespuestaRequestDto request);
    }
}
