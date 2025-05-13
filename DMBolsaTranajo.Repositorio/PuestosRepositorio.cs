using ConexionBD;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.IRepositorio;
using MySqlConnector;
using System.Data;

namespace DMBolsaTrabajo.Repositorio
{
    public class PuestosRepositorio : IPuestosRepositorio
    {
        private readonly IMySQLConexion _mysqlConexion;
        public PuestosRepositorio(IMySQLConexion mysqlConexion)
        {
            _mysqlConexion = mysqlConexion;
        }

        public async Task<EPuestosListaPaginado> ListarPaginado(EPuestosFiltro filtro)
        {
            EPuestosListaPaginado objPaginado = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_PUESTOS_LISTAR_PAGINADO";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_DAUDI_USR_INS", filtro.DAUDI_USR_INS);
                    cmd.Parameters.AddWithValue("P_CPUEST_TITULO", filtro.CPUEST_TITULO);
                    cmd.Parameters.AddWithValue("P_UBICACION", filtro.UBICACION);
                    cmd.Parameters.AddWithValue("P_NPUEST_ESTADO", filtro.NPUEST_ESTADO);
                    cmd.Parameters.AddWithValue("P_PAGE_SIZE", filtro.PAGE_SIZE);
                    cmd.Parameters.AddWithValue("P_PAGE_NUMBER", filtro.PAGE_NUMBER);
                    cmd.Parameters.AddWithValue("P_ORDER_BY", filtro.P_ORDER_BY);
                    cmd.Parameters.AddWithValue("P_ORDER", filtro.P_ORDER);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        objPaginado = new EPuestosListaPaginado();
                        objPaginado.lstPuestosPaginado = new List<EPuestosLista>();
                        EPuestosLista oUsuario = null;

