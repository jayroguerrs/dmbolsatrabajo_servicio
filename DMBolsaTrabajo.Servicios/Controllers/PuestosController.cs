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
        [SwaggerResponse(Constants.Ok, Constants.Aceptado, typeof(RespuestaGen<Int32>))]
        public async Task<ActionResult> Postular([FromForm] IFormFile archivo, [FromForm] PostularInsUpdDto request)
        {
            var response = await _puestosAplicacion.Postular(archivo, request);
            return Ok(response);
        }
    }
}
