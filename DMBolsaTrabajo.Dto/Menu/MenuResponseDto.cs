namespace DMBolsaTrabajo.Dto.Menu
{
    public class MenuResponseDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int IdOrigen { get; set; }
        public int Ordenamiento { get; set; }
        public string? UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? EstadoRegistro { get; set; }
        public string? Icono { get; set; }
        public string? Ruta { get; set; }
        public int Estado { get; set; }

    }

    public class MenuPermisosDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int IdOrigen { get; set; }
        public int Ordenamiento { get; set; }
        public string? Ruta { get; set; }
        public string? Icono { get; set; }
        public int IdRolMenu { get; set; }
        public int Estado { get; set; }

    }

}
