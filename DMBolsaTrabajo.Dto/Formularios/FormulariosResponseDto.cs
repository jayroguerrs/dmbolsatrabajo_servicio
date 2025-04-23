using DMBolsaTrabajo.Dto.Paginado;
using DMBolsaTrabajo.Dto.Preguntas;

namespace DMBolsaTrabajo.Dto.Formularios
{
    public class FormularioResponseDto
    {
        public int Id { get; set; }
        public string Evento { get; set; }
        public string Titulo { get; set; }
        public string? Subtitulo { get; set; }
        public string? EstadoTexto { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

    }

    public class FormularioInicialDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public string Subtitulo { get; set; }
        public string Encuesta { get; set; }
        public List<PreguntasDto> lstPreguntasDto { get; set; }
    }

    public class ListarFormularioResponseDto : PaginadoResponseDto
    {
        public List<FormularioResponseDto> lista { get; set; }
    }
}
