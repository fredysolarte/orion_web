using System;
using System.Data;
using System.Text;
using DataAccess;
using Mapping;
using BE.Administracion;
using System.Collections.Generic;
using XUSS.DAL.Genericas;

namespace DAL.Administracion
{
	public class AdmiOpcionDB
	{
		public static string GetUrlPrincipalPorIdModulo(SessionManager oSessionManager, int idModulo)
		{
			StringBuilder sSql = new StringBuilder();
			sSql.AppendLine("SELECT admi_tformulario.form_nombre");
			sSql.AppendLine("FROM admi_topcion, admi_tformulario");
			sSql.AppendLine("WHERE admi_topcion.opci_opcion = admi_tformulario.opci_opcion AND admi_topcion.opci_principal = 1 AND ");
			sSql.AppendLine("admi_topcion.modu_modulo = @p0");
			object url = DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, idModulo);
			if (url != null || !(url is DBNull))
			{
				return Convert.ToString(url);
			}
			else
			{
				return null;
			}
		}

        public List<AdmiOpcion> GetListByIdModuloAndIdSistema(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, int idModulo, int idSistema)
        {
            StringBuilder sSql = new StringBuilder();
            
            try
            {
                sSql.AppendLine("SELECT admi_topcion.opci_opcion, admi_topcion.sist_sistema, admi_topcion.modu_modulo, admi_topcion.icon_icono, admi_topcion.blob_ayuda, ");
                sSql.AppendLine("admi_topcion.opci_nombre, admi_topcion.opci_etiqueta, admi_topcion.para_clase2, admi_topcion.opci_comando, admi_topcion.opci_padre,");
                sSql.AppendLine("admi_topcion.opci_orden, admi_topcion.opci_hint, admi_topcion.opci_estado, admi_topcion.logs_usuario, admi_topcion.logs_fecha, ");
                sSql.AppendLine("admi_topcion.opci_principal,'' form_nombre, '1' opci_reporte ");
                sSql.AppendLine("FROM admi_topcion ");
                //sSql.AppendLine("LEFT OUTER JOIN admi_tblob ON admi_topcion.blob_ayuda = admi_tblob.blob_blob ");
                //sSql.AppendLine("LEFT OUTER JOIN admi_tblob AS admi_tblob_1 ON admi_topcion.icon_icono = admi_tblob_1.blob_blob");
                sSql.AppendLine("WHERE (admi_topcion.modu_modulo = @p0) AND (admi_topcion.sist_sistema = @p1)");
                return MappingData<AdmiOpcion>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, idModulo, idSistema));
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

        public int Add(SessionManager oSessionManager, AdmiOpcion objEntity)
        {            
            StringBuilder sSql = new StringBuilder();
            object[] parametersSequence = new object[2];
            int seq = -1;
            try
            {
                parametersSequence[0] = ((Table)objEntity.GetType().GetCustomAttributes(typeof(Table), true)[0]).EntityId;
                parametersSequence[1] = 0;
                seq = GenericaBD.GetSecuencia(oSessionManager, 8, 0);
                //parametersSequence[2] = 0;
                //seq = DBAccess.GetSequence(oSessionManager, "admi_pGeneraSecuencia", parametersSequence);
                //seq = 0;
                sSql.AppendLine("INSERT INTO admi_topcion");
                sSql.AppendLine("(opci_opcion, sist_sistema, modu_modulo, icon_icono, blob_ayuda, opci_nombre, opci_etiqueta, para_clase2, opci_comando, opci_padre, opci_orden, opci_hint,");
                sSql.AppendLine("opci_estado, logs_usuario, logs_fecha,opci_principal,opci_reporte)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16)");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, seq, objEntity.SistSistema, objEntity.ModuModulo, objEntity.IconIcono, objEntity.BlobAyuda, objEntity.OpciNombre, 
                                                                                            objEntity.OpciEtiqueta, objEntity.ParaClase2, objEntity.OpciComando, objEntity.OpciPadre, objEntity.OpciOrden, 
                                                                                            objEntity.OpciHint, objEntity.OpciEstado, objEntity.LogsUsuario, objEntity.LogsFecha, objEntity.OpciPrincipal,
                                                                                            objEntity.opciReporte);
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

        public void Update(SessionManager oSessionManager, AdmiOpcion objEntity)
        {            
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE admi_topcion");
                sSql.AppendLine("SET sist_sistema = @p1, modu_modulo = @p2, icon_icono = @p3, blob_ayuda = @p4, opci_nombre = @p5, opci_etiqueta = @p6, para_clase2 = @p7, ");
                sSql.AppendLine("opci_comando = @p8, opci_padre = @p9, opci_orden = @p10, opci_hint = @p11, opci_estado = @p12, logs_usuario = @p13, logs_fecha = @p14, opci_principal=@p15");
                sSql.AppendLine("WHERE  (opci_opcion = @p0)");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, objEntity.OpciOpcion, objEntity.SistSistema, objEntity.ModuModulo, objEntity.IconIcono, objEntity.BlobAyuda, objEntity.OpciNombre, objEntity.OpciEtiqueta, objEntity.ParaClase2, objEntity.OpciComando, objEntity.OpciPadre, objEntity.OpciOrden, objEntity.OpciHint, objEntity.OpciEstado, objEntity.LogsUsuario, objEntity.LogsFecha, objEntity.OpciPrincipal);
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

        public List<AdmiOpcion> GetAllList(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();            
            try
            {
                sSql.AppendLine("SELECT admi_topcion.opci_opcion, admi_topcion.sist_sistema, admi_topcion.modu_modulo, admi_topcion.icon_icono, admi_topcion.blob_ayuda, ");
                sSql.AppendLine("admi_topcion.opci_nombre, admi_topcion.opci_etiqueta, admi_topcion.para_clase2, admi_topcion.opci_comando, admi_topcion.opci_padre,");
                sSql.AppendLine("admi_topcion.opci_orden, admi_topcion.opci_hint, admi_topcion.opci_estado, admi_topcion.logs_usuario, admi_topcion.logs_fecha, ");
                sSql.AppendLine("admi_topcion.opci_principal,admi_tformulario.form_nombre, opci_reporte, admi_tformulario.form_formulario ");
                sSql.AppendLine("FROM admi_topcion WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN admi_tformulario ON (admi_tformulario.opci_opcion = admi_topcion.opci_opcion)");
                //sSql.AppendLine("LEFT OUTER JOIN admi_tblob ON admi_topcion.blob_ayuda = admi_tblob.blob_blob LEFT OUTER JOIN");
                //sSql.AppendLine("admi_tblob AS admi_tblob_1 ON admi_topcion.icon_icono = admi_tblob_1.blob_blob");
                if (!string.IsNullOrEmpty(filter))
                {
                    sSql.AppendLine(" WHERE " + filter);
                }
                return MappingData<AdmiOpcion>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text));
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

        public static DataTable GetlstReportes(SessionManager oSessionManager)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT RR_CODIGO,(RC_NOMBRE+'-->'+RR_NOMBRE) Nombre");
                sSql.AppendLine("  FROM TB_RCARPETAS WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TB_RREPORTE WITH(NOLOCK) ON(RC_CODEMP = RR_CODEMP AND RC_CONINT = RR_CARPETA AND RR_ESTADO ='AC')");
                sSql.AppendLine("WHERE RC_ESTADO ='AC'");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            }
        }
	}
}