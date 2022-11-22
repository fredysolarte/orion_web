using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Facturacion;
using XUSS.DAL.Parametros;
using XUSS.DAL.Pedidos;
using XUSS.BLL.Inventarios;
using XUSS.DAL.Inventarios;
using XUSS.DAL.Comun;
using XUSS.DAL.Articulos;
using XUSS.BLL.Comun;
using XUSS.DAL.Terceros;
using XUSS.BLL.Parametros;

namespace XUSS.BLL.Facturacion
{
    public class FacturacionBL
    {
        public DataTable GetFacturaHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
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
        public DataTable GetFacturaDT_Tasas(string connection, string DTCODEMP, string DTTIPFAC, int DTNROFAC)
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
        public DataTable GetFacturaDTIMP(string connection, string DTCODEMP, string DTTIPFAC, int DTNROFAC)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return FacturacionBD.GetFacturaDTIMP(oSessionManager, DTCODEMP, DTTIPFAC, DTNROFAC);
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
        public DataTable GetPagos(string connection, string PGCODEMP, string PGTIPFAC, int PGNROFAC)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return FacturacionBD.GetPagos(oSessionManager, PGCODEMP, PGTIPFAC, PGNROFAC);
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
        public string InsertFaturacion(string connection,string HDCODEMP, string HDTIPFAC, DateTime? HDFECFAC, int HDCODCLI, int HDCODSUC, DateTime? HDFECVEN,
                                          double HDSUBTOT, double HDTOTDES, double HDTOTIVA, double HDTOTFAC, string HDMONEDA, double HDSUBTTL, double HDTOTDSL,
                                          double HDTOTIVL, double HDTOTFCL, double HDSUBTTD, double HDTOTDSD, double HDTOTIVD, double HDTOTFCD, string HDCODNIT,
                                          string HDCDPAIS, string HDCIUDAD, string HDMODDES, string HDTERDES, string HDTERPAG, int HDAGENTE, string HDRSDIAN,
                                          string HDCATEGO, string HDCAJCOM, string HDNROALJ, string HDTIPALJ, string HDDIVISI, double HDTOTOTR, double HDTOTSEG,
                                          double HDTOTFLE, string HDCNTFIS, string HDOBSERV, double HDTOTICA, double HDTOTFTE, double HDTOTFIV, string HDESTADO,
                                          string HDCAUSAE, string HDNMUSER, string HDTRASMI, DateTime? HDFECCIE, string HDTIPDEV, int? HDNRODEV, string HDTFCDEV,
                                          int? HDFACDEV, string HDNROCAJA, int? LH_LSTPAQ, object tbDetalle, object tbPagos, string ind_inv, string ind_dev,
                                          string TRNOMBRE, string TRDIRECC, string TRNROTEL, string TRCORREO, string TRAPELLI, DateTime? TRFECNAC,string ind_bon,object tbBalance)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TipoFacturaBD Obj = new TipoFacturaBD();
            MovimientosBL Objm = new MovimientosBL();
            MovimientosBD Objmdb = new MovimientosBD();
            TasaCambioBL ObjT = new TasaCambioBL();

            DataTable tbItems = new DataTable();
            DataTable tbItemsPagos = new DataTable();
            double ln_cantidad = 0;
            int ln_consecutivo = 0, ln_nromovimiento = 0, ln_lote = 0;
            string lc_resolucion = "",lc_dettpg=".",lc_bodega ="",lc_tipmov="",lc_lote=""  ;
            Boolean lb_lote = false, lb_elem = false;

