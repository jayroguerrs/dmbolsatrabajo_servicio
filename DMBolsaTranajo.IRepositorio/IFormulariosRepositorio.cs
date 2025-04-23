using DMBolsaTrabajo.Dominio;

namespace DMBolsaTrabajo.IRepositorio
{
    public interface IFormulariosRepositorio
    {
        Task<int> EnviarFormularios(EFormulario request);
        Task<int> ValidarFormularios(int id);
        Task<EFormularioListaPaginado> ListarPaginado(EFormularioFiltro eParam);
        Task<EFormularioInicial> ObtenerDatos(string url);
        Task<(int, string)> EnviarFormulario(EListaFormularioRespuesta request);
    }
}