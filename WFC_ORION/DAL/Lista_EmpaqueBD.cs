using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFC_ORION.DAL
{
    public class Lista_EmpaqueBD
    {
		public static SqlDataReader getEmpaquesHD(DBAccess ObjDB, string EMPRESA, string inFiltro)
		{
			StringBuilder sql = new StringBuilder();

			try
			{
				sql.AppendLine("SELECT * FROM TB_EMPAQUEHD WITH(NOLOCK) ");					
				sql.AppendLine("WHERE LH_CODEMP = @p0 " + inFiltro);				

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

		public static SqlDataReader getEmpaquesDT(DBAccess ObjDB, string EMPRESA, string inFiltro)
		{
			StringBuilder sql = new StringBuilder();

			try
			{
				sql.AppendLine("SELECT * FROM TB_EMPAQUEDT WITH(NOLOCK) ");
				sql.AppendLine("WHERE LD_CODEMP = @p0 " + inFiltro);

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
