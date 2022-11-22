using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Costos
{
	public class CostosBD
	{
		public static DataTable GetCostosIng(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{								
				sSql.AppendLine("SELECT TBINGCOSTO.*, IM_IMAGEN ");
				sSql.AppendLine("FROM TBINGCOSTO WITH(NOLOCK)		");
                sSql.AppendLine("LEFT OUTER JOIN IMAGENES WITH(NOLOCK) ON( ");
				sSql.AppendLine("ICCODEMP = IM_CODEMP		");
				sSql.AppendLine("AND ICTIPPRO = IM_TIPPRO		");
				sSql.AppendLine("AND ICCLAVE1 = IM_CLAVE1		");
				sSql.AppendLine("AND IM_CLAVE2 = '.'			");
				sSql.AppendLine("AND IM_CLAVE3 = '.'			");
				sSql.AppendLine("AND IM_CLAVE4 = '.')			");

				if (!string.IsNullOrWhiteSpace(filter))
				{
					sSql.AppendLine("WHERE " + filter);
				}
				sSql.AppendLine(" ORDER BY ICCONSE ");
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
		public static DataTable GetProveedor(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
                sSql.AppendLine("  SELECT TRCODTER, TRNOMBRE  FROM  TERCEROS WITH(NOLOCK) WHERE TRINDPRO = 'S' ");
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
                sSql.AppendLine("  SELECT TATIPPRO, TANOMBRE FROM  TBTIPPRO WITH(NOLOCK) WHERE TAESTADO ='AC' ");
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
		public static DataTable GetMarca(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("SELECT TTCODCLA,TTVALORC FROM TBTABLAS WHERE TTCODTAB = 'MARCA' ");
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
		public static int UpdateCostosIng(SessionManager oSessionManager, string ICCODEMP, int ICCONSE, int ICCONSEINT, int ICPROVEE, string ICMARCA,
										  string ICTIPPRO, string ICREFERENCIA, string ICCLAVE1, int ICCANTIDAD, double ICTASA, double ICCOSTOUUSD,
										  double ICTOTALUSD, double ICCOSTOUND, double ICCOSTOTOT, double ICCOSTOSVT, DateTime ICFECHA, DateTime ICFECING,
										  string ICCDUSER, string ICESTADO)
		{			
			StringBuilder sSql = new StringBuilder();
			int retorno = -1;
			try
			{
				sSql.AppendLine("UPDATE TBINGCOSTO ");
				sSql.AppendLine("SET ICCOSTOSVT=@p0 ");
				sSql.AppendLine(" WHERE        ( ICCONSE=@p1) ");
				sSql.AppendLine(" AND (ICCONSEINT = @p2)");
                retorno = DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, ICCOSTOSVT, ICCONSE, ICCONSEINT);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{				
				sSql = null;
			}
			return retorno;
		}

		public static int InsertListaPreciosDT(SessionManager oSessionManager, string PRCODEMP, string PRLISPRE, DateTime PRFECPRE,string PRTIPPRO, string PRCLAVE1, string PRCLAVE2,
												string PRCLAVE3, string PRCLAVE4, string PRCODCLA, string PRCODCAL, string PRUNDPRE, double PRPRECIO, double PRCANMIN,
												string PRESTADO, string PRCAUSAE, string PRNMUSER, DateTime PRFECING, DateTime PRFECMOD)
		{ 
			StringBuilder sSql = new StringBuilder();			
			int retorno = -1;
			try
			{
                sSql.AppendLine("INSERT INTO TB_LSTPRECIODT (P_RCODEMP, P_RLISPRE, P_RFECPRE, P_RTIPPRO, P_RCLAVE1, P_RCLAVE2, ");
				sSql.AppendLine(" P_RCLAVE3, P_RCLAVE4, P_RCODCLA, P_RCODCAL, P_RUNDPRE, P_RPRECIO, P_RCANMIN, P_RESTADO, ");
				sSql.AppendLine(" P_RCAUSAE, P_RNMUSER, P_RFECING, P_RFECMOD) VALUES(@p0,@p1,GETDATE(),@p2,@p3,@p4,@p5,@p6,@p7");
				sSql.AppendLine(",@p8,@p9,@p10,@p11,@p12,@p13,@p14,GETDATE(),GETDATE())");
				
                retorno = DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, PRCODEMP, PRLISPRE, PRTIPPRO, PRCLAVE1, PRCLAVE2, 
				 PRCLAVE3, PRCLAVE4, PRCODCLA, PRCODCAL, PRUNDPRE, PRPRECIO, PRCANMIN, PRESTADO, PRCAUSAE, PRNMUSER);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				sSql = null;				
			}
			return retorno;
		}
		public static int GetExistePrecioDT(SessionManager oSessionManager, string PRCODEMP, string PRLISPRE, string PRTIPPRO, string PRCLAVE1, string PRCLAVE2,
												string PRCLAVE3, string PRCLAVE4)
		{			
			StringBuilder sSql = new StringBuilder();
			try
			{
                sSql.AppendLine("SELECT COUNT(*) FROM TB_LSTPRECIODT WITH(NOLOCK) ");
				sSql.AppendLine("WHERE P_RCODEMP = @p0  ");
				sSql.AppendLine("  AND P_RLISPRE = @p1 ");
				sSql.AppendLine("  AND P_RTIPPRO = @p2 ");
				sSql.AppendLine("  AND P_RCLAVE1 = @p3 ");
				sSql.AppendLine("  AND P_RCLAVE2 = @p4 ");
				sSql.AppendLine("  AND P_RCLAVE3 = @p5 ");
				sSql.AppendLine("  AND P_RCLAVE4 = @p6 ");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, PRCODEMP, PRLISPRE, PRTIPPRO, PRCLAVE1, PRCLAVE2, PRCLAVE3, PRCLAVE4));
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
		public static int UpdateEstadoCosto(SessionManager oSessionManager, int ICCONSE, int ICCONSEINT)
		{			
			StringBuilder sSql = new StringBuilder();
			int retorno = -1;
			try
			{
				sSql.AppendLine("UPDATE TBINGCOSTO ");
				sSql.AppendLine("SET ICESTPRE=@p0,ICFECING=GETDATE() ");
				sSql.AppendLine(" WHERE (ICCONSE=@p1) ");
				sSql.AppendLine("   AND (ICCONSEINT = @p2)");
                retorno = DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, "CE", ICCONSE, ICCONSEINT);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{				
				sSql = null;
			}
			return retorno;
		}
		public static int UpdateListaPreciosDT(SessionManager oSessionManager, string PRCODEMP, string PRLISPRE, string PRTIPPRO, string PRCLAVE1, string PRCLAVE2,
												string PRCLAVE3, string PRCLAVE4, string PRNMUSER, double PRPRECIO)
		{		
			StringBuilder sSql = new StringBuilder();
			int retorno = -1;
			try
			{
                sSql.AppendLine("UPDATE TB_LSTPRECIODT     ");
				sSql.AppendLine("SET P_RPRECIO=@p0, P_RFECMOD=GETDATE()    ");
				sSql.AppendLine(" WHERE P_RCODEMP=@p1 ");
				sSql.AppendLine(" AND P_RLISPRE = @p2 ");
				sSql.AppendLine(" AND P_RTIPPRO = @p3 ");
				sSql.AppendLine(" AND P_RCLAVE1 = @p4 ");
				sSql.AppendLine(" AND P_RCLAVE2 = @p5 ");
				sSql.AppendLine(" AND P_RCLAVE3 = @p6 ");
				sSql.AppendLine(" AND P_RCLAVE4 = @p7 ");


                retorno = DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, PRPRECIO, PRCODEMP, PRLISPRE, PRTIPPRO, PRCLAVE1, PRCLAVE2, PRCLAVE3, PRCLAVE4);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{				
				sSql = null;
			}
			return retorno;
		}

		public static DataTable GetEstados()
		{
			DataTable dt = new DataTable();
			try
			{
				dt.Columns.Add("codigo", typeof(string));
				dt.Columns.Add("nombre", typeof(string));

				dt.Rows.Add("AC", "Sin Precio");
				dt.Rows.Add("CE", "Con Precio");				

			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{ 
			
			}
			return dt;
		}
        public static DataTable GetDatosTotales(SessionManager oSessionManager, int ICCONSE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            { 
                sSql.AppendLine("SELECT ISNULL((SELECT SUM(PI_COSTOTT) FROM TB_PDISENO WHERE PI_CODEMP = ICCODEMP AND DS_TIPPRO = ICTIPPRO AND DS_CONSE = ICCLAVE1 AND PI_TIPO = 'I'),0) INSUMOS,");
	            sSql.AppendLine("ISNULL((SELECT SUM(PI_COSTOTT) FROM TB_PDISENO WHERE PI_CODEMP = ICCODEMP AND DS_TIPPRO = ICTIPPRO AND DS_CONSE = ICCLAVE1 AND PI_TIPO = 'E'),0) EMPAQUE,");
	            sSql.AppendLine("ISNULL((SELECT SUM(MN_VLRTOTAL) FROM TB_MANOOBRA WHERE MN_CODEMP = ICCODEMP AND MN_CONSEI = ICCONSE),0) MANOBRA, ICCOSTOUND");
                sSql.AppendLine("FROM TBINGCOSTO WITH(NOLOCK)");
                sSql.AppendLine("WHERE ICCONSE =@p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, ICCONSE);
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

        public static DataTable GetDatosPrecosteo(SessionManager oSessionManager)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TOP 1 * FROM TB_PARPRECOSTEO WHERE PR_ESTADO='AC'");

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

	}

	
}
