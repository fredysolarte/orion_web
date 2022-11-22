using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;


namespace TESIS.DAL.AdminUser
{
	public class UsuariosBD
	{
		public static int GetUsuarioExiste(SessionManager oSessionManager, string usuario, string password)
		{
			StringBuilder sSql = new StringBuilder();
			try
			{								
				sSql.AppendLine("SELECT count(*) ");				
				sSql.AppendLine("FROM admi_tusuario WHERE usua_usuario = @p0 AND usua_clave = @p1 AND usua_estado = 1");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, usuario, password));
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

        public static DataTable GetUsuarioExiste(SessionManager oSessionManager, string usuario, string password, int sist_sistema)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("select top 1 admi_tusuario.*, admi_tsistema.sist_identifica, (SELECT r.date_format FROM master.sys.dm_exec_requests r WHERE r.session_id = @@SPID) fto_date  ");
                sSql.AppendLine("from admi_tusuario with(nolock)");
                sSql.AppendLine("inner join admi_trolxusuario with(nolock) on (admi_tusuario.usua_usuario = admi_trolxusuario.usua_usuario)");
                sSql.AppendLine("inner join admi_tmoduloxrol with(nolock) on(admi_trolxusuario.rolm_rolm = admi_tmoduloxrol.rolm_rolm )");
                sSql.AppendLine("inner join admi_tsistema with(nolock) on(admi_tsistema.sist_sistema  = admi_tmoduloxrol.sist_sistema )");
                sSql.AppendLine("WHERE admi_tusuario.usua_usuario = @p0 and admi_tusuario.usua_clave = @p1 and usua_estado = 1 and admi_tmoduloxrol.sist_sistema =@p2");


                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, usuario, password, sist_sistema);
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

