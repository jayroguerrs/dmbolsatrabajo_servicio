using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Formularios;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.IRepositorio;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DMBolsaTrabajo.Aplicacion
{
    public class FormulariosAplicacion : IFormulariosAplicacion
    {
        private readonly IFormulariosRepositorio _formulariosRepositorio;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public FormulariosAplicacion(IFormulariosRepositorio repositorio, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _formulariosRepositorio = repositorio;
            _configuration = configuration;
        }

        public async Task<Respuesta> EnviarFormularios(FormulariosRequestDto request)
        {
            var respuesta = new Respuesta();
            try
            {
                var eFormularios = _mapper.Map<EFormulario>(request);
                var resultado = await _formulariosRepositorio.EnviarFormularios(eFormularios);

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

        public async Task<Respuesta> ValidarFormularios(int id)
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _formulariosRepositorio.ValidarFormularios(id);

                if (resultado == 1)
                {
                    //respuesta.data = resultado;
                    respuesta.validations.Add(new GenericMessage("warn", "Perfecto, prosiga"));
                    respuesta.success = true;
                }
                else
                {
                    respuesta.validations.Add(new GenericMessage("warn", "Ya existe una formularios realizada"));
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

        public async Task<Respuesta> ListarPaginado(FormularioRequestPorFiltroDto request)
        {

            var respuesta = new Respuesta();
            try
            {

                var eFiltro = _mapper.Map<EFormularioFiltro>(request);
                var resultado = await _formulariosRepositorio.ListarPaginado(eFiltro);

                if (resultado.lstFormularioPaginado.Count > 0)
                {
                    var data = new ListarFormularioResponseDto
                    {
                        NumeroPagina = request.NumeroPagina,
                        TamanioPagina = request.TamanioPagina,
                        lista = _mapper.Map<List<FormularioResponseDto>>(resultado.lstFormularioPaginado),
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

        public async Task<Respuesta> ObtenerDatos(string url)
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _formulariosRepositorio.ObtenerDatos(url);

                if (resultado != null)
                {
                    respuesta.data = _mapper.Map<FormularioInicialDto>(resultado);
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
        
        public async Task<Respuesta> EnviarFormulario (List<IFormFile> archivos, ListaRespuestaRequestDto request)
        {
            var respuesta = new Respuesta();

            try
            {
                
                var eFormulario = _mapper.Map<EListaFormularioRespuesta>(request);
                var (resultado, msj) = await _formulariosRepositorio.EnviarFormulario(eFormulario);

                if (resultado > 0)
                {
                    // Leer la ruta base desde la configuración
                    string rutaBase = _configuration["RutaArchivos"];

                    if (!Directory.Exists(rutaBase))
                    {
                        Directory.CreateDirectory(rutaBase);
                    }

                    // Crear carpeta del formulario usando el ID del formulario
                    string rutaFormulario = Path.Combine(rutaBase, resultado.ToString());

                    if (!Directory.Exists(rutaFormulario))
                    {
                        Directory.CreateDirectory(rutaFormulario);
                    }

                    // Guardar los archivos en las carpetas correspondientes
                    foreach (var archivo in archivos)
                    {
                        // Obtener el PreguntaId del nombre del archivo (asumiendo que el nombre del archivo contiene el PreguntaId)
                        // Ejemplo: "pregunta_archivo_2.txt"
                        //var nombreArchivo = archivo.FileName;
                        var nombreArchivoSinExtension = Path.GetFileNameWithoutExtension(archivo.FileName);
                        var extensionArchivo = Path.GetExtension(archivo.FileName);

                        var partesNombre = nombreArchivoSinExtension.Split('_');

                        // Obtener el último elemento (PreguntaId)
                        if (partesNombre.Length > 0 && int.TryParse(partesNombre[^1], out int preguntaId))
                        {
                            // Crear carpeta de la pregunta
                            string rutaPregunta = Path.Combine(rutaFormulario, preguntaId.ToString());

                            if (!Directory.Exists(rutaPregunta))
                            {
                                Directory.CreateDirectory(rutaPregunta);
                            }

                            // Generar el nuevo nombre del archivo (sin el último "_" y el número, pero con la extensión original)
                            string nuevoNombreArchivo = string.Join("_", partesNombre.Take(partesNombre.Length - 1)) + extensionArchivo;

                            // Guardar el archivo en la carpeta de la pregunta
                            var rutaArchivo = Path.Combine(rutaPregunta, nuevoNombreArchivo);
                            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                            {
                                await archivo.CopyToAsync(stream);
                            }
                        }
                        else
                        {
                            // Si no se puede obtener el PreguntaId, guardar en la carpeta del formulario
                            var rutaArchivo = Path.Combine(rutaFormulario, archivo.FileName);
                            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                            {
                                await archivo.CopyToAsync(stream);
                            }
                        }
                    }

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
