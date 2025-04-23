using DMBolsaTrabajo.Dto.Paginado;
using DMBolsaTrabajo.Dto.Seguridad;

namespace DMBolsaTrabajo.Dto.CatalogoDetalle
{
    public class CatalogoDetalleRequestPorIdDto
    {
        public int Id { get; set; }
    }

    public class CatalogoDetalleRequestPorIdCatDto
    {
        public int IdCatalogo { get; set; }
        public int? IdOrigen { get; set; }
        public string? Grupo { get; set; }
    }

    public class CatalogoDetalleRequestPorFiltroDto : PaginadoRequestDto
    {
        public string? Nombre { get; set; }
        public int IdCatalogo { get; set; }
        public int? Estado { get; set; }

    }

    public class CatalogoDetalleInsUpdDto : UsuarioBase
    {
        public int Id { get; set; }
        public int IdCatalogo { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public string Descripcion { get; set; }
        public int? IdOrigen { get; set; }
        public int? Ordenamiento { get; set; }
        public int? Estado { get; set; }

    }
}
