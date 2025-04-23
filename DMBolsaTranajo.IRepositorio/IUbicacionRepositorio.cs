using DMBolsaTrabajo.Dominio;

namespace DMBolsaTrabajo.IRepositorio
{
    public interface IUbicacionRepositorio
    {
        Task<List<EEventoCombo>> ListarDepartamentoCmb(EEventoFiltro request);
        Task<List<EEventoCombo>> ListarDistritoCmb(EEventoFiltro request);
    }
}
