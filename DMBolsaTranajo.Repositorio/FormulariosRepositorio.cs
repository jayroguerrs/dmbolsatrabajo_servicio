using ConexionBD;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.IRepositorio;
using MySqlConnector;
using System.Data;

namespace DMBolsaTrabajo.Repositorio
{
    public class FormulariosRepositorio : IFormulariosRepositorio
    {
        private readonly IMySQLConexion _mysqlConexion;
        public FormulariosRepositorio(IMySQLConexion mysqlConexion)
        {
            _mysqlConexion = mysqlConexion;
        }

        public async Task<int> EnviarFormularios(EFormulario request)
        {
            
            var result = 0;
            var resMensaje = "";

            var conn = _mysqlConexion.GetConnection();
            using var cmd = new MySqlCommand("ENVIAR_Formularios", conn); // Nombre del procedimiento
            cmd.CommandType = CommandType.StoredProcedure;

            // Parámetros de entrada
            //cmd.Parameters.AddWithValue("V_ID", request.id);
            //cmd.Parameters.AddWithValue("V_SATISFACCION", request.user_satisfaction);
            //cmd.Parameters.AddWithValue("V_COMENTARIO", request.user_commment);

            // Parámetros de salida
            var pRespuesta = new MySqlParameter("V_RESPUESTA", MySqlDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(pRespuesta);

            var pMensaje = new MySqlParameter("V_MENSAJE", MySqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(pMensaje);

            try
            {
                conn.Open();
                await cmd.ExecuteNonQueryAsync(); // Ejecuta el procedimiento

                // Obtén los valores de los parámetros de salida
                result = (int)pRespuesta.Value;
                resMensaje = pMensaje.Value.ToString();
            }
            catch (MySqlException ex)
            {
                // Maneja las excepciones
                throw new Exception("Error al ejecutar el procedimiento: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            // Puedes loggear o utilizar 'resMensaje' como necesites
            return result; // Devuelve el resultado
        }

        public async Task<int> ValidarFormularios(int id)
        {
            var result = 0;
            var resMensaje = "";

            var conn = _mysqlConexion.GetConnection();
            using var cmd = new MySqlCommand("VALIDAR_Formularios", conn); // Nombre del procedimiento
            cmd.CommandType = CommandType.StoredProcedure;

            // Parámetros de entrada
            cmd.Parameters.AddWithValue("V_ID", id);

            // Parámetros de salida
            var pRespuesta = new MySqlParameter("V_RESPUESTA", MySqlDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(pRespuesta);

            var pMensaje = new MySqlParameter("V_MENSAJE", MySqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(pMensaje);

            try
            {
                conn.Open();
                await cmd.ExecuteNonQueryAsync(); // Ejecuta el procedimiento

                // Obtén los valores de los parámetros de salida
                result = (int)pRespuesta.Value;
                resMensaje = pMensaje.Value.ToString();
            }
            catch (MySqlException ex)
            {
                // Maneja las excepciones
                throw new Exception("Error al ejecutar el procedimiento: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            // Puedes loggear o utilizar 'resMensaje' como necesites
            return result; // Devuelve el resultado
        }

        public async Task<EFormularioListaPaginado> ListarPaginado(EFormularioFiltro filtro)
        {
            EFormularioListaPaginado objPaginado = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_FORMULARIO_LISTAR_PAGINADO";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_DAUDI_USR_INS", filtro.DAUDI_USR_INS);
                    cmd.Parameters.AddWithValue("P_CFORM_TITULO", filtro.CFORM_TITULO);
                    cmd.Parameters.AddWithValue("P_NEVEN_ID", filtro.NEVEN_ID); 
                    cmd.Parameters.AddWithValue("P_NFORM_ESTADO", filtro.NFORM_ESTADO);
                    cmd.Parameters.AddWithValue("P_PAGE_SIZE", filtro.PAGE_SIZE);
                    cmd.Parameters.AddWithValue("P_PAGE_NUMBER", filtro.PAGE_NUMBER);
                    cmd.Parameters.AddWithValue("P_ORDER_BY", filtro.P_ORDER_BY);
                    cmd.Parameters.AddWithValue("P_ORDER", filtro.P_ORDER);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        objPaginado = new EFormularioListaPaginado();
                        objPaginado.lstFormularioPaginado = new List<EFormulario>();
                        EFormulario oFormulario = null;

                        while (await reader.ReadAsync())
                        {
                            oFormulario = new EFormulario();
                            //if (!reader.IsDBNull(reader.GetOrdinal("NUMERO"))) oUsuario.NUMERO = reader.GetInt32("NUMERO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NFORM_ID"))) oFormulario.NFORM_ID = reader.GetInt32("NFORM_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CEVEN_NOMBRE"))) oFormulario.CEVEN_NOMBRE = reader.GetString("CEVEN_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("CFORM_TITULO"))) oFormulario.CFORM_TITULO = reader.GetString("CFORM_TITULO");
                            if (!reader.IsDBNull(reader.GetOrdinal("ESTADO_TEXTO"))) oFormulario.ESTADO_TEXTO = reader.GetString("ESTADO_TEXTO");
                            if (!reader.IsDBNull(reader.GetOrdinal("FECHA_MODIFICACION"))) oFormulario.FECHA_MODIFICACION = reader.GetDateTime("FECHA_MODIFICACION");
                            if (!reader.IsDBNull(reader.GetOrdinal("USUARIO_RESPONSABLE"))) oFormulario.USUARIO_RESPONSABLE = reader.GetString("USUARIO_RESPONSABLE");
                            objPaginado.lstFormularioPaginado.Add(oFormulario);
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

        public async Task<EFormularioInicial> ObtenerDatos(string url)
        {
            EFormularioInicial eFormulario = null;
            EPreguntas ePreguntas = null;
            EAlternativas eAlternativas = null;

            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_FORMULARIO_OBTENER_DATOS";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_URL", url);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        // Leer datos generales del formulario
                        if (await reader.ReadAsync())
                        {
                            eFormulario = new EFormularioInicial();

                            if (!reader.IsDBNull(reader.GetOrdinal("NFORM_ID"))) eFormulario.NFORM_ID = reader.GetInt32("NFORM_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CFORM_TITULO"))) eFormulario.CFORM_TITULO = reader.GetString("CFORM_TITULO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CFORM_IMAGEN"))) eFormulario.CFORM_IMAGEN = reader.GetString("CFORM_IMAGEN");
                            if (!reader.IsDBNull(reader.GetOrdinal("CFORM_SUBTITULO"))) eFormulario.CFORM_SUBTITULO = reader.GetString("CFORM_SUBTITULO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CEVEN_NOMBRE"))) eFormulario.CEVEN_NOMBRE = reader.GetString("CEVEN_NOMBRE");
                        }

                        // Leer preguntas
                        reader.NextResult();
                        var preguntasDict = new Dictionary<int, EPreguntas>(); // Diccionario para mapear preguntas por su ID
                        while (await reader.ReadAsync())
                        {
                            ePreguntas = new EPreguntas();
                            if (!reader.IsDBNull(reader.GetOrdinal("NUMERO"))) ePreguntas.NUMERO = reader.GetInt32("NUMERO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NPREG_ID"))) ePreguntas.NPREG_ID = reader.GetInt32("NPREG_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("NPREG_OBLIGATORIO"))) ePreguntas.NPREG_OBLIGATORIO = reader.GetInt32("NPREG_OBLIGATORIO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCANT_ARCHIVOS"))) ePreguntas.NCANT_ARCHIVOS = reader.GetInt32("NCANT_ARCHIVOS");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPREG_DESCRIPCION"))) ePreguntas.CPREG_DESCRIPCION = reader.GetString("CPREG_DESCRIPCION");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_TIPO_PREG"))) ePreguntas.NCADE_TIPO_PREG = reader.GetInt32("NCADE_TIPO_PREG");
                            if (!reader.IsDBNull(reader.GetOrdinal("NPREG_ES_FECHA"))) ePreguntas.NPREG_ES_FECHA = reader.GetInt32("NPREG_ES_FECHA");
                            if (!reader.IsDBNull(reader.GetOrdinal("NPREG_ES_NUMERO"))) ePreguntas.NPREG_ES_NUMERO = reader.GetInt32("NPREG_ES_NUMERO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NPREG_ES_CORREO"))) ePreguntas.NPREG_ES_CORREO = reader.GetInt32("NPREG_ES_CORREO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NPREG_ES_REGEXP"))) ePreguntas.NPREG_ES_REGEXP = reader.GetInt32("NPREG_ES_REGEXP");

                            preguntasDict[ePreguntas.NPREG_ID] = ePreguntas; // Agregar la pregunta al diccionario
                            eFormulario.lstPreguntas.Add(ePreguntas);
                        }

                        // Leer alternativas y asignarlas a las preguntas correspondientes
                        reader.NextResult();
                        while (await reader.ReadAsync())
                        {
                            eAlternativas = new EAlternativas();
                            if (!reader.IsDBNull(reader.GetOrdinal("NPREG_ID"))) eAlternativas.NPREG_ID = reader.GetInt32("NPREG_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("INDEX"))) eAlternativas.INDEX = reader.GetInt32("INDEX");
                            if (!reader.IsDBNull(reader.GetOrdinal("DESCRIPCION"))) eAlternativas.DESCRIPCION = reader.GetString("DESCRIPCION");
                            
                            // Asignar la alternativa a la pregunta correspondiente
                            if (preguntasDict.ContainsKey(eAlternativas.NPREG_ID))
                            {
                                preguntasDict[eAlternativas.NPREG_ID].lstAlternativas.Add(eAlternativas);
                            }
                            
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
            return eFormulario;
        }

        public async Task<(int, string)> EnviarFormulario(EListaFormularioRespuesta request)
        {
            int result = 0;
            string resMensaje = "Error en la inserción de respuestas";
            using var conn = _mysqlConexion.GetConnection();
            var proc = "SP_FORMULARIO_ENVIAR";

            try
            {
                // Convertir la lista a una cadena "id|respuesta,id|respuesta"
                string respuestasConcatenadas = string.Join(",",
                    request.lstFormularioRespuesta.Select(r => $"{r.NPREG_ID}|{r.CPREG_RESPUESTA}")
                );

                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_RESPUESTA", respuestasConcatenadas);
                    cmd.Parameters.AddWithValue("P_CFORM_LINK", request.CFORM_LINK);

                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        // Leer datos generales del formulario
                        if (await reader.ReadAsync())
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
