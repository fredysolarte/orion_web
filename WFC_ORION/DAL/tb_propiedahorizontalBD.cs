using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFC_ORION.DAL
{
    public class tb_propiedahorizontalBD
	{
		public static SqlDataReader getPropiedahorizontal(DBAccess ObjDB, string inFiltro)
		{
			StringBuilder sql = new StringBuilder();

			try
			{
				sql.AppendLine("SELECT *");
				sql.AppendLine("FROM TB_PROPIEDAHORIZONTAL WITH(NOLOCK)");				
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

		public static SqlDataReader getPropiedadHorizontalDetalle(DBAccess ObjDB, string inFiltro) {

			StringBuilder sql = new StringBuilder();

			try
			{
				sql.AppendLine("SELECT *");
				sql.AppendLine("FROM TB_PROPIEDAHORIZONTAL WITH(NOLOCK)");
				sql.AppendLine("WHERE 1=1 " + inFiltro);

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
