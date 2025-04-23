using AutoMapper;
using DMBolsaTrabajo.Dto.Menu;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.IRepositorio;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.Aplicacion
{
    public class MenuAplicacion : IMenuAplicacion
    {
        private readonly IMenuRepositorio _MenuRepositorio;
        private readonly IMapper _mapper;

        public MenuAplicacion(IMenuRepositorio MenuRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _MenuRepositorio = MenuRepositorio;
        }

        public async Task<Respuesta> ListarPorIdOrigen(int IdOrigen)
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _MenuRepositorio.ListarPorIdOrigen(IdOrigen);

                if (resultado.Count > 0)
                {
                    respuesta.data = _mapper.Map<List<MenuResponseDto>>(resultado);
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

        public async Task<Respuesta> Listar(int idApp)
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _MenuRepositorio.Listar(idApp);

                if (resultado.Count > 0)
                {
                    respuesta.data = _mapper.Map<List<MenuResponseDto>>(resultado);
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

        public async Task<Respuesta> ListarMenuPermisos(int idRol, int idApp)
        {
            var respuesta = new Respuesta();
            try
            {
                var resultado = await _MenuRepositorio.ListarMenuPermisos(idRol, idApp);

                if (resultado.Count > 0)
                {
                    respuesta.data = _mapper.Map<List<MenuPermisosDto>>(resultado);
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
