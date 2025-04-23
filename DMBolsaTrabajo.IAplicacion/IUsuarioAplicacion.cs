using DMBolsaTrabajo.Dto.Usuario;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;

namespace DMBolsaTrabajo.IAplicacion
{
    public interface IUsuarioAplicacion
    {
        Task<Respuesta> ObtenerCargaInicial(int idUsuario);
        Task<Respuesta> ObtenerDatos(int idUsuario);
        Task<Respuesta> ObtenerDatosRolUsuario(int idRolUsuario);
        Task<Respuesta> ListarPaginado(UsuarioFilterRequestDto request);
        Task<Respuesta> Insertar(UsuarioInsUpdDto request);
        Task<Respuesta> Eliminar(UsuarioDelDto request);
        Task<Respuesta> ActualizarDatos(UsuarioRequestDto request);
        Task<Respuesta> AsociarRol(UsuarioAsociarRolDto request);
        Task<Respuesta> ObtenerRolMenuPorUsuario(int idUsuario, int idRol);
        Task<Respuesta> ObtenerMenuPorRol(int idRol);
        Task<Respuesta> EnviarCorreoConfirmacion(int idUsuario);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtro">Nombre: String, Estado: Int</param>
        /// <param name="Tipo">1: Excel, 2:PDF</param>
        /// <returns></returns>
        Task<Respuesta> GenerarReporte(UsuarioFilterRequestDto filtro, int Tipo);
    }
}
