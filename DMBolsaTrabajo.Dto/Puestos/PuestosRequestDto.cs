using DMBolsaTrabajo.Dto.Paginado;
using DMBolsaTrabajo.Dto.Seguridad;

namespace DMBolsaTrabajo.Dto.Puestos
{
    public class PuestosRequestDto
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string? TipoDocumento { get; set; }
        public int? Sexo { get; set; }
        public string? Bio { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }

    public class PuestosFilterRequestDto : PaginadoRequestDto
    {
        public DateOnly? FechaRegistro{ get; set; }
        public string? Titulo { get; set; }
        private int? _Estado { get; set; } = -1;
        public string? Ubicacion { get; set; }
        public int? Estado
        {
            get { return _Estado; }
            set
            {
                _Estado = value ?? -1;
            }
        }

        public string RecaptchaToken { get; set; }
    }

    public class PuestosFilterNoCaptchaRequestDto : PaginadoRequestDto
    {
        public DateOnly? FechaRegistro { get; set; }
        public string? Titulo { get; set; }
        private int? _Estado { get; set; } = -1;
        public string? Ubicacion { get; set; }
        public int? Estado
        {
            get { return _Estado; }
            set
            {
                _Estado = value ?? -1;
            }
        }

    }

    public class PuestosInsUpdDto : UsuarioBase
    {
        public int? IdRolUsuario { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Codigo { get; set; }
        public string? Correo { get; set; }
        public int? RolId { get; set; }
        public int Estado { get; set; }
    }

   
    public class PuestosDelDto : UsuarioBase
    {
        public int Id { get; set; }
    }
}
