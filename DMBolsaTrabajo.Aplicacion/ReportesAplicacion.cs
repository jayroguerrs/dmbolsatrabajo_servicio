using AutoMapper;
using DMBolsaTrabajo.IRepositorio;
using DMBolsaTrabajo.Dto.Reportes;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Globalization;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.Aplicacion;

namespace DMBolsaTrabajo.Aplicacion
{
    public class ReportesAplicacion : IReportesAplicacion
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public ReportesAplicacion(IConfiguration configuration, IHttpContextAccessor httpContextAccessor,
            IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tipo">1: Excel, 2: PDF</param>
        /// <returns></returns>
        public byte[]? ReporteGenerico(DatosReporteGenericoDto datosReporteGenericoDto)
        {
            // Acceder a los valores de los claims
            //int IdUsuario = 0;
            var CodigoUsuario = "";
            var Nombre = "";
            var ApellidoPaterno = "";
            var ApellidoMaterno = "";
            var claims = _httpContextAccessor.HttpContext.User.Claims;

            foreach (var claim in claims)
            {
                //if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                //{
                //    IdUsuario = Convert.ToInt32(claim.Value);
                //    var objUsuario = this._usuarioRepositorio.ObtenerPorId(IdUsuario);
                //    CodigoUsuario = objUsuario.Result.CUSUA_COD_USUARIO;
                //} 
                if (claim.Type == "Codigo")
                    CodigoUsuario = claim.Value;
                if (claim.Type == "Nombres")
                    Nombre = claim.Value;
                if (claim.Type == "ApellidoPaterno")
                    ApellidoPaterno = claim.Value;
                if (claim.Type == "ApellidoMaterno")
                    ApellidoMaterno = claim.Value;
            }
            var NombreUsuario = Nombre + " " + ApellidoPaterno + " " + ApellidoMaterno;
            if (datosReporteGenericoDto.tipo == 1)
            {
                return ReporteGenericoExcel(datosReporteGenericoDto.titulo, datosReporteGenericoDto.subtitulo, CodigoUsuario, NombreUsuario, datosReporteGenericoDto.nombreHoja, datosReporteGenericoDto.reporteGenericoDto);

            }
            else if (datosReporteGenericoDto.tipo == 2)
            {
                return ReporteGenericoPDF(datosReporteGenericoDto.titulo, datosReporteGenericoDto.subtitulo, CodigoUsuario, NombreUsuario, datosReporteGenericoDto.nombreHoja, datosReporteGenericoDto.reporteGenericoDto);
            }
            else
            {
                return null;
            }
        }

        public byte[] ReporteGenericoExcel(string TituloReporte, string SubTituloReporte, string CodigoUsuario, string Usuario, string? NombreHoja, ReporteGenericoDto mantenimientoReporteDto)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excel = new ExcelPackage())
            {
                excel.Workbook.Properties.Title = TituloReporte;
                excel.Workbook.Properties.Author = _configuration["Reportes:DatosGenerales:Autor"];
                excel.Workbook.Properties.Company = _configuration["Reportes:DatosGenerales:Compania"];

                NombreHoja = (NombreHoja == null || NombreHoja == "") ? "Data" : NombreHoja;

                var vSubTitulo = "DM DESARROLLO - DERRAMA MAGISTERIAL";
                var vSubTituloDos = SubTituloReporte == null ? string.Empty : SubTituloReporte;
                bool existeSubtituloDos = vSubTituloDos.Length > 0;
                var vFechaDescarga = DateTime.Now.ToString("dd/MM/yyyy - hh:mm tt", CultureInfo.InvariantCulture);

                var wrkReporte = excel.Workbook.Worksheets.Add(NombreHoja);
                var logoDM = ImagenesAplicacion.getLogoArrayByte();
                Stream stream = new MemoryStream(logoDM);

                var picture = wrkReporte.Drawings.AddPicture("PictureName", stream);

                picture.SetPosition(1, 0, 1, 0); // Ajusta la posición y el tamaño de la imagen según tus necesidades
                picture.SetSize(52);
                var columaTitulo = 1;


                var fila = 8;
                if (existeSubtituloDos) fila++;
                var filaInicio = fila;

                var columna = 1;


                var listaEncabezados = mantenimientoReporteDto.ListarEncabezados();
                for (int i = 0; i < listaEncabezados.Count; i++)
                {
                    var encabezado = listaEncabezados[i];

                    if (encabezado != null && encabezado.Nombre.Trim() != "")
                    {
                        wrkReporte.Column(columna).Width = encabezado.Ancho;
                        wrkReporte.Cells[fila, columna].Style.WrapText = true;
                        wrkReporte.Cells[fila, columna].Value = encabezado.Nombre;
                        wrkReporte.Cells[fila, columna].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        /// INICIO DE BORDER
                        wrkReporte.Cells[fila, columna].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        wrkReporte.Cells[fila, columna].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        wrkReporte.Cells[fila, columna].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        wrkReporte.Cells[fila, columna].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        /// FIN DE BORDER
                        columna++;
                        columaTitulo++;
                    }
                }
                if (columna > 1)
                {
                    columna--;
                    columaTitulo--;
                }
                //Estilo de encabezado de tablas
                wrkReporte.Cells[fila, 1, fila, columna].Style.Font.Bold = true;
                wrkReporte.Cells[fila, 1, fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                #region titulos
                var filatit = 1;
                //Estilo de Titulo
                using (ExcelRange Titulo = wrkReporte.Cells[filatit, 1, filatit, columaTitulo])
                {
                    //Estilos
                    Titulo.Merge = true;// Merge:Combinacion de columnas
                    Titulo.Value = string.Format("{0}", TituloReporte);
                    Titulo.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    Titulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Titulo.Style.WrapText = true;
                    Titulo.Style.Font.Size = 12;
                    Titulo.Style.Font.Bold = true;
                }
                filatit++;
                //Estilo de Subtitulo

                using (ExcelRange SubTitulo = wrkReporte.Cells[filatit, 1, filatit, columaTitulo])
                {
                    //Estilos
                    SubTitulo.Merge = true;// Merge:Combinacion de columnas
                    SubTitulo.Value = string.Format("{0}", vSubTitulo);
                    SubTitulo.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    SubTitulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    SubTitulo.Style.WrapText = true;
                    SubTitulo.Style.Font.Size = 11;
                    SubTitulo.Style.Font.Bold = true;

                }
                filatit++;

                //Estilo de SubtituloDos
                if (existeSubtituloDos)
                {
                    using (ExcelRange SubTitulo = wrkReporte.Cells[filatit, 1, filatit, columaTitulo])
                    {
                        //Estilos
                        SubTitulo.Merge = true;// Merge:Combinacion de columnas
                        SubTitulo.Value = string.Format("{0}", vSubTituloDos);
                        SubTitulo.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        SubTitulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        SubTitulo.Style.WrapText = true;
                        SubTitulo.Style.Font.Size = 11;
                        //SubTitulo.Style.Font.Bold = true;
                    }
                    filatit++;
                }
                filatit++;

                //Código:

                using (ExcelRange SuTitulo = wrkReporte.Cells[filatit, 4, filatit + 2, 4]) //"D4:D6"
                {
                    //Estilos
                    SuTitulo.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    SuTitulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    SuTitulo.Style.WrapText = true;
                    SuTitulo.Style.Font.Size = 11;
                    SuTitulo.Style.Font.Bold = true;

                }

                if (string.IsNullOrEmpty(mantenimientoReporteDto.Encabezado6.Nombre.Trim()) && (mantenimientoReporteDto.Encabezado5 != null && mantenimientoReporteDto.Encabezado5.Nombre.Trim() != ""))
                {
                    wrkReporte.Cells[filatit, 2].Value = "CÓDIGO: "; // "B4"
                    wrkReporte.Cells[filatit, 2].Style.Font.Bold = true;
                    wrkReporte.Cells[filatit, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    wrkReporte.Cells[filatit + 1, 2].Value = "FECHA DE DESCARGA: "; //"B5"
                    wrkReporte.Cells[filatit + 1, 2].Style.Font.Bold = true;
                    wrkReporte.Cells[filatit + 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    wrkReporte.Cells[filatit + 2, 2].Value = "USUARIO: ";
                    wrkReporte.Cells[filatit + 2, 2].Style.Font.Bold = true;
                    wrkReporte.Cells[filatit + 2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    using (ExcelRange SubTitulo = wrkReporte.Cells[filatit, 3, filatit, columaTitulo])
                    {
                        //Estilos
                        SubTitulo.Merge = true;// Merge:Combinacion de columnas
                        SubTitulo.Value = string.Format("{0}", CodigoUsuario);
                        SubTitulo.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        SubTitulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        SubTitulo.Style.WrapText = true;
                        SubTitulo.Style.Font.Size = 11;
                        SubTitulo.Style.Font.Bold = true;

                    }
                    filatit++;

                    using (ExcelRange SubTitulo = wrkReporte.Cells[filatit, 3, filatit, columaTitulo])
                    {
                        //Estilos
                        SubTitulo.Merge = true;// Merge:Combinacion de columnas
                        SubTitulo.Value = string.Format("{0}", vFechaDescarga);
                        SubTitulo.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        SubTitulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        SubTitulo.Style.WrapText = true;
                        SubTitulo.Style.Font.Size = 11;
                        SubTitulo.Style.Font.Bold = true;
                    }
                    filatit++;

                    using (ExcelRange SubTitulo = wrkReporte.Cells[filatit, 3, filatit, columaTitulo])
                    {
                        //Estilos
                        SubTitulo.Merge = true;// Merge:Combinacion de columnas
                        SubTitulo.Value = string.Format("{0}", Usuario);
                        SubTitulo.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        SubTitulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        SubTitulo.Style.WrapText = true;
                        SubTitulo.Style.Font.Size = 11;
                        SubTitulo.Style.Font.Bold = true;
                    }
                }
                else
                {
                    wrkReporte.Cells[filatit, 4].Value = "CÓDIGO";
                    wrkReporte.Cells[filatit + 1, 4].Value = "FECHA DE DESCARGA";
                    wrkReporte.Cells[filatit + 2, 4].Value = "USUARIO";

                    using (ExcelRange SubTitulo = wrkReporte.Cells[filatit, 5, filatit, columaTitulo])
                    {
                        //Estilos
                        SubTitulo.Merge = true;// Merge:Combinacion de columnas
                        SubTitulo.Value = string.Format("{0}", CodigoUsuario);
                        SubTitulo.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        SubTitulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        SubTitulo.Style.WrapText = true;
                        SubTitulo.Style.Font.Size = 11;
                        SubTitulo.Style.Font.Bold = true;

                    }
                    filatit++;
                    using (ExcelRange SubTitulo = wrkReporte.Cells[filatit, 5, filatit, columaTitulo])
                    {
                        //Estilos
                        SubTitulo.Merge = true;// Merge:Combinacion de columnas
                        SubTitulo.Value = string.Format("{0}", vFechaDescarga);
                        SubTitulo.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        SubTitulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        SubTitulo.Style.WrapText = true;
                        SubTitulo.Style.Font.Size = 11;
                        SubTitulo.Style.Font.Bold = true;
                    }
                    filatit++;

                    using (ExcelRange SubTitulo = wrkReporte.Cells[filatit, 5, filatit, columaTitulo])
                    {
                        //Estilos
                        SubTitulo.Merge = true;// Merge:Combinacion de columnas
                        SubTitulo.Value = string.Format("{0}", Usuario);
                        SubTitulo.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        SubTitulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        SubTitulo.Style.WrapText = true;
                        SubTitulo.Style.Font.Size = 11;
                        SubTitulo.Style.Font.Bold = true;
                    }
                }
                #endregion titulos

                if (mantenimientoReporteDto.lstDetalle != null)
                {
                    foreach (var item in mantenimientoReporteDto.lstDetalle)
                    {
                        fila++;
                        var columnadato = 1;
                        var listacampos = item.ListarCampos();
                        for (int i = 0; i < listaEncabezados.Count; i++)
                        {
                            var encabezado = listaEncabezados[i];
                            if (encabezado != null && encabezado.Nombre.Trim() != "")
                            {
                                var campo = listacampos[i];

                                if (campo != null && campo != "")
                                {
                                    wrkReporte.Cells[fila, columnadato].Value = campo;
                                    wrkReporte.Cells[fila, columnadato].Style.WrapText = true;
                                    wrkReporte.Cells[fila, columnadato].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                                }

                                /// INICIO DE BORDER
                                wrkReporte.Cells[fila, columnadato].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                wrkReporte.Cells[fila, columnadato].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                wrkReporte.Cells[fila, columnadato].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                wrkReporte.Cells[fila, columnadato].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                /// FIN DE BORDER

                                columnadato++;
                            }
                        }

                    }

                    wrkReporte.Cells[filaInicio, 1, fila, columaTitulo].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                }

                return excel.GetAsByteArray();
            }
        }

        public byte[] ReporteGenericoPDF(string TituloReporte, string SubTituloReporte, string? CodigoUsuario, string Usuario, string? NombreHoja, ReporteGenericoDto mantenimientoReporteDto)
        {
            using (var memoryStream = new MemoryStream())
            {
                PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                var writer = new PdfWriter(memoryStream);
                var pdfDocument = new PdfDocument(writer);

                var logoDM = ImagenesAplicacion.createImagePDFFromPath(ImagenesAplicacion.getLogoArrayByte());

                logoDM.SetProperty(Property.TOP, 100); //Posición vertical en puntos
                logoDM.SetProperty(Property.LEFT, 100); //Posición horizontal en puntos
                logoDM.SetWidth(120);
                logoDM.SetMaxWidth(160);
                logoDM.ScaleAbsolute(350f, 70f);

                using (var document = new Document(pdfDocument))
                {
                    //document.SetMargins(30, 20, 30, 20);
                    //Tamaño y orientación de página

                    pdfDocument.SetDefaultPageSize(iText.Kernel.Geom.PageSize.A4.Rotate());


                    //Titulo de página

                    var tableTitle = new Table(3).UseAllAvailableWidth();
                    tableTitle.SetBorderTop(SolidBorder.NO_BORDER);
                    var headerCellLogo = new Cell().Add(logoDM);
                    headerCellLogo.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
                    tableTitle.AddHeaderCell(headerCellLogo);

                    var title = new Paragraph(TituloReporte)
                         .SetTextAlignment(TextAlignment.CENTER)
                         .SetFontSize(16);
                    //document.Add(title);

                    var vSubTitulos = "DM DESARROLLO - DERRAMA MAGISTERIAL";
                    var SubTitulos = new Paragraph(vSubTitulos)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(10);
                    SubTitulos.SetFont(boldFont);

                    var vSubTitulosDos = SubTituloReporte == null ? string.Empty : SubTituloReporte;
                    var SubTitulosDos = new Paragraph(vSubTitulosDos)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(10);

                    var headerTitulo = new Cell().Add(title).Add(new Paragraph("").SetFontSize(3)).Add(SubTitulos);
                    if (vSubTitulosDos.Length > 0)
                        headerTitulo.Add(new Paragraph("").SetFontSize(3)).Add(SubTitulosDos);
                    headerTitulo.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
                    tableTitle.AddHeaderCell(headerTitulo);

                    var headerTitulo2 = new Cell().Add(new Paragraph(""));
                    headerTitulo2.SetWidth(70);
                    headerTitulo2.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
                    tableTitle.AddHeaderCell(headerTitulo2);
                    document.Add(tableTitle);

                    document.Add(new Paragraph("").SetFontSize(4));

                    var tableSutitutlo = new Table(3).UseAllAvailableWidth();
                    tableSutitutlo.SetBorderTop(SolidBorder.NO_BORDER);

                    var headerCellTitulo = new Cell().Add(new Paragraph("CÓDIGO : " + CodigoUsuario + '\n' + "FECHA DESCARGA: " + DateTime.Now.ToString("dd/MM/yyyy - hh:mm tt", CultureInfo.InvariantCulture) + '\n' + "USUARIO: " + Usuario));
                    headerCellTitulo.SetTextAlignment(TextAlignment.JUSTIFIED);
                    headerCellTitulo.SetFontSize(9);
                    headerCellTitulo.SetWidth(20);
                    headerCellTitulo.SetBorder(iText.Layout.Borders.Border.NO_BORDER);
                    tableSutitutlo.AddHeaderCell(headerCellTitulo);


                    document.Add(tableSutitutlo);
                    document.Add(new Paragraph(""));
                    //Definimos cuantos campos son diferentes de NULL o vacios para saber cuantas columnas tendrá mi tabla:

                    int totalColumnas = 0;

                    if (mantenimientoReporteDto.Encabezado1 != null && mantenimientoReporteDto.Encabezado1.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado2 != null && mantenimientoReporteDto.Encabezado2.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado3 != null && mantenimientoReporteDto.Encabezado3.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado4 != null && mantenimientoReporteDto.Encabezado4.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado5 != null && mantenimientoReporteDto.Encabezado5.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado6 != null && mantenimientoReporteDto.Encabezado6.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado7 != null && mantenimientoReporteDto.Encabezado7.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado8 != null && mantenimientoReporteDto.Encabezado8.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado9 != null && mantenimientoReporteDto.Encabezado9.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado10 != null && mantenimientoReporteDto.Encabezado10.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado11 != null && mantenimientoReporteDto.Encabezado11.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado12 != null && mantenimientoReporteDto.Encabezado12.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado13 != null && mantenimientoReporteDto.Encabezado13.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado14 != null && mantenimientoReporteDto.Encabezado14.Nombre.Trim() != "")
                        totalColumnas++;
                    if (mantenimientoReporteDto.Encabezado15 != null && mantenimientoReporteDto.Encabezado15.Nombre.Trim() != "")
                        totalColumnas++;


                    //Crear la tabla con el total de columnas
                    var table = new Table(totalColumnas).UseAllAvailableWidth();
                    float tamanioLetra = 6.5f;
                    #region "Agregamos los encabezados de las columnas y definimos el ancho"
                    if (mantenimientoReporteDto.Encabezado1 != null && mantenimientoReporteDto.Encabezado1.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado1.Nombre));
                        //   wrkReporte.Cells["A" + fila].Style.WrapText = true;
                        // wrkReporte.Cells["A" + fila].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado1.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado1.Ancho);
                        table.SetFontSize(tamanioLetra);
                        //table.SetPaddingRight(10);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado2 != null && mantenimientoReporteDto.Encabezado2.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado2.Nombre));
                        // headerCell1.SetTextAlignment(TextAlignment.CENTER);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado2.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado2.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado3 != null && mantenimientoReporteDto.Encabezado3.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado3.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);

                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado3.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado3.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado4 != null && mantenimientoReporteDto.Encabezado4.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado4.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado4.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado4.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado5 != null && mantenimientoReporteDto.Encabezado5.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado5.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado5.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado5.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado6 != null && mantenimientoReporteDto.Encabezado6.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado6.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado6.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado6.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado7 != null && mantenimientoReporteDto.Encabezado7.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado7.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado7.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado7.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado8 != null && mantenimientoReporteDto.Encabezado8.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado8.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado8.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado8.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado9 != null && mantenimientoReporteDto.Encabezado9.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado9.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado9.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado9.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado10 != null && mantenimientoReporteDto.Encabezado10.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado10.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado10.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado10.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado11 != null && mantenimientoReporteDto.Encabezado11.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado11.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado11.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado11.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado12 != null && mantenimientoReporteDto.Encabezado12.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado12.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado12.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado12.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado13 != null && mantenimientoReporteDto.Encabezado13.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado13.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado13.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado13.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado14 != null && mantenimientoReporteDto.Encabezado14.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado14.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);

                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado14.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado14.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        table.AddHeaderCell(headerCell1);
                    }
                    if (mantenimientoReporteDto.Encabezado15 != null && mantenimientoReporteDto.Encabezado15.Nombre.Trim() != "")
                    {
                        var headerCell1 = new Cell().Add(new Paragraph(mantenimientoReporteDto.Encabezado15.Nombre));
                        headerCell1.SetTextAlignment(TextAlignment.CENTER);

                        headerCell1.SetFont(boldFont);
                        headerCell1.SetWidth((float)mantenimientoReporteDto.Encabezado15.Ancho);
                        headerCell1.SetMaxWidth((float)mantenimientoReporteDto.Encabezado15.Ancho);
                        headerCell1.SetFontSize(tamanioLetra);
                        headerCell1.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        table.AddHeaderCell(headerCell1);
                    }

                    //table.AddCell(headerCell1);
                    #endregion

                    #region "Agregamos las filas de datos"
                    if (mantenimientoReporteDto.lstDetalle != null)
                    {
                        foreach (var item in mantenimientoReporteDto.lstDetalle)
                        {
                            if (mantenimientoReporteDto.Encabezado1 != null && mantenimientoReporteDto.Encabezado1.Nombre != null && mantenimientoReporteDto.Encabezado1.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo1 != null) ? item.Campo1 : ""))
                                    .SetWidth((float)mantenimientoReporteDto.Encabezado1.Ancho)
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado1.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));


                            if (mantenimientoReporteDto.Encabezado2 != null && mantenimientoReporteDto.Encabezado2.Nombre != null && mantenimientoReporteDto.Encabezado2.Nombre.Trim() != "")
                            {
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo2 != null) ? item.Campo2 : ""))
                                    .SetWidth((float)mantenimientoReporteDto.Encabezado2.Ancho)
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado2.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            }

                            if (mantenimientoReporteDto.Encabezado3 != null && mantenimientoReporteDto.Encabezado3.Nombre != null && mantenimientoReporteDto.Encabezado3.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo3 != null) ? item.Campo3 : ""))
                                    .SetWidth((float)mantenimientoReporteDto.Encabezado3.Ancho)
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado3.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            if (mantenimientoReporteDto.Encabezado4 != null && mantenimientoReporteDto.Encabezado4.Nombre != null && mantenimientoReporteDto.Encabezado4.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo4 != null) ? item.Campo4 : ""))
                                    .SetWidth((float)mantenimientoReporteDto.Encabezado4.Ancho)
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado4.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            if (mantenimientoReporteDto.Encabezado5 != null && mantenimientoReporteDto.Encabezado5.Nombre != null && mantenimientoReporteDto.Encabezado5.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo5 != null) ? item.Campo5 : ""))
                                    .SetWidth((float)mantenimientoReporteDto.Encabezado5.Ancho)
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado5.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            if (mantenimientoReporteDto.Encabezado6 != null && mantenimientoReporteDto.Encabezado6.Nombre != null && mantenimientoReporteDto.Encabezado6.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo6 != null) ? item.Campo6 : ""))
                                    .SetWidth((float)mantenimientoReporteDto.Encabezado6.Ancho)
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado6.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            if (mantenimientoReporteDto.Encabezado7 != null && mantenimientoReporteDto.Encabezado7.Nombre != null && mantenimientoReporteDto.Encabezado7.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo7 != null) ? item.Campo7 : ""))
                                    .SetWidth((float)mantenimientoReporteDto.Encabezado7.Ancho)
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado7.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            if (mantenimientoReporteDto.Encabezado8 != null && mantenimientoReporteDto.Encabezado8.Nombre != null && mantenimientoReporteDto.Encabezado8.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo8 != null) ? item.Campo8 : ""))
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado8.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            if (mantenimientoReporteDto.Encabezado9 != null && mantenimientoReporteDto.Encabezado9.Nombre != null && mantenimientoReporteDto.Encabezado9.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo9 != null) ? item.Campo9 : ""))
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado9.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            if (mantenimientoReporteDto.Encabezado10 != null && mantenimientoReporteDto.Encabezado10.Nombre != null && mantenimientoReporteDto.Encabezado10.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo10 != null) ? item.Campo10 : ""))
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado10.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            if (mantenimientoReporteDto.Encabezado11 != null && mantenimientoReporteDto.Encabezado11.Nombre != null && mantenimientoReporteDto.Encabezado11.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo11 != null) ? item.Campo11 : ""))
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado11.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            if (mantenimientoReporteDto.Encabezado12 != null && mantenimientoReporteDto.Encabezado12.Nombre != null && mantenimientoReporteDto.Encabezado12.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo12 != null) ? item.Campo12 : ""))
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado12.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            if (mantenimientoReporteDto.Encabezado13 != null && mantenimientoReporteDto.Encabezado13.Nombre != null && mantenimientoReporteDto.Encabezado13.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo13 != null) ? item.Campo13 : ""))
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado13.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            if (mantenimientoReporteDto.Encabezado14 != null && mantenimientoReporteDto.Encabezado14.Nombre != null && mantenimientoReporteDto.Encabezado14.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo14 != null) ? item.Campo14 : ""))
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado14.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            if (mantenimientoReporteDto.Encabezado15 != null && mantenimientoReporteDto.Encabezado15.Nombre != null && mantenimientoReporteDto.Encabezado15.Nombre.Trim() != "")
                                table.AddCell(new Cell().Add(new Paragraph((item.Campo15 != null) ? item.Campo15 : ""))
                                    .SetMaxWidth((float)mantenimientoReporteDto.Encabezado15.Ancho)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));


                        }
                    }
                    #endregion

                    //Agrego la tabla al documento
                    document.Add(table);
                    // Cierro el documento
                    document.Close();

                    return memoryStream.ToArray();
                }
            }

        }

    }
}
