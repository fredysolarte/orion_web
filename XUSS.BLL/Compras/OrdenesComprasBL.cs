using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.BLL.Comun;
using XUSS.BLL.Inventarios;
using XUSS.DAL.Compras;
using XUSS.DAL.Inventarios;
using XUSS.DAL.Parametros;
using System.IO;
using XUSS.DAL.Articulos;
using XUSS.DAL.Comun;
using XUSS.DAL.Facturacion;

namespace XUSS.BLL.Compras
{
    public class OrdenesComprasBL
    {
        //Compras
        #region
        public DataTable GetComprasHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetComprasHD(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetComprasDT(string connection, string CD_CODEMP, int CD_NROCMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetComprasDT(oSessionManager, CD_CODEMP, CD_NROCMP);
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
        public DataTable GetProforma(string connection, string PR_CODEMP, int PR_NROCMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetProforma(oSessionManager, PR_CODEMP, PR_NROCMP);
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
        public DataTable GetProformas(string connection, string PR_CODEMP, string PR_NROCMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetProformas(oSessionManager, PR_CODEMP, PR_NROCMP);
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
        public DataTable GetFacturas(string connection, string PR_CODEMP, string PR_NROCMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetFacturas(oSessionManager, PR_CODEMP, PR_NROCMP);
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
        public DataTable GetProformas(string connection, string inFilter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetProformas(oSessionManager,inFilter);
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
        public DataTable GetFactura(string connection, string PR_CODEMP, int PR_NROCMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetFactura(oSessionManager, PR_CODEMP, PR_NROCMP);
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
        public DataTable GetCostos(string connection, string CT_CODEMP, int CH_NROCMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetCostos(oSessionManager, CT_CODEMP, CH_NROCMP);
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
        public int InsertCompras(string connection, string CH_CODEMP, string CH_BODEGA, int CH_PROVEEDOR, int CH_TIPORD, DateTime CH_FECORD, string CH_TIPCMP,
                                         string CH_TIPDPH, string CH_TERPAG, string CH_NROMUESTRA, string CH_SERVICIO, double CH_VLRTOT, string CH_OBSERVACIONES, string CH_USUARIO, string CH_ESTADO,
                                         int CH_ORDENOR, DateTime CH_FENTREGA, string CH_GENINV, string CH_CMPINT, string CH_MONEDA, string CH_CNROCMPALT, object tbDetalle,
                                         object tbProforma, object tbFactura, object tbCostos, object tbImagenes, object tbBL, object tbBLDT)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            int CH_NROCMP = 0, ln_item = 1, ln_codbl = 0;

            try
            {
                oSessionManager.BeginTransaction();
                CH_NROCMP = ComunBL.GeneraConsecutivo(connection, "COMPRA");
                OrdenesComprasBD.InsertCompraHD(oSessionManager, CH_CODEMP, CH_NROCMP, CH_BODEGA, CH_PROVEEDOR, CH_TIPORD, CH_FECORD, CH_TIPCMP,
                                         CH_TIPDPH, CH_TERPAG, CH_NROMUESTRA, CH_SERVICIO, CH_VLRTOT, CH_OBSERVACIONES, CH_USUARIO, CH_ESTADO,
                                         CH_ORDENOR, CH_FENTREGA, CH_GENINV, CH_CMPINT, CH_MONEDA, CH_CNROCMPALT);

                foreach (DataRow rw in (tbDetalle as DataTable).Rows)
                {
                    if (ArticulosBD.ExisteArticulo(oSessionManager, CH_CODEMP, Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"])) > 0)
                    {
                        foreach (DataRow rt in ArticulosBD.GetArticulos(oSessionManager, " ARCODEMP ='" + CH_CODEMP + "' AND ARTIPPRO ='" + Convert.ToString(rw["CD_TIPPRO"]) + "' AND ARCLAVE1='" + Convert.ToString(rw["CD_CLAVE1"]) + "' AND ARCLAVE2='" + Convert.ToString(rw["CD_CLAVE2"]) + "'", 0, 0).Rows)
                        {                            
                            ArticulosBD.UpdateArticulo(oSessionManager, CH_CODEMP, Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"]), Convert.ToString(rw["ARNOMBRE"]), Convert.ToString(rt["ARUNDINV"]), Convert.ToString(rt["ARUMALT1"]),
                                                       Convert.ToString(rt["ARUMALT2"]), (rt["ARFCA1IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA1IN"]) : 0,(rt["ARFCA2IN"]!=DBNull.Value) ? Convert.ToDouble(rt["ARFCA2IN"]) : 0, Convert.ToString(rt["ARCDALTR"]), Convert.ToString(rt["ARMONEDA"]), Convert.ToDouble(rw["CD_PRECIO"]), 
                                                       (rt["ARCSTMPR"]!=DBNull.Value) ? Convert.ToDouble(rt["ARCSTMPR"]) : 0, (rt["ARCSTMOB"] != DBNull.Value) ? Convert.ToDouble(rt["ARCSTMOB"]) : 0,(rt["ARCSTCIF"] != DBNull.Value) ? Convert.ToDouble(rt["ARCSTCIF"]) : 0, (rt["ARCOSTOB"] != DBNull.Value) ? Convert.ToDouble(rt["ARCOSTOB"]) : 0,
                                                       (rt["ARPRECIO"] != DBNull.Value) ? Convert.ToDouble(rt["ARPRECIO"]) : 0, Convert.ToString(rt["ARCDIMPF"]), Convert.ToString(rt["ARORIGEN"]), Convert.ToString(rt["ARPOSARA"]), (rt["ARPESOUN"] != DBNull.Value) ? Convert.ToDouble(rt["ARPESOUN"]):0, Convert.ToString(rt["ARPESOUM"]), Convert.ToString(rt["ARCDCLA1"]), Convert.ToString(rt["ARCDCLA2"]),
                                                       Convert.ToString(rt["ARCDCLA3"]), Convert.ToString(rt["ARCDCLA4"]), Convert.ToString(rt["ARCDCLA6"]), Convert.ToString(rt["ARCDCLA7"]), Convert.ToString(rt["ARCDCLA8"]), Convert.ToString(rt["ARCDCLA9"]), Convert.ToString(rt["ARCDCLA10"]),
                                                       Convert.ToString(rw["ARDTTEC1"]), Convert.ToString(rw["ARDTTEC2"]), Convert.ToString(rw["ARDTTEC3"]), Convert.ToString(rw["ARDTTEC4"]), Convert.ToString(rw["ARDTTEC5"]), (rt["ARDTTEC6"] != DBNull.Value) ? Convert.ToDouble(rt["ARDTTEC6"]) : 0, Convert.ToString(rw["ARDTTEC7"]), Convert.ToString(rw["ARDTTEC8"]),
                                                       Convert.ToString(rt["ARDTTEC9"]), Convert.ToString(rt["ARDTTEC10"]), (rt["ARCODPRO"] != DBNull.Value) ? Convert.ToInt32(rt["ARCODPRO"]):0, (rt["ARFECCOM"] != DBNull.Value) ? Convert.ToDateTime(rt["ARFECCOM"]): Convert.ToDateTime("01/01/1900"), (rt["ARPRECOM"] != DBNull.Value) ? Convert.ToDouble(rt["ARPRECOM"]) : 0, Convert.ToString(rt["ARMONCOM"]),
                                                       (rt["ARPROCOM"] != DBNull.Value) ? Convert.ToInt32(rt["ARPROCOM"]) : 0, (rt["ARPROGDT"] != DBNull.Value) ? Convert.ToInt32(rt["ARPROGDT"]) : 0, (rt["ARFCINA1"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCINA1"]) : 0,
                                                       Convert.ToString(rt["ARCOMPOS"]), Convert.ToString(rt["ARCONVEN"]), Convert.ToString(rt["ARMERCON"]), Convert.ToString(rt["ARCODCOM"]), Convert.ToString(rt["ARCDCLA5"]), (rt["ARCAOPDS"] != DBNull.Value) ? Convert.ToInt32(rt["ARCAOPDS"]) : 0, Convert.ToString(rt["ARUNDODS"]), Convert.ToString(rt["ARTIPTAR"]), Convert.ToString(rt["ARANO"]),
                                                       Convert.ToString(rt["ARCOLECCION"]), Convert.ToString(rt["ARPRIORIDAD"]), Convert.ToString(rt["TR_PROCEDENCIA"]), Convert.ToString(rt["TR_UEN"]), Convert.ToString(rt["TR_TP"]), Convert.ToString(rt["TR_SCT"]), Convert.ToString(rt["TR_FONDO"]), Convert.ToString(rt["TR_TEJIDO"]),
                                                       "AC", ".", CH_USUARIO);
                        }
                    }
                    else {

                        foreach (DataRow rx in (TipoProductosBD.GetTipoProducto(oSessionManager, "", 0, 0)).Rows)
                        {
                            string lc_impf = ComunBD.GetValorc(oSessionManager, CH_CODEMP, "DEFA", "IMPF");
                            ArticulosBD.InsertArticulo(oSessionManager, CH_CODEMP, Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"]), Convert.ToString(rw["ARNOMBRE"]), "UN", null,
                                                           null, null, null, null, CH_MONEDA, Convert.ToDouble(rw["PR_PRECIO"]),
                                                           null, null, null, null,
                                                           0, lc_impf, null, null, null, null, Convert.ToString(rx["TACDCLA1"]), Convert.ToString(rx["TACDCLA2"]),
                                                           Convert.ToString(rx["TACDCLA3"]), Convert.ToString(rx["TACDCLA5"]), Convert.ToString(rx["TACDCLA6"]), Convert.ToString(rx["TACDCLA7"]), Convert.ToString(rx["TACDCLA8"]), Convert.ToString(rx["TACDCLA9"]), Convert.ToString(rx["TACDCLA10"]),
                                                           Convert.ToString(rw["ARDTTEC1"]), Convert.ToString(rw["ARDTTEC2"]), Convert.ToString(rw["ARDTTEC3"]), Convert.ToString(rw["ARDTTEC4"]), Convert.ToString(rw["ARDTTEC5"]), Convert.ToDouble(rw["ARDTTEC6"]), Convert.ToString(rw["ARDTTEC7"]), Convert.ToString(rw["ARDTTEC8"]),
                                                           null, null, null, null, null, null,
                                                           0, 0, null,
                                                           null, null, null, null, null, null, null, null, null,
                                                           null, null, null, null, null, null, null, null,
                                                           "AC", ".", CH_USUARIO);
                        }
                    }


                    //Valida Cod Barras
                    if (ArticulosBD.ExisteBarras(oSessionManager, CH_CODEMP, Convert.ToString(rw["BARRAS"])) == 0)
                        ArticulosBD.InserTbBarras(oSessionManager, CH_CODEMP, Convert.ToString(rw["BARRAS"]), Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"]), ".", "770", "001", CH_USUARIO);

                    OrdenesComprasBD.InsertCompraDT(oSessionManager, CH_CODEMP, CH_NROCMP, ln_item, CH_BODEGA, Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"]),
                                                    CH_PROVEEDOR, Convert.ToString(rw["CD_REFPRO"]), Convert.ToString(rw["CD_COLPRO"]), Convert.ToDouble(rw["CD_CANTIDAD"]), Convert.ToDouble(rw["CD_CANTIDAD"]), "UN", Convert.ToDouble(rw["CD_PRECIO"]), Convert.ToString(rw["CD_OBSERVACIONES"]), CH_USUARIO, "AC");
                    ln_item++;
                }
                //Proforma                
                foreach (DataRow rw in (tbProforma as DataTable).Rows)
                {
                    //OrdenesComprasBD.InsertCMPFacturaPRO(oSessionManager, CH_CODEMP, CH_NROCMP, ln_item, CH_BODEGA, Convert.ToString(rw["PR_TIPPRO"]),
                    if (OrdenesComprasBD.ExisteItemCMPFacturaPRO(oSessionManager,CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["PR_NROITEM"])) > 0)
                    {
                        OrdenesComprasBD.UpdateCMPFacturaPRO(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["PR_NROITEM"]), CH_BODEGA, Convert.ToString(rw["PR_TIPPRO"]),
                                Convert.ToString(rw["PR_CLAVE1"]), Convert.ToString(rw["PR_CLAVE2"]), Convert.ToString(rw["PR_CLAVE3"]),
                                Convert.ToString(rw["PR_CLAVE4"]), 0, Convert.ToString(rw["PR_REFPRO"]), Convert.ToString(rw["PR_COLPRO"]),
                                Convert.ToDouble(rw["PR_CANTIDAD"]), "UN", Convert.ToDouble(rw["PR_PRECIO"]), null, CH_USUARIO, "AC", Convert.ToString(rw["PR_NROFACPROFORMA"]), Convert.ToDateTime(rw["PR_FECPROFORMA"]), Convert.ToString(rw["PR_REFERENCIA"]), Convert.ToInt32(rw["PR_DIAS"]),
                                Convert.ToString(rw["PR_PAGO"]), Convert.ToString(rw["PR_ORIGEN"]), Convert.ToString(rw["PR_POSARA"]));
                    }
                    else
                    {
                        OrdenesComprasBD.InsertCMPFacturaPRO(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["PR_NROITEM"]), CH_BODEGA, Convert.ToString(rw["PR_TIPPRO"]),
                                Convert.ToString(rw["PR_CLAVE1"]), Convert.ToString(rw["PR_CLAVE2"]), Convert.ToString(rw["PR_CLAVE3"]),
                                Convert.ToString(rw["PR_CLAVE4"]), 0, Convert.ToString(rw["PR_REFPRO"]), Convert.ToString(rw["PR_COLPRO"]),
                                Convert.ToDouble(rw["PR_CANTIDAD"]), "UN", Convert.ToDouble(rw["PR_PRECIO"]), null, CH_USUARIO, "AC", Convert.ToString(rw["PR_NROFACPROFORMA"]), Convert.ToDateTime(rw["PR_FECPROFORMA"]), Convert.ToString(rw["PR_REFERENCIA"]), Convert.ToInt32(rw["PR_DIAS"]),
                                Convert.ToString(rw["PR_PAGO"]), Convert.ToString(rw["PR_ORIGEN"]), Convert.ToString(rw["PR_POSARA"]));
                    }                    
                }
                //Factura                
                foreach (DataRow rw in (tbFactura as DataTable).Rows)
                {
                    if (OrdenesComprasBD.ExisteItemCMPFacturaPRO(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["PR_NROITEM"])) > 0)
                    {
                        OrdenesComprasBD.UpdateCMPFacturaDT(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["FD_NROITEM"]), CH_BODEGA, Convert.ToString(rw["FD_TIPPRO"]),
                            Convert.ToString(rw["FD_CLAVE1"]), Convert.ToString(rw["FD_CLAVE2"]), Convert.ToString(rw["FD_CLAVE3"]),
                            Convert.ToString(rw["FD_CLAVE4"]), 0, Convert.ToString(rw["FD_REFPRO"]), Convert.ToString(rw["FD_COLPRO"]),
                            Convert.ToDouble(rw["FD_CANTIDAD"]), "UN", Convert.ToDouble(rw["FD_PRECIO"]), null, CH_USUARIO, "AC", Convert.ToString(rw["FD_NROFACTURA"]), Convert.ToDateTime(rw["FD_FECFAC"]), Convert.ToString(rw["FD_REFERENCIA"]), Convert.ToInt32(rw["FD_DIAS"]),
                            Convert.ToString(rw["FD_ORIGEN"]), Convert.ToString(rw["FD_POSARA"]), Convert.ToString(rw["FD_PAGO"]));
                    }
                    else
                    {
                        OrdenesComprasBD.InsertCMPFacturaDT(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["FD_NROITEM"]), CH_BODEGA, Convert.ToString(rw["FD_TIPPRO"]),
                            Convert.ToString(rw["FD_CLAVE1"]), Convert.ToString(rw["FD_CLAVE2"]), Convert.ToString(rw["FD_CLAVE3"]),
                            Convert.ToString(rw["FD_CLAVE4"]), 0, Convert.ToString(rw["FD_REFPRO"]), Convert.ToString(rw["FD_COLPRO"]),
                            Convert.ToDouble(rw["FD_CANTIDAD"]), "UN", Convert.ToDouble(rw["FD_PRECIO"]), null, CH_USUARIO, "AC", Convert.ToString(rw["FD_NROFACTURA"]), Convert.ToDateTime(rw["FD_FECFAC"]), Convert.ToString(rw["FD_REFERENCIA"]), Convert.ToInt32(rw["FD_DIAS"]),
                            Convert.ToString(rw["FD_ORIGEN"]), Convert.ToString(rw["FD_POSARA"]), Convert.ToString(rw["FD_PAGO"]));
                    }                    
                }
                //Costos
                foreach (DataRow rw in (tbCostos as DataTable).Rows)
                {                    
                        OrdenesComprasBD.InsertCMCostos(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToString(rw["CT_TIPPRO"]),
                            Convert.ToString(rw["CT_CLAVE1"]), Convert.ToString(rw["CT_CLAVE2"]), Convert.ToString(rw["CT_CLAVE3"]),
                            Convert.ToString(rw["CT_CLAVE4"]), Convert.ToDouble(rw["CT_PRECIO"]), Convert.ToInt32(rw["TRCODTER"]),
                            Convert.ToString(rw["CT_TIPDOC"]), Convert.ToString(rw["CT_NUMDOC"]), Convert.ToDateTime(rw["CT_FECDOC"]),
                            Convert.ToString(rw["CT_MONEDA"]), null, CH_USUARIO, "AC");                    
                }
                //Soportes
                foreach (DataRow row in (tbImagenes as DataTable).Rows)
                {

                    if (SoportesBD.ExisteImagen(oSessionManager, Convert.ToInt32(row["SP_CONSECUTIVO"]), CH_NROCMP) == 0)
                    {
                        Stream ioArchivo = File.OpenRead(Convert.ToString(row["RUTA"]));
                        byte[] result;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ioArchivo.CopyTo(ms);
                            result = ms.ToArray();
                        }

                        SoportesBD.InsertSoporte(oSessionManager, Convert.ToString(row["SP_TIPO"]), CH_NROCMP, Convert.ToString(row["SP_DESCRIPCION"]), Convert.ToString(row["SP_EXTENCION"]), result, CH_USUARIO, null, null);

                    }
                }
                //BL X COMPRAS
                foreach (DataRow row in (tbBL as DataTable).Rows)
                {
                    ln_codbl = ComunBL.GeneraConsecutivo(connection, "CNBL");

                    BillofLadingBD.InsertBLHD(oSessionManager, ln_codbl, CH_CODEMP, Convert.ToDateTime(row["BLH_FECHA"]), Convert.ToInt32(row["BLH_CODEXPORTER"]), Convert.ToInt32(row["BLH_CODRECEPTOR"]), Convert.ToInt32(row["BLH_CODNOTIFY"]), Convert.ToString(row["BLH_MODTRANS"]),
                        Convert.ToString(row["BLH_CIUREC"]), Convert.ToString(row["BLH_NROVIAJE"]), Convert.ToString(row["BLH_PURORIGEN"]), Convert.ToString(row["BLH_PURDESTINO"]), Convert.ToString(row["BLH_CIUDESTI"]), Convert.ToString(row["BLH_BOOKINGNO"]),
                        Convert.ToString(row["BLH_NROBILLOFL"]), Convert.ToString(row["BLH_EXPORTREF"]), Convert.ToString(row["BLH_PTOPAISORI"]), Convert.ToString(row["BLH_TIPOENVIO"]), CH_USUARIO);

                    foreach (DataRow rowdt in (tbBLDT as DataTable).Rows)
                        BillofLadingBD.InsertBLDT(oSessionManager, ln_codbl, Convert.ToString(rowdt["BLD_NROCONTAINER"]), Convert.ToDouble(rowdt["BLD_NROPACK"]), Convert.ToString(rowdt["BLD_DESCRIPTION"]), Convert.ToDouble(rowdt["BLD_GROSSWEIGHT"]), Convert.ToString(rowdt["BLD_GROSSUN"]),
                            Convert.ToDouble(rowdt["BLD_DIMESION"]), Convert.ToString(rowdt["BLD_DIMESIONUN"]));

                    BillofLadingBD.InsertBL_COMPRAS(oSessionManager, CH_NROCMP, ln_codbl);
                }
               
                oSessionManager.CommitTranstaction();
                return CH_NROCMP;
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

        public int UpdateCompras(string connection, string CH_CODEMP, int CH_NROCMP, string CH_BODEGA, int CH_PROVEEDOR, int CH_TIPORD, DateTime CH_FECORD, string CH_TIPCMP,
                                         string CH_TIPDPH, string CH_TERPAG, string CH_NROMUESTRA, string CH_SERVICIO, double CH_VLRTOT, string CH_OBSERVACIONES, string CH_USUARIO, string CH_ESTADO,
                                        int CH_ORDENOR, DateTime CH_FENTREGA, string CH_GENINV, string CH_CMPINT, string CH_MONEDA, string CH_CNROCMPALT, object tbDetalle, object tbProforma, 
                                        object tbFactura, object tbCostos, object tbImagenes, object tbBL, object tbBLDT,object tbSummari)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            int ln_item = 1,ln_codbl = 0;

            try
            {
                oSessionManager.BeginTransaction();                

                OrdenesComprasBD.UpdateCompraHD(oSessionManager, CH_CODEMP, CH_NROCMP, CH_BODEGA, CH_PROVEEDOR, CH_TIPORD, CH_FECORD, CH_TIPCMP,
                                         CH_TIPDPH, CH_TERPAG, CH_NROMUESTRA, CH_SERVICIO, CH_VLRTOT, CH_OBSERVACIONES, CH_USUARIO, CH_ESTADO,
                                         CH_ORDENOR, CH_FENTREGA, CH_GENINV, CH_CMPINT, CH_MONEDA, CH_CNROCMPALT);

                //OrdenesComprasBD.DeleteCompraDT(oSessionManager, CH_CODEMP, CH_NROCMP);
                foreach (DataRow rw in (tbDetalle as DataTable).Rows)
                {
                    if (OrdenesComprasBD.ExisteItemCMPComprasDT(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["CD_NROITEM"])) > 0)
                    {
                        OrdenesComprasBD.UpdateCompraDT(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["CD_NROITEM"]), CH_BODEGA, Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"]),
                                                        CH_PROVEEDOR, Convert.ToString(rw["CD_REFPRO"]), Convert.ToString(rw["CD_COLPRO"]), Convert.ToDouble(rw["CD_CANTIDAD"]), (rw["CD_CANSOL"] != DBNull.Value) ? Convert.ToDouble(rw["CD_CANSOL"]) : 0, "UN", Convert.ToDouble(rw["CD_PRECIO"]), Convert.ToString(rw["CD_OBSERVACIONES"]), CH_USUARIO, "AC");
                    }
                    else
                    {
                        OrdenesComprasBD.InsertCompraDT(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["CD_NROITEM"]), CH_BODEGA, Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"]),
                                                        CH_PROVEEDOR, Convert.ToString(rw["CD_REFPRO"]), Convert.ToString(rw["CD_COLPRO"]), Convert.ToDouble(rw["CD_CANTIDAD"]), (rw["CD_CANSOL"] != DBNull.Value) ? Convert.ToDouble(rw["CD_CANSOL"]) : 0, "UN", Convert.ToDouble(rw["CD_PRECIO"]), Convert.ToString(rw["CD_OBSERVACIONES"]), CH_USUARIO, "AC");
                    }


                    //Valida Articulo
                    if (ArticulosBD.ExisteArticulo(oSessionManager, CH_CODEMP, Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"])) > 0)
                    {
                        foreach (DataRow rt in ArticulosBD.GetArticulos(oSessionManager, " ARCODEMP ='" + CH_CODEMP + "' AND ARTIPPRO ='" + Convert.ToString(rw["CD_TIPPRO"]) + "' AND ARCLAVE1='" + Convert.ToString(rw["CD_CLAVE1"]) + "' AND ARCLAVE2='" + Convert.ToString(rw["CD_CLAVE2"]) + "'", 0, 0).Rows)
                        {
                            double? ln_precio_ll = Convert.ToDouble(rw["CD_PRECIO"]);
                            if (Convert.ToDouble(rt["ARCOSTOA"]) > Convert.ToDouble(rw["CD_PRECIO"]))
                                ln_precio_ll = Convert.ToDouble(rt["ARCOSTOA"]);

                            ArticulosBD.UpdateArticulo(oSessionManager, CH_CODEMP, Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"]), Convert.ToString(rw["ARNOMBRE"]), Convert.ToString(rt["ARUNDINV"]), Convert.ToString(rt["ARUMALT1"]),
                                                       //Convert.ToString(rt["ARUMALT2"]), (rt["ARFCA1IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA1IN"]) : 0, (rt["ARFCA2IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA2IN"]) : 0, Convert.ToString(rt["ARCDALTR"]), Convert.ToString(rt["ARMONEDA"]), Convert.ToDouble(rw["CD_PRECIO"]),
                                                       Convert.ToString(rt["ARUMALT2"]), (rt["ARFCA1IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA1IN"]) : 0, (rt["ARFCA2IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA2IN"]) : 0, Convert.ToString(rt["ARCDALTR"]), Convert.ToString(rt["ARMONEDA"]), Convert.ToDouble(ln_precio_ll),
                                                       (rt["ARCSTMPR"] != DBNull.Value) ? Convert.ToDouble(rt["ARCSTMPR"]) : 0, (rt["ARCSTMOB"] != DBNull.Value) ? Convert.ToDouble(rt["ARCSTMOB"]) : 0, (rt["ARCSTCIF"] != DBNull.Value) ? Convert.ToDouble(rt["ARCSTCIF"]) : 0, (rt["ARCOSTOB"] != DBNull.Value) ? Convert.ToDouble(rt["ARCOSTOB"]) : 0,
                                                       (rt["ARPRECIO"] != DBNull.Value) ? Convert.ToDouble(rt["ARPRECIO"]) : 0, Convert.ToString(rt["ARCDIMPF"]), Convert.ToString(rt["ARORIGEN"]), Convert.ToString(rt["ARPOSARA"]), (rt["ARPESOUN"] != DBNull.Value) ? Convert.ToDouble(rt["ARPESOUN"]) : 0, Convert.ToString(rt["ARPESOUM"]), Convert.ToString(rt["ARCDCLA1"]), Convert.ToString(rt["ARCDCLA2"]),
                                                       Convert.ToString(rt["ARCDCLA3"]), Convert.ToString(rt["ARCDCLA4"]), Convert.ToString(rt["ARCDCLA6"]), Convert.ToString(rt["ARCDCLA7"]), Convert.ToString(rt["ARCDCLA8"]), Convert.ToString(rt["ARCDCLA9"]), Convert.ToString(rt["ARCDCLA10"]),
                                                       Convert.ToString(rw["ARDTTEC1"]), Convert.ToString(rw["ARDTTEC2"]), Convert.ToString(rw["ARDTTEC3"]), Convert.ToString(rw["ARDTTEC4"]), Convert.ToString(rw["ARDTTEC5"]), (rt["ARDTTEC6"] != DBNull.Value) ? Convert.ToDouble(rt["ARDTTEC6"]) : 0, Convert.ToString(rw["ARDTTEC7"]), Convert.ToString(rw["ARDTTEC8"]),
                                                       Convert.ToString(rt["ARDTTEC9"]), Convert.ToString(rt["ARDTTEC10"]), (rt["ARCODPRO"] != DBNull.Value) ? Convert.ToInt32(rt["ARCODPRO"]) : 0, (rt["ARFECCOM"] != DBNull.Value) ? Convert.ToDateTime(rt["ARFECCOM"]) : Convert.ToDateTime("01/01/1900"), (rt["ARPRECOM"] != DBNull.Value) ? Convert.ToDouble(rt["ARPRECOM"]) : 0, Convert.ToString(rt["ARMONCOM"]),
                                                       (rt["ARPROCOM"] != DBNull.Value) ? Convert.ToInt32(rt["ARPROCOM"]) : 0, (rt["ARPROGDT"] != DBNull.Value) ? Convert.ToInt32(rt["ARPROGDT"]) : 0, (rt["ARFCINA1"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCINA1"]) : 0,
                                                       Convert.ToString(rt["ARCOMPOS"]), Convert.ToString(rt["ARCONVEN"]), Convert.ToString(rt["ARMERCON"]), Convert.ToString(rt["ARCODCOM"]), Convert.ToString(rt["ARCDCLA5"]), (rt["ARCAOPDS"] != DBNull.Value) ? Convert.ToInt32(rt["ARCAOPDS"]) : 0, Convert.ToString(rt["ARUNDODS"]), Convert.ToString(rt["ARTIPTAR"]), Convert.ToString(rt["ARANO"]),
                                                       Convert.ToString(rt["ARCOLECCION"]), Convert.ToString(rt["ARPRIORIDAD"]), Convert.ToString(rt["TR_PROCEDENCIA"]), Convert.ToString(rt["TR_UEN"]), Convert.ToString(rt["TR_TP"]), Convert.ToString(rt["TR_SCT"]), Convert.ToString(rt["TR_FONDO"]), Convert.ToString(rt["TR_TEJIDO"]),
                                                       "AC", ".", CH_USUARIO);
                        }
                    }
                    else
                    {

                        foreach (DataRow rx in (TipoProductosBD.GetTipoProducto(oSessionManager, "", 0, 0)).Rows)
                        {
                            string lc_impf = ComunBD.GetValorc(oSessionManager, CH_CODEMP, "DEFA", "IMPF");
                            ArticulosBD.InsertArticulo(oSessionManager, CH_CODEMP, Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"]), Convert.ToString(rw["ARNOMBRE"]), "UN", null,
                                                           null, null, null, null, CH_MONEDA, Convert.ToDouble(rw["PR_PRECIO"]),
                                                           null, null, null, null,
                                                           0, lc_impf, null, null, null, null, Convert.ToString(rx["TACDCLA1"]), Convert.ToString(rx["TACDCLA2"]),
                                                           Convert.ToString(rx["TACDCLA3"]), Convert.ToString(rx["TACDCLA5"]), Convert.ToString(rx["TACDCLA6"]), Convert.ToString(rx["TACDCLA7"]), Convert.ToString(rx["TACDCLA8"]), Convert.ToString(rx["TACDCLA9"]), Convert.ToString(rx["TACDCLA10"]),
                                                           Convert.ToString(rw["ARDTTEC1"]), Convert.ToString(rw["ARDTTEC2"]), Convert.ToString(rw["ARDTTEC3"]), Convert.ToString(rw["ARDTTEC4"]), Convert.ToString(rw["ARDTTEC5"]), Convert.ToDouble(rw["ARDTTEC6"]), Convert.ToString(rw["ARDTTEC7"]), Convert.ToString(rw["ARDTTEC8"]),
                                                           null, null, null, null, null, null,
                                                           0, 0, null,
                                                           null, null, null, null, null, null, null, null, null,
                                                           null, null, null, null, null, null, null, null,
                                                           "AC", ".", CH_USUARIO);
                        }
                    }

                    //Valida Cod Barras
                    if (ArticulosBD.ExisteBarras(oSessionManager, CH_CODEMP, Convert.ToString(rw["BARRAS"])) == 0)
                        ArticulosBD.InserTbBarras(oSessionManager, CH_CODEMP, Convert.ToString(rw["BARRAS"]), Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"]), ".", "770", "001", CH_USUARIO);

                }

                //Proforma
                //OrdenesComprasBD.DeleteCMPFacturaPRO(oSessionManager, CH_CODEMP, CH_NROCMP);                
                foreach (DataRow rw in (tbProforma as DataTable).Rows)
                {
                    if (OrdenesComprasBD.ExisteItemCMPFacturaPRO(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["PR_NROITEM"])) > 0)
                    {
                        OrdenesComprasBD.UpdateCMPFacturaPRO(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["PR_NROITEM"]), CH_BODEGA, Convert.ToString(rw["PR_TIPPRO"]),
                            Convert.ToString(rw["PR_CLAVE1"]), Convert.ToString(rw["PR_CLAVE2"]), Convert.ToString(rw["PR_CLAVE3"]),
                            Convert.ToString(rw["PR_CLAVE4"]), 0, Convert.ToString(rw["PR_REFPRO"]), Convert.ToString(rw["PR_COLPRO"]),
                            Convert.ToDouble(rw["PR_CANTIDAD"]), "UN", Convert.ToDouble(rw["PR_PRECIO"]), null, CH_USUARIO, "AC", Convert.ToString(rw["PR_NROFACPROFORMA"]), Convert.ToDateTime(rw["PR_FECPROFORMA"]), Convert.ToString(rw["PR_REFERENCIA"]), Convert.ToInt32(rw["PR_DIAS"]), 
                            Convert.ToString(rw["PR_PAGO"]), Convert.ToString(rw["PR_ORIGEN"]), Convert.ToString(rw["PR_POSARA"]));
                    }
                    else
                    {
                        OrdenesComprasBD.InsertCMPFacturaPRO(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["PR_NROITEM"]), CH_BODEGA, Convert.ToString(rw["PR_TIPPRO"]),
                            Convert.ToString(rw["PR_CLAVE1"]), Convert.ToString(rw["PR_CLAVE2"]), Convert.ToString(rw["PR_CLAVE3"]),
                            Convert.ToString(rw["PR_CLAVE4"]), 0, Convert.ToString(rw["PR_REFPRO"]), Convert.ToString(rw["PR_COLPRO"]),
                            Convert.ToDouble(rw["PR_CANTIDAD"]), "UN", Convert.ToDouble(rw["PR_PRECIO"]), null, CH_USUARIO, "AC", Convert.ToString(rw["PR_NROFACPROFORMA"]), Convert.ToDateTime(rw["PR_FECPROFORMA"]), Convert.ToString(rw["PR_REFERENCIA"]), Convert.ToInt32(rw["PR_DIAS"]), 
                            Convert.ToString(rw["PR_PAGO"]), Convert.ToString(rw["PR_ORIGEN"]), Convert.ToString(rw["PR_POSARA"]));
                    }

                    foreach (DataRow rt in ArticulosBD.GetArticulos(oSessionManager, " ARCODEMP ='" + CH_CODEMP + "' AND ARTIPPRO ='" + Convert.ToString(rw["PR_TIPPRO"]) + "' AND ARCLAVE1='" + Convert.ToString(rw["PR_CLAVE1"]) + "' AND ARCLAVE2='" + Convert.ToString(rw["PR_CLAVE2"]) + "'", 0, 0).Rows)
                    {
                        double? ln_precio_ll = Convert.ToDouble(rw["PR_PRECIO"]);
                        if (Convert.ToDouble(rt["ARCOSTOA"]) > Convert.ToDouble(rw["PR_PRECIO"]))
                            ln_precio_ll = Convert.ToDouble(rt["ARCOSTOA"]);

                        ArticulosBD.UpdateArticulo(oSessionManager, CH_CODEMP, Convert.ToString(rw["PR_TIPPRO"]), Convert.ToString(rw["PR_CLAVE1"]), Convert.ToString(rw["PR_CLAVE2"]), Convert.ToString(rw["PR_CLAVE3"]), Convert.ToString(rw["PR_CLAVE4"]), Convert.ToString(rt["ARNOMBRE"]), Convert.ToString(rt["ARUNDINV"]), Convert.ToString(rt["ARUMALT1"]),
                                                   //Convert.ToString(rt["ARUMALT2"]), (rt["ARFCA1IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA1IN"]) : 0, (rt["ARFCA2IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA2IN"]) : 0, Convert.ToString(rt["ARCDALTR"]), Convert.ToString(rt["ARMONEDA"]), Convert.ToDouble(rw["PR_PRECIO"]),
                                                   Convert.ToString(rt["ARUMALT2"]), (rt["ARFCA1IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA1IN"]) : 0, (rt["ARFCA2IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA2IN"]) : 0, Convert.ToString(rt["ARCDALTR"]), Convert.ToString(rt["ARMONEDA"]), Convert.ToDouble(ln_precio_ll),
                                                   (rt["ARCSTMPR"] != DBNull.Value) ? Convert.ToDouble(rt["ARCSTMPR"]) : 0, (rt["ARCSTMOB"] != DBNull.Value) ? Convert.ToDouble(rt["ARCSTMOB"]) : 0, (rt["ARCSTCIF"] != DBNull.Value) ? Convert.ToDouble(rt["ARCSTCIF"]) : 0, (rt["ARCOSTOB"] != DBNull.Value) ? Convert.ToDouble(rt["ARCOSTOB"]) : 0,
                                                   (rt["ARPRECIO"] != DBNull.Value) ? Convert.ToDouble(rt["ARPRECIO"]) : 0, Convert.ToString(rt["ARCDIMPF"]), Convert.ToString(rt["ARORIGEN"]), Convert.ToString(rw["PR_POSARA"]), (rt["ARPESOUN"] != DBNull.Value) ? Convert.ToDouble(rt["ARPESOUN"]) : 0, Convert.ToString(rt["ARPESOUM"]), Convert.ToString(rt["ARCDCLA1"]), Convert.ToString(rt["ARCDCLA2"]),
                                                   Convert.ToString(rt["ARCDCLA3"]), Convert.ToString(rt["ARCDCLA4"]), Convert.ToString(rt["ARCDCLA6"]), Convert.ToString(rt["ARCDCLA7"]), Convert.ToString(rt["ARCDCLA8"]), Convert.ToString(rt["ARCDCLA9"]), Convert.ToString(rt["ARCDCLA10"]),
                                                   Convert.ToString(rw["ARDTTEC1"]), Convert.ToString(rw["ARDTTEC2"]), Convert.ToString(rw["ARDTTEC3"]), Convert.ToString(rw["ARDTTEC4"]), Convert.ToString(rw["ARDTTEC5"]), (rt["ARDTTEC6"] != DBNull.Value) ? Convert.ToDouble(rt["ARDTTEC6"]) : 0, Convert.ToString(rw["ARDTTEC7"]), Convert.ToString(rw["ARDTTEC8"]),
                                                   Convert.ToString(rt["ARDTTEC9"]), Convert.ToString(rt["ARDTTEC10"]), (rt["ARCODPRO"] != DBNull.Value) ? Convert.ToInt32(rt["ARCODPRO"]) : 0, (rt["ARFECCOM"] != DBNull.Value) ? Convert.ToDateTime(rt["ARFECCOM"]) : Convert.ToDateTime("01/01/1900"), (rt["ARPRECOM"] != DBNull.Value) ? Convert.ToDouble(rt["ARPRECOM"]) : 0, Convert.ToString(rt["ARMONCOM"]),
                                                   (rt["ARPROCOM"] != DBNull.Value) ? Convert.ToInt32(rt["ARPROCOM"]) : 0, (rt["ARPROGDT"] != DBNull.Value) ? Convert.ToInt32(rt["ARPROGDT"]) : 0, (rt["ARFCINA1"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCINA1"]) : 0,
                                                   Convert.ToString(rt["ARCOMPOS"]), Convert.ToString(rt["ARCONVEN"]), Convert.ToString(rt["ARMERCON"]), Convert.ToString(rt["ARCODCOM"]), Convert.ToString(rt["ARCDCLA5"]), (rt["ARCAOPDS"] != DBNull.Value) ? Convert.ToInt32(rt["ARCAOPDS"]) : 0, Convert.ToString(rt["ARUNDODS"]), Convert.ToString(rt["ARTIPTAR"]), Convert.ToString(rt["ARANO"]),
                                                   Convert.ToString(rt["ARCOLECCION"]), Convert.ToString(rt["ARPRIORIDAD"]), Convert.ToString(rt["TR_PROCEDENCIA"]), Convert.ToString(rt["TR_UEN"]), Convert.ToString(rt["TR_TP"]), Convert.ToString(rt["TR_SCT"]), Convert.ToString(rt["TR_FONDO"]), Convert.ToString(rt["TR_TEJIDO"]),
                                                   "AC", ".", CH_USUARIO);
                    }
                }

                //Factura
                //OrdenesComprasBD.DeleteCMPFacturaDT(oSessionManager, CH_CODEMP, CH_NROCMP);
                foreach (DataRow rw in (tbFactura as DataTable).Rows)
                {
                    if (OrdenesComprasBD.ExisteItemCMPFacturaDT(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["FD_NROITEM"])) > 0)
                    {
                        OrdenesComprasBD.UpdateCMPFacturaDT(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["FD_NROITEM"]), CH_BODEGA, Convert.ToString(rw["FD_TIPPRO"]),
                            Convert.ToString(rw["FD_CLAVE1"]), Convert.ToString(rw["FD_CLAVE2"]), Convert.ToString(rw["FD_CLAVE3"]),
                            Convert.ToString(rw["FD_CLAVE4"]), 0, Convert.ToString(rw["FD_REFPRO"]), Convert.ToString(rw["FD_COLPRO"]),
                            Convert.ToDouble(rw["FD_CANTIDAD"]), "UN", Convert.ToDouble(rw["FD_PRECIO"]), null, CH_USUARIO, "AC", Convert.ToString(rw["FD_NROFACTURA"]), Convert.ToDateTime(rw["FD_FECFAC"]), Convert.ToString(rw["FD_REFERENCIA"]), Convert.ToInt32(rw["FD_DIAS"]),
                            Convert.ToString(rw["FD_ORIGEN"]), Convert.ToString(rw["FD_POSARA"]), Convert.ToString(rw["FD_PAGO"]));
                    }
                    else
                    {
                        OrdenesComprasBD.InsertCMPFacturaDT(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToInt32(rw["FD_NROITEM"]), CH_BODEGA, Convert.ToString(rw["FD_TIPPRO"]),
                            Convert.ToString(rw["FD_CLAVE1"]), Convert.ToString(rw["FD_CLAVE2"]), Convert.ToString(rw["FD_CLAVE3"]),
                            Convert.ToString(rw["FD_CLAVE4"]), 0, Convert.ToString(rw["FD_REFPRO"]), Convert.ToString(rw["FD_COLPRO"]),
                            Convert.ToDouble(rw["FD_CANTIDAD"]), "UN", Convert.ToDouble(rw["FD_PRECIO"]), null, CH_USUARIO, "AC", Convert.ToString(rw["FD_NROFACTURA"]), Convert.ToDateTime(rw["FD_FECFAC"]), Convert.ToString(rw["FD_REFERENCIA"]), Convert.ToInt32(rw["FD_DIAS"]),
                            Convert.ToString(rw["FD_ORIGEN"]), Convert.ToString(rw["FD_POSARA"]), Convert.ToString(rw["FD_PAGO"]));
                    }


                    foreach (DataRow rt in ArticulosBD.GetArticulos(oSessionManager, " ARCODEMP ='" + CH_CODEMP + "' AND ARTIPPRO ='" + Convert.ToString(rw["FD_TIPPRO"]) + "' AND ARCLAVE1='" + Convert.ToString(rw["FD_CLAVE1"]) + "' AND ARCLAVE2='" + Convert.ToString(rw["FD_CLAVE2"]) + "'", 0, 0).Rows)
                    {
                        double? ln_precio_ll = Convert.ToDouble(rw["FD_PRECIO"]);
                        if (Convert.ToDouble(rt["ARCOSTOA"]) > Convert.ToDouble(rw["FD_PRECIO"]))
                            ln_precio_ll = Convert.ToDouble(rt["ARCOSTOA"]);

                        ArticulosBD.UpdateArticulo(oSessionManager, CH_CODEMP, Convert.ToString(rw["FD_TIPPRO"]), Convert.ToString(rw["FD_CLAVE1"]), Convert.ToString(rw["FD_CLAVE2"]), Convert.ToString(rw["FD_CLAVE3"]), Convert.ToString(rw["FD_CLAVE4"]), Convert.ToString(rt["ARNOMBRE"]), Convert.ToString(rt["ARUNDINV"]), Convert.ToString(rt["ARUMALT1"]),
                                                   //Convert.ToString(rt["ARUMALT2"]), (rt["ARFCA1IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA1IN"]) : 0, (rt["ARFCA2IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA2IN"]) : 0, Convert.ToString(rt["ARCDALTR"]), Convert.ToString(rt["ARMONEDA"]), Convert.ToDouble(rw["FD_PRECIO"]),
                                                   Convert.ToString(rt["ARUMALT2"]), (rt["ARFCA1IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA1IN"]) : 0, (rt["ARFCA2IN"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCA2IN"]) : 0, Convert.ToString(rt["ARCDALTR"]), Convert.ToString(rt["ARMONEDA"]), Convert.ToDouble(ln_precio_ll),
                                                   (rt["ARCSTMPR"] != DBNull.Value) ? Convert.ToDouble(rt["ARCSTMPR"]) : 0, (rt["ARCSTMOB"] != DBNull.Value) ? Convert.ToDouble(rt["ARCSTMOB"]) : 0, (rt["ARCSTCIF"] != DBNull.Value) ? Convert.ToDouble(rt["ARCSTCIF"]) : 0, (rt["ARCOSTOB"] != DBNull.Value) ? Convert.ToDouble(rt["ARCOSTOB"]) : 0,
                                                   (rt["ARPRECIO"] != DBNull.Value) ? Convert.ToDouble(rt["ARPRECIO"]) : 0, Convert.ToString(rt["ARCDIMPF"]), Convert.ToString(rt["ARORIGEN"]), Convert.ToString(rw["FD_POSARA"]), (rt["ARPESOUN"] != DBNull.Value) ? Convert.ToDouble(rt["ARPESOUN"]) : 0, Convert.ToString(rt["ARPESOUM"]), Convert.ToString(rt["ARCDCLA1"]), Convert.ToString(rt["ARCDCLA2"]),
                                                   Convert.ToString(rt["ARCDCLA3"]), Convert.ToString(rt["ARCDCLA4"]), Convert.ToString(rt["ARCDCLA6"]), Convert.ToString(rt["ARCDCLA7"]), Convert.ToString(rt["ARCDCLA8"]), Convert.ToString(rt["ARCDCLA9"]), Convert.ToString(rt["ARCDCLA10"]),
                                                   Convert.ToString(rw["ARDTTEC1"]), Convert.ToString(rw["ARDTTEC2"]), Convert.ToString(rw["ARDTTEC3"]), Convert.ToString(rw["ARDTTEC4"]), Convert.ToString(rw["ARDTTEC5"]), (rt["ARDTTEC6"] != DBNull.Value) ? Convert.ToDouble(rt["ARDTTEC6"]) : 0, Convert.ToString(rw["ARDTTEC7"]), Convert.ToString(rw["ARDTTEC8"]),
                                                   Convert.ToString(rt["ARDTTEC9"]), Convert.ToString(rt["ARDTTEC10"]), (rt["ARCODPRO"] != DBNull.Value) ? Convert.ToInt32(rt["ARCODPRO"]) : 0, (rt["ARFECCOM"] != DBNull.Value) ? Convert.ToDateTime(rt["ARFECCOM"]) : Convert.ToDateTime("01/01/1900"), (rt["ARPRECOM"] != DBNull.Value) ? Convert.ToDouble(rt["ARPRECOM"]) : 0, Convert.ToString(rt["ARMONCOM"]),
                                                   (rt["ARPROCOM"] != DBNull.Value) ? Convert.ToInt32(rt["ARPROCOM"]) : 0, (rt["ARPROGDT"] != DBNull.Value) ? Convert.ToInt32(rt["ARPROGDT"]) : 0, (rt["ARFCINA1"] != DBNull.Value) ? Convert.ToDouble(rt["ARFCINA1"]) : 0,
                                                   Convert.ToString(rt["ARCOMPOS"]), Convert.ToString(rt["ARCONVEN"]), Convert.ToString(rt["ARMERCON"]), Convert.ToString(rt["ARCODCOM"]), Convert.ToString(rt["ARCDCLA5"]), (rt["ARCAOPDS"] != DBNull.Value) ? Convert.ToInt32(rt["ARCAOPDS"]) : 0, Convert.ToString(rt["ARUNDODS"]), Convert.ToString(rt["ARTIPTAR"]), Convert.ToString(rt["ARANO"]),
                                                   Convert.ToString(rt["ARCOLECCION"]), Convert.ToString(rt["ARPRIORIDAD"]), Convert.ToString(rt["TR_PROCEDENCIA"]), Convert.ToString(rt["TR_UEN"]), Convert.ToString(rt["TR_TP"]), Convert.ToString(rt["TR_SCT"]), Convert.ToString(rt["TR_FONDO"]), Convert.ToString(rt["TR_TEJIDO"]),
                                                   "AC", ".", CH_USUARIO);
                    }

                    //Valida Cod Barras
                    if (ArticulosBD.ExisteBarras(oSessionManager, CH_CODEMP, Convert.ToString(rw["BARRAS"])) == 0)
                        ArticulosBD.InserTbBarras(oSessionManager, CH_CODEMP, Convert.ToString(rw["BARRAS"]), Convert.ToString(rw["FD_TIPPRO"]), Convert.ToString(rw["FD_CLAVE1"]), Convert.ToString(rw["FD_CLAVE2"]), Convert.ToString(rw["FD_CLAVE3"]), Convert.ToString(rw["FD_CLAVE4"]), ".", "770", "001", CH_USUARIO);
                }

                //Cerrar Documentos
                foreach (DataRow rw in ((DataTable)tbSummari).Rows)
                {
                    switch (Convert.ToString(rw["ITM"]))
                    {
                        case "1":
                            OrdenesComprasBD.UpdateCompraDT(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToString(rw["CD_ESTADO"]));
                            break;
                        case "2":
                            OrdenesComprasBD.UpdateCMPFacturaPRO(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToString(rw["CD_NROCMP"]), Convert.ToString(rw["CD_ESTADO"]));
                            break;
                        case "3":
                            OrdenesComprasBD.UpdateCMPFacturaDT(oSessionManager, CH_CODEMP, CH_NROCMP, Convert.ToString(rw["CD_NROCMP"]), Convert.ToString(rw["CD_ESTADO"]));
                            break;
                    }
                }

                //Soportes
                foreach (DataRow row in (tbImagenes as DataTable).Rows)
                {

                    if (SoportesBD.ExisteImagen(oSessionManager, Convert.ToInt32(row["SP_CONSECUTIVO"]), CH_NROCMP) == 0)
                    {
                        Stream ioArchivo = File.OpenRead(Convert.ToString(row["RUTA"]));
                        byte[] result;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ioArchivo.CopyTo(ms);
                            result = ms.ToArray();
                        }
                        SoportesBD.InsertSoporte(oSessionManager, Convert.ToString(row["SP_TIPO"]), CH_NROCMP, Convert.ToString(row["SP_DESCRIPCION"]), Convert.ToString(row["SP_EXTENCION"]), result, CH_USUARIO, null, null);
                    }
                }
                //BL X COMPRAS
                foreach (DataRow row in (tbBL as DataTable).Rows)
                {
                    ln_codbl = ComunBL.GeneraConsecutivo(connection, "CNBL");

                    BillofLadingBD.InsertBLHD(oSessionManager, ln_codbl, CH_CODEMP, Convert.ToDateTime(row["BLH_FECHA"]),Convert.ToInt32(row["BLH_CODEXPORTER"]), Convert.ToInt32(row["BLH_CODRECEPTOR"]), Convert.ToInt32(row["BLH_CODNOTIFY"]), Convert.ToString(row["BLH_MODTRANS"]),
                        Convert.ToString(row["BLH_CIUREC"]), Convert.ToString(row["BLH_NROVIAJE"]), Convert.ToString(row["BLH_PURORIGEN"]), Convert.ToString(row["BLH_PURDESTINO"]), Convert.ToString(row["BLH_CIUDESTI"]), Convert.ToString(row["BLH_BOOKINGNO"]),
                        Convert.ToString(row["BLH_NROBILLOFL"]), Convert.ToString(row["BLH_EXPORTREF"]), Convert.ToString(row["BLH_PTOPAISORI"]), Convert.ToString(row["BLH_TIPOENVIO"]), CH_USUARIO);

                    foreach (DataRow rowdt in (tbBLDT as DataTable).Rows)
                        BillofLadingBD.InsertBLDT(oSessionManager, ln_codbl, Convert.ToString(rowdt["BLD_NROCONTAINER"]), Convert.ToDouble(rowdt["BLD_NROPACK"]), Convert.ToString(rowdt["BLD_DESCRIPTION"]), Convert.ToDouble(rowdt["BLD_GROSSWEIGHT"]), Convert.ToString(rowdt["BLD_GROSSUN"]),
                            Convert.ToDouble(rowdt["BLD_DIMESION"]), Convert.ToString(rowdt["BLD_DIMESIONUN"]));

                    BillofLadingBD.InsertBL_COMPRAS(oSessionManager, CH_NROCMP, ln_codbl);
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
            }
        }        
        public int DeleteCompraDT(string connection, string CD_CODEMP, int CD_NROCMP, int CD_NROITEM)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.DeleteCompraDT(oSessionManager, CD_CODEMP, CD_NROCMP, CD_NROITEM);
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
        public int DeleteCMPFacturaDT(string connection, string CD_CODEMP, int CD_NROCMP, int CD_NROITEM)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.DeleteCMPFacturaDT(oSessionManager, CD_CODEMP, CD_NROCMP, CD_NROITEM);
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
        public int DeleteCMPFacturaPRO(string connection, string CD_CODEMP, int PR_NROCMP, int PR_NROITEM)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.DeleteCMPFacturaPRO(oSessionManager, CD_CODEMP, PR_NROCMP, PR_NROITEM);
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
        public int UpdateCompras(string connection, string CH_CODEMP, int CH_NROCMP, string CH_BODEGA, int CH_PROVEEDOR, int CH_TIPORD, DateTime CH_FECORD, string CH_TIPCMP,
                                         string CH_TIPDPH, string CH_TERPAG, string CH_NROMUESTRA, string CH_SERVICIO, double CH_VLRTOT, string CH_OBSERVACIONES, string CH_USUARIO, string CH_ESTADO,
                                        int CH_ORDENOR, DateTime CH_FENTREGA, string CH_GENINV, string CH_CMPINT, string CH_MONEDA, string CH_CNROCMPALT, object tbDetalle)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            int ln_item = 1;

            try
            {
                oSessionManager.BeginTransaction();                

                OrdenesComprasBD.UpdateCompraHD(oSessionManager, CH_CODEMP, CH_NROCMP, CH_BODEGA, CH_PROVEEDOR, CH_TIPORD, CH_FECORD, CH_TIPCMP,
                                         CH_TIPDPH, CH_TERPAG, CH_NROMUESTRA, CH_SERVICIO, CH_VLRTOT, CH_OBSERVACIONES, CH_USUARIO, CH_ESTADO,
                                         CH_ORDENOR, CH_FENTREGA, CH_GENINV, CH_CMPINT, CH_MONEDA, CH_CNROCMPALT);
                
                foreach (DataRow rw in (tbDetalle as DataTable).Rows)
                {
                    OrdenesComprasBD.UpdateCompraDT(oSessionManager, CH_CODEMP, CH_NROCMP, ln_item, CH_BODEGA, Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"]),
                                                    CH_PROVEEDOR, Convert.ToString(rw["CD_REFPRO"]), Convert.ToString(rw["CD_COLPRO"]), Convert.ToDouble(rw["CD_CANTIDAD"]), Convert.ToDouble(rw["CD_CANSOL"]), "UN", Convert.ToDouble(rw["CD_PRECIO"]), Convert.ToString(rw["CD_OBSERVACIONES"]), CH_USUARIO, "AC");
                    ln_item++;
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
            }
        }
        public DataTable GetReciboHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return OrdenesComprasBD.GetReciboHD(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetReciboDT(string connection, string CD_CODEMP, int CD_NROCMP, int RD_NRORECIBO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetReciboDT(oSessionManager, CD_CODEMP, CD_NROCMP, RD_NRORECIBO);
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
        public int InsertRecibos(string connection, string CH_CODEMP, int CH_NROCMP, string CH_BODEGA, int CH_PROVEEDOR, int CH_TIPORD, DateTime CH_FECORD, string CH_TIPCMP,
                                         string CH_TIPDPH, string CH_TERPAG, string CH_NROMUESTRA, string CH_SERVICIO, double CH_VLRTOT, string CH_OBSERVACIONES, string CH_USUARIO, string CH_ESTADO,
                                        int CH_ORDENOR, DateTime CH_FENTREGA, string CH_GENINV, string CH_CMPINT, string CH_MONEDA, object tbDetalle)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TipoFacturaBD Obj = new TipoFacturaBD();
            MovimientosBL Objm = new MovimientosBL();
            MovimientosBD Objmdb = new MovimientosBD();
            int RH_NRORECIBO = 0, ln_nromovimiento = 0, ln_lote = 0;
            Boolean lb_indc = true;
            try {
                oSessionManager.BeginTransaction();
                RH_NRORECIBO = ComunBL.GeneraConsecutivo(connection, "RECIBO");
                OrdenesComprasBD.InsertReciboHD(oSessionManager, CH_CODEMP, RH_NRORECIBO, CH_NROCMP, "", "", null, "", "AC", CH_USUARIO);


                ln_nromovimiento = Objm.InsertMovimiento(oSessionManager, CH_CODEMP, 0, CH_BODEGA, null, "16", null, null, null, Convert.ToString(RH_NRORECIBO), System.DateTime.Today, null,
                                                    "CE", ".", CH_USUARIO, null, null, null, null, null,null,null,null);


                foreach (DataRow rw in (tbDetalle as DataTable).Rows)
                {
                    Objm.InsertMovimiento(oSessionManager, CH_CODEMP, CH_BODEGA, null, System.DateTime.Today, "16", Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]),
                                          Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"]), ".", Convert.ToDouble(rw["RD_CANTIDAD"]),
                                          Convert.ToDouble(rw["RD_CANTIDAD"]), "UN", ln_nromovimiento, 0, Convert.ToInt32(rw["CD_NROITEM"]), Convert.ToString(rw["LOT1"]), null,
                                          null, null, null, null, 0, 0, null, "CE", ".", CH_USUARIO, ln_lote, 0);
                    ln_lote++;
                    //rw["RD_IDMOVI"] = ln_nromovimiento;  

                    OrdenesComprasBD.InsertReciboDT(oSessionManager, CH_CODEMP, RH_NRORECIBO, Convert.ToInt32(rw["CD_NROITEM"]), Convert.ToString(rw["CD_TIPPRO"]), Convert.ToString(rw["CD_CLAVE1"]), Convert.ToString(rw["CD_CLAVE2"]), Convert.ToString(rw["CD_CLAVE3"]), Convert.ToString(rw["CD_CLAVE4"]),
                                                    "UN", Convert.ToDouble(rw["RD_CANTIDAD"]), ln_nromovimiento, CH_USUARIO, "AC");

                    if ((Convert.ToDouble(rw["RD_CANTIDAD"]) - Convert.ToDouble(rw["RD_CANTIDAD"])) != 0)
                        lb_indc = true;
                }

                if (lb_indc)
                    OrdenesComprasBD.UpdateEstCompraHD(oSessionManager, CH_CODEMP, CH_NROCMP, "CE", CH_USUARIO);

                oSessionManager.CommitTranstaction();
                return RH_NRORECIBO;
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
        public IDataReader GetComprasHD(string connection, string CH_CODEMP, int CH_NROCMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetComprasHD(oSessionManager, CH_CODEMP, CH_NROCMP);
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
        public DataTable GetSeguimiento(string connection, string CH_CODEMP, int CH_NROCMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetSeguimiento(oSessionManager, CH_CODEMP, CH_NROCMP);
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
        public int InsertSeguimiento(string connection, string CH_CODEMP, int CH_NROCMP, string SG_DESCRIPCION, string SG_USUARIO, string SG_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.InsertSeguimiento(oSessionManager, CH_CODEMP, CH_NROCMP, SG_DESCRIPCION, SG_USUARIO, SG_ESTADO);
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
        public int UpdateCompras(string connection, string CH_CODEMP, int CH_NROCMP, string InUsuario)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                oSessionManager.BeginTransaction();
                OrdenesComprasBD.UpdateEstCompraHD(oSessionManager, CH_CODEMP, CH_NROCMP, "AP", InUsuario);
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

            }
        }
        public double GetUltimoPrecioOC(string connection, string CD_CODEMP, string CD_TIPPRO,string CD_CLAVE1)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetUltimoPrecioOC(oSessionManager, CD_CODEMP, CD_TIPPRO, CD_CLAVE1);

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
        public int AnularOrdenCompra(string connection, string CH_CODEMP, int CH_NROCMP, string CH_ESTADO, string CH_USUARIO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                //return OrdenesComprasBD.GetUltimoPrecioOC(oSessionManager, CD_CODEMP, CD_TIPPRO, CD_CLAVE1);
                OrdenesComprasBD.AnularOrdenCompraHD(oSessionManager, CH_CODEMP, CH_NROCMP, CH_ESTADO, CH_USUARIO);
                OrdenesComprasBD.AnularOrdenCompraDT(oSessionManager, CH_CODEMP, CH_NROCMP, CH_ESTADO, CH_USUARIO);
                OrdenesComprasBD.AnularProforma(oSessionManager, CH_CODEMP, CH_NROCMP, CH_ESTADO, CH_USUARIO);
                OrdenesComprasBD.AnularFactura(oSessionManager, CH_CODEMP, CH_NROCMP, CH_ESTADO, CH_USUARIO);
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

        public DataTable GetSummari(string connection, string CH_CODEMP, int CH_NROCMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {                                                
                return OrdenesComprasBD.GetSummari(oSessionManager, CH_CODEMP, CH_NROCMP);
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
        //wr in
        #region
        public DataTable GetWRIN(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetWRIN(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetWRINDT(string connection, int WIH_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetWRINDT(oSessionManager, WIH_CONSECUTIVO);
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
        public DataTable GetCompraDTWRIN(string connection, string FD_CODEMP, string inFilter)
        {
            SessionManager oSessionManager = new SessionManager(connection);            
            try
            {                
                return OrdenesComprasBD.GetCompraDTWRIN(oSessionManager, FD_CODEMP, inFilter);
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
        public int InsertWRIN(string connection, string WIH_CODEMP, DateTime WIH_FECHA, double WIH_KILOS, double WIH_BULTOS, string BDBODEGA,
                              string WIH_OBSERVACIONES, string WIH_USUARIO, string WIH_ESTADO, string WIH_NROALT, string WIH_DESPACHO, object tbItems, object tbBL, object tbBLDT,object tbSoportes)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBL ObjM = new MovimientosBL();
            TrasladosBD Obj = new TrasladosBD();
            int WIH_CONSECUTIVO = 0, ln_codbl=0,MIIDMOVI = 0,i=0, MIMOVT=0, MIOTMOVI=0, TSNROTRA=0;
            string MICDTRAN = "25"; //COMPRAS WR IN
            string MIBODEGA = "TR";//BODEGA TRANSITO
            try
            {
                oSessionManager.BeginTransaction();
                WIH_CONSECUTIVO = ComunBL.GeneraConsecutivo(connection, "WRIN");
                

                //Genera Cabecera del MOv Inv
                //MIIDMOVI = ObjM.InsertMovimiento(oSessionManager, WIH_CODEMP, 0, BDBODEGA, null, MICDTRAN, 0, 0, 0, null, null,
                MIIDMOVI = ObjM.InsertMovimiento(oSessionManager, WIH_CODEMP, 0, MIBODEGA, null, MICDTRAN, 0, 0, 0, null, null,
                                                 "comentario", "CE", ".", WIH_USUARIO, 0, 0, 0, null, 0,null,null,null);

                //Nro Traslado
                TSNROTRA = ComunBD.GeneraConsecutivo(oSessionManager, "NROTRA", WIH_CODEMP);
                //Genera Movimiento Salida
                MIMOVT = ObjM.InsertMovimiento(oSessionManager, WIH_CODEMP, MIOTMOVI, MIBODEGA, BDBODEGA, "99", 0, 0, 0, null, WIH_FECHA, null, "AC", ".", WIH_USUARIO, 0, 0, TSNROTRA, null, 0,null,null,null);
                //Movimiento de Entrada
                MIOTMOVI = ObjM.InsertMovimiento(oSessionManager, WIH_CODEMP, MIOTMOVI, BDBODEGA, MIBODEGA, "98", 0, 0, 0, null, WIH_FECHA, null, "AC", ".", WIH_USUARIO, 0, 0, TSNROTRA, null, 0, null, null, null);

                OrdenesComprasBD.InsertWRIN(oSessionManager, WIH_CONSECUTIVO, WIH_CODEMP, WIH_FECHA, WIH_KILOS, WIH_BULTOS, BDBODEGA, WIH_OBSERVACIONES, WIH_USUARIO, WIH_ESTADO, WIH_NROALT,TSNROTRA, WIH_DESPACHO);

                foreach (DataRow rw in (tbItems as DataTable).Rows)
                {
                    i++;
                    //Genera movimiento Inv Detallado
                    //ObjM.InsertMovimiento(oSessionManager, WIH_CODEMP, BDBODEGA, null, System.DateTime.Today, MICDTRAN, Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]),
                    ObjM.InsertMovimiento(oSessionManager, WIH_CODEMP, MIBODEGA, null, System.DateTime.Today, MICDTRAN, Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]),
                                          Convert.ToString(rw["ARCLAVE4"]), ".", Convert.ToDouble(rw["WID_CANTIDAD"]), Convert.ToDouble(rw["WID_CANTIDAD"]), "UN", MIIDMOVI, 0, i, "", null, null, null,
                                          null, null, 0, 0, null, "CE", ".", WIH_USUARIO, 0, 0);
                    //Insertar Traslado


                    //Inserta WR DT
                    OrdenesComprasBD.InsertWRINDT(oSessionManager, WIH_CONSECUTIVO, i ,Convert.ToString(rw["ARCODEMP"]), Convert.ToInt32(rw["CD_NROCMP"]), Convert.ToInt32(rw["CD_NROITEM"]),
                                                  Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]),
                                                  Convert.ToString(rw["ARCLAVE4"]), Convert.ToString(rw["WID_NROFACTURA"]), Convert.ToInt32(rw["WID_CANTIDAD"]), Convert.ToDouble(rw["WID_PRECIO"]), Convert.ToDouble(rw["WID_PRECIOVTA"]), WIH_USUARIO, MIIDMOVI);
                    rw["WID_ITEM"] = i;
                }

                //Genera Nro Traslado
                #region               

                //Genera Movimiento Salida
                //MIIDMOVI = ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, MIOTMOVI, BDBODEGA, TSOTBODE, "99", 0, 0, 0, null, WOH_FECHASAL, null, "AC", ".", WOH_USUARIO, 0, 0, TSNROTRA, null, 0);
                foreach (DataRow rw in (tbItems as DataTable).Rows)
                {
                    ObjM.InsertMovimiento(oSessionManager, WIH_CODEMP, MIBODEGA, BDBODEGA, WIH_FECHA, "99", Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]),
                                          Convert.ToString(rw["ARCLAVE4"]), ".", Convert.ToDouble(rw["WID_CANTIDAD"]), Convert.ToDouble(rw["WID_CANTIDAD"]), "UN", MIMOVT, MIOTMOVI, Convert.ToInt32(rw["WID_ITEM"]), "", null, null, null,
                                          null, null, 0, 0, "AC", "AC", ".", WIH_USUARIO, 0, 0);
                }

                //Movimiento de Entrada
                //MIOTMOVI = ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, MIOTMOVI, BDBODEGA, TSOTBODE, "98", 0, 0, 0, null, WOH_FECHASAL, null, "AC", ".", WOH_USUARIO, 0, 0, TSNROTRA, null, 0);
                foreach (DataRow rw in (tbItems as DataTable).Rows)
                {
                    ObjM.InsertMovimiento(oSessionManager, WIH_CODEMP, BDBODEGA, MIBODEGA, System.DateTime.Today, "98", Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]),
                                          Convert.ToString(rw["ARCLAVE4"]), ".", Convert.ToDouble(rw["WID_CANTIDAD"]), Convert.ToDouble(rw["WID_CANTIDAD"]), "UN", MIOTMOVI, MIMOVT, Convert.ToInt32(rw["WID_ITEM"]), "", null, null, null,
                                          null, null, 0, 0, "AC", "AC", ".", WIH_USUARIO, 0, 0);
                }

                //Inserta Traslado
                Obj.InsertTraslado(oSessionManager, WIH_CODEMP, TSNROTRA, WIH_FECHA, MIBODEGA, BDBODEGA, "99", "98", MIOTMOVI, MIMOVT, "Traslado de Mercacias WR IN", null, "AC", ".", WIH_USUARIO, null, null, null,0);
                #endregion

                //BL x WR IN
                foreach (DataRow row in (tbBL as DataTable).Rows)
                {
                    ln_codbl = ComunBL.GeneraConsecutivo(connection, "CNBL");

                    BillofLadingBD.InsertBLHD(oSessionManager, ln_codbl, WIH_CODEMP, Convert.ToDateTime(row["BLH_FECHA"]), Convert.ToInt32(row["BLH_CODEXPORTER"]), Convert.ToInt32(row["BLH_CODRECEPTOR"]), Convert.ToInt32(row["BLH_CODNOTIFY"]), Convert.ToString(row["BLH_MODTRANS"]),
                        Convert.ToString(row["BLH_CIUREC"]), Convert.ToString(row["BLH_NROVIAJE"]), Convert.ToString(row["BLH_PURORIGEN"]), Convert.ToString(row["BLH_PURDESTINO"]), Convert.ToString(row["BLH_CIUDESTI"]), Convert.ToString(row["BLH_BOOKINGNO"]),
                        Convert.ToString(row["BLH_NROBILLOFL"]), Convert.ToString(row["BLH_EXPORTREF"]), Convert.ToString(row["BLH_PTOPAISORI"]), Convert.ToString(row["BLH_TIPOENVIO"]), WIH_USUARIO);

                    foreach (DataRow rowdt in (tbBLDT as DataTable).Rows)
                        BillofLadingBD.InsertBLDT(oSessionManager, ln_codbl, Convert.ToString(rowdt["BLD_NROCONTAINER"]), Convert.ToDouble(rowdt["BLD_NROPACK"]), Convert.ToString(rowdt["BLD_DESCRIPTION"]), Convert.ToDouble(rowdt["BLD_GROSSWEIGHT"]), Convert.ToString(rowdt["BLD_GROSSUN"]),
                            Convert.ToDouble(rowdt["BLD_DIMESION"]), Convert.ToString(rowdt["BLD_DIMESIONUN"]));

                    BillofLadingBD.InsertBL_WRIN(oSessionManager, WIH_CONSECUTIVO, ln_codbl);
                }

                //Soportes
                foreach (DataRow rw in (tbSoportes as DataTable).Rows)
                {                    
                        Stream ioArchivo = File.OpenRead(Convert.ToString(rw["RUTA"]));
                        byte[] result;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ioArchivo.CopyTo(ms);
                            result = ms.ToArray();
                        }

                        SoportesBD.InsertSoporte(oSessionManager, Convert.ToString(rw["SP_TIPO"]), WIH_CONSECUTIVO, Convert.ToString(rw["SP_DESCRIPCION"]), Convert.ToString(rw["SP_EXTENCION"]), result, WIH_USUARIO, ".", ".");                 
                }


                oSessionManager.CommitTranstaction();
                return WIH_CONSECUTIVO;
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
        public int UpdatetWRIN(string connection, string WIH_CODEMP, int WIH_CONSECUTIVO, string WIH_OBSERVACIONES, string WIH_USUARIO, string WIH_ESTADO, string WIH_NROALT, string WIH_DESPACHO, object tbBL, object tbBLDT, object tbSoportes)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBL ObjM = new MovimientosBL();
            int ln_codbl = 0;
            
            try
            {
                oSessionManager.BeginTransaction();

                OrdenesComprasBD.UpdateWRIN(oSessionManager, WIH_CONSECUTIVO, WIH_OBSERVACIONES, WIH_USUARIO, WIH_ESTADO, WIH_NROALT, WIH_DESPACHO);
                //BL x WR IN
                foreach (DataRow row in (tbBL as DataTable).Rows)
                {
                    if (BillofLadingBD.ExisteBL_WRIN(oSessionManager,Convert.ToInt32(row["BLH_CODIGO"]), WIH_CONSECUTIVO) == 0)
                    {
                        ln_codbl = ComunBL.GeneraConsecutivo(connection, "CNBL");

                        BillofLadingBD.InsertBLHD(oSessionManager, ln_codbl, WIH_CODEMP, Convert.ToDateTime(row["BLH_FECHA"]), Convert.ToInt32(row["BLH_CODEXPORTER"]), Convert.ToInt32(row["BLH_CODRECEPTOR"]), Convert.ToInt32(row["BLH_CODNOTIFY"]), Convert.ToString(row["BLH_MODTRANS"]),
                            Convert.ToString(row["BLH_CIUREC"]), Convert.ToString(row["BLH_NROVIAJE"]), Convert.ToString(row["BLH_PURORIGEN"]), Convert.ToString(row["BLH_PURDESTINO"]), Convert.ToString(row["BLH_CIUDESTI"]), Convert.ToString(row["BLH_BOOKINGNO"]),
                            Convert.ToString(row["BLH_NROBILLOFL"]), Convert.ToString(row["BLH_EXPORTREF"]), Convert.ToString(row["BLH_PTOPAISORI"]), Convert.ToString(row["BLH_TIPOENVIO"]), WIH_USUARIO);

                        foreach (DataRow rowdt in (tbBLDT as DataTable).Rows)
                            BillofLadingBD.InsertBLDT(oSessionManager, ln_codbl, Convert.ToString(rowdt["BLD_NROCONTAINER"]), Convert.ToDouble(rowdt["BLD_NROPACK"]), Convert.ToString(rowdt["BLD_DESCRIPTION"]), Convert.ToDouble(rowdt["BLD_GROSSWEIGHT"]), Convert.ToString(rowdt["BLD_GROSSUN"]),
                                Convert.ToDouble(rowdt["BLD_DIMESION"]), Convert.ToString(rowdt["BLD_DIMESIONUN"]));

                        BillofLadingBD.InsertBL_WRIN(oSessionManager, WIH_CONSECUTIVO, ln_codbl);
                    }
                }

                //Soportes
                foreach (DataRow rw in (tbSoportes as DataTable).Rows)
                {
                    if (Convert.ToInt32(rw["SP_CONSECUTIVO"]) == 0)
                    {
                        Stream ioArchivo = File.OpenRead(Convert.ToString(rw["RUTA"]));
                        byte[] result;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ioArchivo.CopyTo(ms);
                            result = ms.ToArray();
                        }

                        SoportesBD.InsertSoporte(oSessionManager, Convert.ToString(rw["SP_TIPO"]), WIH_CONSECUTIVO, Convert.ToString(rw["SP_DESCRIPCION"]), Convert.ToString(rw["SP_EXTENCION"]), result, WIH_USUARIO, ".", ".");
                    }
                }


                oSessionManager.CommitTranstaction();
                return WIH_CONSECUTIVO;
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
            }
        }
        public int AnularWRIN(string connection, string WIH_CODEMP, int WIH_CONSECUTIVO, string WIH_USUARIO,object tbItems)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBL Obj = new MovimientosBL();
            try
            {
                oSessionManager.BeginTransaction();
                foreach (DataRow rw in ((DataTable)tbItems).Rows)
                {
                    Obj.AnularMovimiento(oSessionManager, WIH_CODEMP, Convert.ToInt32(rw["MBIDMOVI"]), Convert.ToInt32(rw["WID_ITEM"]), WIH_USUARIO);
                }
                OrdenesComprasBD.AnularWRIN(oSessionManager, WIH_CODEMP, WIH_CONSECUTIVO, WIH_USUARIO, "AN");
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
        #endregion
        //wr out
        #region
        public DataTable GetWROUT(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetWROUT(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetWROUTDT(string connection, int WOH_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetWROUTDT(oSessionManager, WOH_CONSECUTIVO);
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
        public DataTable GetTrasladosWROUT(string connection, int WOH_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetTrasladosWROUT(oSessionManager, WOH_CONSECUTIVO);
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
        public DataTable GetCompraDTWROUT(string connection, string FD_CODEMP, string inFilter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetCompraDTWROUT(oSessionManager, FD_CODEMP, inFilter);
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
        public int InsertWROUT(string connection, string WOH_CODEMP, DateTime WOH_FECHASAL, DateTime WOH_FECHAENT, string BDBODEGA, string BDBODEGA1, string BDBODEGA2, int TRCODTER,string WOH_OBSERVACIONES, string WOH_USUARIO, string WOH_ESTADO, 
            object tbItems, object tbBL, object tbBLDT,object tbSoportes, object tbSegregacion)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBL ObjM = new MovimientosBL();
            TrasladosBD Obj = new TrasladosBD();
            MovimientosBD ObjMD = new MovimientosBD();
            
            DataTable dt, dtL;
            int WOH_CONSECUTIVO = 0, BLH_CODIGO=0, i=0, MIOTMOVI = 0, MIIDMOVI = 0, TSNROTRA=0;
            string TSOTBODE = "", WIH_CONSECUTIVO = "";
            Boolean lb_lote = false;
            try
            {
                oSessionManager.BeginTransaction();
                
                WOH_CONSECUTIVO = ComunBL.GeneraConsecutivo(connection, "WROUT");
                //Descargue Normal
                #region
                //Insert WR OUT HD
                OrdenesComprasBD.InsertWROUT(oSessionManager, WOH_CONSECUTIVO, WOH_CODEMP, WOH_FECHASAL, WOH_FECHAENT, BDBODEGA, BDBODEGA1, BDBODEGA2, TRCODTER, WOH_OBSERVACIONES, WOH_USUARIO, WOH_ESTADO, 0);
                
                //Obtener Otra Bodega
                foreach (DataRow rw in (tbItems as DataTable).Rows)                
                    WIH_CONSECUTIVO += Convert.ToString(rw["WIH_CONSECUTIVO"])+",";                    
                                
                foreach (DataRow rz in OrdenesComprasBD.GetWRIN(oSessionManager, " WIH_CONSECUTIVO IN (" + WIH_CONSECUTIVO + "-1)", 0, 0).Rows)
                {
                    TSNROTRA = ComunBD.GeneraConsecutivo(oSessionManager, "NROTRA", WOH_CODEMP);
                    //TSOTBODE = Convert.ToString(rz["BDBODEGA"]);
                    TSOTBODE = BDBODEGA1;

                    //Genera Movimiento Salida
                    MIIDMOVI = ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, MIOTMOVI, TSOTBODE, BDBODEGA, "99", 0, 0, 0, null, WOH_FECHASAL, null, "AC", ".", WOH_USUARIO, 0, 0, TSNROTRA, null, 0, null, null, null);
                    //Movimiento de Entrada
                    MIOTMOVI = ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, MIOTMOVI, BDBODEGA, TSOTBODE, "98", 0, 0, 0, null, WOH_FECHASAL, null, "AC", ".", WOH_USUARIO, 0, 0, TSNROTRA, null, 0, null, null, null);

                    i = 0;
                    foreach (DataRow rw in (tbItems as DataTable).Rows)
                    {
                        if (Convert.ToInt32(rw["WIH_CONSECUTIVO"]) == Convert.ToInt32(rz["WIH_CONSECUTIVO"]))
                        {
                            i++;
                            rw["MBIDITEM"] = i;
                            rw["MIIDMOVI"] = MIIDMOVI;
                            rw["MIOTMOVI"] = MIOTMOVI;
                            rw["TSNROTRA"] = TSNROTRA;
                        }
                    }
                    //////GENERA TRASLADOS DE INVENTARIOS PARA DEJAR TRAZABILIDAD DE LA SALIDAS Y ENTRADAS DEL INVENTARIO EN TRANSITO
                    //Genera Traslado
                    #region               

                    //Genera Movimiento Salida                    
                    foreach (DataRow rw in (tbItems as DataTable).Rows)
                    {
                        if (Convert.ToInt32(rw["WIH_CONSECUTIVO"]) == Convert.ToInt32(rz["WIH_CONSECUTIVO"]))
                        {
                            ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, BDBODEGA, TSOTBODE, WOH_FECHASAL, "99", Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]),
                                                  Convert.ToString(rw["ARCLAVE4"]), ".", Convert.ToDouble(rw["WOD_CANTIDAD"]), Convert.ToDouble(rw["WOD_CANTIDAD"]), "UN", MIIDMOVI, MIOTMOVI, Convert.ToInt32(rw["MBIDITEM"]), "", null, null, null,
                                                  null, null, 0, 0, "AC", "AC", ".", WOH_USUARIO, 0, 0);
                        }
                    }

                    //Movimiento de Entrada                    
                    foreach (DataRow rw in (tbItems as DataTable).Rows)
                    {
                        if (Convert.ToInt32(rw["WIH_CONSECUTIVO"]) == Convert.ToInt32(rz["WIH_CONSECUTIVO"]))
                        {
                            ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, TSOTBODE, BDBODEGA, System.DateTime.Today, "98", Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]),
                                              Convert.ToString(rw["ARCLAVE4"]), ".", Convert.ToDouble(rw["WOD_CANTIDAD"]), Convert.ToDouble(rw["WOD_CANTIDAD"]), "UN", MIOTMOVI, MIIDMOVI, Convert.ToInt32(rw["MBIDITEM"]), "", null, null, null,
                                              null, null, 0, 0, "AC", "AC", ".", WOH_USUARIO, 0, 0);
                        }
                    }

                    //Inserta Traslado
                    Obj.InsertTraslado(oSessionManager, WOH_CODEMP, TSNROTRA, WOH_FECHASAL, BDBODEGA, TSOTBODE, "99", "98", MIOTMOVI, MIIDMOVI, "Traslado de Mercacias WR OUT", null, "AC", ".", WOH_USUARIO, null, null, null, 0);
                    #endregion

                    //Insert Traslado WR
                    OrdenesComprasBD.InsertTrasladosWROUT(oSessionManager, WOH_CONSECUTIVO, TSNROTRA);
                }

                //Insert WR OUT DT
                foreach (DataRow rw in (tbItems as DataTable).Rows)
                {
                    OrdenesComprasBD.InsertWROUTDT(oSessionManager, WOH_CONSECUTIVO, Convert.ToString(rw["ARCODEMP"]), Convert.ToInt32(rw["WID_ID"]), Convert.ToInt32(rw["WIH_CONSECUTIVO"]),
                                                  Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]),
                                                  Convert.ToString(rw["ARCLAVE4"]), Convert.ToDouble(rw["WOD_CANTIDAD"]), Convert.ToDouble(rw["WOD_PRECIO"]), Convert.ToDouble(rw["WOD_PRECIOVTA"]),
                                                  Convert.ToInt32(rw["TSNROTRA"]), Convert.ToInt32(rw["MIIDMOVI"]), Convert.ToInt32(rw["MIOTMOVI"]), Convert.ToInt32(rw["MBIDITEM"]), WOH_USUARIO);
                }

                #endregion
                
                //CERRAR MOVIMIENTO DE INVENTARIO --TRASLADOS
                //Se Bloquea Temporalmente para efectos 
                #region
                /*
                // Movimiento Entrada
                //dt = ObjMD.CargarMovimiento(oSessionManager, WOH_CODEMP, MIOTMOVI);
                //foreach (DataRow rw in dt.Rows)
                //{
                //    //Bodegas
                //    using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, WOH_CODEMP, BDBODEGA, Convert.ToString(rw["MBTIPPRO"])))
                //    {
                //        while (reader.Read())
                //        {
                //            if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                //                lb_lote = true;
                //        }
                //    }

                //    if (lb_lote)
                //    {
                //        dtL = ObjM.CargarMovimientoLot(null, WOH_CODEMP, MIOTMOVI, Convert.ToInt32(rw["MBIDITEM"]));
                //        foreach (DataRow rl in dtL.Rows)
                //            ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, BDBODEGA, TSOTBODE, System.DateTime.Today, "98", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]),
                //                Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(rl["MLCANTID"]), Convert.ToDouble(rl["MLCANTID"]), "UN", MIIDMOVI, MIIDMOVI, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToString(rl["MLCDLOTE"]), "", "", "", "", "", 0, 0, "AC", "CE", ".", WOH_USUARIO, 0);
                //    }
                //    else
                //    {
                //        ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, BDBODEGA, TSOTBODE, System.DateTime.Today, "98", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), 
                //            Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(rw["MBCANTID"]), Convert.ToDouble(rw["MBCANTID"]), "UN", MIOTMOVI, MIIDMOVI, Convert.ToInt32(rw["MBIDITEM"]), "", "", "", "", "", "", 0, 0, "AC", "CE", ".", WOH_USUARIO, 0);
                //    }
                //}
                ////Movimiento Salida                
                //dt = ObjMD.CargarMovimiento(oSessionManager, WOH_CODEMP, MIIDMOVI);
                //foreach (DataRow rw in dt.Rows)
                //{
                //    //Bodegas
                //    using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, WOH_CODEMP, BDBODEGA, Convert.ToString(rw["MBTIPPRO"])))
                //    {
                //        while (reader.Read())
                //        {
                //            if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                //                lb_lote = true;
                //        }
                //    }

                //    if (lb_lote)
                //    {
                //        dtL = ObjMD.CargarMovimientoLot(oSessionManager, WOH_CODEMP, MIIDMOVI, Convert.ToInt32(rw["MBIDITEM"]));
                //        foreach (DataRow rl in dtL.Rows)
                //            ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, TSOTBODE, BDBODEGA, System.DateTime.Today, "99", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), 
                //                Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(rl["MLCANTID"]), Convert.ToDouble(rl["MLCANTID"]), "UN", MIIDMOVI, MIIDMOVI, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToString(rl["MLCDLOTE"]), "", "", "", "", "", 0, 0, "AC", "CE", ".", WOH_USUARIO, 0);
                //    }
                //    else
                //    {
                //        ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, TSOTBODE, BDBODEGA, System.DateTime.Today, "99", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), 
                //            Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(rw["MBCANTID"]), Convert.ToDouble(rw["MBCANTID"]), "UN", MIIDMOVI, MIOTMOVI, Convert.ToInt32(rw["MBIDITEM"]), "", "", "", "", "", "", 0, 0, "AC", "CE", ".", WOH_USUARIO, 0);
                //    }
                //}

                ////Cierra Traslado
                //Obj.UpdateTraslado(oSessionManager, WOH_CODEMP, TSNROTRA, WOH_FECHASAL, TSOTBODE, BDBODEGA, "99", "98", MIOTMOVI, MIIDMOVI, "Traslado de Mercacias WR", ".", "CE", null, WOH_USUARIO);
                */
                #endregion
                
                //Inserta BL para WR OUT
                foreach (DataRow rw in ((DataTable)tbBL).Rows)
                {                    
                        BLH_CODIGO = ComunBL.GeneraConsecutivo(connection, "CNBL");

                        BillofLadingBD.InsertBLHD(oSessionManager, BLH_CODIGO, WOH_CODEMP, Convert.ToDateTime(rw["BHL_FECHA"]), Convert.ToInt32(rw["BLH_CODEXPORTER"]), Convert.ToInt32(rw["BLH_CODRECEPTOR"]), Convert.ToInt32(rw["BLH_CODNOTIFY"]), Convert.ToString(rw["BLH_MODTRANS"]), Convert.ToString(rw["BLH_CIUREC"]), Convert.ToString(rw["BLH_NROVIAJE"]),
                            Convert.ToString(rw["BLH_PURORIGEN"]), Convert.ToString(rw["BLH_PURDESTINO"]), Convert.ToString(rw["BLH_CIUDESTI"]), Convert.ToString(rw["BLH_BOOKINGNO"]), Convert.ToString(rw["BLH_NROBILLOFL"]), Convert.ToString(rw["BLH_EXPORTREF"]), Convert.ToString(rw["BLH_PTOPAISORI"]),
                            Convert.ToString(rw["BLH_TIPOENVIO"]), WOH_USUARIO);
                        foreach (DataRow rx in (tbBLDT as DataTable).Rows)
                        {
                            BillofLadingBD.InsertBLDT(oSessionManager, BLH_CODIGO, Convert.ToString(rx["BLD_NROCONTAINER"]), Convert.ToDouble(rx["BLD_NROPACK"]), Convert.ToString(rx["BLD_DESCRIPTION"]), Convert.ToDouble(rx["BLD_GROSSWEIGHT"]), Convert.ToString(rx["BLD_GROSSUN"]),
                                Convert.ToDouble(rx["BLD_DIMESION"]), Convert.ToString(rx["BLD_DIMESIONUN"]));
                        }

                        BillofLadingBD.InsertBL_WROUT(oSessionManager, WOH_CONSECUTIVO, BLH_CODIGO);                    
                }
                //Inserta Soportes
                foreach (DataRow rw in ((DataTable)tbSoportes).Rows)
                {                    
                        Stream ioArchivo = File.OpenRead(Convert.ToString(rw["RUTA"]));
                        byte[] result;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ioArchivo.CopyTo(ms);
                            result = ms.ToArray();
                        }

                        SoportesBD.InsertSoporte(oSessionManager, Convert.ToString(rw["SP_TIPO"]), WOH_CONSECUTIVO, Convert.ToString(rw["SP_DESCRIPCION"]), Convert.ToString(rw["SP_EXTENCION"]), result, WOH_USUARIO, ".", ".");
                    
                }
                //Inserta Segregaciones
                foreach (DataRow rw in ((DataTable)tbSegregacion).Rows)
                    SegregacionBD.InsertSegregacionxWROUT(oSessionManager, WOH_CONSECUTIVO, Convert.ToInt32(rw["SGH_CODIGO"]));
                
                // Cuando se Carga desde Segregacion se Mueven los saldos Restantes a los almacenes 
                if (((DataTable)tbSegregacion).Rows.Count > 0)
                {
                    WOH_CONSECUTIVO = ComunBL.GeneraConsecutivo(connection, "WROUT");
                    //Insert WR OUT HD
                    //OrdenesComprasBD.InsertWROUT(oSessionManager, WOH_CONSECUTIVO, WOH_CODEMP, WOH_FECHASAL, WOH_FECHAENT, BDBODEGA, TRCODTER, WOH_OBSERVACIONES, WOH_USUARIO, WOH_ESTADO, 0);
                    OrdenesComprasBD.InsertWROUT(oSessionManager, WOH_CONSECUTIVO, WOH_CODEMP, WOH_FECHASAL, WOH_FECHAENT, BDBODEGA, BDBODEGA1, BDBODEGA2, TRCODTER, WOH_OBSERVACIONES, WOH_USUARIO, WOH_ESTADO, 0);

                    WIH_CONSECUTIVO = "";
                    //Obtener Otra Bodega
                    foreach (DataRow rw in (tbItems as DataTable).Rows)
                        WIH_CONSECUTIVO += Convert.ToString(rw["WIH_CONSECUTIVO"]) + ",";

                    foreach (DataRow rz in OrdenesComprasBD.GetWRIN(oSessionManager, " WIH_CONSECUTIVO IN (" + WIH_CONSECUTIVO + "-1)", 0, 0).Rows)
                    {
                        TSNROTRA = ComunBD.GeneraConsecutivo(oSessionManager, "NROTRA", WOH_CODEMP);
                        TSOTBODE = BDBODEGA2;
                        /*foreach (DataRow rsg in ((DataTable)tbSegregacion).Rows)
                        {
                            if (Convert.ToInt32(rsg["WIH_CONSECUTIVO"]) == Convert.ToInt32(rz["WIH_CONSECUTIVO"]))
                            {
                                TSOTBODE = Convert.ToString(rsg["TSOTBODE"]);
                                BDBODEGA = Convert.ToString(rsg["TSBODEGA"]);
                                break;
                            }
                        }*/
                        //BDBODEGA = Convert.ToString(rz["BDBODEGA"]);
                        //TSOTBODE = Convert.ToString(rz["BDBODEGA"]);

                        //Genera Movimiento Salida                        
                        MIIDMOVI = ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, MIOTMOVI, BDBODEGA, TSOTBODE, "99", 0, 0, 0, null, WOH_FECHASAL, null, "AC", ".", WOH_USUARIO, 0, 0, TSNROTRA, null, 0, null, null, null);
                        //Movimiento de Entrada                        
                        MIOTMOVI = ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, MIOTMOVI, TSOTBODE, BDBODEGA , "98", 0, 0, 0, null, WOH_FECHASAL, null, "AC", ".", WOH_USUARIO, 0, 0, TSNROTRA, null, 0, null, null, null);

                        i = 0;
                        foreach (DataRow rw in (tbItems as DataTable).Rows)
                        {
                            if (Convert.ToInt32(rw["WIH_CONSECUTIVO"]) == Convert.ToInt32(rz["WIH_CONSECUTIVO"]))
                            {
                                i++;
                                rw["MBIDITEM"] = i;
                                rw["MIIDMOVI"] = MIIDMOVI;
                                rw["MIOTMOVI"] = MIOTMOVI;
                                rw["TSNROTRA"] = TSNROTRA;
                            }
                        }
                        //////GENERA TRASLADOS DE INVENTARIOS PARA DEJAR TRAZABILIDAD DE LA SALIDAS Y ENTRADAS DEL INVENTARIO EN TRANSITO
                        //Genera Nro Traslado
                        #region               

                        //Genera Movimiento Salida                        
                        foreach (DataRow rw in (tbItems as DataTable).Rows)
                        {
                            if (Convert.ToInt32(rw["WIH_CONSECUTIVO"]) == Convert.ToInt32(rz["WIH_CONSECUTIVO"]))
                            {
                                ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, BDBODEGA, TSOTBODE, WOH_FECHASAL, "99", Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]),                                                      
                                                      Convert.ToString(rw["ARCLAVE4"]), ".", Convert.ToDouble(rw["CAN_DIF"]), Convert.ToDouble(rw["CAN_DIF"]), "UN", MIIDMOVI, MIOTMOVI, Convert.ToInt32(rw["MBIDITEM"]), "", null, null, null,
                                                      null, null, 0, 0, "AC", "AC", ".", WOH_USUARIO, 0, 0);
                            }
                        }

