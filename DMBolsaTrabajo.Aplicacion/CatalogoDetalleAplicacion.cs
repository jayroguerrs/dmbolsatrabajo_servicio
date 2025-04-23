using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.CatalogoDetalle;
using DMBolsaTrabajo.Dto.Reportes;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.IRepositorio;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.Aplicacion
{
    public class CatalogoDetalleAplicacion : ICatalogoDetalleAplicacion
    {
        private readonly ICatalogoDetalleRepositorio _CatalogoDetalleRepositorio;
        private readonly IMapper _mapper;
        private readonly IReportesAplicacion _reportesAplicacion;
        public CatalogoDetalleAplicacion(ICatalogoDetalleRepositorio CatalogoDetalleRepositorio, IMapper mapper, IReportesAplicacion reportesAplicacion)
        {
            _mapper = mapper;
            _CatalogoDetalleRepositorio = CatalogoDetalleRepositorio;
            _reportesAplicacion = reportesAplicacion;
        }

        public async Task<Respuesta> Listar(int IdCatalogo)
        {
            var respuesta = new Respuesta();
            try
            {
                ECatalogoDetallePorIdCatalogo sParam = new ECatalogoDetallePorIdCatalogo();
                sParam.NCATA_ID = IdCatalogo;
                var resultado = await _CatalogoDetalleRepositorio.Listar(sParam);

                if (resultado.Count > 0)
                {
                    respuesta.data = _mapper.Map<List<CatalogoDetalleResponsePorIdDto>>(resultado);
                    respuesta.success = true;
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "No se han encontrado registros"));
                    respuesta.success = false;
                }
            }
            catch (Exception ex)
            {
                respuesta.validations.Add(new GenericMessage("error", ex.Message));
                respuesta.success = false;
            }
            return respuesta;
        }

        public async Task<Respuesta> ListarCatalogo()
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _CatalogoDetalleRepositorio.ListarCatalogo();

                if (resultado.Count > 0)
                {
                    respuesta.data = _mapper.Map<List<CatalogoResponseDto>>(resultado);
                    respuesta.success = true;
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "No se han encontrado registros"));
                    respuesta.success = false;
                }
            }
            catch (Exception ex)
            {
                respuesta.validations.Add(new GenericMessage("error", ex.Message));
                respuesta.success = false;
            }
            return respuesta;
        }

        public async Task<Respuesta> ListarPaginado(CatalogoDetalleRequestPorFiltroDto request)
        {

            var respuesta = new Respuesta();
            try
            {

                var eFiltro = _mapper.Map<ECatalogoDetalleFiltro>(request);
                var resultado = await _CatalogoDetalleRepositorio.ListarPaginado(eFiltro);

                if (resultado.lstCatalogoDetallePaginado.Count > 0)
                {
                    var data = new ListarCatalogoDetalleResponseDto
                    {
                        NumeroPagina = request.NumeroPagina,
                        TamanioPagina = request.TamanioPagina,
                        lista = _mapper.Map<List<CatalogoDetalleResponseDto>>(resultado.lstCatalogoDetallePaginado),
                        Total = resultado.RECORDCOUNT
                    };
                    respuesta.data = data;
                    respuesta.success = true;
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "No se han encontrado registros"));
                    respuesta.success = false;
                }

            }
            catch (Exception ex)
            {
                respuesta.validations.Add(new GenericMessage("error", ex.Message));
                respuesta.success = false;
            }
            return respuesta;
        }

        public async Task<Respuesta> ObtenerPorId(int Id)
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _CatalogoDetalleRepositorio.ObtenerPorId(Id);

                if (resultado != null)
                {
                    respuesta.data = _mapper.Map<CatalogoDetalleResponsePorIdDto>(resultado);
                    respuesta.success = true;
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "No se han encontrado registros"));
                    respuesta.success = false;
                }
            }
            catch (Exception ex)
            {
                respuesta.validations.Add(new GenericMessage("error", ex.Message));
                respuesta.success = false;
            }
            return respuesta;
        }

        public async Task<Respuesta> Insertar(CatalogoDetalleInsUpdDto request)
        {
            var respuesta = new Respuesta();
            try
            {
                var eParam = _mapper.Map<ECatalogoDetalleResponsePorId>(request);
                var (resultado, msj) = await _CatalogoDetalleRepositorio.Insertar(eParam);

                if (resultado > 0)
                {
                    respuesta.data = resultado;
                    respuesta.success = true;
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", msj));
                    respuesta.success = false;
                }
            }
            catch (Exception ex)
            {
                respuesta.validations.Add(new GenericMessage("error", ex.Message));
                respuesta.success = false;
            }
            return respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtro">Nombre: String, Estado: Int</param>
        /// <param name="Tipo">1: Excel, 2:PDF</param>
        /// <returns></returns>
        public async Task<Respuesta> GenerarReporte(CatalogoDetalleRequestPorFiltroDto filtro, int Tipo)
        {
            var titulos = "";
            var respuesta = new Respuesta();
            try
            {
                var eFiltro = _mapper.Map<ECatalogoDetalleFiltro>(filtro);
                var resultado = await _CatalogoDetalleRepositorio.Listar(eFiltro);

                if (resultado.Count > 0)
                {
                    #region Armar Reporte
                    var objReporte = new ReporteGenericoDto();

                    #region Encabezado
                    if (objReporte.Encabezado1 != null)
                        objReporte.Encabezado1.Nombre = "N°";
                    if (objReporte.Encabezado2 != null)
                        objReporte.Encabezado2.Nombre = "CÓDIGO";
                    objReporte.Encabezado2.Ancho = 10.71;
                    if (objReporte.Encabezado3 != null)
                        objReporte.Encabezado3.Nombre = "NOMBRE";
                    objReporte.Encabezado3.Ancho = 40.71;
                    if (objReporte.Encabezado4 != null)
                        objReporte.Encabezado4.Nombre = "ABREVIATURA";
                    objReporte.Encabezado4.Ancho = 20.71;
                    if (objReporte.Encabezado5 != null)
                        objReporte.Encabezado5.Nombre = "CATÁLOGO";
                    objReporte.Encabezado5.Ancho = 25.15;
                    if (objReporte.Encabezado6 != null)
                        objReporte.Encabezado6.Nombre = "ESTADO";
                    objReporte.Encabezado6.Ancho = 15.71;
                    if (objReporte.Encabezado7 != null)
                        objReporte.Encabezado7.Nombre = "MODIFICADO";
                    objReporte.Encabezado7.Ancho = 15.71;
                    if (objReporte.Encabezado8 != null)
                        objReporte.Encabezado8.Nombre = "MODIFICADO POR";
                    objReporte.Encabezado8.Ancho = 25.15;
                    #endregion
                    objReporte.lstDetalle = new List<MantenimientoDetalleDto>();
                    #region Detalle
                    foreach (var item in resultado)
                    {
                        var itemReporte = new MantenimientoDetalleDto
                        {
                            Campo1 = item.NUMERO.ToString(),
                            Campo2 = item.CCADE_CODIGO.ToString(),
                            Campo3 = item.CCADE_NOMBRE.ToString(),
                            Campo4 = item.CCADE_ABREVIATURA.ToString(),
                            Campo5 = item.CCATA_NOMBRE.ToString(),
                            Campo6 = item.ESTADO_TEXTO?.ToString(),
                            Campo7 = Convert.ToDateTime(item.FECHA_MODIFICACION).ToString("dd/MM/yyyy hh:mm tt"),
                            Campo8 = item.USUARIO_RESPONSABLE?.ToString(),
                        };
                        objReporte.lstDetalle.Add(itemReporte);
                    }
                    #endregion
                    #endregion

                    if (filtro.Estado == 2)
                    {
                        titulos = "LISTADO DE MANTENIMIENTO - CATÁLOGO DETALLE (ELIMINADOS)";
                    }
                    else
                    {
                        titulos = "LISTADO DE MANTENIMIENTO - CATÁLOGO DETALLE";
                    }
                    var objDatosReporte = new DatosReporteGenericoDto

                    {

                        titulo = titulos,

                        reporteGenericoDto = objReporte,
                        tipo = Tipo
                    };

                    var resReporte = _reportesAplicacion.ReporteGenerico(objDatosReporte);
                    if (resReporte != null)
                    {
                        respuesta.success = true;
                        respuesta.data = resReporte;
                    }
                    else
                    {
                        respuesta.success = false;
                    }
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "No se han encontrado registros"));
                    respuesta.success = false;
                }
            }
            catch (Exception ex)
            {
                respuesta.validations.Add(new GenericMessage("error", ex.Message));
                respuesta.success = false;
            }
            return respuesta;
        }
    }
}
