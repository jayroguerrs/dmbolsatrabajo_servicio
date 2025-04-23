using DMBolsaTrabajo.Dto.CatalogoDetalle;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using DMBolsaTrabajo.Utilitarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace DMBolsaTrabajo.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CatalogoDetalleController : Controller
    {
        protected readonly ICatalogoDetalleAplicacion _catalogoDetalleAplicacion;

        public CatalogoDetalleController(ICatalogoDetalleAplicacion catalogoDetalleAplicacion)
        {
            _catalogoDetalleAplicacion = catalogoDetalleAplicacion;
        }

        #region Estado
        [HttpGet("listarEstado")]
        public async Task<ActionResult<Respuesta>> ListarEstado()
        {
            return await _catalogoDetalleAplicacion.Listar(12);
        }
        #endregion

        #region Tipo de Documento de Identidad
        [HttpGet("listarTipoDocumento")]
        public async Task<ActionResult<Respuesta>> ListarTipoDocumento()
        {
            return await _catalogoDetalleAplicacion.Listar(10);
        }
        #endregion

        #region Genero
        [HttpGet("listarGenero")]
        public async Task<ActionResult<Respuesta>> ListarGenero()
        {
            return await _catalogoDetalleAplicacion.Listar(11);
        }
        #endregion

        #region Catalogo
        [HttpGet("listarCatalogoCmb")]
        public async Task<ActionResult<Respuesta>> ListarCatalogo()
        {
            return await _catalogoDetalleAplicacion.ListarCatalogo();
        }
        #endregion

        #region Catalogo Detalle
        [HttpGet("ListarPaginado")]
        public async Task<ActionResult> ListarPaginado([FromQuery] CatalogoDetalleRequestPorFiltroDto request)
        {
            return Ok(await _catalogoDetalleAplicacion.ListarPaginado(request));

        }

        [HttpGet("ObtenerPorId")]
        public async Task<ActionResult> ObtenerPorId(int Id)
        {
            return Ok(await _catalogoDetalleAplicacion.ObtenerPorId(Id));
        }

        [HttpPost("Insertar")]
        [SwaggerResponse(Constants.Ok, Constants.Aceptado, typeof(RespuestaGen<Int32>))]
        public async Task<ActionResult> Insertar([FromBody] CatalogoDetalleInsUpdDto request)
        {
            request.Usuario = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var response = await _catalogoDetalleAplicacion.Insertar(request);
            return Ok(response);
        }

        // <summary>
        /// Genera Reporte en Excel o PDF
        /// </summary>
        /// <param name="Tipo">1: Excel, 2:PDF</param>
        [HttpGet("generarReporte")]
        public async Task<ActionResult<Respuesta>> GenerarReporte([FromQuery] CatalogoDetalleRequestPorFiltroDto request, [FromQuery] int Tipo)
        {
            /*
             * Tipo 1: Excel
             * 2:PDF
             */
            var respuesta = await _catalogoDetalleAplicacion.GenerarReporte(request, Tipo);
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
        #endregion
    }
}
