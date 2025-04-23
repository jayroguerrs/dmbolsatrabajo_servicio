using ConexionBD;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.IRepositorio;
using MySqlConnector;
using System.Data;

namespace DMBolsaTrabajo.Repositorio
{
    public class RolRepositorio : IRolRepositorio
    {
        private readonly IMySQLConexion _mysqlConexion;
        public RolRepositorio(IMySQLConexion mysqlConexion)
        {
            _mysqlConexion = mysqlConexion;
        }

        public async Task<List<ERolCombo>> ListarCmb(ERolFiltro request)
        {
            List<ERolCombo>? lista = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_ROLES_LISTAR_CMB";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NTIRO_ID", request.NTIRO_ID);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        lista = new List<ERolCombo>();
                        ERolCombo? eRol = null;
                        while (await reader.ReadAsync())
                        {
                            eRol = new ERolCombo();
                            if (!reader.IsDBNull(reader.GetOrdinal("NROLE_ID"))) eRol.NROLE_ID = reader.GetInt32("NROLE_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CROLE_NOMBRE"))) eRol.CROLE_NOMBRE = reader.GetString("CROLE_NOMBRE");
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
