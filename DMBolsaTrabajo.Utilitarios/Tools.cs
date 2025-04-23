namespace DMBolsaTrabajo.Utilitarios
{
    public class Tools
    {
        /// <summary>
        /// Permite obteer el tipo de conteniedo de archivos con las extensiones definidas en la presente libreria.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        public static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
                {".xml", "application/xml"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".zip", "application/zip"}
            };
        }
        /// <summary>
        /// Permite Obtener una fecha sin la hora que es obtenido en el motor de datos señalado
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static DateTime GetFechaSinHora(DateTime fecha)
        {
            int year = fecha.Year;
            int mes = fecha.Month;
            int dia = fecha.Day;
            DateTime fechaFinal = new DateTime(year, mes, dia, 00, 00, 00);
            return fechaFinal;
        }
        /// <summary>
        /// Permite obtener el nombre del mes solo con enviar su numero correspondiente
        /// </summary>
        /// <param name="mes"></param>
        /// <returns></returns>
        public static string nombreMes(int? mes)
        {

            return mes switch
            {
                1 => "Ene",
                2 => "Feb",
                3 => "Mar",
                4 => "Abr",
                5 => "May",
                6 => "Jun",
                7 => "Jul",
                8 => "Ago",
                9 => "Sep",
                10 => "Oct",
                11 => "Nov",
                12 => "Dic",
                _ => ""
            };
        }
        /// <summary>
        /// Convierte la fecha cadena en tipo de dato DateTime 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime? stringToDateTime(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return null;
            }
            else
            {
                string dateString = date;
                string timestampString = dateString.Substring(6, dateString.Length - 8);
                long timestamp = long.Parse(timestampString);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                DateTime dateTime = dateTimeOffset.DateTime;
                return dateTime;
            }
            
        }
        public static string LimpiarCadena(string cadena)
        {
            try
            {
                // Quitando Tilde de cadena
                string accentedStr = cadena;
                byte[] tempBytes;
                tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr);
                string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
                cadena = asciiStr;

                // Quitando caracteres especiales de cadena
                cadena = System.Text.RegularExpressions.Regex.Replace(cadena, @"[^\w\s.!@$%^&*()\-\/]+", "");
                //cadena = cadena.Replace(".", "_");
            }
            catch (Exception ex) { }
            return cadena;
        }
    }
}

