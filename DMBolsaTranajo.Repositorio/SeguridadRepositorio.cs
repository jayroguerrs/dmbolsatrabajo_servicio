using DMBolsaTrabajo.IRepositorio;
using ConexionBD;
using System.Data;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.Utilitarios;
using MySqlConnector;

namespace DMBolsaTrabajo.Repositorio
{
    public class SeguridadRepositorio : ISeguridadRepositorio
    {
        private readonly IMySQLConexion _mysqlConexion;

        public SeguridadRepositorio(IMySQLConexion mysqlConexion)
        {
            _mysqlConexion = mysqlConexion;
        }

        public async Task<EUsuarioLogin> IniciarSesion(EUsuarioLoginFiltro pSeguridad)
        {
            EUsuarioLogin? eSeguridad = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_SEGURIDAD_INGRESAR";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_CUSUA_USERNAME", pSeguridad.CUSUA_USERNAME);
                    cmd.Parameters.AddWithValue("P_CUSUA_PASSWORD", pSeguridad.CUSUA_PASSWORD);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();

                    // Verificar si hay filas en el resultado
                    if (await reader.ReadAsync())
                    {
                        eSeguridad = new EUsuarioLogin();

                        if (!reader.IsDBNull(reader.GetOrdinal("NUSUA_ID"))) eSeguridad.NUSUA_ID = reader.GetInt32("NUSUA_ID");
                        if (!reader.IsDBNull(reader.GetOrdinal("CPERS_APE_PATERNO"))) eSeguridad.CPERS_APE_PATERNO = reader.GetString("CPERS_APE_PATERNO");
                        if (!reader.IsDBNull(reader.GetOrdinal("CPERS_APE_MATERNO"))) eSeguridad.CPERS_APE_MATERNO = reader.GetString("CPERS_APE_MATERNO");
                        if (!reader.IsDBNull(reader.GetOrdinal("CPERS_NOMBRES"))) eSeguridad.CPERS_NOMBRES = reader.GetString("CPERS_NOMBRES");
                        if (!reader.IsDBNull(reader.GetOrdinal("CPERS_CORREO"))) eSeguridad.CPERS_CORREO = reader.GetString("CPERS_CORREO");
                        if (!reader.IsDBNull(reader.GetOrdinal("NROLE_ID"))) eSeguridad.NROLE_ID = reader.GetInt32("NROLE_ID");
                    }
                    else
                    {
                        // No hay coincidencias, devolver null
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogDeError.GestorError(ex, "Seguridad Repositorio");
                throw;
            }
            finally
            {
                conn.Close();
            }
            return eSeguridad;
        }

        public async Task<ECorreoElectronico> EnviarCorreoRecuperacion(string email, string url)
        {
            var conn = _mysqlConexion.GetConnection();
            var proc = "PG_FACT_USUARIO.PA_FACT_ENVIAR_RESTABLEC";
            ECorreoElectronico? usuarioConfirmacion = null;
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_CPERS_CORREO", email);
                    cmd.Parameters.AddWithValue("P_URL", url);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        while (await reader.ReadAsync())
                        {
                            usuarioConfirmacion = new ECorreoElectronico();
                            if (!reader.IsDBNull(reader.GetOrdinal("ASUNTO"))) usuarioConfirmacion.ASUNTO = reader.GetString("ASUNTO");
                            if (!reader.IsDBNull(reader.GetOrdinal("EMAIL_DESTINO"))) usuarioConfirmacion.EMAIL_DESTINO = reader.GetString("EMAIL_DESTINO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NOMBRE_DESTINO"))) usuarioConfirmacion.NOMBRE_DESTINO = reader.GetString("NOMBRE_DESTINO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CONTENIDO_TEXTO"))) usuarioConfirmacion.CONTENIDO_TEXTO = reader.GetString("CONTENIDO_TEXTO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CONTENIDO_HTML"))) usuarioConfirmacion.CONTENIDO_HTML = reader.GetString("CONTENIDO_HTML");
                            if (!reader.IsDBNull(reader.GetOrdinal("ESTADO"))) usuarioConfirmacion.ESTADO = reader.GetInt32("ESTADO");
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
            return usuarioConfirmacion;
        }

        public async Task<(int, string)> ValidarCredencialesRestablecer(int IdUsuario, string Token)
        {
            int result = 0;
            string resMensaje = "";
            var conn = _mysqlConexion.GetConnection();
            var proc = "PG_FACT_USUARIO.PA_FACT_VALIDAR_CREDENCIAL";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUSUA_ID", IdUsuario);
                    cmd.Parameters.AddWithValue("P_CUSUA_TOKEN", Token);
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

        public async Task<(int, string)> CambiarClave(EClave request)
        {
            int result = 0;
            string resMensaje = "";
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_SEGURIDAD_CAMBIAR_PASSWORD";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUSUA_ID", request.NUSUA_ID);
                    cmd.Parameters.AddWithValue("P_CUSUA_PASSWORD1", request.CUSUA_PASSWORD1);
                    cmd.Parameters.AddWithValue("P_CUSUA_PASSWORD2", request.CUSUA_PASSWORD2);
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

        public async Task<(int, string)> CambiarClaveNoAuth(EClave request)
        {
            int result = 0;
            string resMensaje = "";
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_SEGURIDAD_CAMBIAR_PASSWORD_NA";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUSUA_ID", request.NUSUA_ID);
                    cmd.Parameters.AddWithValue("P_CUSUA_PASSWORD1", request.CUSUA_PASSWORD1);
                    cmd.Parameters.AddWithValue("P_CUSUA_PASSWORD2", request.CUSUA_PASSWORD2);
                    cmd.Parameters.AddWithValue("P_CUSUA_TOKEN", request.CUSUA_TOKEN);
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
    }
}