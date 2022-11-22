using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFC_ORION.DAL
{
    public class articuloBD
    {
		public static SqlDataReader getArticulos(DBAccess ObjDB, string inFiltro)
		{
			StringBuilder sql = new StringBuilder();

			try
			{
				sql.AppendLine("SELECT top 100 ARTICULO.*,A.ASNOMBRE ASNOMBRE1,B.ASNOMBRE ASNOMBRE2,C.ASNOMBRE ASNOMBRE3");
				sql.AppendLine("FROM articulo WITH(NOLOCK)");
				sql.AppendLine("LEFT OUTER JOIN ARTICSEC A WITH(NOLOCK) ON(A.ASCODEMP = ARCODEMP AND A.ASTIPPRO = ARTIPPRO AND A.ASCLAVEO = ARDTTEC1 AND A.ASNIVELC = 5)");
				sql.AppendLine("LEFT OUTER JOIN ARTICSEC B WITH(NOLOCK) ON(B.ASCODEMP = ARCODEMP AND B.ASTIPPRO = ARTIPPRO AND B.ASCLAVEO = ARDTTEC2 AND B.ASNIVELC = 6)");
				sql.AppendLine("LEFT OUTER JOIN ARTICSEC C WITH(NOLOCK) ON(C.ASCODEMP = ARCODEMP AND C.ASTIPPRO = ARTIPPRO AND C.ASCLAVEO = ARDTTEC3 AND C.ASNIVELC = 7)");
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

		public static SqlDataReader getImagenArticulo(DBAccess ObjDB, string ARCODEMP, string ARTIPPRO, string ARCLAVE1)
		{
			StringBuilder sSql = new StringBuilder();
			try
			{
				sSql.AppendLine("SELECT *              ");
				sSql.AppendLine("  FROM IMAGENES WITH(NOLOCK) ");
				sSql.AppendLine(" WHERE IM_CODEMP = @p0");
				sSql.AppendLine("   AND IM_TIPPRO = @p1");
				sSql.AppendLine("   AND IM_CLAVE1 = @p2");

				return ObjDB.ExecuteReader(sSql.ToString(), ARCODEMP,ARTIPPRO,ARCLAVE1);
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
		public static DataTable getArticulosTable(DBAccess ObjDB, string inFiltro)
		{
			StringBuilder sql = new StringBuilder();

			try
			{
				sql.AppendLine("SELECT top 100 ARTICULO.*,A.ASNOMBRE ASNOMBRE1,B.ASNOMBRE ASNOMBRE2,C.ASNOMBRE ASNOMBRE3");
				sql.AppendLine("FROM articulo WITH(NOLOCK)");
				sql.AppendLine("LEFT OUTER JOIN ARTICSEC A WITH(NOLOCK) ON(A.ASCODEMP = ARCODEMP AND A.ASTIPPRO = ARTIPPRO AND A.ASCLAVEO = ARDTTEC1 AND A.ASNIVELC = 5)");
				sql.AppendLine("LEFT OUTER JOIN ARTICSEC B WITH(NOLOCK) ON(B.ASCODEMP = ARCODEMP AND B.ASTIPPRO = ARTIPPRO AND B.ASCLAVEO = ARDTTEC2 AND B.ASNIVELC = 6)");
				sql.AppendLine("LEFT OUTER JOIN ARTICSEC C WITH(NOLOCK) ON(C.ASCODEMP = ARCODEMP AND C.ASTIPPRO = ARTIPPRO AND C.ASCLAVEO = ARDTTEC3 AND C.ASNIVELC = 7)");
				sql.AppendLine("WHERE 1=1 " + inFiltro);

				return ObjDB.ExecuteDataTable(sql.ToString());
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
		public static DataTable getImagenArticuloTable(DBAccess ObjDB, string ARCODEMP, string ARTIPPRO, string ARCLAVE1)
		{
			StringBuilder sSql = new StringBuilder();
			try
			{
				sSql.AppendLine("SELECT *              ");
				sSql.AppendLine("  FROM IMAGENES WITH(NOLOCK) ");
				sSql.AppendLine(" WHERE IM_CODEMP = @p0");
				sSql.AppendLine("   AND IM_TIPPRO = @p1");
				sSql.AppendLine("   AND IM_CLAVE1 = @p2");

				return ObjDB.ExecuteDataTable(sSql.ToString(), ARCODEMP, ARTIPPRO, ARCLAVE1);
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
