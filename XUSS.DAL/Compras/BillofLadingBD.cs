using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Compras
{
    public class BillofLadingBD
    {
        public static DataTable GetBLHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("");

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
        public static int InsertBLHD(SessionManager oSessionManager, int BLH_CODIGO, string BLH_CODEMP, DateTime BHL_FECHA, int BLH_CODEXPORTER, int BLH_CODRECEPTOR,int BLH_CODNOTIFY, string BLH_MODTRANS,string BLH_CIUREC,string BLH_NROVIAJE, string BLH_PURORIGEN, string BLH_PURDESTINO, string BLH_CIUDESTI,
                                     string BLH_BOOKINGNO, string BLH_NROBILLOFL, string BLH_EXPORTREF, string BLH_PTOPAISORI, string BLH_TIPOENVIO, string BLH_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_BLHD (BLH_CODIGO,BLH_CODEMP,BLH_FECHA,BLH_CODEXPORTER,BLH_CODRECEPTOR,BLH_CODNOTIFY,BLH_MODTRANS,BLH_CIUREC,BLH_NROVIAJE,BLH_PURORIGEN,BLH_PURDESTINO,BLH_CIUDESTI,");
                sSql.AppendLine("BLH_BOOKINGNO,BLH_NROBILLOFL,BLH_EXPORTREF,BLH_PTOPAISORI,BLH_TIPOENVIO,BLH_USUARIO,BLH_FECING)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BLH_CODIGO, BLH_CODEMP, BHL_FECHA, BLH_CODEXPORTER, BLH_CODRECEPTOR, BLH_CODNOTIFY, BLH_MODTRANS, BLH_CIUREC, BLH_NROVIAJE, BLH_PURORIGEN, BLH_PURDESTINO, BLH_CIUDESTI, 
                                                BLH_BOOKINGNO, BLH_NROBILLOFL, BLH_EXPORTREF, BLH_PTOPAISORI, BLH_TIPOENVIO, BLH_USUARIO);
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
        public static int InsertBLDT(SessionManager oSessionManager, int BLH_CODIGO,string BLD_NROCONTAINER,double BLD_NROPACK,string BLD_DESCRIPTION, double BLD_GROSSWEIGHT,string BLD_GROSSUN,double BLD_DIMESION,string BLD_DIMESIONUN)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_BLDT (BLH_CODIGO,BLD_NROCONTAINER,BLD_NROPACK,BLD_DESCRIPTION,BLD_GROSSWEIGHT,BLD_GROSSUN,BLD_DIMESION,BLD_DIMESIONUN)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BLH_CODIGO, BLD_NROCONTAINER, BLD_NROPACK, BLD_DESCRIPTION, BLD_GROSSWEIGHT, BLD_GROSSUN, BLD_DIMESION, BLD_DIMESIONUN);
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
        public static DataTable GetBLWROUT(SessionManager oSessionManager, int WOH_CONSECUTIVO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_BLHD.*,BLW_CONSECUTIVO ");
                sSql.AppendLine("FROM TB_BLH_WROUT WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TB_BLHD WITH(NOLOCK) ON(TB_BLH_WROUT.BLH_CODIGO = TB_BLHD.BLH_CODIGO) WHERE WOH_CONSECUTIVO =@p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, WOH_CONSECUTIVO);
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
        public static DataTable GetBLWRIN(SessionManager oSessionManager, int WOH_CONSECUTIVO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_BLHD.*,BLI_CONSECUTIVO ");
                sSql.AppendLine("FROM TB_BLH_WRIN WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TB_BLHD WITH(NOLOCK) ON(TB_BLH_WRIN.BLH_CODIGO = TB_BLHD.BLH_CODIGO) WHERE WIH_CONSECUTIVO =@p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, WOH_CONSECUTIVO);
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
        public static DataTable GetBLTraslado(SessionManager oSessionManager, int TSNROTRA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_BLH_TRASLADO.*,BLW_CONSECUTIVO,BLH_CODEMP,BLH_CODEXPORTER,BLH_CODRECEPTOR,BLH_CODNOTIFY,BLH_MODTRANS,BLH_CIUREC,BLH_NROVIAJE,BLH_PURORIGEN,");
                sSql.AppendLine("BLH_PURDESTINO,BLH_CIUDESTI,BLH_BOOKINGNO,BLH_NROBILLOFL,BLH_EXPORTREF,BLH_PTOPAISORI,BLH_TIPOENVIO,BLH_USUARIO,BLH_FECING,BLH_FECHA ");
                sSql.AppendLine("FROM TB_BLH_TRASLADO WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TB_BLHD WITH(NOLOCK) ON(TB_BLH_TRASLADO.BLH_CODIGO = TB_BLHD.BLH_CODIGO) WHERE TSNROTRA =@p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TSNROTRA);
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
        public static int DeteleBLCompra(SessionManager oSessionManager,int BLC_CONSECUTIVO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_BLH_COMPRAS WHERE BLC_CONSECUTIVO=@p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BLC_CONSECUTIVO);
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
        public static DataTable GetBLCompra(SessionManager oSessionManager, int CH_NROCMP)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_BLHD.*,BLC_CONSECUTIVO ");
                sSql.AppendLine("FROM TB_BLH_COMPRAS WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TB_BLHD WITH(NOLOCK) ON(TB_BLH_COMPRAS.BLH_CODIGO = TB_BLHD.BLH_CODIGO) WHERE CH_NROCMP =@p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, CH_NROCMP);
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
        public static DataTable GetBLDT(SessionManager oSessionManager, int BLH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_BLDT WITH(NOLOCK) WHERE BLH_CODIGO =@p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, BLH_CODIGO);
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
        public static int InsertBL_WROUT(SessionManager oSessionManager, int WOH_CONSECUTIVO, int BLH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_BLH_WROUT (WOH_CONSECUTIVO,BLH_CODIGO) VALUES (@p0,@p1)");
                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text, WOH_CONSECUTIVO, BLH_CODIGO);
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
        public static int InsertBL_COMPRAS(SessionManager oSessionManager, int CH_NROCMP, int BLH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_BLH_COMPRAS (CH_NROCMP,BLH_CODIGO) VALUES (@p0,@p1)");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CH_NROCMP, BLH_CODIGO);
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
        public static int InsertBL_WRIN(SessionManager oSessionManager, int WIH_CONSECUTIVO, int BLH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_BLH_WRIN (WIH_CONSECUTIVO,BLH_CODIGO) VALUES (@p0,@p1)");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, WIH_CONSECUTIVO, BLH_CODIGO);
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

        public static int DeleteBL_Traslado(SessionManager oSessionManager, string TSCODEMP, int TSNROTRA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_BLH_TRASLADO WHERE TSCODEMP = @p0 AND TSNROTRA =  @p1 ");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TSCODEMP, TSNROTRA);
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

        public static int InsertBL_Traslado(SessionManager oSessionManager, string TSCODEMP, int TSNROTRA, int BLH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_BLH_TRASLADO (TSCODEMP,TSNROTRA,BLH_CODIGO) VALUES (@p0,@p1,@p2)");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TSCODEMP, TSNROTRA, BLH_CODIGO);
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

        public static int ExisteBL_WRIN(SessionManager oSessionManager, int BLH_CODIGO,int WIH_CONSECUTIVO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_BLH_WRIN WITH(NOLOCK) WHERE BLH_CODIGO=@p0 AND WIH_CONSECUTIVO=@p1");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, BLH_CODIGO, WIH_CONSECUTIVO));
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

        public static int ExisteBL_WROUT(SessionManager oSessionManager, int BLH_CODIGO, int WOH_CONSECUTIVO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_BLH_WROUT WITH(NOLOCK) WHERE BLH_CODIGO=@p0 AND WOH_CONSECUTIVO=@p1");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, BLH_CODIGO, WOH_CONSECUTIVO));
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
