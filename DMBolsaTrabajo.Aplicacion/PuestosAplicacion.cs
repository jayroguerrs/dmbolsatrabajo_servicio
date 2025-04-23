using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Puestos;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.IRepositorio;
using DMBolsaTrabajo.ServiciosExt;
using DMBolsaTrabajo.ServiciosExt.Recaptcha;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DMBolsaTrabajo.Aplicacion
{
    public class PuestosAplicacion : IPuestosAplicacion
    {
        private readonly IPuestosRepositorio _repositorio;
        private readonly IMapper _mapper;
        private readonly IRecaptchaService _recaptchaService;
        private readonly IConfiguration _configuration;

        public PuestosAplicacion(IPuestosRepositorio repositorio, IMapper mapper, IRecaptchaService recaptchaService, IConfiguration configuration)
        {
            _mapper = mapper;
            _repositorio = repositorio;
            _recaptchaService = recaptchaService;
            _configuration = configuration;
        }

        public async Task<Respuesta> ListarPaginado(PuestosFilterRequestDto request)
        {
            var respuesta = new Respuesta();

            try
            {
                var recaptchaResponse = await _recaptchaService.ValidateRecaptchaWithExternalService(request.RecaptchaToken);
                if (recaptchaResponse.Success)
                {
                    var eUsuarioFiltro = _mapper.Map<EPuestosFiltro>(request);
                    var resultado = await _repositorio.ListarPaginado(eUsuarioFiltro);

                    if (resultado.lstPuestosPaginado.Count >= 0)
                    {
                        var data = new ListarPuestosResponseDto
                        {
                            NumeroPagina = request.NumeroPagina,
                            TamanioPagina = request.TamanioPagina,
                            lista = _mapper.Map<List<PuestosResponseDto>>(resultado.lstPuestosPaginado),
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
                } else
                {
                    respuesta.validations.Add(new GenericMessage("error", "Error en la validación del reCAPTCHA"));
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

        public async Task<Respuesta> ListarPaginadoNoCaptcha(PuestosFilterNoCaptchaRequestDto request)
        {
            var respuesta = new Respuesta();

            try
            {
                var eUsuarioFiltro = _mapper.Map<EPuestosFiltro>(request);
                var resultado = await _repositorio.ListarPaginado(eUsuarioFiltro);

                if (resultado.lstPuestosPaginado.Count >= 0)
                {
                    var data = new ListarPuestosResponseDto
                    {
                        NumeroPagina = request.NumeroPagina,
                        TamanioPagina = request.TamanioPagina,
                        lista = _mapper.Map<List<PuestosResponseDto>>(resultado.lstPuestosPaginado),
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

        public async Task<Respuesta> ObtenerPorId(int id)
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _repositorio.ObtenerPorId(id);
                if (resultado != null)
                {
                    respuesta.data = _mapper.Map<PuestosResponseDto>(resultado);
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

        public async Task<Respuesta> Postular(IFormFile archivo, PostularInsUpdDto request)
        {
            string archivoUrl = "";
            string filePath = null;

            var respuesta = new Respuesta();
            try
            {
                var recaptchaResponse = await _recaptchaService.ValidateRecaptchaWithExternalService(request.RecaptchaToken);
                if (recaptchaResponse.Success)
                {
                    // Guardar la imagen de manera temporal
                    if (archivo != null && archivo.Length > 0)
                    {
                        // Validaciones de imagen
                        var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
                        var fileExtension = Path.GetExtension(archivo.FileName).ToLower();

                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            respuesta.validations.Add(new GenericMessage("warn", "Formato de documento no permitido"));
                            respuesta.success = false;
                            return respuesta;
                        }

                        if (archivo.Length > 6 * 1024 * 1024)
                        {
                            respuesta.validations.Add(new GenericMessage("warn", "El archivo no debe superar los 6MB"));
                            respuesta.success = false;
                            return respuesta;
                        }

                        // Guardar archivo
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Public", "Postulaciones", request.PuestoId.ToString() );

                        // Crear directorio si no existe
                        if (!Directory.Exists(uploadsFolder))
                            Directory.CreateDirectory(uploadsFolder);

                        // Nombre único para el archivo
                        var uniqueFileName = $"{request.NumeroDocumento.ToString()}{fileExtension}";
                        filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Guardar imagen
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await archivo.CopyToAsync(fileStream);
                        }

                        archivoUrl = $"/Postulaciones/{request.PuestoId}/{uniqueFileName}";
                        request.NombreArchivo = archivoUrl;
                    }

                    var ePostularInsUpd = _mapper.Map<EPostularInsUpd>(request);
                    var (resultado, msj) = await _repositorio.Postular(ePostularInsUpd);

                    if (resultado > 0)
                    {
                        // Guardar la imagen de manera temporal
                        respuesta.data = resultado;
                        respuesta.success = true;
                    }
                    else
                    {
                        // Eliminar la imagen temporal en caso de excepción
                        if (filePath != null && File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }

                        respuesta.validations.Add(new GenericMessage("warn", msj));
                        respuesta.success = false;
                    }
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("error", "Error en la validación del reCAPTCHA"));
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