                        while (await reader.ReadAsync())
                        {
                            oUsuario = new EPuestosLista();
                            if (!reader.IsDBNull(reader.GetOrdinal("NPUEST_ID"))) oUsuario.NPUEST_ID = reader.GetInt32("NPUEST_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPUEST_TITULO"))) oUsuario.CPUEST_TITULO = reader.GetString("CPUEST_TITULO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPUEST_DESCRIPCION"))) oUsuario.CPUEST_DESCRIPCION = reader.GetString("CPUEST_DESCRIPCION");
                            if (!reader.IsDBNull(reader.GetOrdinal("DPUEST_FECHA_INI"))) oUsuario.DPUEST_FECHA_INI = reader.GetDateOnly("DPUEST_FECHA_INI");
                            if (!reader.IsDBNull(reader.GetOrdinal("DPUEST_FECHA_FIN"))) oUsuario.DPUEST_FECHA_FIN = reader.GetDateOnly("DPUEST_FECHA_FIN");
                            if (!reader.IsDBNull(reader.GetOrdinal("UBICACION"))) oUsuario.UBICACION = reader.GetString("UBICACION");
                            if (!reader.IsDBNull(reader.GetOrdinal("FECHA_MODIFICACION"))) oUsuario.FECHA_MODIFICACION = reader.GetDateTime("FECHA_MODIFICACION");
                            if (!reader.IsDBNull(reader.GetOrdinal("USUARIO_RESPONSABLE"))) oUsuario.USUARIO_RESPONSABLE = reader.GetString("USUARIO_RESPONSABLE");
                            if (!reader.IsDBNull(reader.GetOrdinal("NPUEST_ESTADO"))) oUsuario.NPUEST_ESTADO = reader.GetInt32("NPUEST_ESTADO");
                            if (!reader.IsDBNull(reader.GetOrdinal("ESTADO_TEXTO"))) oUsuario.ESTADO_TEXTO = reader.GetString("ESTADO_TEXTO");
                            objPaginado.lstPuestosPaginado.Add(oUsuario);
                        }
                        reader.NextResult();
                        while (await reader.ReadAsync())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("RECORDCOUNT"))) objPaginado.RECORDCOUNT = reader.GetInt32("RECORDCOUNT");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return objPaginado;
        }

        public async Task<EPostulantesListaPaginado> ListarPostulantesPaginado(EPostulantesFiltro filtro)
        {
            EPostulantesListaPaginado objPaginado = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_PUESTOS_POSTULANTES_LISTAR_PAGINADO";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_CPOST_NUMDOC", filtro.CPOST_NUMDOC);
                    cmd.Parameters.AddWithValue("P_NOMBRES", filtro.NOMBRES);
                    cmd.Parameters.AddWithValue("P_NPUEST_ID", filtro.NPUEST_ID);
                    cmd.Parameters.AddWithValue("P_NUARO_ESTADO", filtro.NUARO_ESTADO);
                    cmd.Parameters.AddWithValue("P_PAGE_SIZE", filtro.PAGE_SIZE);
                    cmd.Parameters.AddWithValue("P_PAGE_NUMBER", filtro.PAGE_NUMBER);
                    cmd.Parameters.AddWithValue("P_ORDER_BY", filtro.P_ORDER_BY);
                    cmd.Parameters.AddWithValue("P_ORDER", filtro.P_ORDER);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        objPaginado = new EPostulantesListaPaginado();
                        objPaginado.lstPostulantesPaginado = new List<EPostulantesLista>();
                        EPostulantesLista oPostulantes = null;

                        while (await reader.ReadAsync())
                        {
                            oPostulantes = new EPostulantesLista();
                            if (!reader.IsDBNull(reader.GetOrdinal("NPOST_ID"))) oPostulantes.NPOST_ID = reader.GetInt32("NPOST_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("NOMBRES"))) oPostulantes.NOMBRES = reader.GetString("NOMBRES");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPOST_CORREO"))) oPostulantes.CPOST_CORREO = reader.GetString("CPOST_CORREO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPOST_ARCHIVO"))) oPostulantes.CPOST_ARCHIVO = reader.GetString("CPOST_ARCHIVO");
                            if (!reader.IsDBNull(reader.GetOrdinal("FECHA_MODIFICACION"))) oPostulantes.FECHA_MODIFICACION = reader.GetDateTime("FECHA_MODIFICACION");
                            if (!reader.IsDBNull(reader.GetOrdinal("USUARIO_RESPONSABLE"))) oPostulantes.USUARIO_RESPONSABLE = reader.GetString("USUARIO_RESPONSABLE");
                            if (!reader.IsDBNull(reader.GetOrdinal("ESTADO_TEXTO"))) oPostulantes.ESTADO_TEXTO = reader.GetString("ESTADO_TEXTO");
                            objPaginado.lstPostulantesPaginado.Add(oPostulantes);
                        }
                        reader.NextResult();
                        while (await reader.ReadAsync())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("RECORDCOUNT"))) objPaginado.RECORDCOUNT = reader.GetInt32("RECORDCOUNT");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return objPaginado;
        }

        public async Task<EPuestosResponseId> ObtenerPorId(int Id)
        {
            EPuestosResponseId puestos = null;

            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_PUESTOS_OBTENER_POR_ID";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NPUEST_ID", Id);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        if (await reader.ReadAsync())
                        {
                            puestos = new EPuestosResponseId();

                            if (!reader.IsDBNull(reader.GetOrdinal("NPUEST_ID"))) puestos.NPUEST_ID = reader.GetInt32("NPUEST_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPUEST_TITULO"))) puestos.CPUEST_TITULO = reader.GetString("CPUEST_TITULO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPUEST_DESCRIPCION"))) puestos.CPUEST_DESCRIPCION = reader.GetString("CPUEST_DESCRIPCION");
                            if (!reader.IsDBNull(reader.GetOrdinal("DPUEST_FECHA_INI"))) puestos.DPUEST_FECHA_INI = reader.GetDateOnly("DPUEST_FECHA_INI");
                            if (!reader.IsDBNull(reader.GetOrdinal("DPUEST_FECHA_FIN"))) puestos.DPUEST_FECHA_FIN = reader.GetDateOnly("DPUEST_FECHA_FIN");
                            if (!reader.IsDBNull(reader.GetOrdinal("NDIST_ID"))) puestos.NDIST_ID = reader.GetInt32("NDIST_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("NDEPA_ID"))) puestos.NDEPA_ID = reader.GetInt32("NDEPA_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPUEST_IMAGEN"))) puestos.CPUEST_IMAGEN = reader.GetString("CPUEST_IMAGEN");
                            if (!reader.IsDBNull(reader.GetOrdinal("NPUEST_ESTADO"))) puestos.NPUEST_ESTADO = reader.GetInt32("NPUEST_ESTADO");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return puestos;
        }
    
        public async Task<(int, string)> Postular(EPostularInsUpd request)
        {
            int result = 0;
            string resMensaje = "";
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_PUESTOS_POSTULAR";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NPOST_ID", request.NPOST_ID);
                    cmd.Parameters.AddWithValue("P_NUSUA_ID", request.NUSUA_ID);
                    cmd.Parameters.AddWithValue("P_NCADE_TIPO_DOCUMENTO", request.NCADE_TIPO_DOCUMENTO);
                    cmd.Parameters.AddWithValue("P_CPOST_NUMDOC", request.CPOST_NUMDOC);
                    cmd.Parameters.AddWithValue("P_CPOST_NOMBRES", request.CPOST_NOMBRES);
                    cmd.Parameters.AddWithValue("P_CPOST_PATERNO", request.CPOST_PATERNO);
                    cmd.Parameters.AddWithValue("P_CPOST_MATERNO", request.CPOST_MATERNO);
                    cmd.Parameters.AddWithValue("P_CPOST_CELULAR", request.CPOST_CELULAR);
                    cmd.Parameters.AddWithValue("P_CPOST_CORREO", request.CPOST_CORREO);
                    cmd.Parameters.AddWithValue("P_NPUEST_ID", request.NPUEST_ID);
                    cmd.Parameters.AddWithValue("P_CPOST_ARCHIVO", request.CPOST_ARCHIVO);
                    conn.Open();

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null && await reader.ReadAsync())
                        {
                            resMensaje = reader.GetString(reader.GetOrdinal("CMESSAGE"));
                            result = reader.GetInt32(reader.GetOrdinal("NSUCCESS"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return (result, resMensaje);
        }

        public async Task<(int, string)> Eliminar(EPuestosDel request)
        {
            int result = 0;
            string resMensaje = "";
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_PUESTOS_ELIMINAR";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NPUEST_ID", request.NPUEST_ID);
                    cmd.Parameters.AddWithValue("P_NAUDI_USR_UPD", request.NAUDI_USR_UPD);
                    conn.Open();

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null && await reader.ReadAsync())
                        {
                            resMensaje = reader.GetString(reader.GetOrdinal("CMESSAGE"));
                            result = reader.GetInt32(reader.GetOrdinal("NSUCCESS"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return (result, resMensaje);
        }

        public async Task<(int, string)> Insertar(EPuestosInsUpd request)
        {
            int result = 0;
            string resMensaje = "";
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_PUESTOS_INS_UPD";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NPUEST_ID", request.NPUEST_ID);
                    cmd.Parameters.AddWithValue("P_CPUEST_TITULO", request.CPUEST_TITULO);
                    cmd.Parameters.AddWithValue("P_CPUEST_DESCRIPCION", request.CPUEST_DESCRIPCION);
                    cmd.Parameters.AddWithValue("P_DPUEST_FECHA_INI", request.DPUEST_FECHA_INI);
                    cmd.Parameters.AddWithValue("P_DPUEST_FECHA_FIN", request.DPUEST_FECHA_FIN);
                    cmd.Parameters.AddWithValue("P_NDIST_ID", request.NDIST_ID);
                    cmd.Parameters.AddWithValue("P_NPUEST_ESTADO", request.NPUEST_ESTADO);
                    cmd.Parameters.AddWithValue("P_NAUDI_USR_INS", request.NAUDI_USR_INS);
                    conn.Open();

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null && await reader.ReadAsync())
                        {
                            resMensaje = reader.GetString(reader.GetOrdinal("CMESSAGE"));
                            result = reader.GetInt32(reader.GetOrdinal("NSUCCESS"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return (result, resMensaje);
        }

        public async Task<(int, string)> CambiarEstado(EEstadoCambio request)
        {

            int result = 0;
            string resMensaje = "Error en la inserción de respuestas";
            using var conn = _mysqlConexion.GetConnection();
            var proc = "SP_PUESTOS_CAMBIAR_ESTADO";

            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NPUEST_ID", request.NPUEST_ID);
                    cmd.Parameters.AddWithValue("P_NAUDI_USR_UPD", request.NAUDI_USR_UPD);
                    conn.Open();

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null && await reader.ReadAsync())
                        {
                            resMensaje = reader.GetString(reader.GetOrdinal("CMESSAGE"));
                            result = reader.GetInt32(reader.GetOrdinal("NSUCCESS"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resMensaje = ex.Message;
            }
            finally
            {
                await conn.CloseAsync();
            }
            return (result, resMensaje);
        }
    }
}
