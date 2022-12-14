using System;
using System.Data;
using System.Text;
using BE.Administracion;
using DataAccess;
using Mapping;
using System.Collections.Generic;
using XUSS.DAL.Genericas;

namespace DAL.Administracion
{
	public class AdmiUsuarioDB : GenericBaseDB<AdmiUsuario>
	{
		public DataTable GetUserPerson(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();
			sSql.AppendLine("SELECT admi_tusuario.usua_usuario, gral_tpersona.pers_nombre");
			sSql.AppendLine("FROM admi_tusuario INNER JOIN");
			sSql.AppendLine("gral_tpersona ON admi_tusuario.usua_identifica = gral_tpersona.pers_nrodoc");
			return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
		}
        public IDataReader GetDataUser(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.AppendLine("SELECT * ");
            sSql.AppendLine("FROM admi_tusuario WITH(NOLOCK)");
            sSql.AppendLine(" WHERE 1=1 "+filter);
            return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text);
        }
        public DataTable GetDataUser(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.AppendLine("SELECT * ");
            sSql.AppendLine("FROM admi_tusuario WITH(NOLOCK)");
            sSql.AppendLine(" WHERE 1=1 " + filter);
            return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
        }
        //solo para el demo
        public DataTable GetUserPerson(SessionManager oSessionManager, string userLogon)
		{
			StringBuilder sSql = new StringBuilder();
			sSql.AppendLine("select * from admi_tusuario ");
			sSql.AppendLine("inner join personas");
			sSql.AppendLine("on admi_tusuario.usua_identifica = personas.pers_nrodoc");
			sSql.AppendLine("WHERE usua_usuario = @p0");
			return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, userLogon);
		}

		public void ChangePassword(SessionManager oSessionManager, string userLogon, string password)
		{
			DBAccess.ExecuteNonQuery(oSessionManager, "UPDATE admi_tusuario SET usua_clave = @p0 , usua_fechavence = @p2, usua_cambiaclave = 0 WHERE usua_usuario = @p1",
									CommandType.Text, password, userLogon, DateTime.Now.AddMonths(3));
		}

		public AdmiUsuario UserLogin(SessionManager oSessionManager, string userLogon, string password, int system)
		{
			StringBuilder sSql = new StringBuilder();
			//				parameterValues[1] = userLogon,password;
			sSql.AppendLine("SELECT usua_usuario");
			sSql.AppendLine("      ,usua_secuencia");
			sSql.AppendLine("      ,usua_identifica");
			sSql.AppendLine("      ,usua_nombres");
			sSql.AppendLine("      ,usua_direccion");
			sSql.AppendLine("      ,usua_telefonos");
			sSql.AppendLine("      ,usua_email");
			sSql.AppendLine("      ,usua_clave");
			sSql.AppendLine("      ,usua_fechavence");
			sSql.AppendLine("      ,usua_encripta");
			sSql.AppendLine("      ,usua_clavecaduca");
			sSql.AppendLine("      ,usua_publicareporte");
			sSql.AppendLine("      ,usua_restringeip");
			sSql.AppendLine("      ,usua_restringehora");
			sSql.AppendLine("      ,usua_ultimoacceso");
			sSql.AppendLine("      ,usua_ultimoequipo");
			sSql.AppendLine("      ,usua_ultimaip");
			sSql.AppendLine("      ,usua_cambiaclave");
			sSql.AppendLine("      ,usua_fallos");
			sSql.AppendLine("      ,usua_estado");
			sSql.AppendLine("      ,logs_usuario");
			sSql.AppendLine("      ,logs_fecha");
			sSql.AppendLine("FROM admi_tusuario WHERE usua_usuario = @p0 AND usua_clave = @p1 AND usua_estado = 1");
			AdmiUsuario oEntity = MappingData<AdmiUsuario>.Mapping(DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, userLogon, password));
			if (oEntity == null)
			{
				DBAccess.ExecuteNonQuery(oSessionManager, "UPDATE admi_tusuario SET usua_fallos = usua_fallos + 1 WHERE usua_usuario = @p0",
										CommandType.Text, userLogon);
				if (Convert.ToInt32(DBAccess.GetScalar(oSessionManager, "SELECT usua_fallos FROM admi_tusuario WHERE usua_usuario = @p0", CommandType.Text, userLogon)) >= 3)
				{
					DBAccess.ExecuteNonQuery(oSessionManager, "UPDATE admi_tusuario SET usua_estado = 0 WHERE usua_usuario = @p0", CommandType.Text, userLogon);
					sSql = null;
					throw new ApplicationException("Usuario bloqueado por intentos fallidos de acceso");
				}
				else
				{
					throw new ApplicationException("El usuario no existe o la contraseña es incorrecta");
				}
			}
			else
			{
				sSql = new StringBuilder();
				sSql.AppendLine("select count(*) as nummodulos from admi_tmodulo ");
				sSql.AppendLine("inner join admi_tmoduloxrol");
				sSql.AppendLine("on admi_tmodulo.modu_modulo = admi_tmoduloxrol.modu_modulo");
				sSql.AppendLine("inner join admi_trolxusuario");
				sSql.AppendLine("on admi_tmoduloxrol.rolm_rolm = admi_trolxusuario.rolm_rolm");
				sSql.AppendLine("where admi_tmoduloxrol.sist_sistema = @p0 and admi_trolxusuario.usua_usuario = @p1 ");
				sSql.AppendLine("and admi_tmoduloxrol.modu_modulo not in ");
				sSql.AppendLine("(        select admi_trestringemodulo.modu_modulo ");
				sSql.AppendLine("         from admi_trestringemodulo ");
				sSql.AppendLine("         where sist_sistema = @p0 and usua_usuario = @p1 )");

				if (Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, system, userLogon)) == 0)
				{
					throw new ApplicationException("No tiene permisos de acceso al sistema");
				}
				DBAccess.ExecuteNonQuery(oSessionManager, "UPDATE admi_tusuario SET usua_fallos = 0 WHERE usua_usuario = @p0", CommandType.Text, userLogon);
				sSql = null;
			}
			return oEntity;
		}

        public string Add(SessionManager oSessionManager, AdmiUsuario item)
        {
            StringBuilder sSql = new StringBuilder();
            string result = string.Empty;
            int seq = 0;
            try
            {
                //seq = DBAccess.GetSequence(oSessionManager, "admi_pGeneraSecuencia", ((Table)item.GetType().GetCustomAttributes(typeof(Table), true)[0]).EntityId, 0, 0);
                seq = GenericaBD.GetSecuencia(oSessionManager,14,0);
                sSql = new StringBuilder();
                sSql.AppendLine("INSERT INTO admi_tusuario");
                sSql.AppendLine("(usua_usuario, usua_secuencia, usua_identifica, usua_nombres, usua_direccion, usua_telefonos, usua_email, usua_clave, usua_fechavence, usua_encripta, ");
                sSql.AppendLine("usua_clavecaduca, usua_publicareporte, usua_restringeip, usua_restringehora, usua_ultimoacceso, usua_ultimoequipo, usua_ultimaip, usua_cambiaclave, ");
                sSql.AppendLine("usua_fallos, usua_estado, logs_usuario, logs_fecha, usua_administrador)");
                sSql.AppendLine("VALUES        (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,GETDATE(),@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,GETDATE(),@p20)");
                //DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, item.UsuaUsuario, seq, item.UsuaIdentifica, item.UsuaNombres, item.UsuaDireccion, item.UsuaTelefonos, item.UsuaEmail, item.UsuaClave, item.UsuaFechavence, item.UsuaEncripta,
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, item.UsuaUsuario, seq, item.UsuaIdentifica, item.UsuaNombres, item.UsuaDireccion, item.UsuaTelefonos, item.UsuaEmail, item.UsuaClave, item.UsuaEncripta,
                                         item.UsuaClavecaduca, item.UsuaPublicareporte, item.UsuaRestringeip, item.UsuaRestringehora, item.UsuaUltimoacceso, item.UsuaUltimoequipo, item.UsuaUltimaip, item.UsuaCambiaClave,
                                         0, item.UsuaEstado, item.LogsUsuario, item.usuaadministrador);
                                         //0, item.UsuaEstado, item.LogsUsuario, item.LogsFecha);
                result = item.UsuaUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
            return result;
        }

        public List<AdmiRol> GetListByUserId(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, string userId)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT admi_tusuario.usua_usuario, gral_tpersona.pers_nombre");
                sSql.AppendLine("FROM admi_tusuario INNER JOIN");
                sSql.AppendLine("gral_tpersona ON admi_tusuario.usua_identifica = gral_tpersona.pers_nrodoc");
                return MappingData<AdmiRol>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString().Trim(), CommandType.Text, null));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
        }

        public void SetRolesByUser(SessionManager oSessionManager, List<AdmiRolxUsuario> olEntity, List<AdmiArbolOpcion> olRestictions, string userLogon, int userId)
        {
            StringBuilder sSql;
            AdmiRolxUsuarioDB objDB = new AdmiRolxUsuarioDB();

            try
            {
                object[] parameterValues = new object[1];
                parameterValues[0] = userLogon;
                sSql = new StringBuilder();
                sSql.AppendLine("DELETE admi_trolxusuario WHERE usua_usuario = @p0;");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, parameterValues);
                
                sSql.Clear();
                sSql.AppendLine("DELETE admi_trestringeopcion WHERE usua_usuario = @p0;");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, parameterValues);

                sSql.Clear();
                sSql.AppendLine("DELETE admi_trestringemodulo WHERE usua_usuario = @p0;");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, parameterValues);

                sSql.Clear();
                sSql.AppendLine("DELETE admi_trestringecontrol WHERE usua_usuario = @p0;");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, parameterValues);

                foreach (AdmiRolxUsuario oEntity in olEntity)
                {
                    objDB.Add(oSessionManager, oEntity);
                }

                foreach (AdmiArbolOpcion oEntity in olRestictions)
                {
                    switch (oEntity.AropEntidad)
                    {
                        case "M":
                            AdmiRestringeModulo oRestringeModuloEntity = new AdmiRestringeModulo();
                            AdmiRestringeModuloDB oRestringeModuloDB = new AdmiRestringeModuloDB();

                            oRestringeModuloEntity.ModuModulo = Convert.ToInt32(oEntity.AropIdOriginal);
                            oRestringeModuloEntity.UsuaUsuario = userLogon;
                            oRestringeModuloEntity.SistSistema = Convert.ToInt32(oEntity.SistSistema);

                            oRestringeModuloDB.Add(oSessionManager, oRestringeModuloEntity);

                            break;
                        case "O":
                            AdmiRestringeOpcion oRestringeOpcionEntity = new AdmiRestringeOpcion();
                            AdmiRestringeOpcionDB oRestringeOpcionDB = new AdmiRestringeOpcionDB();

                            oRestringeOpcionEntity.OpciOpcion = Convert.ToInt32(oEntity.AropIdOriginal);
                            oRestringeOpcionEntity.UsuaUsuario = userLogon;
                            oRestringeOpcionEntity.LogsUsuario = userId;
                            oRestringeOpcionEntity.LogsFecha = DateTime.Now;
                            oRestringeOpcionDB.Add(oSessionManager, oRestringeOpcionEntity);
                            break;
                        case "C":
                            AdmiRestringeControl oRestringeControlEntity = new AdmiRestringeControl();
                            AdmiRestringeControlDB oRestringeControlDB = new AdmiRestringeControlDB();

                            oRestringeControlEntity.CtrlControl = Convert.ToInt32(oEntity.AropIdOriginal);
                            oRestringeControlEntity.UsuaUsuario = userLogon;
                            oRestringeControlEntity.LogsUsuario = userId;
                            oRestringeControlEntity.LogsFecha = DateTime.Now;
                            oRestringeControlDB.Add(oSessionManager, oRestringeControlEntity);
                            break;
                        default:
                            break;
                    }
                    //objDB.Add(oSessionManager, oEntity);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
                objDB = null;
            }
        }

        public void ResetPassword(SessionManager oSessionManager, string userLogon, string defaultPassword, int userId)
        {
            StringBuilder sSql;

            try
            {
                object[] parameterValues = new object[5];
                parameterValues[0] = defaultPassword;
                parameterValues[1] = userId;
                parameterValues[2] = DateTime.Now;
                parameterValues[3] = userLogon;
                parameterValues[4] = DateTime.Now.AddDays(-1);
                sSql = new StringBuilder();
                sSql.AppendLine("UPDATE admi_tusuario SET usua_clave = @p0 , usua_fechavence = @p4, logs_usuario = @p1 , logs_fecha = @p2 WHERE usua_usuario = @p3");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, parameterValues);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
        }
	}
}