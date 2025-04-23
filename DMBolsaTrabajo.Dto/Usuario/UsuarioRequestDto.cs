using DMBolsaTrabajo.Dto.Paginado;
using DMBolsaTrabajo.Dto.Seguridad;

namespace DMBolsaTrabajo.Dto.Usuario
{
    public class UsuarioRequestDto
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

    public class UsuarioFilterRequestDto : PaginadoRequestDto
    {
        public string? Usuario { get; set; }
        private int? _Estado { get; set; } = -1;
        private int? _TipoRolId { get; set; } = -1;
        public string? Email { get; set; }
        public string? Sexo { get; set; }
        private int? _RolId { get; set; } = -1;
        public int? RolId
        {
            get { return _RolId; }
            set
            {
                _RolId = value ?? 0;
            }
        }
        public int? Estado
        {
            get { return _Estado; }
            set
            {
                _Estado = value ?? -1;
            }
        }
        public int? TipoRolId
        {
            get { return _TipoRolId; }
            set
            {
                _TipoRolId = value ?? 0;
            }
        }
    }

    public class UsuarioInsUpdDto : UsuarioBase
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

    public class UsuarioAsociarRolDto : UsuarioBase
    {
        public int IdRolUsuario { get; set; }
        public int RolId { get; set; }
    }

    public class UsuarioDelDto : UsuarioBase
    {
        public int Id { get; set; }
    }
}
