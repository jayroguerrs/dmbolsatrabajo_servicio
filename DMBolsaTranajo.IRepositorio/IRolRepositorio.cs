using DMBolsaTrabajo.Dominio;

namespace DMBolsaTrabajo.IRepositorio
{
    public interface IRolRepositorio
    {
        Task<List<ERolCombo>> ListarCmb(ERolFiltro request);
    }
}
