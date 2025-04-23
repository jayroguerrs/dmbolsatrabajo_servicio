using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Reportes;
using DMBolsaTrabajo.Dto.Usuario;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.IRepositorio;
using DMBolsaTrabajo.ServiciosExt.EnviarCorreo;
using DMBolsaTrabajo.ServiciosExt;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using Microsoft.Extensions.Configuration;

namespace DMBolsaTrabajo.Aplicacion
{
    public class UsuarioAplicacion : IUsuarioAplicacion
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IServicioEnviarCorreo _servicioEnviarCorreo;
        private readonly IReportesAplicacion _reportesAplicacion;

        public UsuarioAplicacion(IUsuarioRepositorio repositorio, IMapper mapper, IConfiguration configuration, IServicioEnviarCorreo servicioEnviarCorreo, IReportesAplicacion reportesAplicacion)
        {
            _mapper = mapper;
            _repositorio = repositorio;
            _configuration = configuration;
            _servicioEnviarCorreo = servicioEnviarCorreo;
            _reportesAplicacion = reportesAplicacion;
        }

        public async Task<Respuesta> ObtenerCargaInicial(int idUsuario)
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _repositorio.ObtenerCargaInicial(idUsuario);

                if (resultado != null)
                {
                    respuesta.data = _mapper.Map<UsuarioInicialDto>(resultado);
                    respuesta.success = true;
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "No se han encontrado el registro"));
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

        public async Task<Respuesta> ObtenerDatos(int idUsuario)
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _repositorio.ObtenerDatos(idUsuario);

                if (resultado != null)
                {
                    respuesta.data = _mapper.Map<UsuarioResponseDto>(resultado);
                    respuesta.success = true;
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "No se han encontrado el registro"));
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

        public async Task<Respuesta> ObtenerDatosRolUsuario(int idRolUsuario)
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _repositorio.ObtenerDatosRolUsuario(idRolUsuario);

                if (resultado != null)
                {
                    respuesta.data = _mapper.Map<UsuarioRolResponseDto>(resultado);
                    respuesta.success = true;
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "No se han encontrado el registro"));
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

        public async Task<Respuesta> ActualizarDatos(UsuarioRequestDto request)
        {
            var respuesta = new Respuesta();
            try
            {
                var eUsuario = _mapper.Map<EUsuarioAct>(request);
                var (resultado, msj) = await _repositorio.ActualizarDatos(eUsuario);

                if (resultado > 0)
                {
                    respuesta.data = resultado;
                    respuesta.success = true;
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "No se han encontrado el registro"));
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

        public async Task<Respuesta> AsociarRol(UsuarioAsociarRolDto request)
        {
            var respuesta = new Respuesta();
            try
            {
                var eUsuario = _mapper.Map<EAsociarRol>(request);
                var (resultado, msj) = await _repositorio.AsociarRol(eUsuario);

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

        public async Task<Respuesta> Insertar(UsuarioInsUpdDto request)
        {
            var respuesta = new Respuesta();
            try
            {
                var eUsuario = _mapper.Map<EUsuarioInsUpd>(request);
                var (resultado, msj) = await _repositorio.Insertar(eUsuario);

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

        public async Task<Respuesta> Eliminar(UsuarioDelDto request)
        {
            var respuesta = new Respuesta();
            try
            {
                var eUsuario = _mapper.Map<EUsuarioDel>(request);
                var (resultado, msj) = await _repositorio.Eliminar(eUsuario);

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

        public async Task<Respuesta> ObtenerRolMenuPorUsuario(int idUsuario, int idRol)
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _repositorio.ObtenerRolMenuPorUsuario(idUsuario, idRol);

                if (resultado != null)
                {
                    respuesta.data = _mapper.Map<UsuarioRolMenuInicialDto>(resultado);
                    respuesta.success = true;
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "No se han encontrado el registro"));
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

        public async Task<Respuesta> ObtenerMenuPorRol(int idRol)
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _repositorio.ObtenerMenuPorRol(idRol);

                if (resultado != null)
                {
                    respuesta.data = _mapper.Map<List<UsuarioMenuDto>>(resultado);
                    respuesta.success = true;
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "No se han encontrado el registro"));
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

        public async Task<Respuesta> ListarPaginado(UsuarioFilterRequestDto request)
        {

            var respuesta = new Respuesta();
            try
            {

                var eUsuarioFiltro = _mapper.Map<EUsuarioFiltro>(request);
                var resultado = await _repositorio.ListarPaginado(eUsuarioFiltro);

                if (resultado.lstUsuarioPaginado.Count > 0)
                {
                    var data = new ListarUsuarioResponseDto
                    {
                        NumeroPagina = request.NumeroPagina,
                        TamanioPagina = request.TamanioPagina,
                        lista = _mapper.Map<List<UsuarioResponseDto>>(resultado.lstUsuarioPaginado),
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

        public async Task<Respuesta> EnviarCorreoConfirmacion(int idUsuario)
        {
            var respuesta = new Respuesta();
            try
            {
                ECorreoElectronico eSolicitudAccesoCfm = await _repositorio.ObtenerConfirmacionPorId(idUsuario);

                if (eSolicitudAccesoCfm != null)
                {
                    if (eSolicitudAccesoCfm.ESTADO == 1)
                    {
                        MessageSendGridDto message = _mapper.Map<MessageSendGridDto>(eSolicitudAccesoCfm);
                        var resul = await _servicioEnviarCorreo.EnviarCorreo(message);
                        if (resul.success)
                        {
                            respuesta.data = eSolicitudAccesoCfm;
                            respuesta.success = true;
                        }
                        else
                        {
                            respuesta.validations.Add(new GenericMessage("warn", "No fue posible enviar correo"));
                            respuesta.success = false;
                        }

                    }
                    else
                    {
                        respuesta.validations.Add(new GenericMessage("warn", eSolicitudAccesoCfm.MSG));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtro">Nombre: String, Estado: Int</param>
        /// <param name="Tipo">1: Excel, 2:PDF</param>
        /// <returns></returns>
        public async Task<Respuesta> GenerarReporte(UsuarioFilterRequestDto filtro, int Tipo)
        {
            var titulos = "";
            var respuesta = new Respuesta();
            try
            {
                var eProcesoFiltro = _mapper.Map<EUsuarioFiltro>(filtro);
                var resultado = await _repositorio.Listar(eProcesoFiltro);

                if (resultado.Count > 0)
                {
                    #region Armar Reporte
                    var objReporte = new ReporteGenericoDto();

                    #region Encabezado
                    if (objReporte.Encabezado1 != null)
                        objReporte.Encabezado1.Nombre = "N°";
                    if (objReporte.Encabezado2 != null)
                        objReporte.Encabezado2.Nombre = "APELLIDOS Y NOMBRES";
                        objReporte.Encabezado2.Ancho = 40.71;
                    if (objReporte.Encabezado3 != null)
                        objReporte.Encabezado3.Nombre = "ROL";
                        objReporte.Encabezado3.Ancho = 10.71;
                    if (objReporte.Encabezado4 != null)
                        objReporte.Encabezado4.Nombre = "CORREO";
                        objReporte.Encabezado4.Ancho = 25.15;
                    if (objReporte.Encabezado5 != null)
                        objReporte.Encabezado5.Nombre = "ESTADO";
                    if (objReporte.Encabezado6 != null)
                        objReporte.Encabezado6.Nombre = "MODIFICADO";
                    if (objReporte.Encabezado7 != null)
                        objReporte.Encabezado7.Nombre = "MODIFICADO POR";
                    #endregion
                    objReporte.lstDetalle = new List<MantenimientoDetalleDto>();
                    #region Detalle
                    foreach (var item in resultado)
                    {
                        var itemReporte = new MantenimientoDetalleDto
                        {
                            Campo1 = item.NUMERO.ToString(),
                            Campo2 = item.CPERS_APE_PATERNO.ToString() + " " + item.CPERS_APE_MATERNO.ToString() + ", " + item.CPERS_NOMBRES.ToString(),
                            Campo3 = item.CROLE_NOMBRE.ToString(),
                            Campo4 = item.CPERS_CORREO.ToString(),
                            Campo5 = item.ESTADO_TEXTO?.ToString(),
                            Campo6 = Convert.ToDateTime(item.DAUDI_REG_UPD).ToString("dd/MM/yyyy hh:mm tt"),
                            Campo7 = item.USUARIO_RESPONSABLE?.ToString(),
                        };
                        objReporte.lstDetalle.Add(itemReporte);
                    }
                    #endregion
                    #endregion

                    if (filtro.Estado == 2)
                    {
                        titulos = "LISTADO DE MANTENIMIENTO - USUARIOS (ELIMINADOS)";
                    }
                    else
                    {
                        titulos = "LISTADO DE MANTENIMIENTO - USUARIOS";
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
