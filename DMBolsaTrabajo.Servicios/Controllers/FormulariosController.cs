using DMBolsaTrabajo.Dto.Formularios;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.Utilitarios;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DMBolsaTrabajo.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormulariosController : ControllerBase
    {
        protected readonly IFormulariosAplicacion _formulariosAplicacion;
        private readonly IConfiguration _configuration;

        public FormulariosController(IFormulariosAplicacion consolidadoAplicacion, IConfiguration configuration)
        {
            _formulariosAplicacion = consolidadoAplicacion;
            _configuration = configuration;
        }

        [HttpPatch("enviar")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<Int32>))]
        public async Task<ActionResult<Respuesta>> EnviarFormularios([FromBody] FormulariosRequestDto request)
        {
            return Ok(await _formulariosAplicacion.EnviarFormularios(request));
        }

        [HttpGet("validar/{id}")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<FormulariosRequestDto>))]
        public async Task<ActionResult<Respuesta>> ValidarFormularios(int id)
        {
            return await _formulariosAplicacion.ValidarFormularios(id);
        }

        [HttpGet("listarPaginado")]
        public async Task<ActionResult> ListarPaginado([FromQuery] FormularioRequestPorFiltroDto request)
        {
            return Ok(await _formulariosAplicacion.ListarPaginado(request));

        }

        [HttpGet("obtenerDatos")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<FormulariosRequestDto>))]
        public async Task<ActionResult<Respuesta>> ObtenerDatos(string url)
        {
            return await _formulariosAplicacion.ObtenerDatos(url);
        }

        [HttpPost("enviarFormulario")]
        [Consumes("multipart/form-data")]
        [SwaggerResponse(Constants.Ok, Constants.Aceptado, typeof(RespuestaGen<int>))]
        public async Task<ActionResult> EnviarFormulario([FromForm] List<IFormFile> archivos, [FromForm] string request)
        {   
            // Deserializar el JSON recibido en `request`
            var listaRespuestas = JsonConvert.DeserializeObject<ListaRespuestaRequestDto>(request);

            return Ok(await _formulariosAplicacion.EnviarFormulario(archivos, listaRespuestas)); ;
        }
    }
}