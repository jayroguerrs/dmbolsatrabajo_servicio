using DMBolsaTrabajo.Dominio;

namespace DMBolsaTrabajo.IRepositorio
{
    public interface IPuestosRepositorio
    {
        Task<EPuestosListaPaginado> ListarPaginado(EPuestosFiltro filtro);
        Task<EPostulantesListaPaginado> ListarPostulantesPaginado(EPostulantesFiltro filtro);
        Task<EPuestosResponseId> ObtenerPorId(int Id);
        Task<(int, string)> Postular(EPostularInsUpd filtro);
        Task<(int, string)> Eliminar(EPuestosDel request);
        Task<(int, string)> Insertar(EPuestosInsUpd request);
        Task<(int, string)> CambiarEstado(EEstadoCambio request);
    }
}
