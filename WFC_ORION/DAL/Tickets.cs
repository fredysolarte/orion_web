using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFC_ORION.DAL
{
    public class Tickets
    {
		public static SqlDataReader GetTickets(DBAccess ObjDB, string usua_usuario)
		{
			StringBuilder sSql = new StringBuilder();
			
			try
			{
				sSql.AppendLine("SELECT TB_TICKETHD.*,TTVALORC,Convert(Date,isnull(inicio,getdate()),101) inicio,usua_usuario,usua_clave,usua_nombres,usua_estado,usua_secuencia,usua_administrador,final ");
				sSql.AppendLine("  FROM TB_TICKETHD WITH(NOLOCK) ");
				sSql.AppendLine("INNER JOIN admi_tusuario ON (usua_usuario = TK_PROPIETARIO)");
				sSql.AppendLine("LEFT OUTER JOIN TBTABLAS ON (TTCODEMP = '001' AND TTCODTAB='AREAS' AND TTCODCLA = usua_area)");
				sSql.AppendLine("LEFT OUTER JOIN Appointments WITH(NOLOCK) ON(Appointments.TK_NUMERO = TB_TICKETHD.TK_NUMERO)");
				sSql.AppendLine("WHERE  TB_TICKETHD.TK_RESPONSABLE=@p0 AND TK_ESTADO='AC'");
				sSql.AppendLine("ORDER BY inicio");
				
				return ObjDB.ExecuteReader(sSql.ToString(), usua_usuario);				
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

		public static int updateTicket(DBAccess ObjDB, int? AT_CODIGO) {
			StringBuilder sSql = new StringBuilder();

			try
			{
				sSql.AppendLine("UPDATE TB_TICKETHD SET TK_ESTADO='CE',TK_FECVEN=GETDATE() ");
				sSql.AppendLine("WHERE AT_CODIGO=@p0");

				return ObjDB.ExecuteNonQuery(sSql.ToString(), AT_CODIGO);
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
