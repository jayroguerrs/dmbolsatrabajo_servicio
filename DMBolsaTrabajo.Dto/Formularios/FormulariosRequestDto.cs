using DMBolsaTrabajo.Dto.Paginado;

namespace DMBolsaTrabajo.Dto.Formularios
{
    public class FormulariosRequestDto
    {
        public int Id { get; set; }
        public int Satisfaccion { get; set; }
        public string Comentario { get; set; }

    }

    public class FormularioRequestPorFiltroDto : PaginadoRequestDto
    {
        public string? Fecha { get; set; }
        public int? Titulo { get; set; }
        public int? EventoId { get; set; }
        public string? Estado { get; set; }

    }

    public class ListaRespuestaRequestDto
    {
        public string FormularioLink { get; set; }
        public List<RespuestaRequestDto> ListaRespuesta { get; set; }
    }

    public class RespuestaRequestDto
    {
        public int PreguntaId { get; set; }
        public string Respuesta { get; set; }
    }

}
