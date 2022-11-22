using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFC_ORION.DAL
{
    public class Administracion
    {
		public static SqlDataReader GetDatosUsuario(DBAccess ObjDB, string usua_usuario)
		{
			StringBuilder sql = new StringBuilder();
			
			try
			{
				sql.AppendLine("SELECT usua_usuario,usua_secuencia,usua_nombres,usua_clave,usua_estado,usua_administrador,(SELECT vr_version FROM admi_tversion WITH(NOLOCK)) vr_version ");
				sql.AppendLine(" FROM admi_tusuario WITH(NOLOCK) WHERE usua_usuario = @p0 ");

				return ObjDB.ExecuteReader(sql.ToString(), usua_usuario);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				sql = null;
			}
		}		
	}
}
