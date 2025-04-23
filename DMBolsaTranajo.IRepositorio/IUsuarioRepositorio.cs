using DMBolsaTrabajo.Dominio;

namespace DMBolsaTrabajo.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        Task<EUsuarioInicial> ObtenerCargaInicial(int idUsuario);
        Task<EUsuario> ObtenerDatos(int idUsuario);
        Task<EUsuarioRol> ObtenerDatosRolUsuario(int idRolUsuario);
        Task<(int, string)> ActualizarDatos(EUsuarioAct request);
        Task<(int, string)> AsociarRol(EAsociarRol request);
        Task<(int, string)> Insertar(EUsuarioInsUpd request);
        Task<(int, string)> Eliminar(EUsuarioDel request);
        Task<EUsuarioRolMenuInicial> ObtenerRolMenuPorUsuario(int idUsuario, int idRol);
        Task<List<EUsuarioMenu>> ObtenerMenuPorRol(int idRol);
        Task<EUsuarioListaPaginado> ListarPaginado(EUsuarioFiltro filtro);
        Task<ECorreoElectronico> ObtenerConfirmacionPorId(int idUsuario);
        Task<List<EUsuarioRolLista>> Listar(EUsuarioFiltro filtro);
    }
}
