using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Correspondencia
{
    public class CorrespondenciaBD
    {
        //Corespondencia Entrada
        #region
        public static DataTable GetCorrespondenciaHDIN(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_CORRESPONDENCIAHDIN WITH(NOLOCK) WHERE 1=1");
                if (!string.IsNullOrWhiteSpace(filter))
                    sSql.AppendLine("AND " + filter);

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
        public static DataTable GetCorrespondenciDTIN(SessionManager oSessionManager, int CIH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_CORRESPONDENCIADTIN.*,AA.TRNOMBRE,AA.TRDIRECC,PH_EDIFICIO,PH_ESCALERA,TB_COMERCIAL.CO_PRECIO,TB_COMERCIAL.CO_CUOTAS,MECDELEM,ARNOMBRE,CO_FECHA,CO_FECCOMODATO,(BB.TRNOMBRE+' '+ISNULL(BB.TRNOMBR2,'')+' '+ISNULL(BB.TRAPELLI,'')) CLIENTE,BB.TRCODNIT,PH_POLIZA,PH_CTACONTRATO"); 
                sSql.AppendLine("FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TB_PROPIEDAHORIZONTAL WITH(NOLOCK) ON(TB_CORRESPONDENCIADTIN.PH_CODIGO = TB_PROPIEDAHORIZONTAL.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN TERCEROS AA WITH(NOLOCK) ON(AA.TRCODEMP = TB_PROPIEDAHORIZONTAL.PH_CODEMP AND AA.TRCODTER = TB_PROPIEDAHORIZONTAL.TRCODTER)");
                sSql.AppendLine("LEFT OUTER JOIN TB_COMERCIAL WITH(NOLOCK) ON(TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_COMERCIAL.PH_CODIGO)");
                sSql.AppendLine("LEFT OUTER JOIN TERCEROS BB WITH(NOLOCK) ON(BB.TRCODEMP = TB_PROPIEDAHORIZONTAL.PH_CODEMP AND BB.TRCODTER = TB_COMERCIAL.TRCODTER)");
                sSql.AppendLine("INNER JOIN TB_INSTALACION WITH(NOLOCK) ON(TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_INSTALACION.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN MOVIMELE WITH(NOLOCK) ON(MOVIMELE.MECODEMP = TB_INSTALACION.IT_CODEMP AND MOVIMELE.MEIDMOVI = TB_INSTALACION.MEIDMOVI AND MOVIMELE.MEIDITEM = TB_INSTALACION.MEIDITEM");
                sSql.AppendLine("                 AND MOVIMELE.MEIDLOTE = TB_INSTALACION.MEIDLOTE AND MOVIMELE.MEIDELEM = TB_INSTALACION.MEIDELEM)");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON(ARCODEMP = MOVIMELE.MECODEMP AND ARTIPPRO = METIPPRO AND ARCLAVE1 = MECLAVE1 AND ARCLAVE2 = MECLAVE2 AND ARCLAVE3 = MECLAVE3 AND ARCLAVE4 = MECLAVE4) ");
                sSql.AppendLine("WHERE CIH_CODIGO=@p0 ");
                sSql.AppendLine("ORDER BY AA.TRCODTER,PH_EDIFICIO,PH_ESCALERA");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, CIH_CODIGO);
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
        public static DataTable GetCtasRestantes(SessionManager oSessionManager, int TRCODTER,string inFiltro)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_PROPIEDAHORIZONTAL.PH_CODIGO,AA.TRNOMBRE,AA.TRDIRECC,PH_EDIFICIO,PH_ESCALERA,TB_COMERCIAL.CO_PRECIO,TB_COMERCIAL.CO_CUOTAS,MECDELEM,ARNOMBRE,'N' CHK,CO_FECHA,CO_FECCOMODATO,(BB.TRNOMBRE+' '+ISNULL(BB.TRNOMBR2,'')+' '+ISNULL(BB.TRAPELLI,'')) CLIENTE,BB.TRCODNIT,PH_POLIZA,PH_CTACONTRATO");
                sSql.AppendLine("FROM TB_PROPIEDAHORIZONTAL WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TERCEROS AA WITH(NOLOCK) ON(AA.TRCODEMP = TB_PROPIEDAHORIZONTAL.PH_CODEMP AND AA.TRCODTER = TB_PROPIEDAHORIZONTAL.TRCODTER)");
                sSql.AppendLine("LEFT OUTER JOIN TB_COMERCIAL WITH(NOLOCK) ON(TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_COMERCIAL.PH_CODIGO)");
                sSql.AppendLine("LEFT OUTER JOIN TERCEROS BB WITH(NOLOCK) ON(BB.TRCODEMP = TB_PROPIEDAHORIZONTAL.PH_CODEMP AND BB.TRCODTER = TB_COMERCIAL.TRCODTER)");
                sSql.AppendLine("INNER JOIN TB_INSTALACION WITH(NOLOCK) ON(TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_INSTALACION.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN MOVIMELE WITH(NOLOCK) ON(MOVIMELE.MECODEMP = TB_INSTALACION.IT_CODEMP AND MOVIMELE.MEIDMOVI = TB_INSTALACION.MEIDMOVI AND MOVIMELE.MEIDITEM = TB_INSTALACION.MEIDITEM");
                sSql.AppendLine("                 AND MOVIMELE.MEIDLOTE = TB_INSTALACION.MEIDLOTE AND MOVIMELE.MEIDELEM = TB_INSTALACION.MEIDELEM)");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON(ARCODEMP = MOVIMELE.MECODEMP AND ARTIPPRO = METIPPRO AND ARCLAVE1 = MECLAVE1 AND ARCLAVE2 = MECLAVE2 AND ARCLAVE3 = MECLAVE3 AND ARCLAVE4 = MECLAVE4) ");
                sSql.AppendLine("WHERE AA.TRCODTER=@p0 AND TB_PROPIEDAHORIZONTAL.PH_CODIGO NOT IN (SELECT TB_CORRESPONDENCIADTOUT.PH_CODIGO FROM TB_CORRESPONDENCIADTOUT WITH(NOLOCK) WHERE COD_ESTADO NOT IN ('AN') ) ");
                sSql.AppendLine("  AND TB_PROPIEDAHORIZONTAL.PH_CODIGO NOT IN (SELECT TB_DESISTALACION.PH_CODIGO FROM TB_DESISTALACION WITH(NOLOCK)) " +inFiltro);
                sSql.AppendLine(" ORDER BY PH_EDIFICIO,PH_ESCALERA");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TRCODTER);
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
        public static int InsertCorrespondenciaHDIN(SessionManager oSessionManager, int CIH_CODIGO, string CIH_CODEMP, string CIH_DESCRIPCION, DateTime CIH_FECHA, string CIH_USUARIO, string CIH_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_CORRESPONDENCIAHDIN (CIH_CODIGO,CIH_CODEMP,CIH_DESCRIPCION,CIH_FECHA,CIH_USUARIO,CIH_ESTADO,CIH_FECING)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CIH_CODIGO, CIH_CODEMP, CIH_DESCRIPCION, CIH_FECHA, CIH_USUARIO, CIH_ESTADO);
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
        public static int InsertCorrespondenciaDTIN(SessionManager oSessionManager, int CIH_CODIGO, int CID_ITEM, int PH_CODIGO, string CID_ASESOR, string CID_USUARIO, string CID_ESTADO,string CID_CAUSAE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_CORRESPONDENCIADTIN (CIH_CODIGO,CID_ITEM,PH_CODIGO,CID_ASESOR,CID_USUARIO,CID_ESTADO,CID_CAUSAE,CID_FECING,CID_FECMOD)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,GETDATE(),GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CIH_CODIGO, CID_ITEM, PH_CODIGO, CID_ASESOR, CID_USUARIO, CID_ESTADO, CID_CAUSAE);
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
        public static int UpdateCorrespondenciaDTIN(SessionManager oSessionManager, int PH_CODIGO, string CID_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_CORRESPONDENCIADTIN SET CID_ESTADO=@p0");
                sSql.AppendLine(" WHERE PH_CODIGO =@p1");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CID_ESTADO, PH_CODIGO);
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
        public static int UpdateCorrespondenciaDTIN(SessionManager oSessionManager, int CIH_CODIGO, int CID_ITEM, string CID_ESTADO, string CID_CAUSAE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_CORRESPONDENCIADTIN SET CID_ESTADO=@p0,CID_CAUSAE=@p1 WHERE CIH_CODIGO=@p2 AND CID_ITEM=@p3");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CID_ESTADO, CID_CAUSAE, CIH_CODIGO, CID_ITEM);
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
        //Corespondencia Salida
        #region
        public static DataTable GetCorrespondenciaHDOUT(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_CORRESPONDENCIAHDOUT WITH(NOLOCK) WHERE 1=1");
                if (!string.IsNullOrWhiteSpace(filter))
                    sSql.AppendLine("AND " + filter);

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
        public static DataTable GetCorrespondenciDTOUT(SessionManager oSessionManager, int CIH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_CORRESPONDENCIADTOUT.*,AA.TRNOMBRE,AA.TRDIRECC,PH_EDIFICIO,PH_ESCALERA,TB_COMERCIAL.CO_PRECIO,TB_COMERCIAL.CO_CUOTAS,MECDELEM,ARNOMBRE,CO_FECHA,CO_FECCOMODATO,(BB.TRNOMBRE+' '+ISNULL(BB.TRNOMBR2,'')+' '+ISNULL(BB.TRAPELLI,'')) CLIENTE,BB.TRCODNIT,PH_POLIZA,PH_CTACONTRATO");
                sSql.AppendLine("FROM TB_CORRESPONDENCIADTOUT WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TB_PROPIEDAHORIZONTAL WITH(NOLOCK) ON(TB_CORRESPONDENCIADTOUT.PH_CODIGO = TB_PROPIEDAHORIZONTAL.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN TERCEROS AA WITH(NOLOCK) ON(AA.TRCODEMP = TB_PROPIEDAHORIZONTAL.PH_CODEMP AND AA.TRCODTER = TB_PROPIEDAHORIZONTAL.TRCODTER)");
                sSql.AppendLine("INNER JOIN TB_COMERCIAL WITH(NOLOCK) ON(TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_COMERCIAL.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN TERCEROS BB WITH(NOLOCK) ON(BB.TRCODEMP = TB_PROPIEDAHORIZONTAL.PH_CODEMP AND BB.TRCODTER = TB_COMERCIAL.TRCODTER)");
                sSql.AppendLine("INNER JOIN TB_INSTALACION WITH(NOLOCK) ON(TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_INSTALACION.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN MOVIMELE WITH(NOLOCK) ON(MOVIMELE.MECODEMP = TB_INSTALACION.IT_CODEMP AND MOVIMELE.MEIDMOVI = TB_INSTALACION.MEIDMOVI AND MOVIMELE.MEIDITEM = TB_INSTALACION.MEIDITEM");
                sSql.AppendLine("                 AND MOVIMELE.MEIDLOTE = TB_INSTALACION.MEIDLOTE AND MOVIMELE.MEIDELEM = TB_INSTALACION.MEIDELEM)");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON(ARCODEMP = MOVIMELE.MECODEMP AND ARTIPPRO = METIPPRO AND ARCLAVE1 = MECLAVE1 AND ARCLAVE2 = MECLAVE2 AND ARCLAVE3 = MECLAVE3 AND ARCLAVE4 = MECLAVE4) ");
                sSql.AppendLine("WHERE COH_CODIGO=@p0 ");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, CIH_CODIGO);
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
        public static DataTable GetCtasRestantesIN(SessionManager oSessionManager, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_PROPIEDAHORIZONTAL.PH_CODIGO,AA.TRNOMBRE,AA.TRDIRECC,PH_EDIFICIO,PH_ESCALERA,TB_COMERCIAL.CO_PRECIO,TB_COMERCIAL.CO_CUOTAS,MECDELEM,ARNOMBRE,'N' CHK,CO_FECHA,CO_FECCOMODATO,(BB.TRNOMBRE+' '+ISNULL(BB.TRNOMBR2,'')+' '+ISNULL(BB.TRAPELLI,'')) CLIENTE,BB.TRCODNIT,PH_POLIZA,PH_CTACONTRATO");
                sSql.AppendLine("FROM TB_PROPIEDAHORIZONTAL WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TERCEROS AA WITH(NOLOCK) ON(AA.TRCODEMP = TB_PROPIEDAHORIZONTAL.PH_CODEMP AND AA.TRCODTER = TB_PROPIEDAHORIZONTAL.TRCODTER)");
                sSql.AppendLine("INNER JOIN TB_COMERCIAL WITH(NOLOCK) ON(TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_COMERCIAL.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN TERCEROS BB WITH(NOLOCK) ON(BB.TRCODEMP = TB_PROPIEDAHORIZONTAL.PH_CODEMP AND BB.TRCODTER = TB_COMERCIAL.TRCODTER)");
                sSql.AppendLine("INNER JOIN TB_INSTALACION WITH(NOLOCK) ON(TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_INSTALACION.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN MOVIMELE WITH(NOLOCK) ON(MOVIMELE.MECODEMP = TB_INSTALACION.IT_CODEMP AND MOVIMELE.MEIDMOVI = TB_INSTALACION.MEIDMOVI AND MOVIMELE.MEIDITEM = TB_INSTALACION.MEIDITEM");
                sSql.AppendLine("                 AND MOVIMELE.MEIDLOTE = TB_INSTALACION.MEIDLOTE AND MOVIMELE.MEIDELEM = TB_INSTALACION.MEIDELEM)");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON(ARCODEMP = MOVIMELE.MECODEMP AND ARTIPPRO = METIPPRO AND ARCLAVE1 = MECLAVE1 AND ARCLAVE2 = MECLAVE2 AND ARCLAVE3 = MECLAVE3 AND ARCLAVE4 = MECLAVE4) ");
                sSql.AppendLine("WHERE AA.TRCODTER=@p0 AND TB_PROPIEDAHORIZONTAL.PH_CODIGO NOT IN (SELECT TB_CORRESPONDENCIADTOUT.PH_CODIGO FROM TB_CORRESPONDENCIADTOUT WITH(NOLOCK) WHERE COD_ESTADO IN ('AC') ) ");
                sSql.AppendLine("  AND TB_PROPIEDAHORIZONTAL.PH_CODIGO IN (SELECT TB_CORRESPONDENCIADTIN.PH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK) WHERE CID_ESTADO IN ('AC') ) ");                                                
                sSql.AppendLine("  AND TB_PROPIEDAHORIZONTAL.PH_CODIGO NOT IN (SELECT TB_DESISTALACION.PH_CODIGO FROM TB_DESISTALACION WITH(NOLOCK)) ");
                sSql.AppendLine(" ORDER BY PH_EDIFICIO,PH_ESCALERA");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TRCODTER);
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
        public static int InsertCorrespondenciaHDOUT(SessionManager oSessionManager, int CIH_CODIGO, string CIH_CODEMP, string CIH_DESCRIPCION, DateTime CIH_FECHA, string CIH_USUARIO, string CIH_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_CORRESPONDENCIAHDOUT (COH_CODIGO,COH_CODEMP,COH_DESCRIPCION,COH_FECHA,COH_USUARIO,COH_ESTADO,COH_FECING)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CIH_CODIGO, CIH_CODEMP, CIH_DESCRIPCION, CIH_FECHA, CIH_USUARIO, CIH_ESTADO);
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
        public static int InsertCorrespondenciaDTOUT(SessionManager oSessionManager, int COH_CODIGO, int COD_ITEM, int PH_CODIGO, string COD_USUARIO,string COD_ESTADO,string COD_CAUSAE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_CORRESPONDENCIADTOUT (COH_CODIGO,COD_ITEM,PH_CODIGO,COD_USUARIO,COD_ESTADO,COD_CAUSAE,COD_FECING,COD_FECMOD)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,GETDATE(),GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, COH_CODIGO, COD_ITEM, PH_CODIGO, COD_USUARIO, COD_ESTADO,COD_CAUSAE);
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
