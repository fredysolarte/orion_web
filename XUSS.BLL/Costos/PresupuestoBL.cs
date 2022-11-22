using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Costos;
using XUSS.BLL.Terceros;

namespace XUSS.BLL.Costos
{
    public class PresupuestoBL
    {
        #region
        public DataTable GetPresupuesto(string connection, int inmes, int inano, string inbodega)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                if (PresupuestoBD.ExistePresupuesto(oSessionManager, inmes, inano, inbodega) == 0)
                    return GeneraPresupuesto(inmes, inano, inbodega);
                else
                    return PresupuestoBD.GetPresupuesto(oSessionManager, inmes, inano, inbodega);
                
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
        public static DataTable GeneraPresupuesto(int inmes, int inano, string inbodega)
        {
            
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("ano", typeof(Int32));
                dt.Columns.Add("mes", typeof(Int32));
                dt.Columns.Add("dia", typeof(Int32));
                dt.Columns.Add("valor", typeof(double));
                dt.Columns.Add("bodega", typeof(string));

                if (inbodega != null)
                {
                    for (int i = 0; i < System.DateTime.DaysInMonth(inano, inmes); i++)
                        dt.Rows.Add(inano, inmes, i + 1, 0, inbodega);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dt = null;
            }
        }
        public static int InsertPresupuesto(string connection, string PR_CODEMP, int PR_ANO, int PR_MES, int PR_DIA, string PR_BODEGA, double PR_VALOR, string PR_USUARIO,
                                    string PR_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                if (PresupuestoBD.ExistePresupuesto(oSessionManager, PR_MES, PR_ANO, PR_DIA, PR_BODEGA) == 0)
                    return PresupuestoBD.InsertPresupuesto(oSessionManager, PR_CODEMP, PR_ANO, PR_MES, PR_DIA, PR_BODEGA, PR_VALOR, PR_USUARIO, PR_ESTADO);
                else
                    return PresupuestoBD.UpdatePresupuesto(oSessionManager, PR_MES, PR_ANO, PR_DIA, PR_BODEGA, PR_VALOR,PR_USUARIO);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        #endregion
        #region
        public DataTable GetPresupuestoVendedor(string connection, int inmes, int inano)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                if (PresupuestoBD.ExistePresupuestoVendedor(oSessionManager, inmes, inano) == 0)
                    return GeneraPresupuestoVendedor(inmes, inano);
                else
                    return PresupuestoBD.GetPresupuestoVendedor(oSessionManager, inmes, inano);

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
        public DataTable GetPresupuestoVendedor(string connection, int inmes, int inano, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                if (PresupuestoBD.ExistePresupuestoVendedor(oSessionManager, inmes, inano, TRCODTER) == 0)
                    return GeneraPresupuestoVendedor(inmes, inano);
                else
                    return PresupuestoBD.GetPresupuestoVendedor(oSessionManager, inmes, inano);

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
        public static DataTable GeneraPresupuestoVendedor(int inmes, int inano)
        {
            TercerosBL ObjT = new TercerosBL();
            DataTable dt = new DataTable();
            try
            {                
                dt.Columns.Add("ano", typeof(Int32));
                dt.Columns.Add("mes", typeof(Int32));
                dt.Columns.Add("Ventas", typeof(double));
                dt.Columns.Add("Cartera", typeof(double));
                dt.Columns.Add("cod_vendedor", typeof(Int32));
                dt.Columns.Add("vendedor", typeof(string));

                foreach (DataRow rw in ObjT.GetTerceros(null, " TRINDVEN='S' AND TRESTADO='AC'", 0, 0).Rows)                
                    dt.Rows.Add(inano, inmes, 0, 0, Convert.ToInt32(rw["TRCODTER"]), (Convert.ToString(rw["TRNOMBRE"]) + " " + Convert.ToString(rw["TRNOMBR2"]) + " " + Convert.ToString(rw["TRAPELLI"])));                                    
                //for (int i = 0; i < System.DateTime.DaysInMonth(inano, inmes); i++)
                //    dt.Rows.Add(inano, inmes, i + 1, 0);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dt = null;
                ObjT = null;
            }
        }
        public static int InsertPresupuestoVendedor(string connection, string PP_CODEMP, int PP_ANO, int PP_MES, int TRCODTER, double PP_VENTAS, double PP_CARTERA, string PR_USUARIO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                if (PresupuestoBD.ExistePresupuestoVendedor(oSessionManager, PP_MES, PP_ANO, TRCODTER) == 0)
                    return PresupuestoBD.InsertPresupuestoVendedor(oSessionManager, PP_CODEMP, PP_ANO, PP_MES, TRCODTER,PP_VENTAS, PP_CARTERA, PR_USUARIO);
                else
                    return PresupuestoBD.UpdatePresupuestoVendedor(oSessionManager, TRCODTER, PP_MES, PP_ANO, PP_VENTAS, PP_CARTERA, PR_USUARIO);
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
        #endregion

    }
}
