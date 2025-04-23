using DMBolsaTrabajo.Dominio;

namespace DMBolsaTrabajo.IRepositorio
{
    public interface ICatalogoDetalleRepositorio
    {
        Task<List<ECatalogoDetalleResponsePorId>> Listar(ECatalogoDetallePorIdCatalogo eParam);
        Task<List<ECatalogoResponse>> ListarCatalogo();
        Task<ECatalogoDetalleListaPaginado> ListarPaginado(ECatalogoDetalleFiltro eParam);
        Task<ECatalogoDetalleResponsePorId> ObtenerPorId(int Id);
        Task<(int, string)> Insertar(ECatalogoDetalleResponsePorId eParam);
        Task<List<ECatalogoDetalle>> Listar(ECatalogoDetalleFiltro filtro);
    }
}
