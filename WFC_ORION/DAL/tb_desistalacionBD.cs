using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFC_ORION.DAL
{
    public class tb_desistalacionBD
	{
		public static SqlDataReader getDesistalacion(DBAccess ObjDB, string inFiltro)
		{
			StringBuilder sql = new StringBuilder();

			try
			{
				sql.AppendLine("SELECT *");
				sql.AppendLine("FROM TB_DESISTALACION WITH(NOLOCK)");				
				sql.AppendLine("WHERE 1=1 "+ inFiltro);

				return ObjDB.ExecuteReader(sql.ToString());
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
