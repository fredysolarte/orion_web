using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFC_ORION.DAL
{
    public class facturasBD
    {
		public static SqlDataReader getFacturasHD(DBAccess ObjDB, string EMPRESA, string inFiltro)
		{
			StringBuilder sql = new StringBuilder();

			try
			{
				sql.AppendLine("SELECT * FROM FACTURAHD WITH(NOLOCK) ");
				sql.AppendLine("WHERE HDCODEMP = @p0 " + inFiltro);

				return ObjDB.ExecuteReader(sql.ToString(), EMPRESA);
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
