using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.BLL.Comun;
using XUSS.DAL.Articulos;
using XUSS.DAL.Facturacion;
using XUSS.DAL.Parametros;
using XUSS.DAL.Terceros;

namespace XUSS.BLL.Facturacion
{
    public class GarantiasBL
    {
        public DataTable GetFacturaHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return FacturacionBD.GetFacturaHD(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetFacturaDT(string connection, string DTCODEMP, string DTTIPFAC, int DTNROFAC)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return FacturacionBD.GetFacturaDT(oSessionManager, DTCODEMP, DTTIPFAC, DTNROFAC);
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
        public DataTable GetGarantia(string connection, string GT_CODEMP, string GT_TIPFAC, int GT_NROFAC)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return GarantiasBD.GetGarantia(oSessionManager, GT_CODEMP, GT_TIPFAC, GT_NROFAC);
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
        public string InsertGaratia(string connection, string HDCODEMP, string HDTIPFAC, DateTime? HDFECFAC, int HDCODCLI, int HDCODSUC, DateTime? HDFECVEN,
                                          double HDSUBTOT, double HDTOTDES, double HDTOTIVA, double HDTOTFAC, string HDMONEDA, double HDSUBTTL, double HDTOTDSL,
                                          double HDTOTIVL, double HDTOTFCL, double HDSUBTTD, double HDTOTDSD, double HDTOTIVD, double HDTOTFCD, string HDCODNIT,
                                          string HDCDPAIS, string HDCIUDAD, string HDMODDES, string HDTERDES, string HDTERPAG, int HDAGENTE, string HDRSDIAN,
                                          string HDCATEGO, string HDCAJCOM, string HDNROALJ, string HDTIPALJ, string HDDIVISI, double HDTOTOTR, double HDTOTSEG,
                                          double HDTOTFLE, string HDCNTFIS, string HDOBSERV, double HDTOTICA, double HDTOTFTE, double HDTOTFIV, string HDESTADO,
                                          string HDCAUSAE, string HDNMUSER, string HDTRASMI, DateTime? HDFECCIE, string HDTIPDEV, int? HDNRODEV, string HDTFCDEV,
                                          int? HDFACDEV, string HDNROCAJA, int? LH_LSTPAQ, object tbDetalle, object tbPagos, string ind_inv, string ind_dev,
                                          string TRNOMBRE, string TRDIRECC, string TRNROTEL, string TRCORREO, string TRAPELLI)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TipoFacturaBD Obj = new TipoFacturaBD();            

            DataTable tbItems = new DataTable();
            DataTable tbItemsPagos = new DataTable();
            int ln_consecutivo = 0;
            string lc_resolucion = "", lc_dettpg = ".", lc_bodega = "", lc_tipmov = "", lc_lote = "";            

            try
            {
                oSessionManager.BeginTransaction();
                tbItems = (tbDetalle as DataTable);
                tbItemsPagos = (tbPagos as DataTable);

                //Genera Tercero
                if (HDCODCLI == 0)
                {
                    if (TercerosBD.ExisteTerceroIden(oSessionManager, HDCODEMP, HDCODNIT) == 0)
                    {
                        HDCODCLI = ComunBL.GeneraConsecutivo(connection, "CODTER", HDCODEMP);
                        TercerosBD.InsertTercero(oSessionManager, HDCODEMP, HDCODCLI, TRNOMBRE, null, null, null, HDCODNIT, null, TRDIRECC, null, null, null, TRNROTEL, null, null, TRCORREO, null, null, null, null, null, null, null, null, null,
                                                null, null, "LBASE07", null, null, null, "S", "N", "N", "N", "N", "N","N", null, null, null, null, null, null, null, null, null, null, null, null, null, "AC", ".", HDNMUSER, null, null, null, null, TRAPELLI, null, "CC", null, null, null, null, null,null);
                    }
                    else
                    {
                        using (IDataReader reader = TercerosBD.GetTercerosR(null, " TRCODNIT='" + HDCODNIT.Trim() + "' ", 0, 0))
                        {
                            while (reader.Read())
                            {
                                HDCODCLI = Convert.ToInt32(reader["TRCODTER"]);
                            }
                        }
                    }
                }

                Obj.UpdateTipoFactura(oSessionManager, HDCODEMP, HDTIPFAC);
                ln_consecutivo = Obj.GetUltimoNroFac(oSessionManager, HDCODEMP, HDTIPFAC);
                lc_resolucion = Obj.GetNumeroResolucion(oSessionManager, HDCODEMP, HDTIPFAC);
                FacturacionBD.InsertFacturaHD(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, System.DateTime.Today, HDCODCLI, HDCODSUC, null,
                                              HDSUBTOT, HDTOTDES, HDTOTIVA, HDTOTFAC, HDMONEDA, HDSUBTTL, HDTOTDSL, HDTOTIVL, HDTOTFCL, HDSUBTTD, HDTOTDSD, HDTOTIVD,
                                              HDTOTFCD, HDCODNIT, HDCDPAIS, HDCIUDAD, HDMODDES, HDTERDES, HDTERPAG, HDAGENTE, lc_resolucion,
                                              HDCATEGO, HDCAJCOM, HDNROALJ, HDTIPALJ, HDDIVISI, HDTOTOTR, HDTOTSEG,
                                              HDTOTFLE, HDCNTFIS, HDOBSERV, HDTOTICA, HDTOTFTE, HDTOTFIV, HDESTADO,
                                              HDCAUSAE, HDNMUSER, HDTRASMI, HDFECCIE, HDTIPDEV, HDNRODEV, HDTFCDEV,
                                              HDFACDEV, HDNROCAJA, LH_LSTPAQ,null,null);
                

                //Detalle
                foreach (DataRow rw in tbItems.Rows)
                {
                    //Movimientos Inventario
                    

                    if (Convert.ToString(rw["DEV"]) == "N")
                    {
                        FacturacionBD.InsertFacturaDT(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["DTNROITM"]), rw.IsNull("DTPEDIDO") ? null : (Int32?)Convert.ToInt32(rw["DTPEDIDO"]), rw.IsNull("DTLINNUM") ? null : (Int32?)Convert.ToInt32(rw["DTLINNUM"]), Convert.ToString(rw["DTTIPPRO"]),
                                                      Convert.ToString(rw["DTCLAVE1"]), Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), Convert.ToString(rw["DTCODCAL"]), Convert.ToString(rw["DTUNDPED"]), Convert.ToDouble(rw["DTCANPED"]), Convert.ToDouble(rw["DTCANTID"]), Convert.ToDouble(rw["DTCANKLG"]),
                                                      Convert.ToString(rw["DTLISPRE"]), null, Convert.ToDouble(rw["DTPRELIS"]), Convert.ToDouble(rw["DTPRECIO"]), Convert.ToDouble(rw["DTDESCUE"]), Convert.ToDouble(rw["DTSUBTOT"]), Convert.ToDouble(rw["DTTOTDES"]), Convert.ToDouble(rw["DTTOTIVA"]), Convert.ToDouble(rw["DTTOTFAC"]),
                                                      Convert.ToDouble(rw["DTSUBTTL"]), Convert.ToDouble(rw["DTTOTDSL"]), Convert.ToDouble(rw["DTTOTIVL"]), Convert.ToDouble(rw["DTTOTFCL"]), Convert.ToDouble(rw["DTSUBTTD"]), Convert.ToDouble(rw["DTTOTDSD"]), Convert.ToDouble(rw["DTTOTIVD"]), Convert.ToDouble(rw["DTTOTFCD"]), 0,
                                                      0, Convert.ToString(rw["DTESTADO"]), HDCAUSAE, HDNMUSER, 0, Convert.ToString(rw["DTTIPPED"]), Convert.ToString(rw["DTTIPLIN"]), Convert.ToInt32(rw["DTCODDES"]), Convert.ToString(rw["DTNROCAJA"]));                        
                    }
                }
                //Pagos
                foreach (DataRow rw in tbItemsPagos.Rows)
                {                    
                    GarantiasBD.Insertgarantia(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, HDCAUSAE,Convert.ToString(rw["GT_OBSERVACIONES"]),"AC");
                }                
                
                oSessionManager.CommitTranstaction();
                return HDTIPFAC + "-" + Convert.ToString(ln_consecutivo);
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                Obj = null;                
            }
        }
    }
}
