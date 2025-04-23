namespace DMBolsaTrabajo.Dto.Preguntas
{
    public class PreguntasDto
    {
        public int Index { get; set; }
        public int Id { get; set; }
        public string Pregunta { get; set; }
        public string? Alternativa { get; set; }
        public int CantidadArchivos { get; set; }
        public int? TipoPregunta { get; set; }
        public int Obligatorio { get; set; }
        public int EsFecha { get; set; }
        public int EsNumero { get; set; }
        public int EsCorreo { get; set; }
        public int EsRegExp { get; set; }
        public List<AlternativasDto> lstAlternativasDto { get; set; }
    }

    public class AlternativasDto
    {
        public string Id { get; set; }
        public string Index { get; set; }
        public string Descripcion { get; set; }
    }

}