                        //Movimiento de Entrada                        
                        foreach (DataRow rw in (tbItems as DataTable).Rows)
                        {
                            if (Convert.ToInt32(rw["WIH_CONSECUTIVO"]) == Convert.ToInt32(rz["WIH_CONSECUTIVO"]))
                            {                                
                                ObjM.InsertMovimiento(oSessionManager, WOH_CODEMP, TSOTBODE, BDBODEGA, System.DateTime.Today, "98", Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]),                                                  
                                                  Convert.ToString(rw["ARCLAVE4"]), ".", Convert.ToDouble(rw["CAN_DIF"]), Convert.ToDouble(rw["CAN_DIF"]), "UN", MIOTMOVI, MIIDMOVI, Convert.ToInt32(rw["MBIDITEM"]), "", null, null, null,
                                                  null, null, 0, 0, "AC", "AC", ".", WOH_USUARIO, 0, 0);
                            }
                        }

                        //Inserta Traslado
                        Obj.InsertTraslado(oSessionManager, WOH_CODEMP, TSNROTRA, WOH_FECHASAL, BDBODEGA, TSOTBODE, "99", "98", MIOTMOVI, MIIDMOVI, "Traslado de Mercacias WR OUT Saldos", null, "AC", ".", WOH_USUARIO, null, null, null, 0);
                        #endregion
                        //Insert Traslado WR
                        OrdenesComprasBD.InsertTrasladosWROUT(oSessionManager, WOH_CONSECUTIVO, TSNROTRA);
                    }
                    //Insert WR OUT DT
                    foreach (DataRow rw in (tbItems as DataTable).Rows)
                    {
                        OrdenesComprasBD.InsertWROUTDT(oSessionManager, WOH_CONSECUTIVO, Convert.ToString(rw["ARCODEMP"]), Convert.ToInt32(rw["WID_ID"]), Convert.ToInt32(rw["WIH_CONSECUTIVO"]),
                                                      Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]),
                                                      Convert.ToString(rw["ARCLAVE4"]), Convert.ToDouble(rw["CAN_DIF"]), Convert.ToDouble(rw["WOD_PRECIO"]), Convert.ToDouble(rw["WOD_PRECIOVTA"]),
                                                      Convert.ToInt32(rw["TSNROTRA"]), Convert.ToInt32(rw["MIIDMOVI"]), Convert.ToInt32(rw["MIOTMOVI"]), Convert.ToInt32(rw["MBIDITEM"]), WOH_USUARIO);
                    }

                    //Inserta BL para WR OUT
                    foreach (DataRow rw in ((DataTable)tbBL).Rows)
                    {
                        BLH_CODIGO = ComunBL.GeneraConsecutivo(connection, "BLCOD");

                        BillofLadingBD.InsertBLHD(oSessionManager, BLH_CODIGO, WOH_CODEMP, Convert.ToDateTime(rw["BHL_FECHA"]), Convert.ToInt32(rw["BLH_CODEXPORTER"]), Convert.ToInt32(rw["BLH_CODRECEPTOR"]), Convert.ToInt32(rw["BLH_CODNOTIFY"]), Convert.ToString(rw["BLH_MODTRANS"]), Convert.ToString(rw["BLH_CIUREC"]), Convert.ToString(rw["BLH_NROVIAJE"]),
                            Convert.ToString(rw["BLH_PURORIGEN"]), Convert.ToString(rw["BLH_PURDESTINO"]), Convert.ToString(rw["BLH_CIUDESTI"]), Convert.ToString(rw["BLH_BOOKINGNO"]), Convert.ToString(rw["BLH_NROBILLOFL"]), Convert.ToString(rw["BLH_EXPORTREF"]), Convert.ToString(rw["BLH_PTOPAISORI"]),
                            Convert.ToString(rw["BLH_TIPOENVIO"]), WOH_USUARIO);
                        foreach (DataRow rx in (tbBLDT as DataTable).Rows)
                        {
                            BillofLadingBD.InsertBLDT(oSessionManager, BLH_CODIGO, Convert.ToString(rx["BLD_NROCONTAINER"]), Convert.ToDouble(rx["BLD_NROPACK"]), Convert.ToString(rx["BLD_DESCRIPTION"]), Convert.ToDouble(rx["BLD_GROSSWEIGHT"]), Convert.ToString(rx["BLD_GROSSUN"]),
                                Convert.ToDouble(rx["BLD_DIMESION"]), Convert.ToString(rx["BLD_DIMESIONUN"]));
                        }

                        BillofLadingBD.InsertBL_WROUT(oSessionManager, WOH_CONSECUTIVO, BLH_CODIGO);
                    }
                    //Inserta Soportes
                    foreach (DataRow rw in ((DataTable)tbSoportes).Rows)
                    {
                        Stream ioArchivo = File.OpenRead(Convert.ToString(rw["RUTA"]));
                        byte[] result;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ioArchivo.CopyTo(ms);
                            result = ms.ToArray();
                        }

                        SoportesBD.InsertSoporte(oSessionManager, Convert.ToString(rw["SP_TIPO"]), WOH_CONSECUTIVO, Convert.ToString(rw["SP_DESCRIPCION"]), Convert.ToString(rw["SP_EXTENCION"]), result, WOH_USUARIO, ".", ".");

                    }
                    //Inserta Segregaciones
                    foreach (DataRow rw in ((DataTable)tbSegregacion).Rows)
                        SegregacionBD.InsertSegregacionxWROUT(oSessionManager, WOH_CONSECUTIVO, Convert.ToInt32(rw["SGH_CODIGO"]));
                }

                //oSessionManager.RollBackTransaction();
                oSessionManager.CommitTranstaction();
                return WOH_CONSECUTIVO;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                ObjMD = null;
                ObjM = null;
                Obj = null;
                dt = null;

            }
        }
        public int UpdateWROUT(string connection, string WOH_CODEMP, int WOH_CONSECUTIVO, DateTime WOH_FECHASAL, DateTime WOH_FECHAENT, string BDBODEGA, int TRCODTER, string WOH_OBSERVACIONES, string WOH_USUARIO, string WOH_ESTADO, object tbBL, object tbBLDT, object tbSoportes)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBL ObjM = new MovimientosBL();
            TrasladosBD Obj = new TrasladosBD();
            MovimientosBD ObjMD = new MovimientosBD();            
            int BLH_CODIGO = 0;            
            try
            {
                oSessionManager.BeginTransaction();                         

                //Inserta BL para WR OUT
                foreach (DataRow rw in (tbBL as DataTable).Rows)
                {
                    if (BillofLadingBD.ExisteBL_WRIN(oSessionManager, Convert.ToInt32(rw["BLH_CODIGO"]), WOH_CONSECUTIVO) == 0)
                    {
                        BLH_CODIGO = ComunBL.GeneraConsecutivo(connection, "BLCOD");

                        BillofLadingBD.InsertBLHD(oSessionManager, BLH_CODIGO, WOH_CODEMP, Convert.ToDateTime(rw["BLH_FECHA"]), Convert.ToInt32(rw["BLH_CODEXPORTER"]), Convert.ToInt32(rw["BLH_CODRECEPTOR"]), Convert.ToInt32(rw["BLH_CODNOTIFY"]), Convert.ToString(rw["BLH_MODTRANS"]), Convert.ToString(rw["BLH_CIUREC"]), Convert.ToString(rw["BLH_NROVIAJE"]),
                            Convert.ToString(rw["BLH_PURORIGEN"]), Convert.ToString(rw["BLH_PURDESTINO"]), Convert.ToString(rw["BLH_CIUDESTI"]), Convert.ToString(rw["BLH_BOOKINGNO"]), Convert.ToString(rw["BLH_NROBILLOFL"]), Convert.ToString(rw["BLH_EXPORTREF"]), Convert.ToString(rw["BLH_PTOPAISORI"]),
                            Convert.ToString(rw["BLH_TIPOENVIO"]), WOH_USUARIO);
                        foreach (DataRow rx in (tbBLDT as DataTable).Rows)
                        {
                            BillofLadingBD.InsertBLDT(oSessionManager, BLH_CODIGO, Convert.ToString(rx["BLD_NROCONTAINER"]), Convert.ToDouble(rx["BLD_NROPACK"]), Convert.ToString(rx["BLD_DESCRIPTION"]), Convert.ToDouble(rx["BLD_GROSSWEIGHT"]), Convert.ToString(rx["BLD_GROSSUN"]),
                                Convert.ToDouble(rx["BLD_DIMESION"]), Convert.ToString(rx["BLD_DIMESIONUN"]));
                        }

                        BillofLadingBD.InsertBL_WROUT(oSessionManager, WOH_CONSECUTIVO, BLH_CODIGO);
                    }
                }

                foreach (DataRow rw in (tbSoportes as DataTable).Rows)
                {
                    if (Convert.ToInt32(rw["SP_CONSECUTIVO"]) == 0)
                    {
                        Stream ioArchivo = File.OpenRead(Convert.ToString(rw["RUTA"]));
                        byte[] result;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ioArchivo.CopyTo(ms);
                            result = ms.ToArray();
                        }

                        SoportesBD.InsertSoporte(oSessionManager, Convert.ToString(rw["SP_TIPO"]), WOH_CONSECUTIVO, Convert.ToString(rw["SP_DESCRIPCION"]), Convert.ToString(rw["SP_EXTENCION"]), result, WOH_USUARIO, ".", ".");
                    }
                }

                oSessionManager.CommitTranstaction();
                return WOH_CONSECUTIVO;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                ObjMD = null;
                ObjM = null;
                Obj = null;               
            }
        }
        public string InsertFactura(string connection,string WOH_CODEMP,string HDTIPFAC,int TRCODTER,string WOH_USUARIO,int WOH_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TipoFacturaBD Obj = new TipoFacturaBD();
            int ln_consecutivo = 0;
            string lc_resolucion = "";
            double ln_subtot = 0, ln_tot = 0;
            DataTable dt;
            try
            {
                oSessionManager.BeginTransaction();
                Obj.UpdateTipoFactura(oSessionManager, WOH_CODEMP, HDTIPFAC);
                ln_consecutivo = Obj.GetUltimoNroFac(oSessionManager, WOH_CODEMP, HDTIPFAC);
                lc_resolucion = Obj.GetNumeroResolucion(oSessionManager, WOH_CODEMP, HDTIPFAC);

                FacturacionBD.InsertFacturaHD(oSessionManager, WOH_CODEMP, HDTIPFAC, ln_consecutivo, System.DateTime.Today, TRCODTER, 0, null,
                                              ln_subtot, 0, 0, ln_tot, "USD", 0, 0, 0, 0, 0, 0, 0,
                                              0, null, null, null, null, null, null, 0, lc_resolucion,
                                              null, null, null, null, null, 0, 0,
                                              0, null, null, 0, 0, 0, "CE",
                                              ".", WOH_USUARIO, "S", null, null, 0, null,
                                              null, null, null, null, null);

                dt = OrdenesComprasBD.GetWROUTDT(oSessionManager, WOH_CONSECUTIVO);
                foreach (DataRow rw in dt.Rows)
                {
                    FacturacionBD.InsertFacturaDT(oSessionManager, WOH_CODEMP, HDTIPFAC, ln_consecutivo, Convert.ToInt32(rw["MBIDITEM"]), null, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToString(rw["ARTIPPRO"]),
                                                      Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]), Convert.ToString(rw["ARCLAVE4"]), ".", "UN", Convert.ToDouble(rw["WOD_CANTIDAD"]), Convert.ToDouble(rw["WOD_CANTIDAD"]), 0,
                                                      null, null, Convert.ToDouble(rw["WOD_PRECIOVTA"]), Convert.ToDouble(rw["WOD_PRECIOVTA"]), 0, (Convert.ToDouble(rw["WOD_CANTIDAD"]) * Convert.ToDouble(rw["WOD_PRECIOVTA"])), 0, 0, (Convert.ToDouble(rw["WOD_CANTIDAD"]) * Convert.ToDouble(rw["WOD_PRECIOVTA"])),
                                                      0, 0, 0, 0, 0, 0, 0, 0, Convert.ToInt32(rw["MIIDMOVI"]),
                                                      Convert.ToInt32(rw["MBIDITEM"]), "CE", ".", WOH_USUARIO, 0, null, null, 0, null);
                }

                OrdenesComprasBD.UpdateWROUT(oSessionManager, WOH_CONSECUTIVO, HDTIPFAC, ln_consecutivo);

                oSessionManager.CommitTranstaction();
                return HDTIPFAC + "-" +Convert.ToString(ln_consecutivo);
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
        #endregion
        //Pagos
        #region
        public DataTable GetConsultaPagos(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetConsultaPagos(oSessionManager, filter);
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
        public DataTable GetDetalleFacturas(string connection, string filter, string FD_CODEMP, int FD_NROCMP, string FD_NROFACTURA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetDetalleFacturas(oSessionManager, filter, FD_CODEMP, FD_NROCMP, FD_NROFACTURA);
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
        public DataTable GetPagosHD(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetPagosHD(oSessionManager, filter);
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
        public DataTable GetPagosDT(string connection, string HP_CODEMP, int HP_NROPAGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetPagosDT(oSessionManager, HP_CODEMP, HP_NROPAGO);
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
        public DataTable GetFacturasxPago(string connection, string CH_CODEMP, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return OrdenesComprasBD.GetFacturasxPago(oSessionManager, CH_CODEMP, TRCODTER);
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
        public int InsertPago(string connection, string HP_CODEMP,DateTime HP_FECHA,int TRCODTER, int HP_NRORECFISICO,string HP_OBSERVACIONES,string inUsuario,object tbDetalle)
        {
            int ln_codigo = 0;
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                oSessionManager.BeginTransaction();
                ln_codigo = ComunBL.GeneraConsecutivo(connection, "CNPAGOS");
                OrdenesComprasBD.InsertPagosHD(oSessionManager, HP_CODEMP, ln_codigo, HP_FECHA, TRCODTER, HP_NRORECFISICO, HP_OBSERVACIONES, "AC", 0);
                foreach (DataRow rw in (tbDetalle as DataTable).Rows)
                {
                    OrdenesComprasBD.InsertPagosDT(oSessionManager, HP_CODEMP, ln_codigo, Convert.ToInt32(rw["CH_NROCMP"]), Convert.ToString(rw["FD_NROFACTURA"]), Convert.ToString(rw["DP_CONCEPTO"]), Convert.ToDouble(rw["DP_VALOR"]), inUsuario, "AC", HP_FECHA);
                }
                oSessionManager.CommitTranstaction();
                return ln_codigo;                
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally {
                oSessionManager = null;
            }
        }
        #endregion

    }
}
