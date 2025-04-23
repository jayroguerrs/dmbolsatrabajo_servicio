namespace DMBolsaTrabajo.Dto.Seguridad
{
    public class SeguridadRequestDto
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
    }

    public class ClaveRequestDto
    {
        public int Id { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
    }
}