using System.Data;
using MySqlConnector;

namespace ConexionBD
{
    public class QueryManager : IQueryManager
    {
        private readonly IMySQLConexion _mySQLConexion;
        public QueryManager(IMySQLConexion mySQLConexion)
        {
            this._mySQLConexion = mySQLConexion;
        }

        private static Dictionary<string, MySqlDbType> dcSQLDBType = new Dictionary<string, MySqlDbType>
        {
            {"INT", MySqlDbType.Int32},
            {"BIGINT", MySqlDbType.Int64},
            {"CHAR", MySqlDbType.String},
            {"VARCHAR", MySqlDbType.VarChar},
            {"TEXT", MySqlDbType.Text},
            {"FLOAT", MySqlDbType.Float},
            {"DOUBLE", MySqlDbType.Double},
            {"DATE", MySqlDbType.Date},
            {"DATETIME", MySqlDbType.DateTime},
            {"BLOB", MySqlDbType.Blob}
        };

        private static Dictionary<string, ParameterDirection> dcParamDir = new Dictionary<string, ParameterDirection>
        {
            {"IN", ParameterDirection.Input},
            {"OUT", ParameterDirection.Output},
            {"IN_OUT", ParameterDirection.InputOutput}
        };
    }

    public class HParametroMySQL
    {
        public string Nombre { get; set; }
        public int Posicion { get; set; }
        public string TipoDato { get; set; }
        public string Direccion { get; set; }
        public int TamanioDato { get; set; }
        public int Caracteres { get; set; }
        public MySqlParameter Parametro { get; set; }

        public HParametroMySQL()
        {
            this.Nombre = string.Empty;
            this.Posicion = 0;
            this.TipoDato = string.Empty;
            this.Direccion = string.Empty;
            this.TamanioDato = 0;
            this.Caracteres = 0;
            this.Parametro = new MySqlParameter();
        }
    }
}
