namespace DMBolsaTrabajo.Dto.Paginado
{
    public class PaginadoRequestDto
    {
        public int TamanioPagina { get; set; }
        public int NumeroPagina { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
    }
    public class PaginadoOneRequestDto
    {
        private int? _NumeroPagina { get; set; } = 1;
        private int? _TamanioPagina { get; set; } = 10;
        public int? NumeroPagina
        {
            get { return _NumeroPagina; }
            set
            {
                _NumeroPagina = value ?? 1;
            }
        }
        public int? TamanioPagina
        {
            get { return _TamanioPagina; }
            set
            {
                _TamanioPagina = value ?? 10;
            }
        }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
    }
}
