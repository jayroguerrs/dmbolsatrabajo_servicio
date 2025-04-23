using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace ConexionBD
{
    public class MySQLConexion : IMySQLConexion
    {
        IConfiguration _configuration;
        public MySQLConexion(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public MySqlConnection GetConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var conn = new MySqlConnection(connectionString);
             
            return conn;
        }
    }
}