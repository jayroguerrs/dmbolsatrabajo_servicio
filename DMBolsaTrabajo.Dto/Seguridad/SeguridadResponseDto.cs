namespace DMBolsaTrabajo.Dto.Seguridad
{
    public class SeguridadResponseDto
    {
        public int Id { get; set; }
        public int IdRol { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public string Codigo { get; set; }
    }
}