		public static DataTable GetListByUserAndSystem(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, string userLogon, int systemId)
		{
			StringBuilder sSql = new StringBuilder();			
			object[] parameterValues = new object[2];
			parameterValues[0] = userLogon;
			parameterValues[1] = systemId;

			try
			{
				sSql.AppendLine("SELECT DISTINCT admi_tmodulo.sist_sistema");
				sSql.AppendLine("      ,admi_tmodulo.modu_modulo");
				sSql.AppendLine("      ,admi_tmodulo.modu_nombre");
				sSql.AppendLine("      ,admi_tmodulo.modu_descripcion");
				sSql.AppendLine("      ,admi_tmodulo.blob_ayuda");
				sSql.AppendLine("      ,admi_tmodulo.modu_parametros");
				sSql.AppendLine("      ,admi_tmodulo.icon_icono");
				sSql.AppendLine("      ,admi_tmodulo.modu_estado");
				sSql.AppendLine("      ,admi_tmodulo.logs_usuario");
				sSql.AppendLine("      ,admi_tmodulo.modu_orden");
				sSql.AppendLine("      ,admi_tmodulo.logs_fecha, '' as nombre_ayuda,'' as nombre_icono  FROM admi_tmodulo");
				sSql.AppendLine("INNER JOIN admi_tmoduloxrol");
				sSql.AppendLine("ON admi_tmodulo.modu_modulo = admi_tmoduloxrol.modu_modulo");
				sSql.AppendLine("INNER JOIN admi_trol");
				sSql.AppendLine("ON admi_trol.rolm_rolm = admi_tmoduloxrol.rolm_rolm AND admi_trol.rolm_estado = 1");
				sSql.AppendLine("INNER JOIN admi_trolxusuario ");
				sSql.AppendLine("ON admi_trolxusuario.rolm_rolm = admi_trol.rolm_rolm ");
				sSql.AppendLine("INNER JOIN admi_tusuario");
				sSql.AppendLine("ON admi_tusuario.usua_usuario = admi_trolxusuario.usua_usuario AND admi_tusuario.usua_usuario = @p0");
				sSql.AppendLine("WHERE admi_tmodulo.modu_app not in (1) AND admi_tmodulo.modu_estado = 1 AND admi_tmodulo.sist_sistema = @p1");
                sSql.AppendLine("AND admi_tmodulo.modu_modulo not in");
                sSql.AppendLine("(");
                sSql.AppendLine("    SELECT modu_modulo FROM admi_trestringemodulo");
                sSql.AppendLine("    WHERE usua_usuario = @p0 AND sist_sistema = @p1");
                sSql.AppendLine(") order by admi_tmodulo.modu_orden ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString().Trim(), CommandType.Text, userLogon, systemId);
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

		public static DataTable GetOptionsByUserAndModule(SessionManager oSessionManager, string userLogon, int moduleId, int sistemaId, int parentId)
		{
			StringBuilder sSql = new StringBuilder();		
			try
			{
                sSql.AppendLine("SELECT arop_arbolopcion,arop_idunicopadre,arop_nombre,form_nombre,opci_hint,opci_reporte,opci_opcion FROM ");
				sSql.AppendLine("(");
                sSql.AppendLine("SELECT admi_topcion.opci_opcion,opci_reporte,admi_tarbolopcion.arop_arbolopcion, CASE WHEN arop_idunicopadre IN");
				sSql.AppendLine("(");
				sSql.AppendLine("	SELECT        arop_arbolopcion");
				sSql.AppendLine("	FROM            admi_tarbolopcion");
				sSql.AppendLine("	WHERE        arop_entidad = 'M') THEN 0 ELSE arop_idunicopadre END AS arop_idunicopadre, admi_tarbolopcion.arop_nombre, ");
				sSql.AppendLine("	'~/' + admi_tformulario.form_nombre AS form_nombre, admi_topcion.opci_hint, admi_topcion.opci_orden");
				sSql.AppendLine("	FROM            admi_topcion INNER JOIN");
				sSql.AppendLine("	admi_tarbolopcion ON admi_topcion.opci_opcion = admi_tarbolopcion.arop_idoriginal LEFT OUTER JOIN");
				sSql.AppendLine("	admi_tformulario ON admi_topcion.opci_opcion = admi_tformulario.opci_opcion");
				sSql.AppendLine("	WHERE        (admi_tarbolopcion.arop_entidad = 'O') ");

				sSql.AppendLine(" AND (admi_tarbolopcion.arop_idoriginal IN");
				sSql.AppendLine("	(");
				sSql.AppendLine("		SELECT DISTINCT admi_topcionxrol.opci_opcion");
				sSql.AppendLine("		FROM            admi_topcionxrol INNER JOIN");
				sSql.AppendLine("		admi_trol ON admi_topcionxrol.rolm_rolm = admi_trol.rolm_rolm INNER JOIN");
				sSql.AppendLine("		admi_trolxusuario ON admi_trol.rolm_rolm = admi_trolxusuario.rolm_rolm INNER JOIN");
				sSql.AppendLine("		admi_topcion AS admi_topcion_1 ON admi_topcionxrol.opci_opcion = admi_topcion_1.opci_opcion");
				sSql.AppendLine("		WHERE        (admi_trolxusuario.usua_usuario = @p0) ");
                sSql.AppendLine("       AND (admi_topcionxrol.opci_opcion NOT IN");
                sSql.AppendLine("		(");
                sSql.AppendLine("			SELECT        opci_opcion");
                sSql.AppendLine("			FROM            admi_trestringeopcion");
                sSql.AppendLine("			WHERE        (usua_usuario = @p0)");
                sSql.AppendLine("		) )");
				sSql.AppendLine("		 AND ");
				sSql.AppendLine("		(admi_topcion_1.opci_estado = 1) AND (admi_trol.rolm_estado = 1) AND ");
				sSql.AppendLine("			(admi_topcion_1.modu_modulo = @p1) AND (admi_topcion_1.sist_sistema = @p2)");
				sSql.AppendLine("	)");
				sSql.AppendLine("	) ");

				sSql.AppendLine(" AND (admi_tarbolopcion.modu_modulo = @p1) AND ");
				sSql.AppendLine("			(admi_topcion.opci_estado = 1) AND (admi_topcion.modu_modulo = @p1) AND (admi_tarbolopcion.sist_sistema = @p2)");
				sSql.AppendLine(") AS T");
				sSql.AppendLine(" WHERE T.arop_idunicopadre = @p3 ");
				sSql.AppendLine(" ORDER BY opci_orden");
				//return doObject.GetDataTable(oSessionManager, sSql.ToString().Trim(), CommandType.Text, userLogon, moduleId, sistemaId, parentId);
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString().Trim(), CommandType.Text, userLogon, moduleId, sistemaId, parentId);
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

		public static DataTable GetOptionsByURLForm(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, string urlForm)
		{
			StringBuilder sSql = new StringBuilder();
			try
			{
				sSql.AppendLine("SELECT        admi_topcion.sist_sistema, admi_topcion.modu_modulo");
				sSql.AppendLine("FROM            admi_tformulario INNER JOIN");
				sSql.AppendLine("admi_topcion ON admi_tformulario.opci_opcion = admi_topcion.opci_opcion");
				sSql.AppendLine("WHERE        (upper(admi_tformulario.form_nombre) = upper(@p0)) ");
				if (filter != String.Empty && filter != null)
				{
					sSql.AppendLine(" AND " + filter);
				}
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString().Trim(), CommandType.Text, urlForm);
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

		public static string GetUrlPrincipalPorIdModulo(SessionManager oSessionManager, int idModulo)
		{
			StringBuilder sSql = new StringBuilder();
			try
			{
				sSql.AppendLine("SELECT admi_tformulario.form_nombre");
				sSql.AppendLine("FROM admi_topcion, admi_tformulario");
				sSql.AppendLine("WHERE admi_topcion.opci_opcion = admi_tformulario.opci_opcion AND admi_topcion.opci_principal = 1 AND ");
				sSql.AppendLine("admi_topcion.modu_modulo = @p0");
                object url = DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, idModulo);
				if (url != null)
				{
					return Convert.ToString(url);
				}
				else
				{
					return null;
				}
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

		public static int ChangePassword(SessionManager oSessionManager, string usuario, string password)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("UPDATE admi_tusuario  ");
				sSql.AppendLine("   SET usua_clave=@p0 ");
				sSql.AppendLine(" WHERE usua_usuario=@p1 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, password, usuario);
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

        public static DataTable GetPerimosFormulario(SessionManager oSessionManager, string usuario, int sistema)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("select admi_topcion.opci_opcion,ctrl_nombre");
                sSql.AppendLine("  from admi_tcontrolxrol");
                sSql.AppendLine("inner join admi_tcontrol on (admi_tcontrolxrol.ctrl_control = admi_tcontrol.ctrl_control)");
                sSql.AppendLine("inner join admi_trol on (admi_trol.rolm_rolm = admi_tcontrolxrol.rolm_rolm)");
                sSql.AppendLine("inner join admi_trolxusuario on (admi_trolxusuario.rolm_rolm = admi_trol.rolm_rolm)");
                sSql.AppendLine("inner join admi_tusuario on (admi_tusuario.usua_usuario = admi_trolxusuario.usua_usuario)");
                sSql.AppendLine("inner join admi_tformulario on (admi_tformulario.form_formulario = admi_tcontrol.form_formulario)");
                sSql.AppendLine("inner join admi_topcion on (admi_tformulario.opci_opcion = admi_topcion.opci_opcion)");
                sSql.AppendLine("where admi_tusuario.usua_usuario =@p0 and ctrl_estado = 1 and admi_topcion.sist_sistema =@p1");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, usuario, sistema);
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