            try {
                oSessionManager.BeginTransaction();
                tbItems = (tbDetalle as DataTable);
                tbItemsPagos = (tbPagos as DataTable);

                //Genera Tercero
                if (HDCODCLI == 0)
                {
                    if (TercerosBD.ExisteTerceroIden(oSessionManager, HDCODEMP, HDCODNIT) == 0)
                    {
                        HDCODCLI = ComunBD.GeneraConsecutivo(oSessionManager, "CODTER", HDCODEMP);
                        TercerosBD.InsertTercero(oSessionManager, HDCODEMP, HDCODCLI, TRNOMBRE, null, null, null, HDCODNIT, null, TRDIRECC, null, null, null, TRNROTEL, null, null, TRCORREO, null, null, null, null, null, null, null, null, null,
                                                null, null, "LBASE07", null, null, null, "S", "N", "N", "N", "N", "N","N", null, null, null, null, null, null, null, null, null, null, null, null, null, "AC", ".", HDNMUSER, null, TRFECNAC, null, null, TRAPELLI, null, "CC", null, null, null, null, null,null);
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
                //Extrae Tasas de Cambio para la Fecha de devolucion
                //DataTable tbMonedas = ObjT.GetTasas(null, Convert.ToDateTime(HDFECFAC));
                Obj.UpdateTipoFactura(oSessionManager, HDCODEMP, HDTIPFAC);
                ln_consecutivo = Obj.GetUltimoNroFac(oSessionManager, HDCODEMP, HDTIPFAC);
                lc_resolucion = Obj.GetNumeroResolucion(oSessionManager, HDCODEMP, HDTIPFAC);
                FacturacionBD.InsertFacturaHD(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, System.DateTime.Today, HDCODCLI, HDCODSUC, null,
                                              HDSUBTOT, HDTOTDES, HDTOTIVA, HDTOTFAC, HDMONEDA, HDSUBTTL, HDTOTDSL, HDTOTIVL, HDTOTFCL, HDSUBTTD, HDTOTDSD, HDTOTIVD,
                                              HDTOTFCD, HDCODNIT, HDCDPAIS, HDCIUDAD, HDMODDES, HDTERDES, HDTERPAG, HDAGENTE, lc_resolucion,
                                              HDCATEGO, HDCAJCOM, HDNROALJ, HDTIPALJ, HDDIVISI, HDTOTOTR, HDTOTSEG,
                                              HDTOTFLE, HDCNTFIS, HDOBSERV, HDTOTICA, HDTOTFTE, HDTOTFIV, HDESTADO,
                                              HDCAUSAE, HDNMUSER, HDTRASMI, HDFECCIE, HDTIPDEV, HDNRODEV, HDTFCDEV,
                                              HDFACDEV, HDNROCAJA, LH_LSTPAQ, null, null);
                //Genera Movimiento Inventario
                if (ind_inv == "S")
                {
                    foreach (DataRow rw in Obj.GetTiposFactura(oSessionManager, " TFTIPFAC ='" + HDTIPFAC + "'", 0, 0).Rows)
                    {
                        lc_tipmov = Convert.ToString(rw["TFCDTRAN"]);
                        lc_bodega = Convert.ToString(rw["TFBODEGA"]);
                    }
                    ln_nromovimiento = Objm.InsertMovimiento(oSessionManager, HDCODEMP, 0, lc_bodega, null, lc_tipmov, null, null, null, Convert.ToString(LH_LSTPAQ), System.DateTime.Today, null,
                                                        "CE", ".", HDNMUSER, null, null, null, null, null,null,null,null);                 
                }
                

                //Detalle
                foreach (DataRow rw in tbItems.Rows)
                {
                    //Movimientos Inventario
                    if (ind_inv == "S")
                    {
                        lc_lote = Convert.ToString(rw["LOTE"]);

                        //Valida Tipo de Manejo por Tipo de Producto
                        using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, HDCODEMP, lc_bodega, Convert.ToString(rw["DTTIPPRO"])))
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                                    lb_lote = true;
                                if (Convert.ToString(reader["ABMNELEM"]) == "S")
                                    lb_elem = true;
                                //lc_clapro = Convert.ToString(reader["TACLAPRO"]);
                            }
                        }

                        if (ind_dev == "S")
                        {                                                        
                            if (lb_lote)
                            {
                                foreach (DataRow row in Objmdb.CargarMovimientoLot(oSessionManager, HDCODEMP, Convert.ToInt32(rw["DTNROMOV"]), Convert.ToInt32(rw["DTITMMOV"])).Rows)
                                    lc_lote = Convert.ToString(row["MLCDLOTE"]);
                                
                            }
                        }
                        if (Convert.ToString(rw["DEV"]) == "N")
                        {
                            if (lb_lote)
                            {
                                foreach (DataRow row in (tbBalance as DataTable).Rows)
                                {
                                    if (Convert.ToInt32(rw["DTNROITM"]) == Convert.ToInt32(row["IT"]))
                                    {
                                        lc_lote = Convert.ToString(row["MLCDLOTE"]);

                                        Objm.InsertMovimiento(oSessionManager, HDCODEMP, lc_bodega, null, System.DateTime.Today, lc_tipmov, Convert.ToString(rw["DTTIPPRO"]), Convert.ToString(rw["DTCLAVE1"]),
                                                  Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), ".", Convert.ToDouble(row["MLCANTID"]),
                                                  Convert.ToDouble(row["MLCANTID"]), "UN", ln_nromovimiento, 0, Convert.ToInt32(rw["DTNROITM"]), lc_lote, null,
                                                  null, null, null, null, 0, 0, null, "CE", ".", HDNMUSER, ln_lote, 0);
                                    }
                                }

                            }
                            else
                            {
                                Objm.InsertMovimiento(oSessionManager, HDCODEMP, lc_bodega, null, System.DateTime.Today, lc_tipmov, Convert.ToString(rw["DTTIPPRO"]), Convert.ToString(rw["DTCLAVE1"]),
                                                  Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), ".", Convert.ToDouble(rw["DTCANTID"]),
                                                  Convert.ToDouble(rw["DTCANTID"]), "UN", ln_nromovimiento, 0, Convert.ToInt32(rw["DTNROITM"]), lc_lote, null,
                                                  null, null, null, null, 0, 0, null, "CE", ".", HDNMUSER, ln_lote, 0);
                            }
                        }
                        ln_lote++;
                        rw["DTNROMOV"] = ln_nromovimiento;                        
                    }

                    if (rw.IsNull("DTITMMOV"))
                        rw["DTITMMOV"] = 0;

                    if (Convert.ToString(rw["DEV"]) == "N")
                    {
                        FacturacionBD.InsertFacturaDT(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["DTNROITM"]), rw.IsNull("DTPEDIDO") ? null : (Int32?)Convert.ToInt32(rw["DTPEDIDO"]), rw.IsNull("DTLINNUM") ? null : (Int32?)Convert.ToInt32(rw["DTLINNUM"]), Convert.ToString(rw["DTTIPPRO"]),
                                                      Convert.ToString(rw["DTCLAVE1"]), Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), Convert.ToString(rw["DTCODCAL"]), Convert.ToString(rw["DTUNDPED"]), Convert.ToDouble(rw["DTCANPED"]), Convert.ToDouble(rw["DTCANTID"]), Convert.ToDouble(rw["DTCANKLG"]),
                                                      Convert.ToString(rw["DTLISPRE"]), null, Convert.ToDouble(rw["DTPRELIS"]), Convert.ToDouble(rw["DTPRECIO"]), Convert.ToDouble(rw["DTDESCUE"]), Convert.ToDouble(rw["DTSUBTOT"]), Convert.ToDouble(rw["DTTOTDES"]), Convert.ToDouble(rw["DTTOTIVA"]), Convert.ToDouble(rw["DTTOTFAC"]),
                                                      Convert.ToDouble(rw["DTSUBTTL"]), Convert.ToDouble(rw["DTTOTDSL"]), Convert.ToDouble(rw["DTTOTIVL"]), Convert.ToDouble(rw["DTTOTFCL"]), Convert.ToDouble(rw["DTSUBTTD"]), Convert.ToDouble(rw["DTTOTDSD"]), Convert.ToDouble(rw["DTTOTIVD"]), Convert.ToDouble(rw["DTTOTFCD"]), Convert.ToInt32(rw["DTNROMOV"]),
                                                      Convert.ToInt32(rw["DTITMMOV"]), Convert.ToString(rw["DTESTADO"]), HDCAUSAE, HDNMUSER, 0, Convert.ToString(rw["DTTIPPED"]), Convert.ToString(rw["DTTIPLIN"]), Convert.ToInt32(rw["DTCODDES"]), Convert.ToString(rw["DTNROCAJA"]));

                        //Impuestos por Item
                        string lc_tipimpf = "";
                        foreach (DataRow row in ArticulosBD.GetArticulos(oSessionManager, " ARCODEMP='" + HDCODEMP + "' AND ARTIPPRO='" + Convert.ToString(rw["DTTIPPRO"]) + "' AND ARCLAVE1 ='" + Convert.ToString(rw["DTCLAVE1"]) + "' AND ARCLAVE2='" + Convert.ToString(rw["DTCLAVE2"]) + "' AND ARCLAVE3 ='" + Convert.ToString(rw["DTCLAVE3"]) + "' AND ARCLAVE4='" + Convert.ToString(rw["DTCLAVE4"]) + "'", 0, 0).Rows)
                        {
                            lc_tipimpf = Convert.ToString(row["ARCDIMPF"]);
                        }
                        FacturacionBD.InsertImpuestosxFactura(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["DTNROITM"]), lc_tipimpf, Convert.ToDouble(rw["DTTOTIVA"]));

                        //Tasas de Cambio
                        foreach (DataRow row in (PedidosBD.GetPedidoDT_Moneda(oSessionManager, HDCODEMP, Convert.ToInt32(rw["DTPEDIDO"]))).Rows)
                        {
                            if (Convert.ToInt32(rw["DTLINNUM"]) == Convert.ToInt32(row["PDLINNUM"]))
                                FacturacionBD.InsertTasas(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["DTNROITM"]), Convert.ToInt32(row["PDLINNUM"]), Convert.ToString(row["PMMONEDA"]), Convert.ToDouble(row["PMTASA"]), Convert.ToDouble(row["PMPRECIO"]), Convert.ToDouble(row["PMPRELIS"]), (Convert.ToDouble(rw["DTCANTID"]) * Convert.ToDouble(row["PMPRECIO"])), HDNMUSER);
                        }

                        //Bonos
                        if (ind_bon == "S")
                        {
                            Random random = new Random();
                            string[] letras = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "Z", "W", "X", "Y" };
                            int ln_codbono = ComunBD.GeneraConsecutivo(oSessionManager, "CNBON", HDCODEMP);                           
                            string lc_codigo = letras[random.Next(0, 23)] + letras[random.Next(0, 23)] + letras[random.Next(0, 23)] + this.RellenaCero(Convert.ToString(ln_codbono),12);
                            if (Convert.ToString(rw["ARDTTEC5"]) != "0")
                                lc_codigo = Convert.ToString(rw["ARDTTEC5"]);
                            FacturacionBD.InsertBono(oSessionManager, HDCODEMP, ln_codbono, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["DTNROITM"]), lc_codigo, Convert.ToDouble(rw["DTTOTFAC"]), "AC", "", Convert.ToDouble(rw["DTTOTFAC"]), HDNMUSER, "");
                        }                        
                    }                    
                }
                
                //Pagos
                foreach (DataRow rw in tbItemsPagos.Rows)
                {
                    lc_dettpg = ".";
                    if (!string.IsNullOrEmpty(Convert.ToString(rw["PGDETTPG"])))
                        lc_dettpg =  Convert.ToString(rw["PGDETTPG"]);

                    FacturacionBD.InsertPagos(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["PGNROITM"]), Convert.ToString(rw["PGTIPPAG"]), lc_dettpg,
                                              Convert.ToDouble(rw["PGVLRPAG"]), Convert.ToString(rw["PGSOPORT"]), null, null, "AC", ".", HDNMUSER, HDNROCAJA);
                }

                //Genera Devolucion
                if (ind_dev == "X")
                {
                    string lc_tipfacdev = "", lc_tipmovdev = "";
                    int ln_nrofacdev = 0, ln_nromovimientodev = 0, ln_itemdev = 1;
                    double ln_devtotal = 0, ln_deviva = 0;
                    foreach (DataRow rw in Obj.GetTiposFactura(oSessionManager, " TFBODEGA ='" + lc_bodega + "' AND TFCLAFAC= '2' ", 0, 0).Rows)
                    {
                        lc_tipmovdev = Convert.ToString(rw["TFCDTRAN"]);
                        lc_tipfacdev = Convert.ToString(rw["TFTIPFAC"]);
                    }
                    if (string.IsNullOrEmpty(lc_tipfacdev))
                        throw new System.ArgumentException("¡No se ha Parametrizado Tipo Documento Devolucion a T Documento Factura!");

                    Obj.UpdateTipoFactura(oSessionManager, HDCODEMP, lc_tipfacdev);
                    ln_nrofacdev = Obj.GetUltimoNroFac(oSessionManager, HDCODEMP, lc_tipfacdev);

                    foreach (DataRow rw in tbItems.Rows)
                    {
                        if (Convert.ToString(rw["DEV"]) == "S")
                        {
                            ln_devtotal += Math.Abs(Convert.ToDouble(rw["DTTOTFAC"]));
                            ln_deviva += Math.Abs(Convert.ToDouble(rw["DTTOTIVA"]));
                        }
                    }
                    FacturacionBD.InsertFacturaHD(oSessionManager, HDCODEMP, lc_tipfacdev, ln_nrofacdev, System.DateTime.Today, HDCODCLI, HDCODSUC, null,
                                                  (ln_devtotal - ln_deviva), 0, ln_deviva, ln_devtotal, HDMONEDA, HDSUBTTL, HDTOTDSL, HDTOTIVL, HDTOTFCL, HDSUBTTD, HDTOTDSD, HDTOTIVD,
                                                  HDTOTFCD, HDCODNIT, HDCDPAIS, HDCIUDAD, HDMODDES, HDTERDES, HDTERPAG, HDAGENTE, lc_resolucion,
                                                  HDCATEGO, HDCAJCOM, HDNROALJ, HDTIPALJ, HDDIVISI, HDTOTOTR, HDTOTSEG,
                                                  HDTOTFLE, HDCNTFIS, HDOBSERV, HDTOTICA, HDTOTFTE, HDTOTFIV, HDESTADO,
                                                  HDCAUSAE, HDNMUSER, HDTRASMI, HDFECCIE, HDTIPDEV, HDNRODEV, HDTFCDEV,
                                                  HDFACDEV, HDNROCAJA, LH_LSTPAQ, null, null);

                    ln_nromovimientodev = Objm.InsertMovimiento(oSessionManager, HDCODEMP, 0, lc_bodega, null, lc_tipmov, null, null, null, Convert.ToString(LH_LSTPAQ), System.DateTime.Today, null,
                                                           "CE", ".", HDNMUSER, null, null, null, null, null,null,null,null);
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        if (Convert.ToString(rw["DEV"]) == "S")
                        {
                            Objm.InsertMovimiento(oSessionManager, HDCODEMP, lc_bodega, null, System.DateTime.Today, lc_tipmovdev, Convert.ToString(rw["DTTIPPRO"]), Convert.ToString(rw["DTCLAVE1"]),
                                                 Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), ".", Math.Abs(Convert.ToDouble(rw["DTCANTID"])),
                                                 Math.Abs(Convert.ToDouble(rw["DTCANTID"])), "UN", ln_nromovimientodev, 0, ln_itemdev, lc_lote, null,
                                                 null, null, null, null, 0, 0, null, "CE", ".", HDNMUSER, ln_lote, 0);

                            rw["DTNROMOV"] = ln_nromovimientodev;

                            FacturacionBD.InsertFacturaDT(oSessionManager, HDCODEMP, lc_tipfacdev, ln_nrofacdev, ln_itemdev, rw.IsNull("DTPEDIDO") ? null : (Int32?)Convert.ToInt32(rw["DTPEDIDO"]), rw.IsNull("DTLINNUM") ? null : (Int32?)Convert.ToInt32(rw["DTLINNUM"]), Convert.ToString(rw["DTTIPPRO"]),
                                                          Convert.ToString(rw["DTCLAVE1"]), Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), Convert.ToString(rw["DTCODCAL"]), Convert.ToString(rw["DTUNDPED"]), Convert.ToDouble(rw["DTCANPED"]), Convert.ToDouble(rw["DTCANTID"]), Convert.ToDouble(rw["DTCANKLG"]),
                                                          Convert.ToString(rw["DTLISPRE"]), null, Convert.ToDouble(rw["DTPRELIS"]), Convert.ToDouble(rw["DTPRECIO"]), Convert.ToDouble(rw["DTDESCUE"]), Math.Abs(Convert.ToDouble(rw["DTSUBTOT"])), Math.Abs(Convert.ToDouble(rw["DTTOTDES"])), Math.Abs(Convert.ToDouble(rw["DTTOTIVA"])), Math.Abs(Convert.ToDouble(rw["DTTOTFAC"])),
                                                          Convert.ToDouble(rw["DTSUBTTL"]), Convert.ToDouble(rw["DTTOTDSL"]), Convert.ToDouble(rw["DTTOTIVL"]), Convert.ToDouble(rw["DTTOTFCL"]), Convert.ToDouble(rw["DTSUBTTD"]), Convert.ToDouble(rw["DTTOTDSD"]), Convert.ToDouble(rw["DTTOTIVD"]), Convert.ToDouble(rw["DTTOTFCD"]), Convert.ToInt32(rw["DTNROMOV"]),
                                                          ln_itemdev, Convert.ToString(rw["DTESTADO"]), HDCAUSAE, HDNMUSER, 0, Convert.ToString(rw["DTTIPPED"]), Convert.ToString(rw["DTTIPLIN"]), Convert.ToInt32(rw["DTCODDES"]), Convert.ToString(rw["DTNROCAJA"]));

                            //Tasas de Cambio
                            foreach (DataRow row in (PedidosBD.GetPedidoDT_Moneda(oSessionManager, HDCODEMP, Convert.ToInt32(rw["DTPEDIDO"]))).Rows)
                            {
                                if (Convert.ToInt32(rw["DTLINNUM"]) == Convert.ToInt32(row["PDLINNUM"]))
                                    FacturacionBD.InsertTasas(oSessionManager, HDCODEMP, lc_tipfacdev, ln_nrofacdev, Convert.ToInt32(rw["DTNROITM"]), Convert.ToInt32(row["PDLINNUM"]), Convert.ToString(row["PMMONEDA"]), Convert.ToDouble(row["PMTASA"]), Convert.ToDouble(row["PMPRECIO"]), Convert.ToDouble(row["PMPRELIS"]), (Convert.ToDouble(rw["DTCANTID"]) * Convert.ToDouble(row["PMPRECIO"])), HDNMUSER);
                            }

                            ln_itemdev++;
                        }
                    }

                    //FacturacionBD.UpdateFacturaHD(oSessionManager, HDCODEMP, HDTIPDEV, HDNRODEV, HDTIPFAC, ln_consecutivo);                    
                    FacturacionBD.UpdateFacturaHD(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, lc_tipfacdev, ln_nrofacdev);                                        
                }

                //Cerrar Separado
                if (ind_dev == "Z")
                    FacturacionBD.UpdateFacturaHD(oSessionManager, HDCODEMP, HDTIPDEV, HDNRODEV, HDTIPFAC, ln_consecutivo, "FA", HDNMUSER); 
                //Asocia Devolucion
                if (ind_dev == "S")                
                    FacturacionBD.UpdateFacturaHD(oSessionManager, HDCODEMP, HDTIPDEV, HDNRODEV, HDTIPFAC, ln_consecutivo);                
                //Cerrar Lista Empaque
                if (LH_LSTPAQ != 0)                
                    LtaEmpaqueBD.CerrarEmpaqueHD(oSessionManager, HDCODEMP,Convert.ToInt32(LH_LSTPAQ), HDNMUSER);                
                
                oSessionManager.CommitTranstaction();
                return HDTIPFAC + "-" + Convert.ToString( ln_consecutivo);
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
                Objmdb=null;
                ObjT = null;                
            }
        }
        public string InsertFaturacion(string connection, string HDCODEMP, string HDTIPFAC, DateTime? HDFECFAC, int HDCODCLI, int HDCODSUC, DateTime? HDFECVEN,
                                          double HDSUBTOT, double HDTOTDES, double HDTOTIVA, double HDTOTFAC, string HDMONEDA, double HDSUBTTL, double HDTOTDSL,
                                          double HDTOTIVL, double HDTOTFCL, double HDSUBTTD, double HDTOTDSD, double HDTOTIVD, double HDTOTFCD, string HDCODNIT,
                                          string HDCDPAIS, string HDCIUDAD, string HDMODDES, string HDTERDES, string HDTERPAG, int HDAGENTE, string HDRSDIAN,
                                          string HDCATEGO, string HDCAJCOM, string HDNROALJ, string HDTIPALJ, string HDDIVISI, double HDTOTOTR, double HDTOTSEG,
                                          double HDTOTFLE, string HDCNTFIS, string HDOBSERV, double HDTOTICA, double HDTOTFTE, double HDTOTFIV, string HDESTADO,
                                          string HDCAUSAE, string HDNMUSER, string HDTRASMI, DateTime? HDFECCIE, string HDTIPDEV, int? HDNRODEV, string HDTFCDEV,
                                          int? HDFACDEV, string HDNROCAJA, int? LH_LSTPAQ, object tbDetalle, object tbPagos, string ind_inv, string ind_dev,
                                          string TRNOMBRE, string TRDIRECC, string TRNROTEL, string TRCORREO, string TRAPELLI, DateTime? TRFECNAC, string ind_bon, object tbBalance, object tbAdicionales)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TipoFacturaBD Obj = new TipoFacturaBD();
            MovimientosBL Objm = new MovimientosBL();
            MovimientosBD Objmdb = new MovimientosBD();


            DataTable tbItems = new DataTable();
            DataTable tbItemsPagos = new DataTable();
            double ln_cantidad = 0;
            int ln_consecutivo = 0, ln_nromovimiento = 0, ln_lote = 0;
            string lc_resolucion = "", lc_dettpg = ".", lc_bodega = "", lc_tipmov = "", lc_lote = "";
            Boolean lb_lote = false, lb_elem = false;

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
                        HDCODCLI = ComunBD.GeneraConsecutivo(oSessionManager, "CODTER", HDCODEMP);
                        TercerosBD.InsertTercero(oSessionManager, HDCODEMP, HDCODCLI, TRNOMBRE, null, null, null, HDCODNIT, null, TRDIRECC, null, null, null, TRNROTEL, null, null, TRCORREO, null, null, null, null, null, null, null, null, null,
                                                null, null, "LBASE07", null, null, null, "S", "N", "N", "N", "N", "N","N", null, null, null, null, null, null, null, null, null, null, null, null, null, "AC", ".", HDNMUSER, null, TRFECNAC, null, null, TRAPELLI, null, "CC", null, null, null, null, null,null);
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
                                              HDFACDEV, HDNROCAJA, LH_LSTPAQ, null, null);
                //Genera Movimiento Inventario
                if (ind_inv == "S")
                {
                    foreach (DataRow rw in Obj.GetTiposFactura(oSessionManager, " TFTIPFAC ='" + HDTIPFAC + "'", 0, 0).Rows)
                    {
                        lc_tipmov = Convert.ToString(rw["TFCDTRAN"]);
                        lc_bodega = Convert.ToString(rw["TFBODEGA"]);
                    }
                    ln_nromovimiento = Objm.InsertMovimiento(oSessionManager, HDCODEMP, 0, lc_bodega, null, lc_tipmov, null, null, null, Convert.ToString(LH_LSTPAQ), System.DateTime.Today, null,
                                                        "CE", ".", HDNMUSER, null, null, null, null, null,null,null,null);
                }


                //Detalle
                foreach (DataRow rw in tbItems.Rows)
                {
                    //Movimientos Inventario
                    if (ind_inv == "S")
                    {
                        lc_lote = Convert.ToString(rw["LOTE"]);

                        //Valida Tipo de Manejo por Tipo de Producto
                        using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, HDCODEMP, lc_bodega, Convert.ToString(rw["DTTIPPRO"])))
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                                    lb_lote = true;
                                if (Convert.ToString(reader["ABMNELEM"]) == "S")
                                    lb_elem = true;
                                //lc_clapro = Convert.ToString(reader["TACLAPRO"]);
                            }
                        }

                        if (ind_dev == "S")
                        {
                            if (lb_lote)
                            {
                                foreach (DataRow row in Objmdb.CargarMovimientoLot(oSessionManager, HDCODEMP, Convert.ToInt32(rw["DTNROMOV"]), Convert.ToInt32(rw["DTITMMOV"])).Rows)
                                    lc_lote = Convert.ToString(row["MLCDLOTE"]);

                            }
                        }
                        if (Convert.ToString(rw["DEV"]) == "N")
                        {
                            if (lb_lote)
                            {
                                foreach (DataRow row in (tbBalance as DataTable).Rows)
                                {
                                    if (Convert.ToInt32(rw["DTNROITM"]) == Convert.ToInt32(row["IT"]))
                                    {
                                        lc_lote = Convert.ToString(row["MLCDLOTE"]);

                                        Objm.InsertMovimiento(oSessionManager, HDCODEMP, lc_bodega, null, System.DateTime.Today, lc_tipmov, Convert.ToString(rw["DTTIPPRO"]), Convert.ToString(rw["DTCLAVE1"]),
                                                  Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), ".", Convert.ToDouble(row["MLCANTID"]),
                                                  Convert.ToDouble(row["MLCANTID"]), "UN", ln_nromovimiento, 0, Convert.ToInt32(rw["DTNROITM"]), lc_lote, null,
                                                  null, null, null, null, 0, 0, null, "CE", ".", HDNMUSER, ln_lote, 0);
                                    }
                                }

                            }
                            else
                            {
                                Objm.InsertMovimiento(oSessionManager, HDCODEMP, lc_bodega, null, System.DateTime.Today, lc_tipmov, Convert.ToString(rw["DTTIPPRO"]), Convert.ToString(rw["DTCLAVE1"]),
                                                  Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), ".", Convert.ToDouble(rw["DTCANTID"]),
                                                  Convert.ToDouble(rw["DTCANTID"]), "UN", ln_nromovimiento, 0, Convert.ToInt32(rw["DTNROITM"]), lc_lote, null,
                                                  null, null, null, null, 0, 0, null, "CE", ".", HDNMUSER, ln_lote, 0);
                            }
                        }
                        ln_lote++;
                        rw["DTNROMOV"] = ln_nromovimiento;
                    }

                    if (rw.IsNull("DTITMMOV"))
                        rw["DTITMMOV"] = 0;

                    if (Convert.ToString(rw["DEV"]) == "N")
                    {
                        FacturacionBD.InsertFacturaDT(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["DTNROITM"]), rw.IsNull("DTPEDIDO") ? null : (Int32?)Convert.ToInt32(rw["DTPEDIDO"]), rw.IsNull("DTLINNUM") ? null : (Int32?)Convert.ToInt32(rw["DTLINNUM"]), Convert.ToString(rw["DTTIPPRO"]),
                                                      Convert.ToString(rw["DTCLAVE1"]), Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), Convert.ToString(rw["DTCODCAL"]), Convert.ToString(rw["DTUNDPED"]), Convert.ToDouble(rw["DTCANPED"]), Convert.ToDouble(rw["DTCANTID"]), Convert.ToDouble(rw["DTCANKLG"]),
                                                      Convert.ToString(rw["DTLISPRE"]), null, Convert.ToDouble(rw["DTPRELIS"]), Convert.ToDouble(rw["DTPRECIO"]), Convert.ToDouble(rw["DTDESCUE"]), Convert.ToDouble(rw["DTSUBTOT"]), Convert.ToDouble(rw["DTTOTDES"]), Convert.ToDouble(rw["DTTOTIVA"]), Convert.ToDouble(rw["DTTOTFAC"]),
                                                      Convert.ToDouble(rw["DTSUBTTL"]), Convert.ToDouble(rw["DTTOTDSL"]), Convert.ToDouble(rw["DTTOTIVL"]), Convert.ToDouble(rw["DTTOTFCL"]), Convert.ToDouble(rw["DTSUBTTD"]), Convert.ToDouble(rw["DTTOTDSD"]), Convert.ToDouble(rw["DTTOTIVD"]), Convert.ToDouble(rw["DTTOTFCD"]), Convert.ToInt32(rw["DTNROMOV"]),
                                                      Convert.ToInt32(rw["DTITMMOV"]), Convert.ToString(rw["DTESTADO"]), HDCAUSAE, HDNMUSER, 0, Convert.ToString(rw["DTTIPPED"]), Convert.ToString(rw["DTTIPLIN"]), Convert.ToInt32(rw["DTCODDES"]), Convert.ToString(rw["DTNROCAJA"]));

                        //Impuestos por Item
                        string lc_tipimpf = "";
                        foreach (DataRow row in ArticulosBD.GetArticulos(oSessionManager, " ARCODEMP='" + HDCODEMP + "' AND ARTIPPRO='" + Convert.ToString(rw["DTTIPPRO"]) + "' AND ARCLAVE1 ='" + Convert.ToString(rw["DTCLAVE1"]) + "' AND ARCLAVE2='" + Convert.ToString(rw["DTCLAVE2"]) + "' AND ARCLAVE3 ='" + Convert.ToString(rw["DTCLAVE3"]) + "' AND ARCLAVE4='" + Convert.ToString(rw["DTCLAVE4"]) + "'", 0, 0).Rows)
                        {
                            lc_tipimpf = Convert.ToString(row["ARCDIMPF"]);
                        }
                        FacturacionBD.InsertImpuestosxFactura(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["DTNROITM"]), lc_tipimpf, Convert.ToDouble(rw["DTTOTIVA"]));

                        //Bonos
                        if (ind_bon == "S")
                        {
                            Random random = new Random();
                            string[] letras = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "Z", "W", "X", "Y" };
                            int ln_codbono = ComunBD.GeneraConsecutivo(oSessionManager, "CNBON", HDCODEMP);
                            string lc_codigo = letras[random.Next(0, 23)] + letras[random.Next(0, 23)] + letras[random.Next(0, 23)] + this.RellenaCero(Convert.ToString(ln_codbono), 12);
                            if (Convert.ToString(rw["ARDTTEC5"]) != "0")
                                lc_codigo = Convert.ToString(rw["ARDTTEC5"]);
                            FacturacionBD.InsertBono(oSessionManager, HDCODEMP, ln_codbono, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["DTNROITM"]), lc_codigo, Convert.ToDouble(rw["DTTOTFAC"]), "AC", "", Convert.ToDouble(rw["DTTOTFAC"]), HDNMUSER, "");
                        }
                    }
                }
                //Pagos
                foreach (DataRow rw in tbItemsPagos.Rows)
                {
                    lc_dettpg = ".";
                    if (!string.IsNullOrEmpty(Convert.ToString(rw["PGDETTPG"])))
                        lc_dettpg = Convert.ToString(rw["PGDETTPG"]);

                    FacturacionBD.InsertPagos(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["PGNROITM"]), Convert.ToString(rw["PGTIPPAG"]), lc_dettpg,
                                              Convert.ToDouble(rw["PGVLRPAG"]), Convert.ToString(rw["PGSOPORT"]), null, null, "AC", ".", HDNMUSER, HDNROCAJA);
                }
                //Adicionales
                foreach (DataRow rw in (tbAdicionales as DataTable).Rows)
                {
                    FacturacionBD.InsertAdicionales(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToString(rw["AFCONCEPTO"]), Convert.ToDouble(rw["AFVALOR"]), "AC",HDNMUSER);
                }
                //Genera Devolucion
                if (ind_dev == "X")
                {
                    string lc_tipfacdev = "", lc_tipmovdev = "";
                    int ln_nrofacdev = 0, ln_nromovimientodev = 0, ln_itemdev = 1;
                    double ln_devtotal = 0, ln_deviva = 0;
                    foreach (DataRow rw in Obj.GetTiposFactura(oSessionManager, " TFBODEGA ='" + lc_bodega + "' AND TFCLAFAC= '2' ", 0, 0).Rows)
                    {
                        lc_tipmovdev = Convert.ToString(rw["TFCDTRAN"]);
                        lc_tipfacdev = Convert.ToString(rw["TFTIPFAC"]);
                    }
                    if (string.IsNullOrEmpty(lc_tipfacdev))
                        throw new System.ArgumentException("¡No se ha Parametrizado Tipo Documento Devolucion a T Documento Factura!");

                    Obj.UpdateTipoFactura(oSessionManager, HDCODEMP, lc_tipfacdev);
                    ln_nrofacdev = Obj.GetUltimoNroFac(oSessionManager, HDCODEMP, lc_tipfacdev);

                    foreach (DataRow rw in tbItems.Rows)
                    {
                        if (Convert.ToString(rw["DEV"]) == "S")
                        {
                            ln_devtotal += Math.Abs(Convert.ToDouble(rw["DTTOTFAC"]));
                            ln_deviva += Math.Abs(Convert.ToDouble(rw["DTTOTIVA"]));
                        }
                    }
                    FacturacionBD.InsertFacturaHD(oSessionManager, HDCODEMP, lc_tipfacdev, ln_nrofacdev, System.DateTime.Today, HDCODCLI, HDCODSUC, null,
                                                  (ln_devtotal - ln_deviva), 0, ln_deviva, ln_devtotal, HDMONEDA, HDSUBTTL, HDTOTDSL, HDTOTIVL, HDTOTFCL, HDSUBTTD, HDTOTDSD, HDTOTIVD,
                                                  HDTOTFCD, HDCODNIT, HDCDPAIS, HDCIUDAD, HDMODDES, HDTERDES, HDTERPAG, HDAGENTE, lc_resolucion,
                                                  HDCATEGO, HDCAJCOM, HDNROALJ, HDTIPALJ, HDDIVISI, HDTOTOTR, HDTOTSEG,
                                                  HDTOTFLE, HDCNTFIS, HDOBSERV, HDTOTICA, HDTOTFTE, HDTOTFIV, HDESTADO,
                                                  HDCAUSAE, HDNMUSER, HDTRASMI, HDFECCIE, HDTIPDEV, HDNRODEV, HDTFCDEV,
                                                  HDFACDEV, HDNROCAJA, LH_LSTPAQ, null, null);

                    ln_nromovimientodev = Objm.InsertMovimiento(oSessionManager, HDCODEMP, 0, lc_bodega, null, lc_tipmov, null, null, null, Convert.ToString(LH_LSTPAQ), System.DateTime.Today, null,
                                                           "CE", ".", HDNMUSER, null, null, null, null, null,null,null,null);
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        if (Convert.ToString(rw["DEV"]) == "S")
                        {
                            Objm.InsertMovimiento(oSessionManager, HDCODEMP, lc_bodega, null, System.DateTime.Today, lc_tipmovdev, Convert.ToString(rw["DTTIPPRO"]), Convert.ToString(rw["DTCLAVE1"]),
                                                 Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), ".", Math.Abs(Convert.ToDouble(rw["DTCANTID"])),
                                                 Math.Abs(Convert.ToDouble(rw["DTCANTID"])), "UN", ln_nromovimientodev, 0, ln_itemdev, lc_lote, null,
                                                 null, null, null, null, 0, 0, null, "CE", ".", HDNMUSER, ln_lote, 0);

                            rw["DTNROMOV"] = ln_nromovimientodev;

                            FacturacionBD.InsertFacturaDT(oSessionManager, HDCODEMP, lc_tipfacdev, ln_nrofacdev, ln_itemdev, rw.IsNull("DTPEDIDO") ? null : (Int32?)Convert.ToInt32(rw["DTPEDIDO"]), rw.IsNull("DTLINNUM") ? null : (Int32?)Convert.ToInt32(rw["DTLINNUM"]), Convert.ToString(rw["DTTIPPRO"]),
                                                          Convert.ToString(rw["DTCLAVE1"]), Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), Convert.ToString(rw["DTCODCAL"]), Convert.ToString(rw["DTUNDPED"]), Convert.ToDouble(rw["DTCANPED"]), Convert.ToDouble(rw["DTCANTID"]), Convert.ToDouble(rw["DTCANKLG"]),
                                                          Convert.ToString(rw["DTLISPRE"]), null, Convert.ToDouble(rw["DTPRELIS"]), Convert.ToDouble(rw["DTPRECIO"]), Convert.ToDouble(rw["DTDESCUE"]), Math.Abs(Convert.ToDouble(rw["DTSUBTOT"])), Math.Abs(Convert.ToDouble(rw["DTTOTDES"])), Math.Abs(Convert.ToDouble(rw["DTTOTIVA"])), Math.Abs(Convert.ToDouble(rw["DTTOTFAC"])),
                                                          Convert.ToDouble(rw["DTSUBTTL"]), Convert.ToDouble(rw["DTTOTDSL"]), Convert.ToDouble(rw["DTTOTIVL"]), Convert.ToDouble(rw["DTTOTFCL"]), Convert.ToDouble(rw["DTSUBTTD"]), Convert.ToDouble(rw["DTTOTDSD"]), Convert.ToDouble(rw["DTTOTIVD"]), Convert.ToDouble(rw["DTTOTFCD"]), Convert.ToInt32(rw["DTNROMOV"]),
                                                          ln_itemdev, Convert.ToString(rw["DTESTADO"]), HDCAUSAE, HDNMUSER, 0, Convert.ToString(rw["DTTIPPED"]), Convert.ToString(rw["DTTIPLIN"]), Convert.ToInt32(rw["DTCODDES"]), Convert.ToString(rw["DTNROCAJA"]));
                            ln_itemdev++;
                        }
                    }

                    //FacturacionBD.UpdateFacturaHD(oSessionManager, HDCODEMP, HDTIPDEV, HDNRODEV, HDTIPFAC, ln_consecutivo);                    
                    FacturacionBD.UpdateFacturaHD(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, lc_tipfacdev, ln_nrofacdev);
                }
                //Cerrar Separado
                if (ind_dev == "Z")
                    FacturacionBD.UpdateFacturaHD(oSessionManager, HDCODEMP, HDTIPDEV, HDNRODEV, HDTIPFAC, ln_consecutivo, "FA", HDNMUSER);
                //Asocia Devolucion
                if (ind_dev == "S")
                    FacturacionBD.UpdateFacturaHD(oSessionManager, HDCODEMP, HDTIPDEV, HDNRODEV, HDTIPFAC, ln_consecutivo);
                //Cerrar Lista Empaque
                if (LH_LSTPAQ != 0)
                    LtaEmpaqueBD.CerrarEmpaqueHD(oSessionManager, HDCODEMP, Convert.ToInt32(LH_LSTPAQ), HDNMUSER);

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
                Objm = null;
                Objmdb = null;

            }
        }
        public string InsertFaturacionSegmento(string connection, string HDCODEMP, string HDTIPFAC, DateTime? HDFECFAC, int HDCODCLI, int HDCODSUC, DateTime? HDFECVEN,
                                          double HDSUBTOT, double HDTOTDES, double HDTOTIVA, double HDTOTFAC, string HDMONEDA, double HDSUBTTL, double HDTOTDSL,
                                          double HDTOTIVL, double HDTOTFCL, double HDSUBTTD, double HDTOTDSD, double HDTOTIVD, double HDTOTFCD, string HDCODNIT,
                                          string HDCDPAIS, string HDCIUDAD, string HDMODDES, string HDTERDES, string HDTERPAG, int HDAGENTE, string HDRSDIAN,
                                          string HDCATEGO, string HDCAJCOM, string HDNROALJ, string HDTIPALJ, string HDDIVISI, double HDTOTOTR, double HDTOTSEG,
                                          double HDTOTFLE, string HDCNTFIS, string HDOBSERV, double HDTOTICA, double HDTOTFTE, double HDTOTFIV, string HDESTADO,
                                          string HDCAUSAE, string HDNMUSER, string HDTRASMI, DateTime? HDFECCIE, string HDTIPDEV, int? HDNRODEV, string HDTFCDEV,
                                          int? HDFACDEV, string HDNROCAJA, int? LH_LSTPAQ, object tbDetalle, object tbPagos, string ind_inv, string ind_dev,
                                          string TRNOMBRE, string TRDIRECC, string TRNROTEL, string TRCORREO, string TRAPELLI, string ind_bon, object tbBalance,string HD_TIPREM,int HD_NROREM)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TipoFacturaBD Obj = new TipoFacturaBD();
            MovimientosBL Objm = new MovimientosBL();
            MovimientosBD Objmdb = new MovimientosBD();


            DataTable tbItems = new DataTable();
            DataTable tbItemsPagos = new DataTable();
            double ln_cantidad = 0,ln_tfactura=0,ln_subtotfac=0,ln_totiva=0;
            int ln_consecutivo = 0, ln_nromovimiento = 0, ln_lote = 0,ln_facturas =0,ln_canfac=0,i=1,ln_itemsfac=29;
            string lc_resolucion = "", lc_dettpg = ".", lc_bodega = "", lc_tipmov = "", lc_lote = "";
            Boolean lb_lote = false, lb_elem = false;

            

            try
            {
                oSessionManager.BeginTransaction();
                ln_itemsfac = Obj.getMaxItemFac(oSessionManager, HDCODEMP, HDTIPFAC);
                if (ln_itemsfac == 0)
                    throw new System.ArgumentException("Cantidad de Items No Definidos!");

                ln_facturas = Convert.ToInt32(((tbDetalle as DataTable).Rows.Count / ln_itemsfac));
                if (((tbDetalle as DataTable).Rows.Count % ln_itemsfac) != 0)
                    ln_facturas = Convert.ToInt32(((tbDetalle as DataTable).Rows.Count / ln_itemsfac)) + 1;

                tbItems = (tbDetalle as DataTable);
                tbItemsPagos = (tbPagos as DataTable);

                //Genera Tercero
                if (HDCODCLI == 0)
                {
                    if (TercerosBD.ExisteTerceroIden(oSessionManager, HDCODEMP, HDCODNIT) == 0)
                    {
                        HDCODCLI = ComunBD.GeneraConsecutivo(oSessionManager, "CODTER", HDCODEMP);
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

                while (ln_canfac < ln_facturas)
                {
                    Obj.UpdateTipoFactura(oSessionManager, HDCODEMP, HDTIPFAC);
                    ln_consecutivo = Obj.GetUltimoNroFac(oSessionManager, HDCODEMP, HDTIPFAC);
                    lc_resolucion = Obj.GetNumeroResolucion(oSessionManager, HDCODEMP, HDTIPFAC);

                    i = 1;
                    ln_subtotfac = 0;
                    ln_totiva = 0;
                    ln_tfactura = 0;
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        ln_subtotfac += Convert.ToDouble(rw["DTSUBTOT"]);
                        ln_totiva += Convert.ToDouble(rw["DTTOTIVA"]);
                        ln_tfactura += Convert.ToDouble(rw["DTTOTFAC"]);

                        if (i == ln_itemsfac)
                            break;                        
                        i++;
                    }

                    //FacturacionBD.InsertFacturaHD(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, System.DateTime.Today, HDCODCLI, HDCODSUC, null,
                    FacturacionBD.InsertFacturaHD(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToDateTime(HDFECFAC), HDCODCLI, HDCODSUC, null,
                                                  ln_subtotfac, HDTOTDES, ln_totiva, ln_tfactura, HDMONEDA, HDSUBTTL, HDTOTDSL, HDTOTIVL, HDTOTFCL, HDSUBTTD, HDTOTDSD, HDTOTIVD,
                                                  HDTOTFCD, HDCODNIT, HDCDPAIS, HDCIUDAD, HDMODDES, HDTERDES, HDTERPAG, HDAGENTE, lc_resolucion,
                                                  HDCATEGO, HDCAJCOM, HDNROALJ, HDTIPALJ, HDDIVISI, HDTOTOTR, HDTOTSEG,
                                                  HDTOTFLE, HDCNTFIS, HDOBSERV, HDTOTICA, HDTOTFTE, HDTOTFIV, HDESTADO,
                                                  HDCAUSAE, HDNMUSER, HDTRASMI, HDFECCIE, HDTIPDEV, HDNRODEV, HDTFCDEV,
                                                  HDFACDEV, HDNROCAJA, LH_LSTPAQ, HD_TIPREM, HD_NROREM);
                    //Genera Movimiento Inventario
                    if (ind_inv == "S")
                    {
                        foreach (DataRow rw in Obj.GetTiposFactura(oSessionManager, " TFTIPFAC ='" + HDTIPFAC + "'", 0, 0).Rows)
                        {
                            lc_tipmov = Convert.ToString(rw["TFCDTRAN"]);
                            lc_bodega = Convert.ToString(rw["TFBODEGA"]);
                        }
                        ln_nromovimiento = Objm.InsertMovimiento(oSessionManager, HDCODEMP, 0, lc_bodega, null, lc_tipmov, null, null, null, Convert.ToString(LH_LSTPAQ), System.DateTime.Today, null,
                                                            "CE", ".", HDNMUSER, null, null, null, null, null,null,null,null);
                    }

                    i = 1;
                    //Detalle
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        if (rw.IsNull("DTITMMOV"))
                            rw["DTITMMOV"] = 0;

                        if (Convert.ToString(rw["DEV"]) == "N")
                        {
                            FacturacionBD.InsertFacturaDT(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["DTNROITM"]), rw.IsNull("DTPEDIDO") ? null : (Int32?)Convert.ToInt32(rw["DTPEDIDO"]), rw.IsNull("DTLINNUM") ? null : (Int32?)Convert.ToInt32(rw["DTLINNUM"]), Convert.ToString(rw["DTTIPPRO"]),
                                                          Convert.ToString(rw["DTCLAVE1"]), Convert.ToString(rw["DTCLAVE2"]), Convert.ToString(rw["DTCLAVE3"]), Convert.ToString(rw["DTCLAVE4"]), Convert.ToString(rw["DTCODCAL"]), Convert.ToString(rw["DTUNDPED"]), Convert.ToDouble(rw["DTCANPED"]), Convert.ToDouble(rw["DTCANTID"]), Convert.ToDouble(rw["DTCANKLG"]),
                                                          Convert.ToString(rw["DTLISPRE"]), null, Convert.ToDouble(rw["DTPRELIS"]), Convert.ToDouble(rw["DTPRECIO"]), Convert.ToDouble(rw["DTDESCUE"]), Convert.ToDouble(rw["DTSUBTOT"]), Convert.ToDouble(rw["DTTOTDES"]), Convert.ToDouble(rw["DTTOTIVA"]), Convert.ToDouble(rw["DTTOTFAC"]),
                                                          Convert.ToDouble(rw["DTSUBTTL"]), Convert.ToDouble(rw["DTTOTDSL"]), Convert.ToDouble(rw["DTTOTIVL"]), Convert.ToDouble(rw["DTTOTFCL"]), Convert.ToDouble(rw["DTSUBTTD"]), Convert.ToDouble(rw["DTTOTDSD"]), Convert.ToDouble(rw["DTTOTIVD"]), Convert.ToDouble(rw["DTTOTFCD"]), Convert.ToInt32(rw["DTNROMOV"]),
                                                          Convert.ToInt32(rw["DTITMMOV"]), Convert.ToString(rw["DTESTADO"]), HDCAUSAE, HDNMUSER, 0, Convert.ToString(rw["DTTIPPED"]), Convert.ToString(rw["DTTIPLIN"]), Convert.ToInt32(rw["DTCODDES"]), Convert.ToString(rw["DTNROCAJA"]));

                            //Impuestos por Item
                            string lc_tipimpf = "";
                            foreach (DataRow row in ArticulosBD.GetArticulos(oSessionManager, " ARCODEMP='" + HDCODEMP + "' AND ARTIPPRO='" + Convert.ToString(rw["DTTIPPRO"]) + "' AND ARCLAVE1 ='" + Convert.ToString(rw["DTCLAVE1"]) + "' AND ARCLAVE2='" + Convert.ToString(rw["DTCLAVE2"]) + "' AND ARCLAVE3 ='" + Convert.ToString(rw["DTCLAVE3"]) + "' AND ARCLAVE4='" + Convert.ToString(rw["DTCLAVE4"]) + "'", 0, 0).Rows)
                            {
                                lc_tipimpf = Convert.ToString(row["ARCDIMPF"]);
                            }
                            FacturacionBD.InsertImpuestosxFactura(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["DTNROITM"]), lc_tipimpf, Convert.ToDouble(rw["DTTOTIVA"]));

                            //Tasas de Cambio
                            foreach (DataRow row in (PedidosBD.GetPedidoDT_Moneda(oSessionManager, HDCODEMP, Convert.ToInt32(rw["DTPEDIDO"]))).Rows)
                            {
                                if (Convert.ToInt32(rw["DTLINNUM"]) == Convert.ToInt32(row["PDLINNUM"]))
                                    FacturacionBD.InsertTasas(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["DTNROITM"]), Convert.ToInt32(row["PDLINNUM"]), Convert.ToString(row["PMMONEDA"]), Convert.ToDouble(row["PMTASA"]), Convert.ToDouble(row["PMPRECIO"]), Convert.ToDouble(row["PMPRELIS"]), (Convert.ToDouble(rw["DTCANTID"]) * Convert.ToDouble(row["PMPRECIO"])), HDNMUSER);
                            }
                        }
                        if (i == ln_itemsfac)
                            break;
                        i++;
                    }
                    //Pagos
                    foreach (DataRow rw in tbItemsPagos.Rows)
                    {
                        lc_dettpg = ".";
                        if (!string.IsNullOrEmpty(Convert.ToString(rw["PGDETTPG"])))
                            lc_dettpg = Convert.ToString(rw["PGDETTPG"]);

                        FacturacionBD.InsertPagos(oSessionManager, HDCODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["PGNROITM"]), Convert.ToString(rw["PGTIPPAG"]), lc_dettpg,
                                                  ln_tfactura, Convert.ToString(rw["PGSOPORT"]), null, null, "AC", ".", HDNMUSER, HDNROCAJA);
                    }

                    //Cerrar Lista Empaque
                    if (LH_LSTPAQ != 0)
                        LtaEmpaqueBD.CerrarEmpaqueHD(oSessionManager, HDCODEMP, Convert.ToInt32(LH_LSTPAQ), HDNMUSER);

                    //Cerrar Remision
                    if (HD_NROREM != 0)
                        FacturacionBD.UpdateRemisionHD(oSessionManager, HDCODEMP, HD_TIPREM, HD_NROREM, "CN", "009", HDNMUSER);

                    i = 1;
                    tbItems.AcceptChanges();
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        rw.Delete();
                        if (i == ln_itemsfac)
                            break;
                        i++;
                    }
                    tbItems.AcceptChanges();

                    ln_canfac++;
                }

                oSessionManager.CommitTranstaction();
                //return HDTIPFAC + "-" + Convert.ToString(ln_consecutivo);
                return HDTIPFAC + "-" + Convert.ToString(LH_LSTPAQ);
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
                Objmdb = null;

            }
        }
        public int AnularFacturacion(string connection, string HDCODEMP, string HDTIPFAC, int HDNROFAC,string HDCAUSAE, string HDNMUSER, int LH_LSTPAQ)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                oSessionManager.BeginTransaction();
                FacturacionBD.AnularFacturaHD(oSessionManager, HDCODEMP, HDTIPFAC, HDNROFAC, HDCAUSAE, HDNMUSER);
                FacturacionBD.AnularFacturaDT(oSessionManager, HDCODEMP, HDTIPFAC, HDNROFAC, HDNMUSER);
                FacturacionBD.AnularPagos(oSessionManager, HDCODEMP, HDTIPFAC, HDNROFAC, HDNMUSER);
                LtaEmpaqueBD.ActivarEmpaqueHD(oSessionManager, HDCODEMP, LH_LSTPAQ, HDNMUSER);
                oSessionManager.CommitTranstaction();
                return 0;
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
        public int AnularFacturacionLstEmpaque(string connection, string HDCODEMP, string HDTIPFAC, int HDNROFAC, string HDCAUSAE, string HDNMUSER, int LH_LSTPAQ)
        {
            int ln_nromov = 0;
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            MovimientosBL ObjM = new MovimientosBL();
            try
            {
                oSessionManager.BeginTransaction();
                FacturacionBD.AnularFacturaHD(oSessionManager, HDCODEMP, HDTIPFAC, HDNROFAC, HDCAUSAE, HDNMUSER);
                FacturacionBD.AnularFacturaDT(oSessionManager, HDCODEMP, HDTIPFAC, HDNROFAC, HDNMUSER);
                FacturacionBD.AnularPagos(oSessionManager, HDCODEMP, HDTIPFAC, HDNROFAC, HDNMUSER);
                //Lst Empaque
                LtaEmpaqueBD.AnularEmpaqueHD(oSessionManager, HDCODEMP, LH_LSTPAQ, HDNMUSER);
                LtaEmpaqueBD.AnularEmpaqueDT(oSessionManager, HDCODEMP, LH_LSTPAQ, HDNMUSER);
                //ln_nromov = LtaEmpaqueBD.GetLtaNroMov(oSessionManager, HDCODEMP, LH_LSTPAQ);
                //foreach (DataRow rw in Obj.CargarMovimiento(oSessionManager, Convert.ToString(HDCODEMP), ln_nromov).Rows)
                //{
                //    ObjM.AnularMovimiento(oSessionManager, HDCODEMP, Convert.ToInt32(rw["MBIDMOVI"]), Convert.ToInt32(rw["MBIDITEM"]), HDNMUSER);
                //}
                foreach (DataRow rp in LtaEmpaqueBD.GetLtaEmpaqueDT(oSessionManager, Convert.ToString(HDCODEMP), LH_LSTPAQ).Rows)
                {
                    //foreach (DataRow rw in Obj.CargarMovimiento(oSessionManager, Convert.ToString(LD_CODEMP), ln_nromov).Rows)
                    foreach (DataRow rw in Obj.CargarMovimiento(oSessionManager, Convert.ToString(HDCODEMP), Convert.ToInt32(rp["LD_NRMOV"])).Rows)
                        ObjM.AnularMovimiento(oSessionManager, Convert.ToString(HDCODEMP), Convert.ToInt32(rw["MBIDMOVI"]), Convert.ToInt32(rw["MBIDITEM"]), HDNMUSER);
                }
                oSessionManager.CommitTranstaction();
                return 0;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                Obj = null;
                ObjM = null;
                oSessionManager = null;
            }
        }
        public int AnularFacturacionInventario(string connection, string HDCODEMP, string HDTIPFAC, int HDNROFAC, string HDCAUSAE, string HDNMUSER)
        {
            MovimientosBL ObjM = new MovimientosBL();
            MovimientosBD Obj = new MovimientosBD();
            SessionManager oSessionManager = new SessionManager(connection);
            int ln_nromov = 0;

            try
            {
                oSessionManager.BeginTransaction();
                FacturacionBD.AnularFacturaHD(oSessionManager, HDCODEMP, HDTIPFAC, HDNROFAC, HDCAUSAE, HDNMUSER);
                FacturacionBD.AnularFacturaDT(oSessionManager, HDCODEMP, HDTIPFAC, HDNROFAC, HDNMUSER);
                FacturacionBD.AnularPagos(oSessionManager, HDCODEMP, HDTIPFAC, HDNROFAC, HDNMUSER);
                foreach(DataRow rw in FacturacionBD.GetFacturaDT(oSessionManager,HDCODEMP,HDTIPFAC,HDNROFAC).Rows)
                {
                    ln_nromov = Convert.ToInt32(rw["DTNROMOV"]); 
                }
                foreach (DataRow rw in Obj.CargarMovimiento(oSessionManager, Convert.ToString(HDCODEMP), ln_nromov).Rows)
                {
                    ObjM.AnularMovimiento(oSessionManager, HDCODEMP, Convert.ToInt32(rw["MBIDMOVI"]), Convert.ToInt32(rw["MBIDITEM"]), HDNMUSER);
                }
                oSessionManager.CommitTranstaction();
                return 0;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                ObjM = null;
                Obj = null;
            }
        }
        public DataTable GetDifInvFac(string connection, string HDCODEMP, string HDTIPFAC, DateTime? inFecini, DateTime? inFecFin)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return FacturacionBD.GetDifInvFac(oSessionManager, HDCODEMP, HDTIPFAC, inFecini, inFecFin);
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
        public Boolean GetFacturasCierre(string connection, string HDCODEMP, string HDTIPFAC, DateTime? inFecini)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                if (FacturacionBD.GetFacturasCierre(oSessionManager, HDCODEMP, HDTIPFAC, inFecini) > 0)
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
                oSessionManager =null;
            }
        }
        public int CerrarFacturas(string connection, string HDCODEMP, string HDTIPFAC, DateTime? inFecini)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return FacturacionBD.CerrarFacturas(oSessionManager, HDCODEMP, HDTIPFAC, inFecini);
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
        public string GetEstadoFatcuraHD(string connection, string HDCODEMP, string HDTIPFAC, int HDNROFAC)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return FacturacionBD.GetEstadoFatcuraHD(oSessionManager, HDCODEMP, HDTIPFAC, HDNROFAC);
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
        private string RellenaCero(string inCadena, int inCatidad)
        {
            string lc_prerad = "";
            while (inCadena.Length + lc_prerad.Length < inCatidad)
                lc_prerad += "0";
            lc_prerad += inCadena;
            return lc_prerad;
        }
        
        //Bonos
        #region
        public Boolean ExisteBono(string connection, string DTCODEMP, string T_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                if (FacturacionBD.ExisteBono(oSessionManager, DTCODEMP, T_CODIGO) > 0)
                    return true;
                else
                    return false;
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
        public double GetSaldoBono(string connection, string DTCODEMP, string T_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return FacturacionBD.GetSaldoBono(oSessionManager, DTCODEMP, T_CODIGO);                         
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
        public DataTable GetBonos(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return FacturacionBD.GetBonos(oSessionManager, filter);
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
        //Detalle Movimientos
        #region
        public DataTable GetDetalleMovimientos(string connection, string CODEMP, string DTTIPFAC,int DTNROFAC, int DTNROITM)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return FacturacionBD.GetDetalleMovimientos(oSessionManager, CODEMP, DTTIPFAC, DTNROFAC, DTNROITM);
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
        //Adicionales
        #region
        public DataTable GetAdicionales(string connection, string HDCODEMP, string HDTIPFAC, int HDNROFAC)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return FacturacionBD.GetAdicionales(oSessionManager, HDCODEMP, HDTIPFAC, HDNROFAC);
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
        //Impuestos
        #region

        #endregion
    }
}
