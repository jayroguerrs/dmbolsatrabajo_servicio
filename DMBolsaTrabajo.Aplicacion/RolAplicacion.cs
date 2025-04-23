using AutoMapper;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Dto.Rol;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.IRepositorio;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.Aplicacion
{
    public class RolAplicacion : IRolAplicacion
    {
        private readonly IRolRepositorio _RolRepositorio;
        private readonly IMapper _mapper;

        public RolAplicacion(IRolRepositorio RolRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _RolRepositorio = RolRepositorio;
        }

        public async Task<Respuesta> ListarCmb(RolFiltroRequestDto request)
        {
            var respuesta = new Respuesta();
            try
            {
                var eRolFiltro = _mapper.Map<ERolFiltro>(request);
                var resultado = await _RolRepositorio.ListarCmb(eRolFiltro);

                if (resultado.Count > 0)
                {
                    respuesta.data = _mapper.Map<List<RolComboResponseDto>>(resultado);
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
