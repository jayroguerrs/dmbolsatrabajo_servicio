using DMBolsaTrabajo.Dominio;
using ConexionBD;
using DMBolsaTrabajo.IRepositorio;
using MySqlConnector;
using System.Data;

namespace DMBolsaTrabajo.Repositorio
{
    public class MenuRepositorio : IMenuRepositorio
    {
        private readonly IMySQLConexion _mysqlConexion;
        public MenuRepositorio(IMySQLConexion mysqlConexion)
        {
            _mysqlConexion = mysqlConexion;
        }

        public async Task<List<EMenu>> ListarPorIdOrigen(int idOrigen)
        {
            List<EMenu>? lista = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "PG_FACT_MENU.PA_FACT_LISTAR_POR_ORIGEN";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NMENU_ID_ORIGEN", idOrigen);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        lista = new List<EMenu>();
                        EMenu? eMenu = null;
                        while (await reader.ReadAsync())
                        {
                            eMenu = new EMenu();
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ID"))) eMenu.CMENU_ID = reader.GetString("CMENU_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_NOMBRE"))) eMenu.CMENU_NOMBRE = reader.GetString("CMENU_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("NMENU_ID_ORIGEN"))) eMenu.NMENU_ID_ORIGEN = reader.GetInt32("NMENU_ID_ORIGEN");
                            if (!reader.IsDBNull(reader.GetOrdinal("NMENU_ORDENAMIENTO"))) eMenu.NMENU_ORDENAMIENTO = reader.GetInt32("NMENU_ORDENAMIENTO");
                            lista.Add(eMenu);
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

        public async Task<List<EMenu>> Listar(int idApp)
        {
            List<EMenu>? lista = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "PG_FACT_MENU.PA_FACT_LISTAR";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        lista = new List<EMenu>();
                        EMenu? eMenu = null;
                        while (await reader.ReadAsync())
                        {
                            eMenu = new EMenu();
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ID"))) eMenu.CMENU_ID = reader.GetString("CMENU_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_NOMBRE"))) eMenu.CMENU_NOMBRE = reader.GetString("CMENU_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("NMENU_ID_ORIGEN"))) eMenu.NMENU_ID_ORIGEN = reader.GetInt32("NMENU_ID_ORIGEN");
                            if (!reader.IsDBNull(reader.GetOrdinal("NMENU_ORDENAMIENTO"))) eMenu.NMENU_ORDENAMIENTO = reader.GetInt32("NMENU_ORDENAMIENTO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_RUTA"))) eMenu.CMENU_RUTA = reader.GetString("CMENU_RUTA");
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ICONO"))) eMenu.CMENU_ICONO = reader.GetString("CMENU_ICONO");
                            lista.Add(eMenu);
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

        public async Task<List<ERolMenuPermisos>> ListarMenuPermisos(int idRol, int idApp)
        {
            List<ERolMenuPermisos>? eRolMenuPermisos = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "PG_FACT_MENU.PA_FACT_LISTAR_MENU_PERMISO";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NROLE_ID", idRol);
                    cmd.Parameters.AddWithValue("P_NAPLI_ID", idApp);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        eRolMenuPermisos = new List<ERolMenuPermisos>();
                        ERolMenuPermisos? eMenu = null;

                        while (await reader.ReadAsync())
                        {
                            eMenu = new ERolMenuPermisos();
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ID"))) eMenu.CMENU_ID = reader.GetString("CMENU_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_NOMBRE"))) eMenu.CMENU_NOMBRE = reader.GetString("CMENU_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("NMENU_ID_ORIGEN"))) eMenu.NMENU_ID_ORIGEN = reader.GetInt32("NMENU_ID_ORIGEN");
                            if (!reader.IsDBNull(reader.GetOrdinal("NMENU_ORDENAMIENTO"))) eMenu.NMENU_ORDENAMIENTO = reader.GetInt32("NMENU_ORDENAMIENTO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_RUTA"))) eMenu.CMENU_RUTA = reader.GetString("CMENU_RUTA");
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ICONO"))) eMenu.CMENU_ICONO = reader.GetString("CMENU_ICONO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NROME_ID"))) eMenu.NROME_ID = reader.GetInt32("NROME_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("NROME_ESTADO"))) eMenu.NROME_ESTADO = reader.GetInt32("NROME_ESTADO");
                            eRolMenuPermisos.Add(eMenu);
                        }
                        reader.NextResult();
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
            return eRolMenuPermisos;
        }
    }
}