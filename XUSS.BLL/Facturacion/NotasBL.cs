using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.BLL.Inventarios;
using XUSS.DAL.Comun;
using XUSS.DAL.Facturacion;
using XUSS.DAL.Parametros;
using XUSS.DAL.Pedidos;

namespace XUSS.BLL.Facturacion
{
    public class NotasBL
    {
        //Notas Credito
        #region 
        public DataTable GetNotaHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return NotasBD.GetNotaHD(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetNotaDT(string connection, string ND_CODEMP, string NH_TIPFAC, int NH_NRONOTA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return NotasBD.GetNotaDT(oSessionManager, ND_CODEMP, NH_NRONOTA, NH_TIPFAC);
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
        public string InsertNota(string connection, string NH_CODEMP, string NH_TIPFAC, DateTime NH_FECNOTA, int TRCODTER, int SC_CONSECUTIVO, string NH_DESCRIPCION, double NH_TASA, string NH_INDINV,string NH_ESTADO, 
            string NH_USUARIO, string INMONEDA,object intb,object inMonedas)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TipoFacturaBD Obj = new TipoFacturaBD();
            MovimientosBL Objm = new MovimientosBL();

            int ln_nronota = 0,ln_nroitm=0, ln_nrofacdev = 0, ln_nromovimientodev = 0, ln_itemdev = 1,ln_norconsecutivoorigen=0,ln_lote = 0, ln_nrolstpaq=0;
            string lc_tipfacdev = "", lc_tipmovdev = "",lc_tipfacorigen="", lc_bodega="", lc_lote = "",lc_moneda="",lc_nronit="",lc_pais="",lc_ciudad="", lc_resolucion="";
            double ln_devtotal = 0, ln_deviva = 0;

            try
            {
                oSessionManager.BeginTransaction();
                //ln_nronota = ComunBD.GeneraConsecutivo(oSessionManager, "CONNCRE", NH_CODEMP); //Contador Notas Credito
                Obj.UpdateTipoFactura(oSessionManager, NH_CODEMP, NH_TIPFAC);
                ln_nronota = Obj.GetUltimoNroFac(oSessionManager, NH_CODEMP, NH_TIPFAC);
                NotasBD.InsertNotaHD(oSessionManager, NH_CODEMP, NH_TIPFAC,ln_nronota, NH_FECNOTA, TRCODTER, SC_CONSECUTIVO,NH_DESCRIPCION, NH_TASA, NH_INDINV, NH_ESTADO, NH_USUARIO);
                foreach (DataRow row in ((DataTable)intb).Rows)
                {
                    lc_tipfacorigen = Convert.ToString(row["DTTIPFAC"]);
                    ln_norconsecutivoorigen = Convert.ToInt32(row["DTNROFAC"]);

                    ln_nroitm = ComunBD.GeneraConsecutivo(oSessionManager, "ITMNTCRDT", NH_CODEMP);
                    NotasBD.InsertNotaDT(oSessionManager, ln_nroitm,NH_CODEMP, NH_TIPFAC, ln_nronota, Convert.ToString(row["DTTIPFAC"]), Convert.ToInt32(row["DTNROFAC"]), Convert.ToInt32(row["DTNROITM"]), Convert.ToString(row["ND_TARIFA"]),
                                        Convert.ToString(row["ND_DESCRIPCION"]), Convert.ToDouble(row["ND_SUBTOTAL"]), Convert.ToDouble(row["ND_IMPUESTO"]), Convert.ToDouble(row["ND_VALOR"]), Convert.ToInt32(row["ND_CANTIDAD"]),
                                        "AC", NH_USUARIO);
                    //Moneda Local
                    foreach (DataRow rw in ((DataTable)inMonedas).Rows)
                        NotasBD.InsertNotaMoneda(oSessionManager, ln_nroitm, Convert.ToString(rw["TC_MONEDA"]), Convert.ToDouble(rw["TC_VALOR"]), Math.Round(Convert.ToDouble(row["ND_SUBTOTAL"]) / Convert.ToDouble(rw["TC_VALOR"]), 2), 
                            Math.Round(Convert.ToDouble(row["ND_IMPUESTO"]) / Convert.ToDouble(rw["TC_VALOR"]), 2), Math.Round(Convert.ToDouble(row["ND_VALOR"]) / Convert.ToDouble(rw["TC_VALOR"]), 2));

                    //Moneda Local
                    NotasBD.InsertNotaMoneda(oSessionManager, ln_nroitm, INMONEDA, 0, Convert.ToDouble(row["ND_SUBTOTAL"]), Convert.ToDouble(row["ND_IMPUESTO"]), Convert.ToDouble(row["ND_VALOR"]));
                }

                if (NH_INDINV == "S") {

                    foreach (DataRow rw in (Obj.GetTiposFactura(oSessionManager, " TFTIPFAC='" + lc_tipfacorigen + "'", 0, 0).Rows))
                        lc_bodega = Convert.ToString(rw["TFBODEGA"]);

                    foreach (DataRow rw in Obj.GetTiposFactura(oSessionManager, " TFBODEGA ='" + lc_bodega + "' AND TFCLAFAC= '2' ", 0, 0).Rows)
                    {
                        lc_tipmovdev = Convert.ToString(rw["TFCDTRAN"]);
                        lc_tipfacdev = Convert.ToString(rw["TFTIPFAC"]);
                    }

                    foreach (DataRow rw in FacturacionBD.GetFacturaHD(oSessionManager, " HDTIPFAC='" + lc_tipfacorigen + "' AND HDNROFAC=" + Convert.ToInt32(ln_norconsecutivoorigen) , 0, 0).Rows)
                    {
                        lc_moneda = Convert.ToString(rw["HDMONEDA"]);
                        lc_nronit = Convert.ToString(rw["HDCODNIT"]);
                        lc_pais = Convert.ToString(rw["HDCDPAIS"]);
                        lc_ciudad = Convert.ToString(rw["HDCIUDAD"]);
                        lc_resolucion = Convert.ToString(rw["HDRSDIAN"]);
                        ln_nrolstpaq = Convert.ToInt32(rw["LH_LSTPAQ"]);
                    }

                    if (string.IsNullOrEmpty(lc_tipfacdev))
                        throw new System.ArgumentException("¡No se ha Parametrizado Tipo Documento Devolucion a T Documento Factura!");
                    
                    Obj.UpdateTipoFactura(oSessionManager, NH_CODEMP, lc_tipfacdev);
                    ln_nrofacdev = Obj.GetUltimoNroFac(oSessionManager, NH_CODEMP, lc_tipfacdev);

                    foreach (DataRow rw in ((DataTable)intb).Rows)
                    {                        
                        ln_devtotal += Math.Abs(Convert.ToDouble(rw["ND_VALOR"]));
                        ln_deviva += Math.Abs(Convert.ToDouble(rw["ND_IMPUESTO"]));
                    }
                    FacturacionBD.InsertFacturaHD(oSessionManager, NH_CODEMP, lc_tipfacdev, ln_nrofacdev, System.DateTime.Today, TRCODTER, SC_CONSECUTIVO, null,
                                                  (ln_devtotal - ln_deviva), 0, ln_deviva, ln_devtotal, lc_moneda, 0, 0, 0, 0, 0, 0, 0,0, lc_nronit, lc_pais, lc_ciudad, 
                                                  null, null, null, 0, lc_resolucion, null, null, null, null, null, 0, 0,
                                                  0, null, "Devolucion Origen Nota de Credito", 0, 0, 0, "CE",
                                                  ".", NH_USUARIO, "S", null, null, null, null,
                                                  null, null, ln_nrolstpaq, null, null);

                    ln_nromovimientodev = Objm.InsertMovimiento(oSessionManager, NH_CODEMP, 0, lc_bodega, null, lc_tipmovdev, null, null, null, Convert.ToString(ln_nrolstpaq), System.DateTime.Today, null,
                                                           "CE", ".", NH_USUARIO, null, null, null, null, null, null, null, null);

                    foreach (DataRow rw in FacturacionBD.GetFacturaDT(oSessionManager, NH_CODEMP, lc_tipfacorigen, ln_norconsecutivoorigen).Rows)
                    {
                        foreach (DataRow rx in ((DataTable)intb).Rows)
                        {
                            if (Convert.ToInt32(rw["DTNROITM"]) == Convert.ToInt32(rx["DTNROITM"]))
                            {
                                Objm.InsertMovimiento(oSessionManager, NH_CODEMP, lc_bodega, null, System.DateTime.Today, lc_tipmovdev, Convert.ToString(rw["DTTIPPRO"]), Convert.ToString(rw["DTCLAVE1"]),
                                                     Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), ".", Math.Abs(Convert.ToDouble(rw["DTCANTID"])),
                                                     Math.Abs(Convert.ToDouble(rw["DTCANTID"])), "UN", ln_nromovimientodev, 0, ln_itemdev, lc_lote, null,
                                                     null, null, null, null, 0, 0, null, "CE", ".", NH_USUARIO, ln_lote, 0);

                                rw["DTNROMOV"] = ln_nromovimientodev;

                                FacturacionBD.InsertFacturaDT(oSessionManager, NH_CODEMP, lc_tipfacdev, ln_nrofacdev, Convert.ToInt32(rw["DTNROITM"]), rw.IsNull("DTPEDIDO") ? null : (Int32?)Convert.ToInt32(rw["DTPEDIDO"]), rw.IsNull("DTLINNUM") ? null : (Int32?)Convert.ToInt32(rw["DTLINNUM"]), Convert.ToString(rw["DTTIPPRO"]),
                                                              Convert.ToString(rw["DTCLAVE1"]), Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), Convert.ToString(rw["DTCODCAL"]), Convert.ToString(rw["DTUNDPED"]), Convert.ToDouble(rw["DTCANPED"]), Convert.ToDouble(rw["DTCANTID"]), Convert.ToDouble(rw["DTCANKLG"]),
                                                              Convert.ToString(rw["DTLISPRE"]), null, Convert.ToDouble(rw["DTPRELIS"]), Convert.ToDouble(rw["DTPRECIO"]), Convert.ToDouble(rw["DTDESCUE"]), 
                                                              Convert.ToDouble(rx["ND_SUBTOTAL"]), Math.Abs(Convert.ToDouble(rw["DTTOTDES"])), 
                                                              Convert.ToDouble(rx["ND_IMPUESTO"]), 
                                                              Convert.ToDouble(rx["ND_VALOR"]),
                                                              Convert.ToDouble(rw["DTSUBTTL"]), Convert.ToDouble(rw["DTTOTDSL"]), Convert.ToDouble(rw["DTTOTIVL"]), Convert.ToDouble(rw["DTTOTFCL"]), Convert.ToDouble(rw["DTSUBTTD"]), Convert.ToDouble(rw["DTTOTDSD"]), Convert.ToDouble(rw["DTTOTIVD"]), Convert.ToDouble(rw["DTTOTFCD"]), Convert.ToInt32(rw["DTNROMOV"]),
                                                              ln_itemdev, Convert.ToString(rw["DTESTADO"]), ".", NH_USUARIO, 0, Convert.ToString(rw["DTTIPPED"]), Convert.ToString(rw["DTTIPLIN"]), Convert.ToInt32(rw["DTCODDES"]), Convert.ToString(rw["DTNROCAJA"]));

                                //Tasas de Cambio
                                foreach (DataRow row in (PedidosBD.GetPedidoDT_Moneda(oSessionManager, NH_CODEMP, Convert.ToInt32(rw["DTPEDIDO"]))).Rows)
                                {
                                    if (Convert.ToInt32(rw["DTLINNUM"]) == Convert.ToInt32(row["PDLINNUM"]))
                                        FacturacionBD.InsertTasas(oSessionManager, NH_CODEMP, lc_tipfacdev, ln_nrofacdev, Convert.ToInt32(rw["DTNROITM"]), Convert.ToInt32(row["PDLINNUM"]), Convert.ToString(row["PMMONEDA"]),
                                            Convert.ToDouble(row["PMTASA"]), Convert.ToDouble(row["PMPRECIO"]), Convert.ToDouble(row["PMPRELIS"]), (Convert.ToDouble(rw["DTCANTID"]) * Convert.ToDouble(row["PMPRECIO"])), NH_USUARIO);
                                }

                                ln_itemdev++;
                            }
                        }
                    }
                    FacturacionBD.UpdateFacturaHD(oSessionManager, NH_CODEMP, lc_tipfacorigen, ln_norconsecutivoorigen, lc_tipfacdev, ln_nrofacdev);
                }

                
                
                oSessionManager.CommitTranstaction();
                return NH_TIPFAC + "-" + Convert.ToString(ln_nronota);
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
                Objm = null;
            }
        }
        public int AnulaNota(string connection, string NH_CODEMP, string NH_TIPFAC, int NH_NRONOTA ,string NH_USUARIO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            int ln_nronota = 0;
            try
            {
                oSessionManager.BeginTransaction();
                NotasBD.AnulaNotaHD(oSessionManager, NH_CODEMP, NH_TIPFAC, NH_NRONOTA, NH_USUARIO);
                NotasBD.AnulaNotaDT(oSessionManager, NH_CODEMP, NH_TIPFAC, NH_NRONOTA, NH_USUARIO);
                oSessionManager.CommitTranstaction();
                return ln_nronota;
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
        #endregion
        //Notas Debito
        #region
        public DataTable GetNotaDebHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return NotasBD.GetNotaDebHD(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetNotaDebDT(string connection, string ND_CODEMP,string NH_TIPFAC, int NH_NRONOTA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return NotasBD.GetNotaDebDT(oSessionManager, ND_CODEMP, NH_TIPFAC, NH_NRONOTA);
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
        public string InsertNotaDeb(string connection, string NH_CODEMP,string NH_TIPFAC, DateTime NH_FECNOTA, int TRCODTER, int SC_CONSECUTIVO, string NH_DESCRIPCION, double NH_TASA,string NH_ESTADO, string NH_USUARIO, string INMONEDA, object intb, object inMonedas)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TipoFacturaBD Obj = new TipoFacturaBD();
            int ln_nronota = 0, ln_nroitm = 0; 
            try
            {
                oSessionManager.BeginTransaction();
                //ln_nronota = ComunBD.GeneraConsecutivo(oSessionManager, "CONNDEB", NH_CODEMP); //Contador Notas Credito
                Obj.UpdateTipoFactura(oSessionManager, NH_CODEMP, NH_TIPFAC);
                ln_nronota = Obj.GetUltimoNroFac(oSessionManager, NH_CODEMP, NH_TIPFAC);
                NotasBD.InsertNotaDebHD(oSessionManager, NH_CODEMP, NH_TIPFAC,ln_nronota, NH_FECNOTA, TRCODTER, SC_CONSECUTIVO, NH_DESCRIPCION, NH_TASA,NH_ESTADO, NH_USUARIO);
                foreach (DataRow row in ((DataTable)intb).Rows)
                {
                    ln_nroitm = ComunBD.GeneraConsecutivo(oSessionManager, "ITMNTDBDT", NH_CODEMP);
                    NotasBD.InsertNotaDebDT(oSessionManager, ln_nroitm , NH_CODEMP, NH_TIPFAC, ln_nronota, Convert.ToString(row["DTTIPFAC"]), Convert.ToInt32(row["DTNROFAC"]), Convert.ToInt32(row["DTNROITM"]), Convert.ToString(row["ND_TARIFA"]),
                                        Convert.ToString(row["ND_DESCRIPCION"]), Convert.ToDouble(row["ND_SUBTOTAL"]), Convert.ToDouble(row["ND_IMPUESTO"]), Convert.ToDouble(row["ND_VALOR"]),
                                        "AC", NH_USUARIO);

                    //Moneda Local
                    foreach (DataRow rw in ((DataTable)inMonedas).Rows)
                        NotasBD.InsertNotaDBMoneda(oSessionManager, ln_nroitm, Convert.ToString(rw["TC_MONEDA"]), Convert.ToDouble(rw["TC_VALOR"]), Math.Round(Convert.ToDouble(row["ND_SUBTOTAL"]) / Convert.ToDouble(rw["TC_VALOR"]), 2),
                            Math.Round(Convert.ToDouble(row["ND_IMPUESTO"]) / Convert.ToDouble(rw["TC_VALOR"]), 2), Math.Round(Convert.ToDouble(row["ND_VALOR"]) / Convert.ToDouble(rw["TC_VALOR"]), 2));

                    //Moneda Local
                    NotasBD.InsertNotaDBMoneda(oSessionManager, ln_nroitm, INMONEDA, 0, Convert.ToDouble(row["ND_SUBTOTAL"]), Convert.ToDouble(row["ND_IMPUESTO"]), Convert.ToDouble(row["ND_VALOR"]));
                }
                oSessionManager.CommitTranstaction();
                return NH_TIPFAC +"-"+ Convert.ToString(ln_nronota);
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
        public int AnulaNotaDeb(string connection, string NH_CODEMP, string NH_TIPFAC, int NH_NRONOTA, string NH_USUARIO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            int ln_nronota = 0;
            try
            {
                oSessionManager.BeginTransaction();
                NotasBD.AnulaNotaDebHD(oSessionManager, NH_CODEMP, NH_TIPFAC, NH_NRONOTA, NH_USUARIO);
                NotasBD.AnulaNotaDebDT(oSessionManager, NH_CODEMP, NH_TIPFAC, NH_NRONOTA, NH_USUARIO);
                oSessionManager.CommitTranstaction();
                return ln_nronota;
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
        #endregion

    }
}
