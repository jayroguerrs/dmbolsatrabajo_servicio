using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Rol;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.IRepositorio;
using DMBolsaTrabajo.Repositorio;
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
                var eEventoFiltro = _mapper.Map<EEventoFiltro>(request);
                var resultado = await _UbicacionRepositorio.ListarDepartamentoCmb(eEventoFiltro);

                if (resultado.Count > 0)
                {
                    respuesta.data = _mapper.Map<List<EventoComboResponseDto>>(resultado);
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
                var eEventoFiltro = _mapper.Map<EEventoFiltro>(request);
                var resultado = await _UbicacionRepositorio.ListarDistritoCmb(eEventoFiltro);

                if (resultado.Count > 0)
                {
                    respuesta.data = _mapper.Map<List<EventoComboResponseDto>>(resultado);
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
