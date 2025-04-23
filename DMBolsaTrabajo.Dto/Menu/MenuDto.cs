using DMBolsaTrabajo.Dto.Seguridad;

namespace DMBolsaTrabajo.Dto.Menu
{
    public class MenuInsDto : UsuarioBase
    {
        public string Nombre { get; set; }
        public int? IdOrigen { get; set; }
        public int Ordenamiento { get; set; }
        public string? Ruta { get; set; }
        public string? Icono { get; set; }
    }

    public class MenuUpdDto : UsuarioBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdOrigen { get; set; }
        public int Ordenamiento { get; set; }
        public string? Ruta { get; set; }
        public string? Icono { get; set; }
    }

    public class FiltroPermisosDto
    {
        public int IdSupuesto { get; set; }
        public int IdMenu { get; set; }
    }
}
