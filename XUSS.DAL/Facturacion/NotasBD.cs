using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Facturacion
{
    public class NotasBD
    {
        //Nota Credito
        #region 
        public static DataTable GetNotaHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT *,(TRNOMBRE +ISNULL(TRNOMBR2,'')+' '+ ISNULL(TRAPELLI,'')) NOM_TER ,TRNOMBRE,TRNOMBR2,TRAPELLI,TRCORREO,TRDIRECC,TRNROTEL,TRFECNAC ");
                sSql.AppendLine("FROM TB_NOTAHD WITH(NOLOCK)	");
                sSql.AppendLine("INNER JOIN TERCEROS ON (TRCODEMP = NH_CODEMP AND TERCEROS.TRCODTER = TB_NOTAHD.TRCODTER) ");


                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("WHERE " + filter);
                }
                sSql.AppendLine(" ORDER BY NH_NRONOTA ");
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
        public static DataTable GetNotaDT(SessionManager oSessionManager, string ND_CODEMP, int NH_NRONOTA,string NH_TIPFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                //sSql.AppendLine("SELECT TB_NOTADT.* ");
                sSql.AppendLine("SELECT TB_NOTADT.*");
                sSql.AppendLine("FROM TB_NOTADT WITH(NOLOCK)	");
                //sSql.AppendLine("LEFT OUTER JOIN FACTURAHD  ON (HDCODEMP = ND_CODEMP AND FACTURAHD.HDTIPFAC = TB_NOTADT.DTTIPFAC AND FACTURAHD.HDNROFAC = TB_NOTADT.DTNROFAC) ");
                sSql.AppendLine("WHERE ND_CODEMP =@p0 AND NH_NRONOTA=@p1 AND NH_TIPFAC=@p2");
                
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,ND_CODEMP, NH_NRONOTA, NH_TIPFAC);
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
        public static int InsertNotaHD(SessionManager oSessionManager, string NH_CODEMP,string NH_TIPFAC, int NH_NRONOTA, DateTime NH_FECNOTA, int TRCODTER,int SC_CONSECUTIVO, string NH_DESCRIPCION,double NH_TASA, 
                string NH_INDINV, string NH_ESTADO, string NH_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_NOTAHD (NH_CODEMP, NH_TIPFAC,NH_NRONOTA, NH_FECNOTA, TRCODTER, SC_CONSECUTIVO,NH_DESCRIPCION, NH_TASA, NH_INDINV,NH_ESTADO, NH_USUARIO, NH_FECING, NH_FECMOD)");
                sSql.AppendLine(" VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, NH_CODEMP, NH_TIPFAC ,NH_NRONOTA, NH_FECNOTA, TRCODTER, SC_CONSECUTIVO, NH_DESCRIPCION , NH_TASA, NH_INDINV, NH_ESTADO, NH_USUARIO);
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
        public static int InsertNotaDT(SessionManager oSessionManager, int ND_NROITEM, string ND_CODEMP, string NH_TIPFAC, int NH_NRONOTA, string DTTIPFAC, int? DTNROFAC, int? DTNROITM, string ND_TARIFA, string ND_DESCRIPCION, double ND_SUBTOTAL, 
            double ND_IMPUESTO,double ND_VALOR, int ND_CANTIDAD, string ND_ESTADO, string ND_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_NOTADT (ND_NROITEM,ND_CODEMP,NH_TIPFAC,NH_NRONOTA,DTTIPFAC,DTNROFAC,DTNROITM,ND_TARIFA,ND_DESCRIPCION,ND_SUBTOTAL,ND_IMPUESTO,ND_VALOR,ND_CANTIDAD,ND_ESTADO,ND_USUARIO,ND_FECING,ND_FECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(),CommandType.Text, ND_NROITEM,ND_CODEMP, NH_TIPFAC, NH_NRONOTA, DTTIPFAC, DTNROFAC, DTNROITM, ND_TARIFA, ND_DESCRIPCION, ND_SUBTOTAL, ND_IMPUESTO, ND_VALOR,
                    ND_CANTIDAD, ND_ESTADO, ND_USUARIO);
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
        public static int InsertNotaDBMoneda(SessionManager oSessionManager, int ND_NROITEM, string MDB_MONEDA, double MDB_TASA, double MDB_SUBTOTAL, double MDB_IMPUESTO, double MDB_VALOR)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_NOTADEBDT_MONEDA (ND_NROITEM, MDB_MONEDA, MDB_TASA, MDB_SUBTOTAL, MDB_IMPUESTO, MDB_VALOR) VALUES (@p0,@p1,@p2,@p3,@p4,@p5) ");                

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ND_NROITEM, MDB_MONEDA, MDB_TASA, MDB_SUBTOTAL, MDB_IMPUESTO, MDB_VALOR);
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
        public static int InsertNotaMoneda(SessionManager oSessionManager, int ND_NROITEM, string MDT_MONEDA, double MDT_TASA, double MDT_SUBTOTAL, double MDT_IMPUESTO, double MDT_VALOR)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_NOTADT_MONEDA (ND_NROITEM, MDT_MONEDA, MDT_TASA, MDT_SUBTOTAL, MDT_IMPUESTO, MDT_VALOR) VALUES (@p0,@p1,@p2,@p3,@p4,@p5) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ND_NROITEM, MDT_MONEDA, MDT_TASA, MDT_SUBTOTAL, MDT_IMPUESTO, MDT_VALOR);
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
        public static int AnulaNotaHD(SessionManager oSessionManager, string ND_CODEMP, string NH_TIPFAC, int NH_NRONOTA, string ND_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_NOTAHD SET NH_ESTADO='AN',NH_USUARIO=@p3,NH_FECMOD=GETDATE()");
                sSql.AppendLine(" WHERE NH_CODEMP=@p0 AND NH_NRONOTA=@p1 AND NH_TIPFAC=@p2 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ND_CODEMP, NH_NRONOTA, NH_TIPFAC, ND_USUARIO);
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
        public static int AnulaNotaDT(SessionManager oSessionManager, string ND_CODEMP, string NH_TIPFAC, int NH_NRONOTA, string ND_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_NOTADT SET ND_ESTADO='AN',ND_USUARIO=@p3,ND_FECMOD=GETDATE()");
                sSql.AppendLine(" WHERE ND_CODEMP=@p0 AND NH_NRONOTA=@p1 AND NH_TIPFAC=@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ND_CODEMP, NH_NRONOTA, NH_TIPFAC, ND_USUARIO);
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
        #endregion
        //Notas Debito
        #region
        public static DataTable GetNotaDebHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT *,(TRNOMBRE +ISNULL(TRNOMBR2,'')+' '+ ISNULL(TRAPELLI,'')) NOM_TER ,TRNOMBRE,TRNOMBR2,TRAPELLI,TRCORREO,TRDIRECC,TRNROTEL,TRFECNAC ");
                sSql.AppendLine("FROM TB_NOTADEBHD WITH(NOLOCK)	");
                sSql.AppendLine("INNER JOIN TERCEROS ON (TRCODEMP = NH_CODEMP AND TERCEROS.TRCODTER = TB_NOTADEBHD.TRCODTER) ");


                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("WHERE " + filter);
                }
                sSql.AppendLine(" ORDER BY NH_NRONOTA ");
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
        public static DataTable GetNotaDebDT(SessionManager oSessionManager, string ND_CODEMP, string NH_TIPFAC, int NH_NRONOTA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {                
                sSql.AppendLine("SELECT TB_NOTADEBDT.*");
                sSql.AppendLine("FROM TB_NOTADEBDT WITH(NOLOCK)	");                
                sSql.AppendLine("WHERE ND_CODEMP =@p0 AND NH_NRONOTA=@p1 AND NH_TIPFAC=@p2");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, ND_CODEMP, NH_NRONOTA, NH_TIPFAC);
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
        public static int InsertNotaDebHD(SessionManager oSessionManager, string NH_CODEMP, string NH_TIPFAC, int NH_NRONOTA, DateTime NH_FECNOTA, int TRCODTER, int SC_CONSECUTIVO, string NH_DESCRIPCION, double NH_TASA,string NH_ESTADO, string NH_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_NOTADEBHD (NH_CODEMP, NH_TIPFAC,NH_NRONOTA, NH_FECNOTA, TRCODTER, SC_CONSECUTIVO, NH_DESCRIPCION, NH_TASA, NH_ESTADO, NH_USUARIO, NH_FECING, NH_FECMOD)");
                sSql.AppendLine(" VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, NH_CODEMP, NH_TIPFAC, NH_NRONOTA, NH_FECNOTA, TRCODTER, SC_CONSECUTIVO, NH_DESCRIPCION, NH_TASA ,NH_ESTADO, NH_USUARIO);
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
        public static int InsertNotaDebDT(SessionManager oSessionManager, int ND_NROITEM, string ND_CODEMP, string NH_TIPFAC, int NH_NRONOTA, string DTTIPFAC, int? DTNROFAC, int? DTNROITM, string ND_TARIFA, string ND_DESCRIPCION, double ND_SUBTOTAL, double ND_IMPUESTO, double ND_VALOR, string ND_ESTADO, string ND_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_NOTADEBDT (ND_NROITEM,ND_CODEMP,NH_TIPFAC,NH_NRONOTA,DTTIPFAC,DTNROFAC,DTNROITM,ND_TARIFA,ND_DESCRIPCION,ND_SUBTOTAL,ND_IMPUESTO,ND_VALOR,ND_ESTADO,ND_USUARIO,ND_FECING,ND_FECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ND_NROITEM, ND_CODEMP, NH_TIPFAC, NH_NRONOTA, DTTIPFAC, DTNROFAC, DTNROITM, ND_TARIFA, ND_DESCRIPCION, ND_SUBTOTAL, ND_IMPUESTO, ND_VALOR, ND_ESTADO, ND_USUARIO);
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
        public static int AnulaNotaDebHD(SessionManager oSessionManager, string ND_CODEMP, string NH_TIPFAC, int NH_NRONOTA, string ND_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_NOTADEBHD SET NH_ESTADO='AN',NH_USUARIO=@p3,NH_FECMOD=GETDATE()");
                sSql.AppendLine(" WHERE NH_CODEMP=@p0 AND NH_NRONOTA=@p1 AND NH_TIPFAC=@p2 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ND_CODEMP, NH_NRONOTA, NH_TIPFAC, ND_USUARIO);
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
        public static int AnulaNotaDebDT(SessionManager oSessionManager, string ND_CODEMP, string NH_TIPFAC, int NH_NRONOTA, string ND_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_NOTADEBDT SET ND_ESTADO='AN',ND_USUARIO=@p3,ND_FECMOD=GETDATE()");
                sSql.AppendLine(" WHERE ND_CODEMP=@p0 AND NH_NRONOTA=@p1 AND NH_TIPFAC=@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ND_CODEMP, NH_NRONOTA, NH_TIPFAC, ND_USUARIO);
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
        #endregion
    }
}
