using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XUSS.DAL.Pedidos;
using DataAccess;
using System.Data;
using System.ComponentModel;
using System.Web;
using XUSS.BLL.Comun;
using XUSS.DAL.Comun;
using XUSS.BLL.Inventarios;
using System.IO;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Pedidos
{
    [DataObject(true)]
    public class PedidosBL
    {        
        public DataTable GetTipPro(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return PedidosBD.GetTipPro(oSessionManager, filter, startRowIndex, maximumRows);
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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetPedidos(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return PedidosBD.GetPedidos(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetPedidosEmpaques(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return PedidosBD.GetPedidosEmpaques(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetPedidoDT(string connection, string PDCODEMP ,int PDPEDIDO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PedidosBD.GetPedidoDT(oSessionManager, PDCODEMP, PDPEDIDO);
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
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int InsertPedidoHD(string connection, string PHCODEMP, DateTime PHFECPED, int PHCODCLI, int? PHAGENTE,int PHCODSUC, string PHTIPPED, string PHIDIOMA, string PHMONEDA, double PHTRMLOC, string PHBODEGA, string PHLISPRE, string PHNMUSER, string PHOBSERV, DateTime PHFECLIQ,
            string PHESTADO, object DetPedidos)
        {
            SessionManager oSessionManager = new SessionManager(null);
            int ln_consecutivo = 0;            
            int ln_acu = 0;
            DataTable dt = new DataTable();

            try
            {
                oSessionManager.BeginTransaction();
                dt = (DetPedidos as DataTable);
                ln_consecutivo = ComunBD.GeneraConsecutivo(oSessionManager, "PEDIDO", PHCODEMP);
                PedidosBD.InsertPedidoHD(oSessionManager, PHCODEMP, ln_consecutivo, PHCODCLI, PHCODSUC,PHAGENTE,PHTIPPED, PHBODEGA, PHIDIOMA, PHMONEDA, PHTRMLOC, 0
                                                , 0, "0", 0, 0, 0, 0, 0, PHESTADO, ".", PHNMUSER,PHLISPRE,
                                                PHFECPED.Month, PHFECPED.Year, PHFECPED, PHOBSERV, PHFECLIQ);
                foreach (DataRow row in dt.Rows)
                {
                    ln_acu++;
                    PedidosBD.InsertPedidoDT( oSessionManager, PHCODEMP, ln_consecutivo, ln_acu, PHTIPPED, PHCODCLI, Convert.ToString(row["PDBODEGA"]),
                                             Convert.ToString(row["PDTIPPRO"]), Convert.ToString(row["PDCLAVE1"]), Convert.ToString(row["PDCLAVE2"]), Convert.ToString(row["PDCLAVE3"]),
                                             Convert.ToString(row["PDCLAVE4"]), null, null, Convert.ToString(row["PDUNDPED"]),
                                             Convert.ToDouble(row["PDCANPED"]), Convert.ToDouble(row["PDCANTID"]), 0, Convert.ToString(row["PDLISPRE"]), null, 0, 0, null,
                                             null, null, null, null, null, null, null, null,
                                             0, null, 0, null, 0, 0, 0, null,
                                             null, null, null, null, null, null, null, null, null,
                                             null, null, null, "AC", ".", PHNMUSER, null,
                                             null, null, null, null, null, null, null, null, null, 0,0);
                }
                oSessionManager.CommitTranstaction();
                return ln_consecutivo;
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
        public int UpdatePedidoHD(string connection, string PHCODEMP, DateTime PHFECPED, int PHCODCLI, int PHCODSUC, int? PHAGENTE,string PHTIPPED, string PHIDIOMA, string PHMONEDA, double PHTRMLOC,string PHBODEGA, string PHLISPRE, string PHNMUSER, string PHOBSERV, DateTime PHFECLIQ,
            string PHESTADO, object DetPedidos, int original_PHPEDIDO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            int ln_consecutivo = 0;
            int ln_codter = 0;
            int ln_acu = 0;
            DataTable dt = new DataTable();
            DataTable tbMonedas = new DataTable();
            DataTable tbTasa = new DataTable();
            string moneda = "";

            try
            {
                oSessionManager.BeginTransaction();
                dt = (DetPedidos as DataTable);
                moneda = ComunBL.GetMoneda(null, PHCODEMP);
                tbMonedas = ComunBD.GetTbTablaLista(oSessionManager, PHCODEMP, "MONE");
                tbTasa = TasaCambioBD.GetTasas(oSessionManager, PHFECLIQ);
                ln_consecutivo = original_PHPEDIDO;
                
                PedidosBD.UpdatePedidoHD(oSessionManager,
                                                  PHCODEMP, ln_consecutivo, PHCODCLI, PHCODSUC, PHAGENTE,PHTIPPED, PHIDIOMA, PHMONEDA, PHTRMLOC, 0
                                                , 0, "0", 0, 0, 0, 0, 0, PHESTADO, ".", PHNMUSER, PHLISPRE, PHOBSERV, PHFECLIQ);

                PedidosBD.BorrarItemsPedidoDTMoneda(oSessionManager, PHCODEMP, ln_consecutivo);
                PedidosBD.BorrarItemsPedido(oSessionManager, PHCODEMP, ln_consecutivo);              

                foreach (DataRow row in dt.Rows)
                {
                    ln_acu++;
                    PedidosBD.InsertPedidoDT(oSessionManager, PHCODEMP, ln_consecutivo, Convert.ToInt32(row["PDLINNUM"]), PHTIPPED, PHCODCLI, Convert.ToString(row["PDBODEGA"]),
                                             Convert.ToString(row["PDTIPPRO"]), Convert.ToString(row["PDCLAVE1"]), Convert.ToString(row["PDCLAVE2"]), Convert.ToString(row["PDCLAVE3"]),
                                             Convert.ToString(row["PDCLAVE4"]), null, null, Convert.ToString(row["PDUNDPED"]),
                                             Convert.ToDouble(row["PDCANPED"]), Convert.ToDouble(row["PDCANTID"]), row.IsNull("PDPRECIO") ? null : (double?)Convert.ToDouble(row["PDPRECIO"]), Convert.ToString(row["PDLISPRE"]), null, row.IsNull("PDPRELIS") ? null : (double?)Convert.ToDouble(row["PDPRELIS"]), row.IsNull("PDDESCUE") ? null : (double?)Convert.ToDouble(row["PDDESCUE"]), null,
                                             null, null, null, null, null, null, null, null,
                                             0, null, 0, null, 0, 0, 0, null,
                                             null, null, null, null, null, null, null, null, null,
                                             null, null, null, "AC", ".", PHNMUSER, null,
                                             null, null, null, null, null, null, null, null, null, row.IsNull("PDCODDES") ? null : (Int32?)Convert.ToInt32(row["PDCODDES"]), row.IsNull("PDSUBTOT")? null: (double?)Convert.ToDouble(row["PDSUBTOT"]));
                    //Multimoneda
                    foreach (DataRow rz in tbMonedas.Rows)
                    {
                        if (moneda == Convert.ToString(rz["TTCODCLA"]))
                            PedidosBD.InsertPedidoDTMoneda(oSessionManager, PHCODEMP, ln_consecutivo, Convert.ToInt32(row["PDLINNUM"]), moneda, 0, Convert.ToDouble(row["PDPRECIO"]), Convert.ToDouble(row["PDPRELIS"]), Convert.ToDouble(row["PDSUBTOT"]), PHNMUSER);                        

                        foreach (DataRow rt in tbTasa.Rows)
                        {
                            if (Convert.ToString(rt["TC_MONEDA"]) == Convert.ToString(rz["TTCODCLA"]))
                            {                                
                                PedidosBD.InsertPedidoDTMoneda(oSessionManager, PHCODEMP, ln_consecutivo, Convert.ToInt32(row["PDLINNUM"]), Convert.ToString(rz["TTCODCLA"]), Convert.ToDouble(rt["TC_VALOR"]), Convert.ToDouble(row[Convert.ToString(rz["TTCODCLA"]) + "_PDPRECIO"]), Convert.ToDouble(row[Convert.ToString(rz["TTCODCLA"]) + "_PDPRELIS"]), Convert.ToDouble(row[Convert.ToString(rz["TTCODCLA"]) + "_PDSUBTOT"]), PHNMUSER);
                            }
                        }
                    }
                }
                oSessionManager.CommitTranstaction();
                return ln_consecutivo;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                tbTasa = null;
                tbMonedas = null;
                dt = null;
            }
        }
        public int TieneListaEmpaque(string connection, string PHCODEMP, int PHPEDIDO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PedidosBD.TieneListaEmpaque(oSessionManager, PHCODEMP, PHPEDIDO);
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
        public int AnluarPedido(string connection, string PHCODEMP, int PHPEDIDO,string PHNMUSER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                oSessionManager.BeginTransaction();
                PedidosBD.AnularPedidoHD(oSessionManager, PHCODEMP, PHPEDIDO, PHNMUSER);
                PedidosBD.AnularPedidoDT(oSessionManager, PHCODEMP, PHNMUSER, PHPEDIDO);                
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
        public IDataReader GetPedidoPlano(string connection, string PHCODEMP, int PHPEDIDO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return PedidosBD.GetPedidoPlano(oSessionManager, PHCODEMP, PHPEDIDO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                oSessionManager = null;
            }
        }

        public int InsertPedidoEmpaqueHD(string connection, string PHCODEMP, DateTime PHFECPED, int PHCODCLI, int? PHAGENTE, int PHCODSUC, string PHTIPPED, string PHIDIOMA, string PHMONEDA, double PHTRMLOC,string PHBODEGA, string PHLISPRE, 
                                         string PHNMUSER, string PHOBSERV, string PHESTADO, object DetPedidos,object tbAnexos)
        {
            SessionManager oSessionManager = new SessionManager(null);
            LtaEmpaqueBD Obj = new LtaEmpaqueBD();
            MovimientosBL Objm = new MovimientosBL();
            int ln_consecutivo, LH_LSTPAQ = 0, ln_acu = 0, ln_nromovimiento = 0, ln_lote=0;            
            DataTable dt = new DataTable();

            try
            {
                oSessionManager.BeginTransaction();
                dt = (DetPedidos as DataTable);
                //Pedido
                ln_consecutivo = ComunBD.GeneraConsecutivo(oSessionManager, "PEDIDO", PHCODEMP);
                PedidosBD.InsertPedidoHD(oSessionManager, PHCODEMP, ln_consecutivo, PHCODCLI, PHCODSUC, PHAGENTE, PHTIPPED, PHBODEGA, PHIDIOMA, PHMONEDA, PHTRMLOC, 0
                                                , 0, "0", 0, 0, 0, 0, 0, "CE", ".", PHNMUSER, PHLISPRE,
                                                PHFECPED.Month, PHFECPED.Year, PHFECPED, PHOBSERV,System.DateTime.Today);
                foreach (DataRow row in dt.Rows)
                {
                    ln_acu++;
                    PedidosBD.InsertPedidoDT(oSessionManager, PHCODEMP, ln_consecutivo, ln_acu, PHTIPPED, PHCODCLI, Convert.ToString(row["PDBODEGA"]),
                                             Convert.ToString(row["PDTIPPRO"]), Convert.ToString(row["PDCLAVE1"]), Convert.ToString(row["PDCLAVE2"]), Convert.ToString(row["PDCLAVE3"]),
                                             Convert.ToString(row["PDCLAVE4"]), null, null, Convert.ToString(row["PDUNDPED"]),
                                             Convert.ToDouble(row["PDCANPED"]), Convert.ToDouble(row["PDCANTID"]), 0, Convert.ToString(row["PDLISPRE"]), null, 0, 0, null,
                                             null, null, null, null, null, null, null, null,
                                             0, null, 0, null, 0, 0, 0, null,
                                             null, null, null, null, null, null, null, null, null,
                                             null, null, null, "AC", ".", PHNMUSER, null,
                                             null, null, null, null, null, null, null, null, null, 0, 0);
                }
                //lst Empaque
                LH_LSTPAQ = ComunBD.GeneraConsecutivo(oSessionManager, "NROLST", PHCODEMP);
                Obj.InsertListaHD(oSessionManager, PHCODEMP, LH_LSTPAQ, ln_consecutivo, PHBODEGA, PHNMUSER);
                //Insert Movimiento Inventario
                ln_nromovimiento = Objm.InsertMovimiento(oSessionManager, PHCODEMP, 0, PHBODEGA, null, "50", null, null, null, Convert.ToString(LH_LSTPAQ), System.DateTime.Today, null,
                                                        "CE", ".", PHNMUSER, null, null, null, null, null,null,null,null);
                ln_acu=0;
                //Movimiento Inventario
                foreach (DataRow row in dt.Rows)
                {
                    ln_acu++;
                    Objm.InsertMovimiento(oSessionManager, PHCODEMP, PHBODEGA, null, System.DateTime.Today, "50", Convert.ToString(row["PDTIPPRO"]), Convert.ToString(row["PDCLAVE1"]),
                                          Convert.ToString(row["PDCLAVE2"]), Convert.ToString(row["PDCLAVE3"]), Convert.ToString(row["PDCLAVE4"]), ".", Convert.ToDouble(row["PDCANTID"]),
                                          Convert.ToDouble(row["PDCANTID"]), "UN", ln_nromovimiento, 0, ln_acu++, null, null,
                                          null, null, null, null, 0, 0, null, "CE", ".", PHNMUSER, ln_lote, 0);                   
                    ln_lote++;
                }

                //ITEM LST EMPAQUE
                ln_acu = 0;
                foreach (DataRow row in dt.Rows)
                {
                    ln_acu++;
                    Obj.InsertListaDT(oSessionManager, PHCODEMP, LH_LSTPAQ, ln_acu, Convert.ToString(row["PDTIPPRO"]), Convert.ToString(row["PDCLAVE1"]),
                                      Convert.ToString(row["PDCLAVE2"]), Convert.ToString(row["PDCLAVE3"]), Convert.ToString(row["PDCLAVE4"]), Convert.ToDouble(row["PDCANTID"]),
                                      0, PHBODEGA, ".", 0, ln_nromovimiento, "AC", ".", PHNMUSER);

                }
                // FOTOGRAFIAS
                foreach (DataRow row in (tbAnexos as DataTable).Rows)
                {
                    Stream ioArchivo = File.OpenRead(Convert.ToString(row["ruta"]));
                    byte[] result;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        ioArchivo.CopyTo(ms);
                        result = ms.ToArray();
                    }

                    LtaEmpaqueBD.InsertEvidencia(oSessionManager, PHCODEMP, LH_LSTPAQ, Convert.ToString(row["EV_DESCRIPCION"]), result, PHNMUSER);
                }

                //CAJAS
                ln_acu = 0;

                foreach (DataRow row in dt.Rows)
                {
                    ln_acu++;
                    Obj.InsertCajas(oSessionManager, PHCODEMP, LH_LSTPAQ, ln_acu, 1, Convert.ToInt32(row["PDCANTID"]));
                }
                oSessionManager.CommitTranstaction();
                return ln_consecutivo;
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
        //Lst Empaque
        #region
        public static DataTable GetArticulos(string connection, string filter, string inBodega, string LT)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return PedidosBD.GetArticulos(oSessionManager, filter, inBodega,LT);
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

        //Multimoneda
        public DataTable GetPedidoDT_Moneda(string connection, string PDCODEMP, int PDPEDIDO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return PedidosBD.GetPedidoDT_Moneda(oSessionManager, PDCODEMP, PDPEDIDO);
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

        public DataTable getEvidenciasPedidos(string connection, string EP_CODEMP, int PHPEDIDO) {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return PedidosBD.getEvidenciasPedidos(oSessionManager, EP_CODEMP, PHPEDIDO);
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
        public DataTable getImagenPedido(string connection, int EP_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PedidosBD.getImagenPedido(oSessionManager, EP_CODIGO);
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
