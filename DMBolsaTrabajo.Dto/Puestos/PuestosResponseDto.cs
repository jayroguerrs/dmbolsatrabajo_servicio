using DMBolsaTrabajo.Dto.Paginado;

namespace DMBolsaTrabajo.Dto.Puestos
{
    public class ConsolidadoDto
    {
        public int Id { get; set; }
        public String Codigo { get; set; }
        public int IdCodigo { get; set; }

    }

    public class PuestosResponseDto
    {
        public int? Numero { get; set; }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Distrito { get; set; }
        public string Departamento { get; set; }
        public string Imagen { get; set; }
        public DateOnly FechaIni { get; set; }
        public DateOnly FechaFin { get; set; }
        public string? Ubicacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioResponsable { get; set; }
        public string? EstadoTexto { get; set; }
        public int Estado { get; set; }
    }

    public class PostulantesResponseDto
    {
        public int? Numero { get; set; }
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Archivo { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioResponsable { get; set; }
        public string? EstadoTexto { get; set; }
    }

    public class PuestosResponsePorIdDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int DistritoId { get; set; }
        public int DepartamentoId { get; set; }
        public string Imagen { get; set; }
        public DateOnly FechaIni { get; set; }
        public DateOnly FechaFin { get; set; }
        public int? Estado { get; set; }
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

    public class ListarPuestosResponseDto : PaginadoResponseDto
    {
        public List<PuestosResponseDto> lista { get; set; }
    }

    public class ListarPostulantesResponseDto : PaginadoResponseDto
    {
        public List<PostulantesResponseDto> lista { get; set; }
    }

    public class PostularInsUpdDto
    {
        public int? PostulacionId { get; set; }
        public int? UsuarioId { get; set; }
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int PuestoId { get; set; }
        public string? NombreArchivo { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string RecaptchaToken { get; set; }
    }
}
