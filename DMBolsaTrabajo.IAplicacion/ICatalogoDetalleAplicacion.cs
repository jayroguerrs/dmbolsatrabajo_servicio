using DMBolsaTrabajo.Dto.CatalogoDetalle;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.IAplicacion
{
    public interface ICatalogoDetalleAplicacion
    {
        Task<Respuesta> Listar(int IdCatalogo);
        Task<Respuesta> ListarCatalogo();
        Task<Respuesta> ListarPaginado(CatalogoDetalleRequestPorFiltroDto request);
        Task<Respuesta> ObtenerPorId(int Id);
        Task<Respuesta> Insertar(CatalogoDetalleInsUpdDto request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtro">Nombre: String, Estado: Int</param>
        /// <param name="Tipo">1: Excel, 2:PDF</param>
        /// <returns></returns>
        Task<Respuesta> GenerarReporte(CatalogoDetalleRequestPorFiltroDto filtro, int Tipo);
    }
}
