using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Rol;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.IRepositorio;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.Aplicacion
{
    public class EventoAplicacion : IEventoAplicacion
    {
        private readonly IEventoRepositorio _EventoRepositorio;
        private readonly IMapper _mapper;

        public EventoAplicacion(IEventoRepositorio EventoRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _EventoRepositorio = EventoRepositorio;
        }

        public async Task<Respuesta> ListarCmb(EventoFiltroRequestDto request)
        {
            var respuesta = new Respuesta();
            try
            {
                var eEventoFiltro = _mapper.Map<EEventoFiltro>(request);
                var resultado = await _EventoRepositorio.ListarCmb(eEventoFiltro);

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