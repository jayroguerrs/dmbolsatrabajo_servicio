using DMBolsaTrabajo.Dto.Paginado;

namespace DMBolsaTrabajo.Dto.Usuario
{
    public class ConsolidadoDto
    {
        public int Id { get; set; }
        public String Codigo { get; set; }
        public int IdCodigo { get; set; }

    }

    public class UsuarioResponseDto
    {
        public int? Numero { get; set; }
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Bio { get; set; }
        public string? Nombres { get; set; }
        public int? Sexo { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int? Estado { get; set; }
        public string? Avatar { get; set; }
        public string? EstadoTexto { get; set; }
        public string? Persona { get; set; }
        public string? NombreRol { get; set; }
        public string? NombreTipoRol { get; set; }
        public int? EstadoRol { get; set; }
        public int? EstadoUsuarioRol { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? FechaModificacionText { get; set; }
        public string? UsuarioResponsable { get; set; }
    }

    public class UsuarioRolResponseDto
    {
        public int IdUsuarioRol { get; set; }
        public string Codigo { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string? Estado { get; set; }
        public int RolId { get; set; }
        public string? Correo { get; set; }
    }

    public class UsuarioRolInicialDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }

    public class UsuarioMenuDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string? IdOrigen { get; set; }
        public int Ordenamiento { get; set; }
        public string Ruta { get; set; }
        public string Icono { get; set; }
        public int Visible { get; set; }
    }

    public class UsuarioInicialDto
    {

        public int Id { get; set; }
        public int IdRol { get; set; }
        public string CodigoRol { get; set; }
        public List<UsuarioRolInicialDto> lstUsuarioRolInicialDto { get; set; }
        public List<UsuarioMenuDto> lstUsuarioMenuDto { get; set; }

        public UsuarioInicialDto()
        {
            lstUsuarioRolInicialDto = new List<UsuarioRolInicialDto>();
            lstUsuarioMenuDto = new List<UsuarioMenuDto>();
        }

    }
    public class UsuarioRolMenuInicialDto
    {
        public List<UsuarioRolInicialDto> lstUsuarioRolInicialDto { get; set; }
        public List<UsuarioMenuDto> lstUsuarioMenuDto { get; set; }
        public UsuarioRolMenuInicialDto()
        {
            lstUsuarioRolInicialDto = new List<UsuarioRolInicialDto>();
            lstUsuarioMenuDto = new List<UsuarioMenuDto>();
        }
    }

    public class ListarUsuarioResponseDto : PaginadoResponseDto
    {
        public List<UsuarioResponseDto> lista { get; set; }
    }
}
