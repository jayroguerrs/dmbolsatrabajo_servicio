using DMBolsaTrabajo.Aplicacion;
using DMBolsaTrabajo.Dto.Puestos;
using DMBolsaTrabajo.Dto.Usuario;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.Utilitarios;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace DMBolsaTrabajo.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PuestosController : ControllerBase
    {
        protected readonly IPuestosAplicacion _puestosAplicacion;
        private readonly IConfiguration _configuration;

        public PuestosController(IPuestosAplicacion puestosAplicacion, IConfiguration configuration)
        {
            _puestosAplicacion = puestosAplicacion;
            
            _configuration = configuration;
        }

        [HttpPost("ListarPaginado")]
        public async Task<ActionResult> ListarPaginado([FromBody] PuestosFilterRequestDto requestFilterDto)
        {
            return Ok(await _puestosAplicacion.ListarPaginado(requestFilterDto));
        }

        [HttpPost("ListarPostulantesPaginado")]
        public async Task<ActionResult> ListarPostulantesPaginado([FromBody] PostulantesFilterRequestDto requestFilterDto)
        {
            return Ok(await _puestosAplicacion.ListarPostulantesPaginado(requestFilterDto));
        }

        [HttpPost("ListarPaginadoNoCaptcha")]
        public async Task<ActionResult> ListarPaginadoNoCaptcha([FromBody] PuestosFilterNoCaptchaRequestDto requestFilterDto)
        {
            return Ok(await _puestosAplicacion.ListarPaginadoNoCaptcha(requestFilterDto));
        }

        [HttpGet("obtenerPorId")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<PuestosResponseDto>))]
        public async Task<ActionResult<Respuesta>> ObtenerPorId(int Id)
        {
            return await _puestosAplicacion.ObtenerPorId(Id);
        }

        [HttpPost("postular")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> Postular([FromForm] IFormFile archivo, [FromForm] PostularInsUpdDto request)
        {
            var response = await _puestosAplicacion.Postular(archivo, request);
            return Ok(response);
        }

        [HttpPatch("eliminar")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<Int32>))]
        public async Task<ActionResult> Eliminar([FromBody] PuestosDelDto request)
        {
            request.Usuario = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            return Ok(await _puestosAplicacion.Eliminar(request));
        }

        [HttpPost("Insertar")]
        [SwaggerResponse(Constants.Ok, Constants.Aceptado, typeof(RespuestaGen<Int32>))]
        public async Task<ActionResult> Insertar([FromBody] PuestosInsUpdDto request)
        {
            request.Usuario = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var response = await _puestosAplicacion.Insertar(request);
            return Ok(response);
        }

        [HttpPatch("actualizarEstado")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<Int32>))]
        public async Task<ActionResult> CambiarEstado([FromBody] PuestosEstadoDto request)
        {
            request.Usuario = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            return Ok(await _puestosAplicacion.CambiarEstado(request));
        }
    }
}
