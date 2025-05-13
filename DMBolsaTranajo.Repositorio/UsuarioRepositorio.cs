using ConexionBD;
using DMBolsaTrabajo.Dominio;
using DMBolsaTrabajo.IRepositorio;
using MySqlConnector;
using System.Data;

namespace DMBolsaTrabajo.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IMySQLConexion _mysqlConexion;
        public UsuarioRepositorio(IMySQLConexion mysqlConexion)
        {
            _mysqlConexion = mysqlConexion;
        }

        public async Task<EUsuarioInicial> ObtenerCargaInicial(int idUsuario)
        {
            EUsuarioInicial usuario = null;
            EUsuarioRolInicial eUsuarioRolInicial = null;
            EUsuarioMenu eUsuarioMenu = null;

            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_USUARIO_CARGA_INICIAL";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUSUA_ID", idUsuario);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new EUsuarioInicial();

                            if (!reader.IsDBNull(reader.GetOrdinal("NUSUA_ID"))) usuario.NUSUA_ID = reader.GetInt32("NUSUA_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("NROLE_ID"))) usuario.NROLE_ID = reader.GetInt32("NROLE_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CROLE_CODIGO"))) usuario.CROLE_CODIGO = reader.GetString("CROLE_CODIGO");
                        }
                        reader.NextResult();
                        while (await reader.ReadAsync())
                        {
                            eUsuarioRolInicial = new EUsuarioRolInicial();
                            if (!reader.IsDBNull(reader.GetOrdinal("NROLE_ID"))) eUsuarioRolInicial.NROLE_ID = reader.GetInt32("NROLE_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CROLE_CODIGO"))) eUsuarioRolInicial.CROLE_CODIGO = reader.GetString("CROLE_CODIGO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CROLE_NOMBRE"))) eUsuarioRolInicial.CROLE_NOMBRE = reader.GetString("CROLE_NOMBRE");
                            usuario.lstUsuarioRolInicial.Add(eUsuarioRolInicial);
                        }
                        reader.NextResult();
                        while (await reader.ReadAsync())
                        {
                            eUsuarioMenu = new EUsuarioMenu();
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ID"))) eUsuarioMenu.CMENU_ID = reader.GetString("CMENU_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_NOMBRE"))) eUsuarioMenu.CMENU_NOMBRE = reader.GetString("CMENU_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ID_ORIGEN"))) eUsuarioMenu.CMENU_ID_ORIGEN = reader.GetString("CMENU_ID_ORIGEN");
                            if (!reader.IsDBNull(reader.GetOrdinal("NMENU_ORDENAMIENTO"))) eUsuarioMenu.NMENU_ORDENAMIENTO = reader.GetInt32("NMENU_ORDENAMIENTO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_RUTA"))) eUsuarioMenu.CMENU_RUTA = reader.GetString("CMENU_RUTA");
                            if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ICONO"))) eUsuarioMenu.CMENU_ICONO = reader.GetString("CMENU_ICONO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NROME_VISIBLE"))) eUsuarioMenu.NROME_VISIBLE = reader.GetInt32("NROME_VISIBLE");
                            usuario.lstUsuarioMenu.Add(eUsuarioMenu);
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
            return usuario;
        }

        public async Task<EUsuario> ObtenerDatos(int idUsuario)
        {
            EUsuario usuario = null;

            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_USUARIO_OBTENER_DATOS";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUSUA_ID", idUsuario);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new EUsuario();

                            if (!reader.IsDBNull(reader.GetOrdinal("NUSUA_ID"))) usuario.NUSUA_ID = reader.GetInt32("NUSUA_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_ID_TIPO_DOCUMENTO"))) usuario.NCADE_ID_TIPO_DOCUMENTO = reader.GetInt32("NCADE_ID_TIPO_DOCUMENTO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_NRO_DOCUMENTO"))) usuario.CPERS_NRO_DOCUMENTO = reader.GetString("CPERS_NRO_DOCUMENTO");
                            if (!reader.IsDBNull(reader.GetOrdinal("DPERS_FEC_NACIMIENTO"))) usuario.DPERS_FEC_NACIMIENTO = reader.GetDateTime("DPERS_FEC_NACIMIENTO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_APE_PATERNO"))) usuario.CPERS_APE_PATERNO = reader.GetString("CPERS_APE_PATERNO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_APE_MATERNO"))) usuario.CPERS_APE_MATERNO = reader.GetString("CPERS_APE_MATERNO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_NOMBRES"))) usuario.CPERS_NOMBRES = reader.GetString("CPERS_NOMBRES");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_CORREO"))) usuario.CPERS_CORREO = reader.GetString("CPERS_CORREO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_TELEFONO"))) usuario.CPERS_TELEFONO = reader.GetString("CPERS_TELEFONO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_BIO"))) usuario.CPERS_BIO = reader.GetString("CPERS_BIO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NCADE_ID_SEXO"))) usuario.NCADE_ID_SEXO = reader.GetInt32("NCADE_ID_SEXO");
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
            return usuario;
        }

        public async Task<EUsuarioRol> ObtenerDatosRolUsuario(int idRolUsuario)
        {
            EUsuarioRol usuario = null;

            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_USUARIO_ASIG_ROLES_OBTENER_DATOS_POR_ROL";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUARO_ID", idRolUsuario);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new EUsuarioRol();

                            if (!reader.IsDBNull(reader.GetOrdinal("NUARO_ID"))) usuario.NUARO_ID = reader.GetInt32("NUARO_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_APE_PATERNO"))) usuario.CPERS_APE_PATERNO = reader.GetString("CPERS_APE_PATERNO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_APE_MATERNO"))) usuario.CPERS_APE_MATERNO = reader.GetString("CPERS_APE_MATERNO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_APE_PATERNO"))) usuario.CPERS_APE_PATERNO = reader.GetString("CPERS_APE_PATERNO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_NOMBRES"))) usuario.CPERS_NOMBRES = reader.GetString("CPERS_NOMBRES");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_CORREO"))) usuario.CPERS_CORREO = reader.GetString("CPERS_CORREO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NUARO_ESTADO"))) usuario.NUARO_ESTADO = reader.GetInt32("NUARO_ESTADO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CUSUA_USERNAME"))) usuario.CUSUA_USERNAME = reader.GetString("CUSUA_USERNAME");
                            if (!reader.IsDBNull(reader.GetOrdinal("NROLE_ID"))) usuario.NROLE_ID = reader.GetInt32("NROLE_ID");
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
            return usuario;
        }

        public async Task<(int, string)> ActualizarDatos(EUsuarioAct request)
        {
            int result = 0;
            string resMensaje = "";
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_USUARIO_ACTUALIZAR_DATOS";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUSUA_ID", request.NUSUA_ID);
                    cmd.Parameters.AddWithValue("P_NCADE_ID_TIPO_DOCUMENTO", request.NCADE_ID_TIPO_DOCUMENTO);
                    cmd.Parameters.AddWithValue("P_NCADE_ID_SEXO", request.NCADE_ID_SEXO);
                    cmd.Parameters.AddWithValue("P_CPERS_NRO_DOCUMENTO", request.CPERS_NRO_DOCUMENTO);
                    cmd.Parameters.AddWithValue("P_CPERS_APE_PATERNO", request.CPERS_APE_PATERNO);
                    cmd.Parameters.AddWithValue("P_CPERS_APE_MATERNO", request.CPERS_APE_MATERNO);
                    cmd.Parameters.AddWithValue("P_CPERS_NOMBRES", request.CPERS_NOMBRES);
                    cmd.Parameters.AddWithValue("P_CPERS_CORREO", request.CPERS_CORREO );
                    cmd.Parameters.AddWithValue("P_CPERS_TELEFONO", request.CPERS_TELEFONO);
                    cmd.Parameters.AddWithValue("P_DPERS_FEC_NACIMIENTO", request.DPERS_FEC_NACIMIENTO);
                    cmd.Parameters.AddWithValue("P_CPERS_BIO", request.CPERS_BIO);
                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();

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

        public async Task<(int, string)> AsociarRol(EAsociarRol request)
        {
            int result = 0;
            string resMensaje = "";
            var conn = _mysqlConexion.GetConnection();
            var proc = "PG_FACT_USUARIO_ASIG_ROLES.PA_FACT_ASOCIAR_ROL";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUARO_ID", request.NUARO_ID);
                    cmd.Parameters.AddWithValue("P_NROLE_ID", request.NROLE_ID);
                    cmd.Parameters.AddWithValue("P_NAUDI_USR_INS", request.NAUDI_USR_INS);
                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();

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

        public async Task<(int, string)> Insertar(EUsuarioInsUpd request)
        {
            int result = 0;
            string resMensaje = "";
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_USUARIO_INSERTAR";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUARO_ID", request.NUARO_ID);
                    cmd.Parameters.AddWithValue("P_CUSUA_USERNAME", request.CUSUA_USERNAME);
                    cmd.Parameters.AddWithValue("P_CPERS_APE_PATERNO", request.CPERS_APE_PATERNO);
                    cmd.Parameters.AddWithValue("P_CPERS_APE_MATERNO", request.CPERS_APE_MATERNO);
                    cmd.Parameters.AddWithValue("P_CPERS_NOMBRES", request.CPERS_NOMBRES);
                    cmd.Parameters.AddWithValue("P_CPERS_CORREO", request.CPERS_CORREO);
                    cmd.Parameters.AddWithValue("P_NROLE_ID", request.NROLE_ID);
                    cmd.Parameters.AddWithValue("P_NUARO_ESTADO", request.NUARO_ESTADO);
                    cmd.Parameters.AddWithValue("P_NAUDI_REG_INS", request.NAUDI_USR_INS);
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

        public async Task<(int, string)> Eliminar(EUsuarioDel request)
        {
            int result = 0;
            string resMensaje = "";
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_USUARIO_ELIMINAR";
            try
            {
                using (MySqlCommand cmd = new(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUARO_ID", request.NUARO_ID);
                    cmd.Parameters.AddWithValue("P_NAUDI_USR_UPD", request.NAUDI_USR_UPD);
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

        public async Task<List<EUsuarioMenu>> ObtenerMenuPorRol(int idRol)
        {
            List<EUsuarioMenu> lstMenu = new();
            using (var conn = _mysqlConexion.GetConnection())
            {
                var proc = "PG_FACT_USUARIO.PA_FACT_CARGA_MENU";
                try
                {
                    using (MySqlCommand cmd = new(proc, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("P_NROLE_ID", idRol);
                        conn.Open();
                        using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if (reader != null)
                        {
                            while (await reader.ReadAsync())
                            {
                                EUsuarioMenu eUsuarioMenu = new();
                                if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ID"))) eUsuarioMenu.CMENU_ID = reader.GetString("CMENU_ID");
                                if (!reader.IsDBNull(reader.GetOrdinal("CMENU_NOMBRE"))) eUsuarioMenu.CMENU_NOMBRE = reader.GetString("CMENU_NOMBRE");
                                if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ID_ORIGEN"))) eUsuarioMenu.CMENU_ID_ORIGEN = reader.GetString("CMENU_ID_ORIGEN");
                                if (!reader.IsDBNull(reader.GetOrdinal("NMENU_ORDENAMIENTO"))) eUsuarioMenu.NMENU_ORDENAMIENTO = reader.GetInt32("NMENU_ORDENAMIENTO");
                                if (!reader.IsDBNull(reader.GetOrdinal("CMENU_RUTA"))) eUsuarioMenu.CMENU_RUTA = reader.GetString("CMENU_RUTA");
                                if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ICONO"))) eUsuarioMenu.CMENU_ICONO = reader.GetString("CMENU_ICONO");
                                if (!reader.IsDBNull(reader.GetOrdinal("NROME_VISIBLE"))) eUsuarioMenu.NROME_VISIBLE = reader.GetInt32("NROME_VISIBLE");

                                lstMenu.Add(eUsuarioMenu);
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
            }
            return lstMenu;
        }

        public async Task<EUsuarioRolMenuInicial> ObtenerRolMenuPorUsuario(int idUsuario, int idRol)
        {
            EUsuarioRolMenuInicial usuario = new EUsuarioRolMenuInicial();

            using (var conn = _mysqlConexion.GetConnection())
            {
                var proc = "SP_USUARIO_CARGA_ROL_MENU";
                try
                {
                    using (MySqlCommand cmd = new(proc, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("P_NUSUA_ID", idUsuario);
                        cmd.Parameters.AddWithValue("P_NROLE_ID", idRol);
                        conn.Open();
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader != null)
                            {
                                while (await reader.ReadAsync())
                                {
                                    EUsuarioRolInicial eUsuarioRolInicial = new();
                                    if (!reader.IsDBNull(reader.GetOrdinal("NROLE_ID"))) eUsuarioRolInicial.NROLE_ID = reader.GetInt32("NROLE_ID");
                                    if (!reader.IsDBNull(reader.GetOrdinal("CROLE_CODIGO"))) eUsuarioRolInicial.CROLE_CODIGO = reader.GetString("CROLE_CODIGO");
                                    if (!reader.IsDBNull(reader.GetOrdinal("CROLE_NOMBRE"))) eUsuarioRolInicial.CROLE_NOMBRE = reader.GetString("CROLE_NOMBRE");
                                    usuario.lstUsuarioRolInicial.Add(eUsuarioRolInicial);
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    EUsuarioMenu eUsuarioMenu = new();
                                    if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ID"))) eUsuarioMenu.CMENU_ID = reader.GetString("CMENU_ID");
                                    if (!reader.IsDBNull(reader.GetOrdinal("CMENU_NOMBRE"))) eUsuarioMenu.CMENU_NOMBRE = reader.GetString("CMENU_NOMBRE");
                                    if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ID_ORIGEN"))) eUsuarioMenu.CMENU_ID_ORIGEN = reader.GetString("CMENU_ID_ORIGEN");
                                    if (!reader.IsDBNull(reader.GetOrdinal("NMENU_ORDENAMIENTO"))) eUsuarioMenu.NMENU_ORDENAMIENTO = reader.GetInt32("NMENU_ORDENAMIENTO");
                                    if (!reader.IsDBNull(reader.GetOrdinal("CMENU_RUTA"))) eUsuarioMenu.CMENU_RUTA = reader.GetString("CMENU_RUTA");
                                    if (!reader.IsDBNull(reader.GetOrdinal("CMENU_ICONO"))) eUsuarioMenu.CMENU_ICONO = reader.GetString("CMENU_ICONO");
                                    if (!reader.IsDBNull(reader.GetOrdinal("NROME_VISIBLE"))) eUsuarioMenu.NROME_VISIBLE = reader.GetInt32("NROME_VISIBLE");

                                    usuario.lstUsuarioMenu.Add(eUsuarioMenu);
                                }
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
            }

            return usuario;
        }

        public async Task<EUsuarioListaPaginado> ListarPaginado(EUsuarioFiltro filtro)
        {
            EUsuarioListaPaginado objPaginado = null;
            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_USUARIO_LISTAR_PAGINADO";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUARO_ESTADO", filtro.NUARO_ESTADO);
                    cmd.Parameters.AddWithValue("P_USUARIO", filtro.USUARIO);
                    cmd.Parameters.AddWithValue("P_NTIRO_ID", filtro.NTIRO_ID);
                    cmd.Parameters.AddWithValue("P_NROLE_ID", filtro.NROLE_ID);
                    cmd.Parameters.AddWithValue("P_CPERS_CORREO", filtro.CPERS_CORREO);
                    cmd.Parameters.AddWithValue("P_NCADE_ID_SEXO", filtro.NCADE_ID_SEXO);
                    cmd.Parameters.AddWithValue("P_PAGE_SIZE", filtro.PAGE_SIZE);
                    cmd.Parameters.AddWithValue("P_PAGE_NUMBER", filtro.PAGE_NUMBER);
                    cmd.Parameters.AddWithValue("P_ORDER_BY", filtro.P_ORDER_BY);
                    cmd.Parameters.AddWithValue("P_ORDER", filtro.P_ORDER);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        objPaginado = new EUsuarioListaPaginado();
                        objPaginado.lstUsuarioPaginado = new List<EUsuarioRolLista>();
                        EUsuarioRolLista oUsuario = null;

                        while (await reader.ReadAsync())
                        {
                            oUsuario = new EUsuarioRolLista();
                            if (!reader.IsDBNull(reader.GetOrdinal("NUARO_ID"))) oUsuario.NUARO_ID = reader.GetInt32("NUARO_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("NUSUA_ID"))) oUsuario.NUSUA_ID = reader.GetInt32("NUSUA_ID");
                            //if (!reader.IsDBNull(reader.GetOrdinal("CUSUA_USERNAME"))) oUsuario.CUSUA_USERNAME = reader.GetString("CUSUA_USERNAME");
                            if (!reader.IsDBNull(reader.GetOrdinal("NUSUA_ESTADO"))) oUsuario.NUSUA_ESTADO = reader.GetInt32("NUSUA_ESTADO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CROLE_NOMBRE"))) oUsuario.CROLE_NOMBRE = reader.GetString("CROLE_NOMBRE");
                            //if (!reader.IsDBNull(reader.GetOrdinal("CTIRO_NOMBRE"))) oUsuario.CTIRO_NOMBRE = reader.GetString("CTIRO_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("NROLE_ESTADO"))) oUsuario.NROLE_ESTADO = reader.GetInt32("NROLE_ESTADO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NUARO_ESTADO"))) oUsuario.NUARO_ESTADO = reader.GetInt32("NUARO_ESTADO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_NOMBRES"))) oUsuario.CPERS_NOMBRES = reader.GetString("CPERS_NOMBRES");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_APE_PATERNO"))) oUsuario.CPERS_APE_PATERNO = reader.GetString("CPERS_APE_PATERNO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_APE_MATERNO"))) oUsuario.CPERS_APE_MATERNO = reader.GetString("CPERS_APE_MATERNO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_CORREO"))) oUsuario.CPERS_CORREO = reader.GetString("CPERS_CORREO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_AVATAR"))) oUsuario.CPERS_AVATAR = reader.GetString("CPERS_AVATAR");
                            if (!reader.IsDBNull(reader.GetOrdinal("DAUDI_REG_INS"))) oUsuario.DAUDI_REG_INS = reader.GetDateTime("DAUDI_REG_INS");
                            if (!reader.IsDBNull(reader.GetOrdinal("DAUDI_REG_UPD"))) oUsuario.DAUDI_REG_UPD = reader.GetDateTime("DAUDI_REG_UPD");
                            if (!reader.IsDBNull(reader.GetOrdinal("FECHA_CREACION_TEXT"))) oUsuario.FECHA_CREACION_TEXT = reader.GetString("FECHA_CREACION_TEXT");
                            if (!reader.IsDBNull(reader.GetOrdinal("FECHA_MODIFICACION_TEXT"))) oUsuario.FECHA_MODIFICACION_TEXT = reader.GetString("FECHA_MODIFICACION_TEXT");
                            if (!reader.IsDBNull(reader.GetOrdinal("USUARIO_RESPONSABLE"))) oUsuario.USUARIO_RESPONSABLE = reader.GetString("USUARIO_RESPONSABLE");
                            if (!reader.IsDBNull(reader.GetOrdinal("ESTADO_TEXTO"))) oUsuario.ESTADO_TEXTO = reader.GetString("ESTADO_TEXTO");
                            objPaginado.lstUsuarioPaginado.Add(oUsuario);
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

        public async Task<ECorreoElectronico> ObtenerConfirmacionPorId(int idUsuario)
        {
            var conn = _mysqlConexion.GetConnection();
            var proc = "PG_FACT_USUARIO.PA_FACT_ENVIAR_CONFIRMACION";
            ECorreoElectronico? usuarioConfirmacion = null;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUSUA_ID", idUsuario);
                    conn.Open();
                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null)
                        {
                            while (await reader.ReadAsync())
                            {
                                usuarioConfirmacion = new ECorreoElectronico();
                                if (!reader.IsDBNull(reader.GetOrdinal("ASUNTO"))) usuarioConfirmacion.ASUNTO = reader.GetString("ASUNTO");
                                if (!reader.IsDBNull(reader.GetOrdinal("EMAIL_DESTINO"))) usuarioConfirmacion.EMAIL_DESTINO = reader.GetString("EMAIL_DESTINO");
                                if (!reader.IsDBNull(reader.GetOrdinal("NOMBRE_DESTINO"))) usuarioConfirmacion.NOMBRE_DESTINO = reader.GetString("NOMBRE_DESTINO");
                                if (!reader.IsDBNull(reader.GetOrdinal("EMAIL_COPIA"))) usuarioConfirmacion.EMAIL_COPIA = reader.GetString("EMAIL_COPIA");
                                if (!reader.IsDBNull(reader.GetOrdinal("NOMBRE_COPIA"))) usuarioConfirmacion.NOMBRE_COPIA = reader.GetString("NOMBRE_COPIA");
                                if (!reader.IsDBNull(reader.GetOrdinal("CONTENIDO_TEXTO"))) usuarioConfirmacion.CONTENIDO_TEXTO = reader.GetString("CONTENIDO_TEXTO");
                                if (!reader.IsDBNull(reader.GetOrdinal("CONTENIDO_HTML"))) usuarioConfirmacion.CONTENIDO_HTML = reader.GetString("CONTENIDO_HTML");
                                if (!reader.IsDBNull(reader.GetOrdinal("ESTADO"))) usuarioConfirmacion.ESTADO = reader.GetInt32("ESTADO");
                                if (!reader.IsDBNull(reader.GetOrdinal("MSG"))) usuarioConfirmacion.MSG = reader.GetString("MSG");
                            }
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

        public async Task<List<EUsuarioRolLista>> Listar(EUsuarioFiltro filtro)
        {
            List<EUsuarioRolLista>? lstUsuarioPaginado = null;

            var conn = _mysqlConexion.GetConnection();
            var proc = "SP_USUARIO_LISTAR";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("P_NUARO_ESTADO", filtro.NUARO_ESTADO);
                    cmd.Parameters.AddWithValue("P_USUARIO", filtro.USUARIO);
                    cmd.Parameters.AddWithValue("P_NTIRO_ID", filtro.NTIRO_ID);
                    cmd.Parameters.AddWithValue("P_NROLE_ID", filtro.NROLE_ID);
                    cmd.Parameters.AddWithValue("P_CPERS_CORREO", filtro.CPERS_CORREO);
                    cmd.Parameters.AddWithValue("P_NCADE_ID_SEXO", filtro.NCADE_ID_SEXO);
                    cmd.Parameters.AddWithValue("P_ORDER_BY", filtro.P_ORDER_BY);
                    cmd.Parameters.AddWithValue("P_ORDER", filtro.P_ORDER);
                    conn.Open();
                    using MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        lstUsuarioPaginado = new List<EUsuarioRolLista>();
                        while (await reader.ReadAsync())
                        {
                            var oUsuario = new EUsuarioRolLista();
                            if (!reader.IsDBNull(reader.GetOrdinal("NUMERO"))) oUsuario.NUMERO = reader.GetInt32("NUMERO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NUARO_ID"))) oUsuario.NUARO_ID = reader.GetInt32("NUARO_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("NUSUA_ID"))) oUsuario.NUSUA_ID = reader.GetInt32("NUSUA_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("NUSUA_ESTADO"))) oUsuario.NUSUA_ESTADO = reader.GetInt32("NUSUA_ESTADO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CROLE_NOMBRE"))) oUsuario.CROLE_NOMBRE = reader.GetString("CROLE_NOMBRE");
                            if (!reader.IsDBNull(reader.GetOrdinal("NROLE_ESTADO"))) oUsuario.NROLE_ESTADO = reader.GetInt32("NROLE_ESTADO");
                            if (!reader.IsDBNull(reader.GetOrdinal("NUARO_ESTADO"))) oUsuario.NUARO_ESTADO = reader.GetInt32("NUARO_ESTADO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_NOMBRES"))) oUsuario.CPERS_NOMBRES = reader.GetString("CPERS_NOMBRES");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_APE_PATERNO"))) oUsuario.CPERS_APE_PATERNO = reader.GetString("CPERS_APE_PATERNO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_APE_MATERNO"))) oUsuario.CPERS_APE_MATERNO = reader.GetString("CPERS_APE_MATERNO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_CORREO"))) oUsuario.CPERS_CORREO = reader.GetString("CPERS_CORREO");
                            if (!reader.IsDBNull(reader.GetOrdinal("CPERS_AVATAR"))) oUsuario.CPERS_AVATAR = reader.GetString("CPERS_AVATAR");
                            if (!reader.IsDBNull(reader.GetOrdinal("DAUDI_REG_INS"))) oUsuario.DAUDI_REG_INS = reader.GetDateTime("DAUDI_REG_INS");
                            if (!reader.IsDBNull(reader.GetOrdinal("DAUDI_REG_UPD"))) oUsuario.DAUDI_REG_UPD = reader.GetDateTime("DAUDI_REG_UPD");
                            if (!reader.IsDBNull(reader.GetOrdinal("FECHA_CREACION_TEXT"))) oUsuario.FECHA_CREACION_TEXT = reader.GetString("FECHA_CREACION_TEXT");
                            if (!reader.IsDBNull(reader.GetOrdinal("FECHA_MODIFICACION_TEXT"))) oUsuario.FECHA_MODIFICACION_TEXT = reader.GetString("FECHA_MODIFICACION_TEXT");
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
