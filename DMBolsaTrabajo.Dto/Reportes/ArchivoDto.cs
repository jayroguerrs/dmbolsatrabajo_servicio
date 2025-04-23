namespace DMBolsaTrabajo.Dto.Reportes
{
    public class ArchivoDto
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }
        public string ServerTemp { get; set; }

    }

    public class ExtensionElementos
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
