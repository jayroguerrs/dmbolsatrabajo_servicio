using MySqlConnector;

namespace ConexionBD
{
    public interface IMySQLConexion
    {
        MySqlConnection GetConnection();
    }
}
