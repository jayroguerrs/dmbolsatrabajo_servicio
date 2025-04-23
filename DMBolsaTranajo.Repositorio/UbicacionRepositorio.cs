using ConexionBD;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.IRepositorio;
using MySqlConnector;
using System.Data;

namespace DMBolsaTrabajo.Repositorio
{
    public class UbicacionRepositorio : IUbicacionRepositorio
    {
        private readonly IMySQLConexion _mysqlConexion;
        public UbicacionRepositorio(IMySQLConexion mysqlConexion)
        {
            _mysqlConexion = mysqlConexion;
        }

        public async Task<List<EEventoCombo>> ListarDepartamentoCmb(EEventoFiltro request)
        {
            List<EEventoCombo>? lista = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_EVENTO_LISTAR_CMB";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NEVEN_ESTADO", request.NEVEN_ESTADO);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        lista = new List<EEventoCombo>();
                        EEventoCombo? eRol = null;
                        while (await reader.ReadAsync())
                        {
                            eRol = new EEventoCombo();
                            if (!reader.IsDBNull(reader.GetOrdinal("NEVEN_ID"))) eRol.NEVEN_ID = reader.GetInt32("NEVEN_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CEVEN_NOMBRE"))) eRol.CEVEN_NOMBRE = reader.GetString("CEVEN_NOMBRE");
                            lista.Add(eRol);
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

        public async Task<List<EEventoCombo>> ListarDistritoCmb(EEventoFiltro request)
        {
            List<EEventoCombo>? lista = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_EVENTO_LISTAR_CMB";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NEVEN_ESTADO", request.NEVEN_ESTADO);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        lista = new List<EEventoCombo>();
                        EEventoCombo? eRol = null;
                        while (await reader.ReadAsync())
                        {
                            eRol = new EEventoCombo();
                            if (!reader.IsDBNull(reader.GetOrdinal("NEVEN_ID"))) eRol.NEVEN_ID = reader.GetInt32("NEVEN_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CEVEN_NOMBRE"))) eRol.CEVEN_NOMBRE = reader.GetString("CEVEN_NOMBRE");
                            lista.Add(eRol);
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
    }
}