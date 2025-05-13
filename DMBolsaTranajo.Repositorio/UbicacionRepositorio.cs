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

        public async Task<List<EDepartamentoCombo>> ListarDepartamentoCmb(EDepartamentoFiltro request)
        {
            List<EDepartamentoCombo>? lista = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_DEPARTAMENTO_LISTAR_CMB";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NDEPA_ESTADO", request.NDEPA_ESTADO);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        lista = new List<EDepartamentoCombo>();
                        EDepartamentoCombo? eDepa = null;
                        while (await reader.ReadAsync())
                        {
                            eDepa = new EDepartamentoCombo();
                            if (!reader.IsDBNull(reader.GetOrdinal("NDEPA_ID"))) eDepa.NDEPA_ID = reader.GetInt32("NDEPA_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CDEPA_NOMBRE"))) eDepa.CDEPA_NOMBRE = reader.GetString("CDEPA_NOMBRE");
                            lista.Add(eDepa);
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

        public async Task<List<EDistritoCombo>> ListarDistritoCmb(EDistritoFiltro request)
        {
            List<EDistritoCombo>? lista = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_DISTRITO_LISTAR_CMB";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NDEPA_ID", request.NDEPA_ID);
                    cmd.Parameters.AddWithValue("P_NDIST_ESTADO", request.NDIST_ESTADO);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        lista = new List<EDistritoCombo>();
                        EDistritoCombo? eDistrito = null;
                        while (await reader.ReadAsync())
                        {
                            eDistrito = new EDistritoCombo();
                            if (!reader.IsDBNull(reader.GetOrdinal("NDIST_ID"))) eDistrito.NDIST_ID = reader.GetInt32("NDIST_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CDIST_NOMBRE"))) eDistrito.CDIST_NOMBRE = reader.GetString("CDIST_NOMBRE");
                            lista.Add(eDistrito);
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