using DMBolsaTrabajo.Dto.Paginado;
using DMBolsaTrabajo.Dto.Seguridad;

namespace DMBolsaTrabajo.Dto.CatalogoDetalle
{
    public class CatalogoDetalleResponseDto
    {
        public int Index { get; set; }
        public int Id { get; set; }
        public string IdCatalogo { get; set; }
        public string Catalogo { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public int? Ordenamiento { get; set; }
        public string? UsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string EstadoRegistro { get; set; }
        public int? Estado { get; set; }
        public string EstadoTexto { get; set; }
    }

    public class CatalogoDetalleResponsePorIdDto : UsuarioBase
    {
        public Int32 Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public Int32 IdCatalogo { get; set; }
        public string Descripcion { get; set; }
        public Int32? IdOrigen { get; set; }
        public Int32? Ordenamiento { get; set; }
        public Int32? Estado { get; set; }
    }

    public class CatalogoResponseDto
    {
        public Int32 IdCatalogo { get; set; }
        public string Nombre { get; set; }
    }

    public class ListarCatalogoDetalleResponseDto : PaginadoResponseDto
    {
        public List<CatalogoDetalleResponseDto> lista { get; set; }
    }
}
