using AutoMapper;
using DMBolsaTrabajo.Dto.Seguridad;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.IRepositorio;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using Microsoft.Extensions.Configuration;
using DMBolsaTrabajo.ServiciosExt.EnviarCorreo;
using DMBolsaTrabajo.ServiciosExt;

namespace DMBolsaTrabajo.Aplicacion
{
    public class SeguridadAplicacion : ISeguridadAplicacion
    {
        private readonly ISeguridadRepositorio _SeguridadRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IServicioEnviarCorreo _servicioEnviarCorreo;

        public SeguridadAplicacion(ISeguridadRepositorio SeguridadRepository, IMapper mapper, IConfiguration configuration, IServicioEnviarCorreo servicioEnviarCorreo)
        {
            _mapper = mapper;
            _SeguridadRepository = SeguridadRepository;
            _configuration = configuration;
            _servicioEnviarCorreo = servicioEnviarCorreo;
        }
        public async Task<Respuesta> IniciarSesion(SeguridadRequestDto seccionBusDto)
        {
            var respuesta = new Respuesta();
            try
            {

                var eSeguridad = _mapper.Map<EUsuarioLoginFiltro>(seccionBusDto);

                var resultado = await _SeguridadRepository.IniciarSesion(eSeguridad);

                if (resultado != null)
                {
                    var resultMaper = _mapper.Map<SeguridadResponseDto>(resultado);
                    if (resultMaper.Id <= 0)
                    {
                        respuesta.validations.Add(new GenericMessage("warn", "Usuario no cuenta con rol asignado"));
                        respuesta.success = false;
                    }
                    else
                    {
                        respuesta.data = resultMaper;
                        respuesta.success = true;
                    }

                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "Usuario o contraseña incorrecta"));
                    respuesta.success = false;
                }

            }
            catch (Exception ex)
            {
                //LogDeError.GestorError(ex, "Seguridad Application");
                respuesta.validations.Add(new GenericMessage("error", ex.Message));
                respuesta.success = false;
            }
            return respuesta;
        }

        public async Task<Respuesta> EnviarCorreoRecuperacion(string email, string url)
        {
            var respuesta = new Respuesta();
            try
            {
                ECorreoElectronico eSolicitudAccesoCfm = await _SeguridadRepository.EnviarCorreoRecuperacion(email, url);

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
                //LogDeError.GestorError(ex, "Seguridad Application");
                respuesta.validations.Add(new GenericMessage("error", ex.Message));
                respuesta.success = false;
            }
            return respuesta;
        }

        public async Task<Respuesta> ValidarCredencialesRestablecer(int IdUsuario, string Token)
        {
            var respuesta = new Respuesta();
            try
            {
                var (resultado, msj) = await _SeguridadRepository.ValidarCredencialesRestablecer(IdUsuario, Token);

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

        public async Task<Respuesta> CambiarClave(ClaveRequestDto request)
        {
            var respuesta = new Respuesta();
            try
            {
                var eClave = _mapper.Map<EClave>(request);
                var (resultado, msj) = await _SeguridadRepository.CambiarClave(eClave);

                if (resultado == 1)
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

        public async Task<Respuesta> CambiarClaveNoAuth(ClaveRequestDto request)
        {
            var respuesta = new Respuesta();
            try
            {
                var eClave = _mapper.Map<EClave>(request);
                var (resultado, msj) = await _SeguridadRepository.CambiarClaveNoAuth(eClave);

                if (resultado == 1)
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
    }
}