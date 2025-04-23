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
    public class RolController : ControllerBase
    {

        protected readonly IRolAplicacion _RolAplicacion;
        public RolController(IRolAplicacion RolAplicacion)
        {
            _RolAplicacion = RolAplicacion;
        }

        [HttpPost("listarCmb")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<List<RolFiltroRequestDto>>))]
        public async Task<ActionResult<Respuesta>> ListarCmb([FromBody] RolFiltroRequestDto request)
        {
            return await _RolAplicacion.ListarCmb(request);
        }

    }
}
