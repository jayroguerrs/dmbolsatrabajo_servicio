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
    public class UbicacionController : ControllerBase
    {

        protected readonly IUbicacionAplicacion _UbicacionAplicacion;
        public UbicacionController(IUbicacionAplicacion UbicacionAplicacion)
        {
            _UbicacionAplicacion = UbicacionAplicacion;
        }

        [HttpPost("listarDepartamentoCmb")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<List<DepartamentoFiltroRequestDto>>))]
        public async Task<ActionResult<Respuesta>> ListarDepartamentoCmb([FromBody] DepartamentoFiltroRequestDto request)
        {
            return await _UbicacionAplicacion.ListarDepartamentoCmb(request);
        }

        [HttpPost("listarDistritoCmb")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<List<DistritoFiltroRequestDto>>))]
        public async Task<ActionResult<Respuesta>> ListarDistritoCmb([FromBody] DistritoFiltroRequestDto request)
        {
            return await _UbicacionAplicacion.ListarDistritoCmb(request);
        }
    }
}
