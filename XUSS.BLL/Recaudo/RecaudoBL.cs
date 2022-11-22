using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Recaudo;
using XUSS.BLL.Comun;

namespace XUSS.BLL.Recaudo
{
    public class RecaudoBL
    {
        public DataTable GetRecaudo(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return recaudoBD.GetRecaudo(oSessionManager, filter, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                oSessionManager = null;
            }
        }
        public DataTable GetRecaudoDT(string connection, string RC_CODEMP, int RC_NRORECIBO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return recaudoBD.GetRecaudoDT(oSessionManager, RC_CODEMP,RC_NRORECIBO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public DataTable GetItemsRecaudo(string connection, int InRecuado)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return recaudoBD.GetItemsRecaudo(oSessionManager, InRecuado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }

        }
        public IDataReader GetDatosFactura(string connection, string inTfactura, int inFactura)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return recaudoBD.GetDatosFactura(oSessionManager, inTfactura,inFactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public Boolean ExisteFactura(string connection, string inTfactura, int inFactura)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                if (recaudoBD.ExisteFactura(oSessionManager, inTfactura, inFactura) == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public int InsertRecaudo(string connection, string RH_CODEMP, DateTime RH_FECHA, string RH_NRORECFISICO, string RH_OBSERVACIONES, double RH_VALOR, int RH_BANCO,string RC_USUARIO, object inTbItems)
        {
            int ln_codigo;            
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                
                ln_codigo = ComunBL.GeneraConsecutivo(connection, "CNRECA", RH_CODEMP);

                //Insert HD
                recaudoBD.InsertRecaudoHD(oSessionManager, RH_CODEMP, ln_codigo, RH_FECHA, RH_NRORECFISICO, RH_OBSERVACIONES, RH_VALOR, RH_BANCO, "AC");


                foreach (DataRow row in (inTbItems as DataTable).Rows)
                {
                    if (Convert.ToInt16(row["RC_CONCEPTO"]) > 0)
                    {
                        recaudoBD.InsertRecaudo(oSessionManager, RH_CODEMP, ln_codigo, Convert.ToString(row["RC_TIPFAC"]), Convert.ToInt32(row["RC_NROFAC"]), Convert.ToString(row["RC_CONCEPTO"]), Convert.ToDouble(row["RC_VALOR"]),
                                                RC_USUARIO, "AC", RH_FECHA, Convert.ToString(row["RC_TIPFACSF"]), Convert.ToInt32(row["RC_NROFACSF"]), RH_NRORECFISICO);
                    }
                }
                oSessionManager.CommitTranstaction();
                return ln_codigo;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;                
            }
        }
        public DataTable GetSaldoFavor(string connection, int inTercero)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return recaudoBD.GetSaldoFavor(oSessionManager, inTercero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public int AnularRecaudo(string connection, string RC_CODEMP,int RC_NRORECIBO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                recaudoBD.AnularRecaudo(oSessionManager, RC_CODEMP, RC_NRORECIBO);
                recaudoBD.AnularRecaudoHD(oSessionManager, RC_CODEMP, RC_NRORECIBO);
                oSessionManager.CommitTranstaction();
                return 1;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public DataTable GetDetalle(string connection, string inIdentificacion)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return recaudoBD.GetDetalle(oSessionManager, inIdentificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
    }
}
