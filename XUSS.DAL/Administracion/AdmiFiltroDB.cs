using System.Collections.Generic;
using System.Data;
using System.Text;
using BE.Administracion;
using DataAccess;
using Mapping;

namespace DAL.Administracion
{
	public class AdmiFiltroDB 
	{
		public static List<AdmiFiltro> GetByURLForm(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, string urlForm)
		{
			StringBuilder sSql = new StringBuilder();
			sSql.AppendLine("SELECT admi_tfiltro.filt_filtro, admi_tfiltro.form_formulario, admi_tfiltro.filt_sql, admi_tfiltro.filt_padre, admi_tfiltro.filt_logicalname,admi_tfiltro.filt_nombre");
			sSql.AppendLine("FROM admi_tfiltro INNER JOIN");
			sSql.AppendLine("admi_tformulario ON admi_tfiltro.form_formulario = admi_tformulario.form_formulario");
			sSql.AppendLine("WHERE (admi_tformulario.form_nombre = @p0)");
			if (!string.IsNullOrWhiteSpace(filter))
			{
				sSql.AppendLine(" and (" + filter + ")");
			}
			return MappingData<AdmiFiltro>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, urlForm));
		}
	}
}