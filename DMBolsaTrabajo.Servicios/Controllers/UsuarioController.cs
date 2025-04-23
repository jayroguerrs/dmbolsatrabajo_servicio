using DMBolsaTrabajo.Dto.Usuario;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.Utilitarios;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Security.Claims;

namespace DMBolsaTrabajo.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        protected readonly IUsuarioAplicacion _usuarioAplicacion;
        private readonly IConfiguration _configuration;

        public UsuarioController(IUsuarioAplicacion usuarioAplicacion, IConfiguration configuration)
        {
            _usuarioAplicacion = usuarioAplicacion;
            _configuration = configuration;
        }

        [HttpGet("obtenerCargaInicial")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<ConsolidadoDto>))]
        public async Task<ActionResult<Respuesta>> ObtenerCargaInicial(int idUsuario)
        {
            return await _usuarioAplicacion.ObtenerCargaInicial(idUsuario);
        }

        [HttpGet("obtenerDatos")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<UsuarioResponseDto>))]
        public async Task<ActionResult<Respuesta>> ObtenerDatos(int id)
        {
            return await _usuarioAplicacion.ObtenerDatos(id);
        }

        [HttpGet("obtenerDatosPorRol")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<UsuarioRolResponseDto>))]
        public async Task<ActionResult<Respuesta>> ObtenerDatosRolUsuario(int idRolUsuario)
        {
            return await _usuarioAplicacion.ObtenerDatosRolUsuario(idRolUsuario);
        }

        [HttpPatch("asociarRol")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<Int32>))]
        public async Task<ActionResult> AsociarRol([FromBody] UsuarioAsociarRolDto request)
        {
            request.Usuario = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var response = await _usuarioAplicacion.AsociarRol(request);
            return Ok(response);
        }

        [HttpPatch("actualizarDatos")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<Int32>))]
        public async Task<ActionResult> ActualizarDatos([FromBody] UsuarioRequestDto request)
        {
            return Ok(await _usuarioAplicacion.ActualizarDatos(request));
        }

        [HttpPost("Insertar")]
        [SwaggerResponse(Constants.Ok, Constants.Aceptado, typeof(RespuestaGen<Int32>))]
        public async Task<ActionResult> Insertar([FromBody] UsuarioInsUpdDto request)
        {
            request.Usuario = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var response = await _usuarioAplicacion.Insertar(request);
            return Ok(response);
        }

        [HttpGet("obtenerRolMenuPorUsuario")]
        public async Task<ActionResult<Respuesta>> ObtenerEntidadRolMenuPorUsuario(int idUsuario, int idRol)
        {
            return await _usuarioAplicacion.ObtenerRolMenuPorUsuario(idUsuario, idRol);
        }

        [HttpGet("ListarPaginado")]
        public async Task<ActionResult> ListarPaginado([FromQuery] UsuarioFilterRequestDto requestFilterDto)
        {
            return Ok(await _usuarioAplicacion.ListarPaginado(requestFilterDto));

        }

        [HttpPost("EnviarConfirmacion")]
        [SwaggerOperation(Tags = new[] { "Enviar correo de confirmacion" })]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Respuesta>> EnviarConfirmacion(int idUsuario)
        {
            return await _usuarioAplicacion.EnviarCorreoConfirmacion(idUsuario);
        }

        // <summary>
        /// Genera Reporte en Excel o PDF
        /// </summary>
        /// <param name="Tipo">1: Excel, 2:PDF</param>
        [HttpGet("generarReporte")]
        public async Task<ActionResult<Respuesta>> GenerarReporte([FromQuery] UsuarioFilterRequestDto request, [FromQuery] int Tipo)
        {
            /*
             * Tipo 1: Excel
             * 2:PDF
             */
            var respuesta = await _usuarioAplicacion.GenerarReporte(request, Tipo);
            if (Tipo == 1)
            {
                return File((byte[])respuesta.data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            else if (Tipo == 2)
            {
                return File((byte[])respuesta.data, "application/pdf");
            }
            else
            {
                return respuesta;
            }

        }
    }
}