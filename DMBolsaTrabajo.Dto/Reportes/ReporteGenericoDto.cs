namespace DMBolsaTrabajo.Dto.Reportes.Generico
{
    public class DatosReporteGenericoDto
    {
        public string titulo { get; set; }
        public string? subtitulo { get; set; }
        public string? nombreHoja { get; set; }
        public int tipo { get; set; }
        public ReporteGenericoDto reporteGenericoDto { get; set; }
        public string? origen { get; set; }

        public DatosReporteGenericoDto()
        {
            reporteGenericoDto = new ReporteGenericoDto();
            titulo = string.Empty;
            origen = null;
        }
    }

    public class ReporteGenericoDto
    {
        public List<ItemEncabezado> lstCabecera { get; set; }
        public List<string> lstDetalle { get; set; }
    }

    public class ItemEncabezado
    {
        public string Nombre { get; set; }
        public double Ancho { get; set; }

        public ItemEncabezado()
        {
            Nombre = "";
            Ancho = 0;
        }
    }
}
