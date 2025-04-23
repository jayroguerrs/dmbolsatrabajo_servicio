using DMBolsaTrabajo.Dto.Menu;
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
    public class MenuController : ControllerBase
    {
        
        protected readonly IMenuAplicacion _MenuAplicacion;
        public MenuController(IMenuAplicacion MenuAplicacion)
        {
            _MenuAplicacion = MenuAplicacion;
        }

        [HttpGet("listarMenuPorRol")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<List<MenuPermisosDto>>))]
        public async Task<ActionResult<Respuesta>> ListarMenuPermisos([FromQuery] int idRol, int idApp)
        {
            return await _MenuAplicacion.ListarMenuPermisos(idRol, idApp);
        }

        [HttpGet]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<List<MenuResponseDto>>))]
        public async Task<ActionResult<Respuesta>> Listar(int idApp)
        {
            return await _MenuAplicacion.Listar(idApp);
        }

        [HttpGet("listarPorIdOrigen")]
        public async Task<ActionResult<Respuesta>> ListarPorIdOrigen([FromQuery] MenuRequestPorIdOrigenDto request)
        {
            return await _MenuAplicacion.ListarPorIdOrigen(request.IdOrigen);
        }
    }
}
