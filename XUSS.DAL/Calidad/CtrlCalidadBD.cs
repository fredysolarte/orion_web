using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Calidad
{
	public class CtrlCalidadBD
	{
		public static DataTable GetTb_Calidad(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("  SELECT * FROM TB_CALIDAD ");

				if (!string.IsNullOrWhiteSpace(filter))
				{
					sSql.AppendLine(" WHERE " + filter);
				}
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
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
		public static DataTable GetBodegas(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("  SELECT BDBODEGA,BDNOMBRE FROM TBBODEGA WHERE BDALMACE = 'S'");
				if (!string.IsNullOrWhiteSpace(filter))
				{
					sSql.AppendLine("AND " + filter);
				}
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
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
		public static DataTable GetTipPro(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("  SELECT TATIPPRO, TANOMBRE FROM  TBTIPPRO ");
				if (!string.IsNullOrWhiteSpace(filter))
				{
					sSql.AppendLine("AND " + filter);
				}
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
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
		public static DataTable GetTipError(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("  SELECT TTCODCLA,TTVALORC FROM TBTABLAS WHERE TTCODTAB = 'ERROR' ");
				if (!string.IsNullOrWhiteSpace(filter))
				{
					sSql.AppendLine("AND " + filter);
				}
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
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
		public static DataTable GetParteP(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("  SELECT TTCODCLA,TTVALORC FROM TBTABLAS WHERE TTCODTAB = 'PARTE' ");
				if (!string.IsNullOrWhiteSpace(filter))
				{
					sSql.AppendLine("AND " + filter);
				}
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
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
		public static DataTable GetTerceros(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("  SELECT TRCODNIT,TRNOMBRE FROM TERCEROS ");
				if (!string.IsNullOrWhiteSpace(filter))
				{
					sSql.AppendLine("WHERE " + filter);
				}
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
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
		public static int InsertTb_Calidad(SessionManager oSessionManager, string CA_CODEMP, string CA_BODEGA, int CA_NCONSE, int CA_NRODOC,
										   DateTime CA_FECHA, string CA_NOMBRE, string CA_TELEFONO, string CA_CELULAR, string CA_TIPPRO, string CA_CLAVE1, string CA_CLAVE2, 
										   string CA_CLAVE3, string CA_CLAVE4, string CA_URECIBE, string CA_NOVEDAD, string CA_PIEZA, string CA_OBSERVACIONES,
										   string CA_CDUSER)
		{			
			StringBuilder sSql = new StringBuilder();

			try
			{
				sSql.AppendLine("INSERT INTO TB_CALIDAD ");
				sSql.AppendLine("(CA_CODEMP, CA_BODEGA, CA_NCONSE, CA_NRODOC, CA_FECHA, CA_NOMBRE, CA_TELEFONO, CA_CELULAR, CA_TIPPRO, CA_CLAVE1,CA_CLAVE2,CA_CLAVE3,CA_CLAVE4,  ");
				sSql.AppendLine(" CA_URECIBE, CA_NOVEDAD,CA_PIEZA, CA_OBSERVACIONES, CA_FECING, CA_FECMOD, CA_CDUSER)");
				sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,GETDATE(),GETDATE(),@p17)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, CA_CODEMP, CA_BODEGA, CA_NCONSE, CA_NRODOC, CA_FECHA,
												CA_NOMBRE, CA_TELEFONO, CA_CELULAR, CA_TIPPRO, CA_CLAVE1, CA_CLAVE2, CA_CLAVE3, CA_CLAVE4, CA_URECIBE, CA_NOVEDAD, CA_PIEZA, 
												CA_OBSERVACIONES,CA_CDUSER);

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
		public static int GeneraConsecutivo(SessionManager oSessionManager)
		{			
			StringBuilder sql = new StringBuilder();
			sql.AppendLine("UPDATE TBTABLAS         ");
			sql.AppendLine("SET TTVALORN = TTVALORN + 1 ");
			sql.AppendLine("WHERE TTCODEMP = '001'  ");
			sql.AppendLine("AND TTCODTAB = 'CONT'   ");
			sql.AppendLine("AND TTCODCLA = 'CTRCAL' ");
			try
			{
                DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text);

				sql.Clear();
				sql.AppendLine("SELECT TTVALORN ");
				sql.AppendLine(" FROM TBTABLAS  ");
				sql.AppendLine("WHERE TTCODEMP = '001' ");
				sql.AppendLine("  AND TTCODTAB = 'CONT' ");
				sql.AppendLine("  AND TTCODCLA = 'CTRCAL' ");
				//return Convert.ToInt32(ObjDB.ExecuteScalar(sql.ToString()));
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sql.ToString(), CommandType.Text));
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
		public static DataTable GetClave(SessionManager oSessionManager, string TP, int clave)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine(" SELECT ASCLAVEO, ASNOMBRE");
				sSql.AppendLine(" FROM ARTICSEC a , TBTIPPRO b");
				sSql.AppendLine(" WHERE a.ASCODEMP = b.TACODEMP");
				sSql.AppendLine(" AND a.ASTIPPRO = b.TATIPPRO");
				sSql.AppendLine(" AND b.TATIPPRO =@p0");
				sSql.AppendLine(" AND a.ASNIVELC =@p1");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TP, clave);
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
		public static string GetNomTerceros(SessionManager oSessionManager, string nit)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("SELECT TRNOMBRE FROM TERCEROS WHERE TRCODNIT=@p0");

                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, nit));
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
		public static string GetTP(SessionManager oSessionManager, string clave1)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("SELECT TOP 1 ARTIPPRO FROM ARTICULO WHERE ARCLAVE1 =@p0");

                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, clave1));
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
