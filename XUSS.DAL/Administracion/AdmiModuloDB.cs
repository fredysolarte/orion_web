using System.Collections.Generic;
using System.Data;
using System.Text;
using BE.Administracion;
using DataAccess;
using Mapping;
using System;
using XUSS.DAL.Genericas;

namespace DAL.Administracion
{
	public class AdmiModuloDB
	{
		public static List<AdmiModulo> GetListByUserAndSystem(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, string userLogon, int systemId)
		{
			StringBuilder sSql = new StringBuilder();
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
			sSql.AppendLine("      ,admi_tmodulo.logs_fecha, '' as nombre_ayuda,'' as nombre_icono, admi_tmodulo.modu_app  FROM admi_tmodulo with(nolock)");
			sSql.AppendLine("INNER JOIN admi_tmoduloxrol with(nolock) ON (admi_tmodulo.modu_modulo = admi_tmoduloxrol.modu_modulo)");
			sSql.AppendLine("INNER JOIN admi_trol with(nolock) ON (admi_trol.rolm_rolm = admi_tmoduloxrol.rolm_rolm AND admi_trol.rolm_estado = 1)");
			sSql.AppendLine("INNER JOIN admi_trolxusuario with(nolock) ON (admi_trolxusuario.rolm_rolm = admi_trol.rolm_rolm) ");
			sSql.AppendLine("INNER JOIN admi_tusuario with(nolock) ON (admi_tusuario.usua_usuario = admi_trolxusuario.usua_usuario AND admi_tusuario.usua_usuario = @p0) ");
			sSql.AppendLine("WHERE admi_tmodulo.modu_app not in (1) AND admi_tmodulo.modu_estado = 1 AND admi_tmodulo.sist_sistema = @p1");
			sSql.AppendLine("AND admi_tmodulo.modu_modulo not in");
			sSql.AppendLine("(");
			sSql.AppendLine("    SELECT modu_modulo FROM admi_trestringemodulo");
			sSql.AppendLine("    WHERE usua_usuario = @p0 AND sist_sistema = @p1");
			sSql.AppendLine(")");

			if (!string.IsNullOrWhiteSpace(filter))
			{
				sSql.AppendLine(" AND " + filter);
			}
			sSql.AppendLine("ORDER BY admi_tmodulo.modu_orden");
			return MappingData<AdmiModulo>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, userLogon, systemId));
		}

        public List<AdmiModulo> GetListBySystem(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, int systemId)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT admi_tmodulo.sist_sistema, admi_tmodulo.modu_modulo, admi_tmodulo.modu_nombre, admi_tmodulo.modu_descripcion, admi_tmodulo.blob_ayuda, ");
                sSql.AppendLine("admi_tmodulo.modu_parametros, admi_tmodulo.icon_icono, admi_tmodulo.modu_estado, admi_tmodulo.logs_usuario,  ");
                sSql.AppendLine("admi_tmodulo.logs_fecha, admi_tmodulo.modu_orden,admi_tformulario.form_nombre,admi_tmodulo.modu_app ");
                sSql.AppendLine("FROM admi_tmodulo WITH(NOLOCK) ");                
                sSql.AppendLine("INNER JOIN admi_topcion WITH(NOLOCK) ON (admi_topcion.modu_modulo = admi_tmodulo.modu_modulo AND admi_topcion.sist_sistema = admi_tmodulo.sist_sistema)");
                sSql.AppendLine("INNER JOIN admi_tformulario WITH(NOLOCK) ON (admi_tformulario.opci_opcion = admi_topcion.opci_opcion AND admi_topcion.opci_principal = 1)");
                sSql.AppendLine("WHERE admi_tmodulo.sist_sistema = " + systemId);

                if (filter != String.Empty && filter != null)
                {
                    sSql.AppendLine(" AND " + filter);
                }
                return MappingData<AdmiModulo>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString().Trim(), CommandType.Text, null));
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

        public List<AdmiModulo> GetAllList(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();           
            try
            {
                sSql.AppendLine("SELECT admi_tmodulo.sist_sistema, admi_tmodulo.modu_modulo, admi_tmodulo.modu_nombre, admi_tmodulo.modu_descripcion, admi_tmodulo.blob_ayuda, ");
                sSql.AppendLine("admi_tmodulo.modu_parametros, admi_tmodulo.icon_icono, admi_tmodulo.modu_estado, admi_tmodulo.logs_usuario,  ");
                sSql.AppendLine("admi_tmodulo.logs_fecha, admi_tmodulo.modu_orden, admi_tformulario.form_nombre,admi_tmodulo.modu_app ");
                sSql.AppendLine("FROM admi_tmodulo WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN admi_topcion WITH(NOLOCK) ON (admi_topcion.modu_modulo = admi_tmodulo.modu_modulo AND admi_topcion.sist_sistema = admi_tmodulo.sist_sistema)");
                sSql.AppendLine("INNER JOIN admi_tformulario WITH(NOLOCK) ON (admi_tformulario.opci_opcion = admi_topcion.opci_opcion AND admi_topcion.opci_principal = 1)");
                //sSql.AppendLine("WHERE admi_tmodulo.modu_app not in (1)");

                if (!string.IsNullOrEmpty(filter))
                {
                    sSql.AppendLine(" WHERE " + filter);
                }
                return MappingData<AdmiModulo>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString().Trim(), CommandType.Text, null));
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

        public int Add(SessionManager oSessionManager, AdmiModulo objEntity)
        {            
            StringBuilder sSql = new StringBuilder();
            object[] parametersSequence = new object[2];
            int seq = -1;
            try
            {
                parametersSequence[0] = ((Table)objEntity.GetType().GetCustomAttributes(typeof(Table), true)[0]).EntityId;
                parametersSequence[1] = 0;
                //parametersSequence[2] = 0;
                //seq = DBAccess.GetSequence(oSessionManager, "admi_pgeneraSecuencia",4,0,true);                
                seq = GenericaBD.GetSecuencia(oSessionManager, 19, 0);

                sSql.AppendLine("INSERT INTO admi_tmodulo");
                sSql.AppendLine("(sist_sistema, modu_modulo, modu_nombre, modu_descripcion, blob_ayuda, modu_parametros, icon_icono, modu_estado, logs_usuario, logs_fecha, modu_app)");
                sSql.AppendLine("VALUES        (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, objEntity.SistSistema, seq, objEntity.ModuNombre, objEntity.ModuDescripcion, objEntity.BlobAyuda, objEntity.ModuParametros, objEntity.IconIcono, objEntity.ModuEstado, objEntity.LogsUsuario, objEntity.LogsFecha, objEntity.ModuApp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {                
                sSql = null;
            }
            return seq;
        }



        public void Update(SessionManager oSessionManager, AdmiModulo objEntity)
        {            
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE       admi_tmodulo");
                sSql.AppendLine("SET modu_nombre = @p2, modu_descripcion = @p3, blob_ayuda = @p4, modu_parametros = @p5, icon_icono = @p6, modu_estado = @p7, ");
                sSql.AppendLine("logs_usuario = @p8, logs_fecha = @p9, modu_orden = @p10, modu_app = @p11");
                sSql.AppendLine("WHERE ( modu_modulo= @p0) AND (sist_sistema = @p1)");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, objEntity.ModuModulo, objEntity.SistSistema, objEntity.ModuNombre, objEntity.ModuDescripcion, objEntity.BlobAyuda, objEntity.ModuParametros, objEntity.IconIcono, objEntity.ModuEstado, objEntity.LogsUsuario, objEntity.LogsFecha, objEntity.ModuOrden, objEntity.ModuApp);
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