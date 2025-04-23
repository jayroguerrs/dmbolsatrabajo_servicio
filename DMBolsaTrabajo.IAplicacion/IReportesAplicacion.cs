using DMBolsaTrabajo.Dto.Reportes;

namespace DMBolsaTrabajo.IAplicacion
{
    public interface IReportesAplicacion
    {
        byte[]? ReporteGenerico(DatosReporteGenericoDto datosReporteGenericoDto);
        byte[] ReporteGenericoExcel(string TituloReporte, string SubTituloReporte, string CodigoUsuario, string Usuario, string? NombreHoja, ReporteGenericoDto mantenimientoReporteDto);
        byte[] ReporteGenericoPDF(string TituloReporte, string SubTituloReporte, string? SubTitulo, string Usuario, string? NombreHoja, ReporteGenericoDto mantenimientoReporteDto);
    }
}
