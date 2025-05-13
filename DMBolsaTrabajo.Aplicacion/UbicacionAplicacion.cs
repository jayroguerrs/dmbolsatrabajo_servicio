using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Rol;
using DMBolsaTrabajo.Dto.Ubicacion;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.IRepositorio;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.Aplicacion
{
    public class UbicacionAplicacion : IUbicacionAplicacion
    {
        private readonly IUbicacionRepositorio _UbicacionRepositorio;
        private readonly IMapper _mapper;

        public UbicacionAplicacion(IUbicacionRepositorio UbicacionRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _UbicacionRepositorio = UbicacionRepositorio;
        }

        public async Task<Respuesta> ListarDepartamentoCmb(DepartamentoFiltroRequestDto request)
        {
            var respuesta = new Respuesta();
            try
            {
                var eDepaFiltro = _mapper.Map<EDepartamentoFiltro>(request);
                var resultado = await _UbicacionRepositorio.ListarDepartamentoCmb(eDepaFiltro);

                if (resultado.Count > 0)
                {
                    respuesta.data = _mapper.Map<List<DepartamentoResponseDto>>(resultado);
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

        public async Task<Respuesta> ListarDistritoCmb(DistritoFiltroRequestDto request)
        {
            var respuesta = new Respuesta();
            try
            {
                var eDistritoFiltro = _mapper.Map<EDistritoFiltro>(request);
                var resultado = await _UbicacionRepositorio.ListarDistritoCmb(eDistritoFiltro);

                if (resultado.Count > 0)
                {
                    respuesta.data = _mapper.Map<List<DistritoResponseDto>>(resultado);
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
    }
}