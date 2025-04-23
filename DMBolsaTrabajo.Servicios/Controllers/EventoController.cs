using DMBolsaTrabajo.Dto.Rol;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.Utilitarios;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DMBolsaTrabajo.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventoController : ControllerBase
    {

        protected readonly IEventoAplicacion _EventoAplicacion;
        public EventoController(IEventoAplicacion EventoAplicacion)
        {
            _EventoAplicacion = EventoAplicacion;
        }

        [HttpPost("listarCmb")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<List<EventoFiltroRequestDto>>))]
        public async Task<ActionResult<Respuesta>> ListarCmb([FromBody] EventoFiltroRequestDto request)
        {
            return await _EventoAplicacion.ListarCmb(request);
        }

    }
}
