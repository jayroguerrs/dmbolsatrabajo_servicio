using DMBolsaTrabajo.Dominio;

namespace DMBolsaTrabajo.IRepositorio
{
    public interface IMenuRepositorio
    {
        Task<List<EMenu>> ListarPorIdOrigen(int IdOrigen);
        Task<List<EMenu>> Listar(int idApp);
        Task<List<ERolMenuPermisos>> ListarMenuPermisos(int idRol, int idApp);
    }
}
