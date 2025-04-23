using DMBolsaTrabajo.Dominio;

namespace DMBolsaTrabajo.IRepositorio
{
    public interface IEventoRepositorio
    {
        Task<List<EEventoCombo>> ListarCmb(EEventoFiltro request);
    }
}
