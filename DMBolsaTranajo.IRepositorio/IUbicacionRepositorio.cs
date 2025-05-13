using DMBolsaTrabajo.Dominio;

namespace DMBolsaTrabajo.IRepositorio
{
    public interface IUbicacionRepositorio
    {
        Task<List<EDepartamentoCombo>> ListarDepartamentoCmb(EDepartamentoFiltro request);
        Task<List<EDistritoCombo>> ListarDistritoCmb(EDistritoFiltro request);
    }
}
