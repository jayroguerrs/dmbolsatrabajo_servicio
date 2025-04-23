using DMBolsaTrabajo.Dominio;
using ConexionBD;
using MySqlConnector;
using DMBolsaTrabajo.IRepositorio;
using System.Data;

namespace DMBolsaTrabajo.Repositorio
{
    public class CatalogoDetalleRepositorio : ICatalogoDetalleRepositorio
    {
        private readonly IMySQLConexion _mysqlConexion;
        public CatalogoDetalleRepositorio(IMySQLConexion mysqlConexion)
        {
            _mysqlConexion = mysqlConexion;
        }

        public async Task<List<ECatalogoDetalleResponsePorId>> Listar(ECatalogoDetallePorIdCatalogo eParam)
        {
            List<ECatalogoDetalleResponsePorId>? lista = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_CATALOGO_DETALLE_LISTAR_POR_ID";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NCATA_ID", eParam.NCATA_ID);
                    cmd.Parameters.AddWithValue("P_NCADE_ID_ORIGEN", eParam.NCADE_ID_ORIGEN);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        lista = new List<ECatalogoDetalleResponsePorId>();
                        ECatalogoDetalleResponsePorId? eCatalogoDetalle = null;
                        while (await reader.ReadAsync())
                        {
                            eCatalogoDetalle = new ECatalogoDetalleResponsePorId();
                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_ID"))) eCatalogoDetalle.NCADE_ID = reader.GetInt32("NCADE_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_CODIGO"))) eCatalogoDetalle.CCADE_CODIGO = reader.GetString("CCADE_CODIGO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_NOMBRE"))) eCatalogoDetalle.CCADE_NOMBRE = reader.GetString("CCADE_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_ABREVIATURA"))) eCatalogoDetalle.CCADE_ABREVIATURA = reader.GetString("CCADE_ABREVIATURA");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_DESCRIPCION"))) eCatalogoDetalle.CCADE_DESCRIPCION = reader.GetString("CCADE_DESCRIPCION");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_ID_ORIGEN"))) eCatalogoDetalle.NCADE_ID_ORIGEN = reader.GetInt32("NCADE_ID_ORIGEN");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_ORDENAMIENTO"))) eCatalogoDetalle.NCADE_ORDENAMIENTO = reader.GetInt32("NCADE_ORDENAMIENTO");
                            lista.Add(eCatalogoDetalle);
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
            return lista;
        }

        public async Task<List<ECatalogoResponse>> ListarCatalogo()
        {
            List<ECatalogoResponse>? lista = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_CATALOGO_DETALLE_LISTAR_CATALOGO";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        lista = new List<ECatalogoResponse>();
                        ECatalogoResponse? eCatalogo = null;
                        while (await reader.ReadAsync())
                        {
                            eCatalogo = new ECatalogoResponse();
                            if (!reader.IsDBNull(reader.GetOrdinal("NCATA_ID"))) eCatalogo.NCATA_ID = reader.GetInt32("NCATA_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCATA_NOMBRE"))) eCatalogo.CCATA_NOMBRE = reader.GetString("CCATA_NOMBRE");
                            lista.Add(eCatalogo);
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
            return lista;
        }

        public async Task<ECatalogoDetalleListaPaginado> ListarPaginado(ECatalogoDetalleFiltro filtro)
        {
            ECatalogoDetalleListaPaginado objPaginado = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_CATALOGO_DETALLE_LISTAR_PAGINADO";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NCATA_ID", filtro.NCATA_ID);
                    cmd.Parameters.AddWithValue("P_NCADE_ESTADO", filtro.NCADE_ESTADO);
                    cmd.Parameters.AddWithValue("P_CCADE_NOMBRE", filtro.CCADE_NOMBRE);
                    cmd.Parameters.AddWithValue("P_PAGE_SIZE", filtro.PAGE_SIZE);
                    cmd.Parameters.AddWithValue("P_PAGE_NUMBER", filtro.PAGE_NUMBER);
                    cmd.Parameters.AddWithValue("P_ORDER_BY", filtro.P_ORDER_BY);
                    cmd.Parameters.AddWithValue("P_ORDER", filtro.P_ORDER);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        objPaginado = new ECatalogoDetalleListaPaginado();
                        objPaginado.lstCatalogoDetallePaginado = new List<ECatalogoDetalle>();
                        ECatalogoDetalle oUsuario = null;

                        while (await reader.ReadAsync())
                        {
                            oUsuario = new ECatalogoDetalle();
                            //if (!reader.IsDBNull(reader.GetOrdinal("NUMERO"))) oUsuario.NUMERO = reader.GetInt32("NUMERO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_ID"))) oUsuario.NCADE_ID = reader.GetInt32("NCADE_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_CODIGO"))) oUsuario.CCADE_CODIGO = reader.GetString("CCADE_CODIGO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCATA_NOMBRE"))) oUsuario.CCATA_NOMBRE = reader.GetString("CCATA_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_NOMBRE"))) oUsuario.CCADE_NOMBRE = reader.GetString("CCADE_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_ESTADO"))) oUsuario.NCADE_ESTADO = reader.GetInt32("NCADE_ESTADO");
                            if (!reader.IsDBNull(reader.GetOrdinal("ESTADO_TEXTO"))) oUsuario.ESTADO_TEXTO = reader.GetString("ESTADO_TEXTO");
                            if (!reader.IsDBNull(reader.GetOrdinal("FECHA_MODIFICACION"))) oUsuario.FECHA_MODIFICACION = reader.GetDateTime("FECHA_MODIFICACION");
                            if (!reader.IsDBNull(reader.GetOrdinal("USUARIO_RESPONSABLE"))) oUsuario.USUARIO_RESPONSABLE = reader.GetString("USUARIO_RESPONSABLE");
                            objPaginado.lstCatalogoDetallePaginado.Add(oUsuario);
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

        public async Task<ECatalogoDetalleResponsePorId> ObtenerPorId(int Id)
        {
            ECatalogoDetalleResponsePorId? eCatalogoDetalle = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_OBTENER_POR_ID_CATALOGO_DETALLE";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NCADE_ID", Id);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        if (await reader.ReadAsync())
                        {
                            eCatalogoDetalle = new ECatalogoDetalleResponsePorId();

                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_ID"))) eCatalogoDetalle.NCADE_ID = reader.GetInt32("NCADE_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_CODIGO"))) eCatalogoDetalle.CCADE_CODIGO = reader.GetString("CCADE_CODIGO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_NOMBRE"))) eCatalogoDetalle.CCADE_NOMBRE = reader.GetString("CCADE_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCATA_ID"))) eCatalogoDetalle.NCATA_ID = reader.GetInt32("NCATA_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_ABREVIATURA"))) eCatalogoDetalle.CCADE_ABREVIATURA = reader.GetString("CCADE_ABREVIATURA");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_DESCRIPCION"))) eCatalogoDetalle.CCADE_DESCRIPCION = reader.GetString("CCADE_DESCRIPCION");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_ID_ORIGEN"))) eCatalogoDetalle.NCADE_ID_ORIGEN = reader.GetInt32("NCADE_ID_ORIGEN");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_ORDENAMIENTO"))) eCatalogoDetalle.NCADE_ORDENAMIENTO = reader.GetInt32("NCADE_ORDENAMIENTO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_ESTADO"))) eCatalogoDetalle.NCADE_ESTADO = reader.GetInt32("NCADE_ESTADO");
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
            return eCatalogoDetalle;
        }

        public async Task<(int, string)> Insertar(ECatalogoDetalleResponsePorId eParam)
        {
            int result = 0;
            string resMensaje = "";
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_INSERTAR_CATALOGO_DETALLE";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NCADE_ID", eParam.NCADE_ID);
                    cmd.Parameters.AddWithValue("P_CCADE_NOMBRE", eParam.CCADE_NOMBRE);
                    cmd.Parameters.AddWithValue("P_CCADE_CODIGO", eParam.CCADE_CODIGO);
                    cmd.Parameters.AddWithValue("P_CCADE_ABREVIATURA", eParam.CCADE_ABREVIATURA);
                    cmd.Parameters.AddWithValue("P_CCADE_DESCRIPCION", eParam.CCADE_DESCRIPCION);
                    cmd.Parameters.AddWithValue("P_NCADE_ORDENAMIENTO", eParam.NCADE_ORDENAMIENTO);
                    cmd.Parameters.AddWithValue("P_NCADE_ESTADO", eParam.NCADE_ESTADO);
                    cmd.Parameters.AddWithValue("P_NCATA_ID", eParam.NCATA_ID);
                    cmd.Parameters.AddWithValue("P_NAUDI_USR_INS", eParam.NAUDI_USR_INS);
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

        public async Task<List<ECatalogoDetalle>> Listar(ECatalogoDetalleFiltro filtro)
        {
            List<ECatalogoDetalle>? lstUsuarioPaginado = null;

            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_CATALOGO_DETALLE_LISTAR";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NCATA_ID", filtro.NCATA_ID);
                    cmd.Parameters.AddWithValue("P_NCADE_ESTADO", filtro.NCADE_ESTADO);
                    cmd.Parameters.AddWithValue("P_CCADE_NOMBRE", filtro.CCADE_NOMBRE);
                    cmd.Parameters.AddWithValue("P_ORDER_BY", filtro.P_ORDER_BY);
                    cmd.Parameters.AddWithValue("P_ORDER", filtro.P_ORDER);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        lstUsuarioPaginado = new List<ECatalogoDetalle>();
                        while (await reader.ReadAsync())
                        {
                            var oUsuario = new ECatalogoDetalle();
                            if (!reader.IsDBNull(reader.GetOrdinal("NUMERO"))) oUsuario.NUMERO = reader.GetInt32("NUMERO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_ID"))) oUsuario.NCADE_ID = reader.GetInt32("NCADE_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_CODIGO"))) oUsuario.CCADE_CODIGO = reader.GetString("CCADE_CODIGO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_NOMBRE"))) oUsuario.CCADE_NOMBRE = reader.GetString("CCADE_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCATA_NOMBRE"))) oUsuario.CCATA_NOMBRE = reader.GetString("CCATA_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("CCADE_ABREVIATURA"))) oUsuario.CCADE_ABREVIATURA = reader.GetString("CCADE_ABREVIATURA");
                            if (!reader.IsDBNull(reader.GetOrdinal("FECHA_MODIFICACION"))) oUsuario.FECHA_MODIFICACION = reader.GetDateTime("FECHA_MODIFICACION");
                            if (!reader.IsDBNull(reader.GetOrdinal("USUARIO_RESPONSABLE"))) oUsuario.USUARIO_RESPONSABLE = reader.GetString("USUARIO_RESPONSABLE");
                            if (!reader.IsDBNull(reader.GetOrdinal("ESTADO_TEXTO"))) oUsuario.ESTADO_TEXTO = reader.GetString("ESTADO_TEXTO");
                            lstUsuarioPaginado.Add(oUsuario);
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
            return lstUsuarioPaginado;
        }
    }
}
