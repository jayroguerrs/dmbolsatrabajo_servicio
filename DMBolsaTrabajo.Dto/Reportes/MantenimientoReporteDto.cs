namespace DMBolsaTrabajo.Dto.Reportes
{
    public class DatosReporteGenericoDto
    {
        public string titulo { get; set; }
        public string? subtitulo { get; set; }
        public string? nombreHoja { get; set; }
        public int tipo { get; set; }
        public ReporteGenericoDto reporteGenericoDto { get; set; }

        public DatosReporteGenericoDto()
        {
            reporteGenericoDto = new ReporteGenericoDto();
            titulo = string.Empty;
        }
    }

    public class ReporteGenericoDto
    {
        public ItemEncabezado? Encabezado1 { get; set; }
        public ItemEncabezado? Encabezado2 { get; set; }
        public ItemEncabezado? Encabezado3 { get; set; }
        public ItemEncabezado? Encabezado4 { get; set; }
        public ItemEncabezado? Encabezado5 { get; set; }
        public ItemEncabezado? Encabezado6 { get; set; }
        public ItemEncabezado? Encabezado7 { get; set; }
        public ItemEncabezado? Encabezado8 { get; set; }
        public ItemEncabezado? Encabezado9 { get; set; }
        public ItemEncabezado? Encabezado10 { get; set; }
        public ItemEncabezado? Encabezado11 { get; set; }
        public ItemEncabezado? Encabezado12 { get; set; }
        public ItemEncabezado? Encabezado13 { get; set; }
        public ItemEncabezado? Encabezado14 { get; set; }
        public ItemEncabezado? Encabezado15 { get; set; }
        public ItemEncabezado? Encabezado16 { get; set; }

        public List<MantenimientoDetalleDto> lstDetalle { get; set; }
        public ReporteGenericoDto()
        {
            Encabezado1 = new ItemEncabezado() { Ancho = 4.71 };
            Encabezado2 = new ItemEncabezado() { Ancho = 15.71 };
            Encabezado3 = new ItemEncabezado() { Ancho = 15.71 };
            Encabezado4 = new ItemEncabezado() { Ancho = 40.71 };
            Encabezado5 = new ItemEncabezado() { Ancho = 15.71 };
            Encabezado6 = new ItemEncabezado() { Ancho = 25.71 };
            Encabezado7 = new ItemEncabezado() { Ancho = 25.71 };
            Encabezado8 = new ItemEncabezado() { Ancho = 15.71 };
            Encabezado9 = new ItemEncabezado() { Ancho = 15.71 };
            Encabezado10 = new ItemEncabezado() { Ancho = 15.71 };
            Encabezado11 = new ItemEncabezado() { Ancho = 15.71 };
            Encabezado12 = new ItemEncabezado() { Ancho = 15.71 };
            Encabezado13 = new ItemEncabezado() { Ancho = 15.71 };
            Encabezado14 = new ItemEncabezado() { Ancho = 15.71 };
            Encabezado15 = new ItemEncabezado() { Ancho = 15.71 };
            Encabezado16 = new ItemEncabezado() { Ancho = 15.71 };

        }

        public List<ItemEncabezado?> ListarEncabezados()
        {
            List<ItemEncabezado?> Rspta = new List<ItemEncabezado?>();
            Rspta.Add(Encabezado1);
            Rspta.Add(Encabezado2);
            Rspta.Add(Encabezado3);
            Rspta.Add(Encabezado4);
            Rspta.Add(Encabezado5);
            Rspta.Add(Encabezado6);
            Rspta.Add(Encabezado7);
            Rspta.Add(Encabezado8);
            Rspta.Add(Encabezado9);
            Rspta.Add(Encabezado10);
            Rspta.Add(Encabezado11);
            Rspta.Add(Encabezado12);
            Rspta.Add(Encabezado13);
            Rspta.Add(Encabezado14);
            Rspta.Add(Encabezado15);
            Rspta.Add(Encabezado16);

            return Rspta;
        }
    }

    public class MantenimientoDetalleDto
    {
        public string Campo1 { get; set; }
        public string Campo2 { get; set; }
        public string Campo3 { get; set; }
        public string Campo4 { get; set; }
        public string Campo5 { get; set; }
        public string Campo6 { get; set; }
        public string Campo7 { get; set; }
        public string Campo8 { get; set; }
        public string Campo9 { get; set; }
        public string Campo10 { get; set; }
        public string Campo11 { get; set; }
        public string Campo12 { get; set; }
        public string Campo13 { get; set; }
        public string Campo14 { get; set; }
        public string Campo15 { get; set; }
        public string Campo16 { get; set; }


        public MantenimientoDetalleDto()
        {
            Campo1 = "";
            Campo2 = "";
            Campo3 = "";
            Campo4 = "";
            Campo5 = "";
            Campo6 = "";
            Campo7 = "";
            Campo8 = "";
            Campo9 = "";
            Campo10 = "";
            Campo11 = "";
            Campo12 = "";
            Campo13 = "";
            Campo14 = "";
            Campo15 = "";
            Campo16 = "";
        }
        public List<string> ListarCampos()
        {
            List<string> Rspta = new List<string>();
            Rspta.Add(Campo1);
            Rspta.Add(Campo2);
            Rspta.Add(Campo3);
            Rspta.Add(Campo4);
            Rspta.Add(Campo5);
            Rspta.Add(Campo6);
            Rspta.Add(Campo7);
            Rspta.Add(Campo8);
            Rspta.Add(Campo9);
            Rspta.Add(Campo10);
            Rspta.Add(Campo11);
            Rspta.Add(Campo12);
            Rspta.Add(Campo13);
            Rspta.Add(Campo14);
            Rspta.Add(Campo15);
            Rspta.Add(Campo16);

            return Rspta;
        }
    }

    public class ItemEncabezado
    {
        public string Nombre { get; set; }
        public double Ancho { get; set; }

        public ItemEncabezado()
        {
            Nombre = "";
        }
    }
}
