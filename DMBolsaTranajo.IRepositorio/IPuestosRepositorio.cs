using DMBolsaTrabajo.Dominio;

namespace DMBolsaTrabajo.IRepositorio
{
    public interface IPuestosRepositorio
    {
        Task<EPuestosListaPaginado> ListarPaginado(EPuestosFiltro filtro);
        Task<EPuestosLista> ObtenerPorId(int Id);
        Task<(int, string)> Postular(EPostularInsUpd filtro);
    }
}
