using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace XUSS.DAL.Descuentos
{
    public class DescuentosBD
    {
        private int pCodDcto;
        private int pTipDcto;
        private double pVlrDcto;
        //Retorno Dcto
        private int pCodDctoTT;
        private int pTipDctoTT;
        private double pVlrDctoTT;
        //Variables x Cedula
        private int pCodDctoCC;
        private int pTipDctoCC;
        private double pVlrDctoCC;
        //Variables x Especial Lista
        private int pCodDctoLT;
        private int pTipDctoLT;
        private double pVlrDctoLT;
        //x Articulo
        public int CodDcto
        {
            get { return pCodDcto; }
            set { pCodDcto = value; }
        }
        public double VlrDcto
        {
            get { return pVlrDcto; }
            set { pVlrDcto = value; }
        }
        public int TipDcto
        {
            get { return pTipDcto; }
            set { pTipDcto = value; }
        }
        //x Cedula
        public int CodDctoCC
        {
            get { return pCodDctoCC; }
            set { pCodDctoCC = value; }
        }
        public double VlrDctoCC
        {
            get { return pVlrDctoCC; }
            set { pVlrDctoCC = value; }
        }
        public int TipDctoCC
        {
            get { return pTipDctoCC; }
            set { pTipDctoCC = value; }
        }
        //Especial x Lista
        public int CodDctoLT
        {
            get { return pCodDctoLT; }
            set { pCodDctoLT = value; }
        }
        public double VlrDctoLT
        {
            get { return pVlrDctoLT; }
            set { pVlrDctoLT = value; }
        }
        public int TipDctoLT
        {
            get { return pTipDctoLT; }
            set { pTipDctoLT = value; }
        }
        ////Retorno Dcto
        //public int CodDctoTT
        //{
        //    get { return pCodDctoTT; }
        //    set { pCodDctoTT = value; }
        //}
        //public double VlrDctoTT
        //{
        //    get { return pVlrDctoTT; }
        //    set { pVlrDctoTT = value; }
        //}
        //public int TipDctoTT
        //{
        //    get { return pTipDctoTT; }
        //    set { pTipDctoTT = value; }
        //}

        public void GetDctoArticulo(SessionManager oSessionManager,string inTP, string inC1,string inC2,string inC3,string inC4,string inBD )
        {            
            Database db = oSessionManager.GetDatabase();   
            try {
                TipDcto = 0;
                CodDcto = 0;
                VlrDcto = 0;

                using (DbCommand cmd = db.GetStoredProcCommand("GET_DESCUENTOART"))
                {
                    db.AddInParameter(cmd, "@CODEMP",DbType.String,"001");
                    db.AddInParameter(cmd, "@TP", DbType.String, inTP);
                    db.AddInParameter(cmd, "@C1", DbType.String, inC1);
                    db.AddInParameter(cmd, "@C2", DbType.String, inC2);
                    db.AddInParameter(cmd, "@C3", DbType.String, inC3);
                    db.AddInParameter(cmd, "@C4", DbType.String, inC4);
                    db.AddInParameter(cmd, "@CB", DbType.String, inBD);

                    db.AddOutParameter(cmd, "@TIPO", DbType.Int32, 1);
                    db.AddOutParameter(cmd, "@COD", DbType.Int32, 1);
                    db.AddOutParameter(cmd, "@VALOR", DbType.Double, 16);

                    db.ExecuteScalar(cmd);

                    TipDcto = (Int32)cmd.Parameters["@TIPO"].Value;
                    CodDcto = (Int32)cmd.Parameters["@COD"].Value;
                    VlrDcto = (Double)cmd.Parameters["@VALOR"].Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;   
            }
        }
        public void GetDctoLista(SessionManager oSessionManager, string inTP, string inC1, string inC2, string inC3, string inC4, string inBD,int inCodigo)
        {
            Database db = oSessionManager.GetDatabase();
            try
            {
                TipDctoLT = 0;
                CodDctoLT = 0;
                VlrDctoLT = 0;

                using (DbCommand cmd = db.GetStoredProcCommand("GET_DESCUENTOARTESP"))
                {
                    db.AddInParameter(cmd, "@CODEMP", DbType.String, "001");
                    db.AddInParameter(cmd, "@TP", DbType.String, inTP);
                    db.AddInParameter(cmd, "@C1", DbType.String, inC1);
                    db.AddInParameter(cmd, "@C2", DbType.String, inC2);
                    db.AddInParameter(cmd, "@C3", DbType.String, inC3);
                    db.AddInParameter(cmd, "@C4", DbType.String, inC4);
                    db.AddInParameter(cmd, "@CB", DbType.String, inBD);
                    db.AddInParameter(cmd, "@CODIGO", DbType.Int32, inCodigo);

                    db.AddOutParameter(cmd, "@TIPO", DbType.Int32, 1);
                    db.AddOutParameter(cmd, "@COD", DbType.Int32, 1);
                    db.AddOutParameter(cmd, "@VALOR", DbType.Double, 16);

                    db.ExecuteScalar(cmd);

                    TipDctoLT = (Int32)cmd.Parameters["@TIPO"].Value;
                    CodDctoLT = (Int32)cmd.Parameters["@COD"].Value;
                    VlrDctoLT = (Double)cmd.Parameters["@VALOR"].Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
            }
        }
        public void GetDctoCedula(SessionManager oSessionManager, string inTP, string inC1, string inC2, string inC3, string inC4, string inBD, string inCedula, double inValor)
        {
            Database db = oSessionManager.GetDatabase();
            try
            {
                TipDctoCC = 0;
                CodDctoCC = 0;
                VlrDctoCC = 0;

                using (DbCommand cmd = db.GetStoredProcCommand("GET_VLRDESCUENTOCED"))
                {
                    db.AddInParameter(cmd, "@CEDULA", DbType.String, inCedula);
                    db.AddInParameter(cmd, "@TP", DbType.String, inTP);
                    db.AddInParameter(cmd, "@C1", DbType.String, inC1);
                    db.AddInParameter(cmd, "@C2", DbType.String, inC2);
                    db.AddInParameter(cmd, "@C3", DbType.String, inC3);
                    db.AddInParameter(cmd, "@C4", DbType.String, inC4);
                    db.AddInParameter(cmd, "@CB", DbType.String, inBD);
                    db.AddInParameter(cmd, "@VLCMP", DbType.Double, inValor);

                    db.AddOutParameter(cmd, "@OVLR", DbType.Double, 16);
                    db.AddOutParameter(cmd, "@TIP", DbType.Int32, 1);
                    db.AddOutParameter(cmd, "@COD", DbType.Int32, 1);
                    
                    db.ExecuteScalar(cmd);

                    TipDctoCC = (Int32)cmd.Parameters["@TIP"].Value;
                    CodDctoCC = (Int32)cmd.Parameters["@COD"].Value;
                    VlrDctoCC = (Double)cmd.Parameters["@OVLR"].Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
            }
        }        

        public IDataReader GetPrecio(SessionManager oSessionManager, string inTP, string inC1, string inC2, string inC3, string inC4, string inCodEmp, string Lista)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ISNULL((SELECT B.P_RPRECIO FROM TB_LSTPRECIODT B WHERE A.P_CCODEMP = B.P_RCODEMP AND A.P_CLISPRE = B.P_RLISPRE");
                sSql.AppendLine("           AND B.P_RTIPPRO = @p0");
                sSql.AppendLine("           AND B.P_RCLAVE1 = @p1");
                sSql.AppendLine("           AND B.P_RCLAVE2 = @p2");
                sSql.AppendLine("           AND B.P_RCLAVE3 = @p3");
                sSql.AppendLine("           AND B.P_RCLAVE4 = @p4");
                sSql.AppendLine("           AND B.P_RESTADO <> 'AN' AND (B.P_RCODTER IS NULL OR B.P_RCODTER = '') ");
                sSql.AppendLine("           AND B.P_RUNDPRE = 'UN'),0) PRECIO4, ");
                sSql.AppendLine("ISNULL((SELECT C.P_RPRECIO FROM TB_LSTPRECIODT C WHERE A.P_CCODEMP = C.P_RCODEMP AND A.P_CLISPRE = C.P_RLISPRE");
                sSql.AppendLine("           AND C.P_RTIPPRO =@p0");
                sSql.AppendLine("           AND C.P_RCLAVE1 =@p1");
                sSql.AppendLine("           AND C.P_RCLAVE2 =@p2");
                sSql.AppendLine("           AND C.P_RCLAVE3 =@p3 AND C.P_RCLAVE4 = '.'");
                sSql.AppendLine("           AND C.P_RESTADO <> 'AN' AND (C.P_RCODTER IS NULL OR C.P_RCODTER = '')");
                sSql.AppendLine("           AND C.P_RUNDPRE ='UN' ),0) PRECIO3,");
                sSql.AppendLine("ISNULL((SELECT D.P_RPRECIO FROM TB_LSTPRECIODT D WHERE A.P_CCODEMP = D.P_RCODEMP AND A.P_CLISPRE = D.P_RLISPRE");
                sSql.AppendLine("           AND D.P_RTIPPRO = @p0");
                sSql.AppendLine("           AND D.P_RCLAVE1 = @p1");
                sSql.AppendLine("           AND D.P_RCLAVE2 = @p2");
                sSql.AppendLine("           AND D.P_RCLAVE3 = '.' AND D.P_RCLAVE4 = '.'");
                sSql.AppendLine("           AND D.P_RESTADO <> 'AN' AND (D.P_RCODTER IS NULL OR D.P_RCODTER = '') ");
                sSql.AppendLine("           AND D.P_RUNDPRE ='UN'),0) PRECIO2,");
                sSql.AppendLine("ISNULL((SELECT E.P_RPRECIO FROM TB_LSTPRECIODT E WHERE A.P_CCODEMP = E.P_RCODEMP AND A.P_CLISPRE = E.P_RLISPRE");
                sSql.AppendLine("           AND E.P_RTIPPRO = @p0");
                sSql.AppendLine("           AND E.P_RCLAVE1 = @p1");
                sSql.AppendLine("           AND E.P_RCLAVE2 = '.'");
                sSql.AppendLine("           AND E.P_RCLAVE3 = '.' AND E.P_RCLAVE4 = '.'");
                sSql.AppendLine("           AND E.P_RESTADO <> 'AN' AND (E.P_RCODTER IS NULL OR E.P_RCODTER = '')");
                sSql.AppendLine("           AND E.P_RUNDPRE ='UN'),0) PRECIO1");
                sSql.AppendLine(" FROM TB_LSTPRECIO A ");
                sSql.AppendLine("WHERE A.P_CLISPRE = @p6");
                sSql.AppendLine("  AND A.P_CCODEMP = @p5");
                sSql.AppendLine("  AND A.P_CESTADO <> 'AN'");

                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, inTP, inC1, inC2, inC3, inC4, inCodEmp, Lista);
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
