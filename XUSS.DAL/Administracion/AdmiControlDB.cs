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
	public class AdmiControlDB
	{
		public static List<AdmiControl> GetListControlAllowed(SessionManager oSessionManager, string user, string nombreForm)
		{
			StringBuilder sSql = new StringBuilder();
			sSql.AppendLine("SELECT admi_tcontrol.ctrl_control, admi_tcontrol.form_formulario, admi_tcontrol.ctrl_nombre, admi_tcontrol.ctrl_descripcion, admi_tcontrol.ctrl_padre, ");
			sSql.AppendLine("     admi_tcontrol.ctrl_estado, admi_tcontrol.ctrl_existeforma");
			sSql.AppendLine("FROM admi_tcontrol INNER JOIN");
			sSql.AppendLine("    admi_tformulario ON admi_tcontrol.form_formulario = admi_tformulario.form_formulario");
			sSql.AppendLine("WHERE (admi_tcontrol.ctrl_existeforma = 1) AND (admi_tcontrol.ctrl_estado = 1) AND (admi_tcontrol.ctrl_control NOT IN");
			sSql.AppendLine("(SELECT DISTINCT admi_tcontrol_1.ctrl_control");
			sSql.AppendLine("FROM admi_trol INNER JOIN");
			sSql.AppendLine("admi_tcontrolxrol ON admi_trol.rolm_rolm = admi_tcontrolxrol.rolm_rolm INNER JOIN");
			sSql.AppendLine("admi_trolxusuario ON admi_trol.rolm_rolm = admi_trolxusuario.rolm_rolm INNER JOIN");
			sSql.AppendLine("admi_tcontrol  admi_tcontrol_1 ON admi_tcontrolxrol.ctrl_control = admi_tcontrol_1.ctrl_control INNER JOIN");
			sSql.AppendLine("admi_tformulario  admi_tformulario_1 ON admi_tcontrol_1.form_formulario = admi_tformulario_1.form_formulario");
			sSql.AppendLine("WHERE (admi_trol.rolm_estado = 1) AND (admi_tcontrol_1.ctrl_existeforma = 1) AND (admi_tcontrol_1.ctrl_estado = 1) AND ");
			sSql.AppendLine("(admi_trolxusuario.usua_usuario = @p0) AND (admi_tcontrol_1.ctrl_control NOT IN");
			sSql.AppendLine("(SELECT ctrl_control");
			sSql.AppendLine("FROM admi_trestringecontrol");
			sSql.AppendLine("WHERE (usua_usuario = @p0))) AND (admi_tformulario_1.form_nombre = @p1))) AND (admi_tformulario.form_nombre = @p1)");
			return MappingData<AdmiControl>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, user, nombreForm));
		}
        public List<AdmiControl> GetByIdFormaEstadoForma(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, int idForma, bool presenteForma)
        {
            StringBuilder sSql = new StringBuilder();
            
            try
            {
                sSql.AppendLine("SELECT ctrl_control, form_formulario, ctrl_nombre, ctrl_descripcion,");
                sSql.AppendLine("ctrl_padre, ctrl_estado,ctrl_existeforma FROM admi_tcontrol");
                sSql.AppendLine("WHERE (form_formulario = @p0) and ctrl_existeforma=@p1");
                if (!string.IsNullOrEmpty(filter))
                {
                    sSql.AppendLine(" and (" + filter + ")");
                }
                return MappingData<AdmiControl>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString().Trim(), CommandType.Text, idForma, presenteForma));
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
        public AdmiControl GetByNombreControlAndIdForma(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, string nombreControl, int idForma)
        {
            StringBuilder sSql = new StringBuilder();
            
            try
            {
                sSql.AppendLine("SELECT ctrl_control, form_formulario, ctrl_nombre, ctrl_descripcion,");
                sSql.AppendLine("ctrl_padre, ctrl_estado,ctrl_existeforma FROM admi_tcontrol");
                sSql.AppendLine("WHERE (ctrl_nombre = @p0) and (form_formulario=@p1)");
                if (!string.IsNullOrEmpty(filter))
                {
                    sSql.AppendLine(" and (" + filter + ")");
                }
                return MappingData<AdmiControl>.Mapping(DBAccess.GetDataReader(oSessionManager, sSql.ToString().Trim(), CommandType.Text, nombreControl, idForma));
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
        public int Add(SessionManager oSessionManager, AdmiControl item)
        {
            StringBuilder sSql = new StringBuilder();
            int seqControl = -1;
            try
            {
                object[] parametersSequence = new object[2];
                parametersSequence[0] = ((Table)item.GetType().GetCustomAttributes(typeof(Table), true)[0]).EntityId;
                parametersSequence[1] = 0;
                //parametersSequence[2] = 0;
                //seqFormulario = DBAccess.GetSequence(oSessionManager, "admi_pGeneraSecuencia", parametersSequence[1], 0, true);
                seqControl = GenericaBD.GetSecuencia(oSessionManager, 48, 0);
                sSql = new StringBuilder();
                sSql.AppendLine("INSERT INTO admi_tcontrol");
                sSql.AppendLine("(ctrl_control,form_formulario,ctrl_nombre,ctrl_descripcion,ctrl_padre,ctrl_estado,ctrl_existeforma)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6)");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, seqControl, item.FormFormulario, item.CtrlNombre, item.CtrlDescripcion, item.CtrlPadre, item.CtrlEstado, item.CtrlExisteforma);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
            return seqControl;
        }
        public static DataTable GetControles(SessionManager oSessionManager, int opci_opcion)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT * FROM admi_tcontrol WHERE form_formulario in  (SELECT form_formulario FROM admi_tformulario WHERE opci_opcion =@p0)");
                return DBAccess.GetDataTable(oSessionManager,sSql.ToString(),CommandType.Text,opci_opcion); 
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


        public static DataTable GetControlesPermisos(SessionManager oSessionManager, int opci_opcion,string usua_usuario)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("select opci_opcion,admi_tcontrol.form_formulario,admi_tcontrol.ctrl_nombre");
                sSql.AppendLine("  from admi_tformulario with(nolock)");
                sSql.AppendLine("inner join admi_tcontrol with(nolock) on (admi_tformulario.form_formulario = admi_tcontrol.form_formulario)");
                sSql.AppendLine("inner join admi_tcontrolxrol with(nolock) on (admi_tcontrol.ctrl_control = admi_tcontrolxrol.ctrl_control)");
                sSql.AppendLine("inner join admi_trol with(nolock) on (admi_trol.rolm_rolm = admi_tcontrolxrol.rolm_rolm)");
                sSql.AppendLine("inner join admi_trolxusuario with(nolock) on (admi_trolxusuario.rolm_rolm = admi_trol.rolm_rolm)");
                sSql.AppendLine("where opci_opcion = @p0");
                sSql.AppendLine("  and admi_trolxusuario.usua_usuario = @p1");
                sSql.AppendLine("  and admi_tcontrol.ctrl_control not in (select ctrl_control from admi_trestringecontrol with(nolock) where admi_trestringecontrol.usua_usuario =@p1 )");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, opci_opcion, usua_usuario); 
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