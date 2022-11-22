using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Pedidos;
using XUSS.DAL.Comun;
using XUSS.BLL.Inventarios;
using XUSS.DAL.Inventarios;
using System.IO;

namespace XUSS.BLL.Pedidos
{
    public class LtaEmpaqueBL
    {
        public DataTable GetLtaEmpaque(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return LtaEmpaqueBD.GetLtaEmpaque(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetLtaEmpaqueDT(string connection, string LD_CODEMP, int LH_LSTPAQ)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LtaEmpaqueBD.GetLtaEmpaqueDT(oSessionManager, LD_CODEMP, LH_LSTPAQ);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                oSessionManager = null;
            }
        }
        public DataTable GetDetalleMovimientos(string connection, string CODEMP, int LH_LSTPAQ, int ITEM)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LtaEmpaqueBD.GetDetalleMovimientos(oSessionManager, CODEMP, LH_LSTPAQ, ITEM);
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
        public DataTable GetDisposicion(string connection, string CODEMP, string TP, string C1, string C2, string C3, string C4, string BD, int IT)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LtaEmpaqueBD.GetDisposicion(oSessionManager, CODEMP, TP, C1, C2, C3, C4, BD, IT);
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
        public DataTable GetDisposicion(string connection, string CODEMP, string TP, string C1, string C2, string C3, string C4, string BD, string LOTE,string ELEMENTO,int IT)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LtaEmpaqueBD.GetDisposicion(oSessionManager, CODEMP, TP, C1, C2, C3, C4, BD, LOTE, ELEMENTO, IT);
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
        public int InsertListaEmpaque(string connection, string LH_CODEMP, int LH_PEDIDO, string LH_BODEGA, string LH_NMUSER,object tbDetalle,object tbCajas, object tbAnexos)
        {
            int LH_LSTPAQ = 0, ln_nromovimiento = 0, ln_lote = 0;
            Boolean lb_ipedido = true;
            SessionManager oSessionManager = new SessionManager(connection);
            LtaEmpaqueBD Obj = new LtaEmpaqueBD();
            MovimientosBL Objm = new MovimientosBL();
            DataTable dt = new DataTable();
            DataTable dtItm = new DataTable();


            try {
                oSessionManager.BeginTransaction();
                dt = (tbDetalle as DataTable);
                dt.Columns.Add("nro_mov", typeof(Int32));

                LH_LSTPAQ = ComunBD.GeneraConsecutivo(oSessionManager, "NROLST", LH_CODEMP);
                //Cabecera Lista Empaque
                Obj.InsertListaHD(oSessionManager, LH_CODEMP, LH_LSTPAQ, LH_PEDIDO, LH_BODEGA, LH_NMUSER);
                //Insert Movimiento Inventario
                //ln_nromovimiento = Objm.InsertMovimiento(oSessionManager,LH_CODEMP,0,LH_BODEGA,null,"30",null,null,null,Convert.ToString(LH_LSTPAQ),System.DateTime.Today,null,
                //                                        "CE",".",LH_NMUSER,null,null,null,null,null);

                //Movimiento Inventario
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(row["MLCDLOTE"])))
                    {
                        ln_nromovimiento = Objm.InsertMovimiento(oSessionManager, LH_CODEMP, 0, LH_BODEGA, null, "30", null, null, null, Convert.ToString(LH_LSTPAQ), System.DateTime.Today, null,
                                                        "CE", ".", LH_NMUSER, null, null, null, null, null,null,null,null);

                        //Objm.InsertMovimiento(oSessionManager, LH_CODEMP, LH_BODEGA, null, System.DateTime.Today, "30", Convert.ToString(row["TP"]), Convert.ToString(row["C1"]),
                        Objm.InsertMovimiento(oSessionManager, LH_CODEMP, Convert.ToString(row["MBBODEGA"]), null, System.DateTime.Today, "30", Convert.ToString(row["TP"]), Convert.ToString(row["C1"]),
                                          Convert.ToString(row["C2"]), Convert.ToString(row["C3"]), Convert.ToString(row["C4"]), ".", Convert.ToDouble(row["MLCANTID"]),
                                          Convert.ToDouble(row["MLCANTID"]), "UN", ln_nromovimiento, 0, Convert.ToInt32(row["IT"]), Convert.ToString(row["MLCDLOTE"]), null,
                                          null, null, null, null, 0, 0, null, "CE", ".", LH_NMUSER, ln_lote, 0);
                        row["nro_mov"] = ln_nromovimiento;

                    }
                    else
                    {
                        ln_nromovimiento = Objm.InsertMovimiento(oSessionManager, LH_CODEMP, 0, LH_BODEGA, null, "30", null, null, null, Convert.ToString(LH_LSTPAQ), System.DateTime.Today, null,
                                                        "CE", ".", LH_NMUSER, null, null, null, null, null,null,null,null);

                        //Objm.InsertMovimiento(oSessionManager, LH_CODEMP, LH_BODEGA, null, System.DateTime.Today, "30", Convert.ToString(row["TP"]), Convert.ToString(row["C1"]),
                        Objm.InsertMovimiento(oSessionManager, LH_CODEMP, Convert.ToString(row["MBBODEGA"]), null, System.DateTime.Today, "30", Convert.ToString(row["TP"]), Convert.ToString(row["C1"]),                        
                                          Convert.ToString(row["C2"]), Convert.ToString(row["C3"]), Convert.ToString(row["C4"]), ".", Convert.ToDouble(row["MBCANTID"]),
                                          Convert.ToDouble(row["MBCANTID"]), "UN", ln_nromovimiento, 0, Convert.ToInt32(row["IT"]), Convert.ToString(row["MLCDLOTE"]), null,
                                          null, null, null, null, 0, 0, null, "CE", ".", LH_NMUSER, ln_lote, 0);
                        row["nro_mov"] = ln_nromovimiento;
                    }
                    ln_lote ++;
                }

                //Detalle de Lista
                dtItm.Columns.Add("TP", typeof(string));
                dtItm.Columns.Add("C1", typeof(string));
                dtItm.Columns.Add("C2", typeof(string));
                dtItm.Columns.Add("C3", typeof(string));
                dtItm.Columns.Add("C4", typeof(string));                
                dtItm.Columns.Add("CAN", typeof(Int32));
                dtItm.Columns.Add("IT", typeof(Int32));
                dtItm.Columns.Add("nro_mov", typeof(Int32));
                dtItm.Columns.Add("BD", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    string tp = Convert.ToString(row["TP"]);
                    string c1 = Convert.ToString(row["C1"]);
                    string c2 = Convert.ToString(row["C2"]);
                    string c3 = Convert.ToString(row["C3"]);
                    string c4 = Convert.ToString(row["C4"]);

                    double can =0;
                    Boolean lb_ind = true;

                    foreach (DataRow rw in dt.Rows)
                    {                        
                        if (Convert.ToString(row["IT"]) == Convert.ToString(rw["IT"]))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(row["MLCDLOTE"])))
                                can += Convert.ToDouble(rw["MLCANTID"]);
                            else
                                can += Convert.ToDouble(rw["MBCANTID"]);
                        }
                    }
                    foreach (DataRow rw in dtItm.Rows)
                    {
                        //if (Convert.ToString(row["TP"]) == Convert.ToString(rw["TP"]) &&
                        //     Convert.ToString(row["C1"]) == Convert.ToString(rw["C1"]) &&
                        //     Convert.ToString(row["C2"]) == Convert.ToString(rw["C2"]) &&
                        //     Convert.ToString(row["C3"]) == Convert.ToString(rw["C3"]) &&
                        //     Convert.ToString(row["C4"]) == Convert.ToString(rw["C4"]))
                        if (Convert.ToString(row["IT"]) == Convert.ToString(rw["IT"]))
                            lb_ind = false;

                    }
                    if (lb_ind)
                    {
                        DataRow rw = dtItm.NewRow();
                        rw["TP"] = Convert.ToString(row["TP"]);
                        rw["C1"] = Convert.ToString(row["C1"]);
                        rw["C2"] = Convert.ToString(row["C2"]);
                        rw["C3"] = Convert.ToString(row["C3"]);
                        rw["C4"] = Convert.ToString(row["C4"]);
                        rw["BD"] = Convert.ToString(row["MBBODEGA"]);
                        rw["IT"] = Convert.ToInt32(row["IT"]);
                        rw["nro_mov"] = Convert.ToInt32(row["nro_mov"]);
                        rw["CAN"] = can;
                        dtItm.Rows.Add(rw);
                        rw = null;
                    }
                }   

                foreach (DataRow row in dtItm.Rows)
                {
                    Obj.InsertListaDT(oSessionManager, LH_CODEMP, LH_LSTPAQ, Convert.ToInt32(row["IT"]), Convert.ToString(row["TP"]), Convert.ToString(row["C1"]),
                                      Convert.ToString(row["C2"]), Convert.ToString(row["C3"]), Convert.ToString(row["C4"]), Convert.ToDouble(row["CAN"]),
                                      0, Convert.ToString(row["BD"]), ".", 0, Convert.ToInt32(row["nro_mov"]), "AC", ".", LH_NMUSER);
                    //0, LH_BODEGA, "." , 0, ln_nromovimiento, "AC", ".", LH_NMUSER);
                }
                //Inserta Cajas
                foreach (DataRow row in (tbCajas as DataTable).Rows)
                {
                    Obj.InsertCajas(oSessionManager, LH_CODEMP, LH_LSTPAQ, Convert.ToInt32(row["LD_ITMPAQ"]), Convert.ToInt32(row["CL_CAJA"]), Convert.ToInt32(row["CL_CANTIDAD"]));
                }
                //Cerrar Pedido
                foreach (DataRow row in LtaEmpaqueBD.GetPedidoDT(oSessionManager, LH_CODEMP , LH_PEDIDO).Rows)
                {
                    if ((Convert.ToInt32(row["PDCANTID"]) - Convert.ToInt32(row["CAN_LST"])) != 0)
                    {
                        lb_ipedido = false;
                        break;
                    }
                }
                //Anexos
                foreach (DataRow row in (tbAnexos as DataTable).Rows)
                {
                    if (File.Exists(Convert.ToString(row["ruta"])))
                    {
                        Stream ioArchivo = File.OpenRead(Convert.ToString(row["ruta"]));
                        byte[] result;

                        using (MemoryStream ms = new MemoryStream())
                        {
                            ioArchivo.CopyTo(ms);
                            result = ms.ToArray();
                        }

                        LtaEmpaqueBD.InsertEvidencia(oSessionManager, LH_CODEMP, LH_LSTPAQ, Convert.ToString(row["EV_DESCRIPCION"]), result, LH_NMUSER);
                    }
                }
                if (lb_ipedido)
                    PedidosBD.UpdatePedidoHD(oSessionManager, LH_CODEMP, LH_PEDIDO, 0, 0, null, null, null, 0, 0, 0, null, 0, 0, 0, 0, 0, "CE", ".", LH_NMUSER);

                oSessionManager.CommitTranstaction();
                return LH_LSTPAQ;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally {
                oSessionManager = null;
                Objm = null;
                Obj = null;
                dt = null;
                dtItm = null;
            }

        }
        public DataTable GetPedidoDT(string connection, string PDCODEMP, int PDPEDIDO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LtaEmpaqueBD.GetPedidoDT(oSessionManager, PDCODEMP, PDPEDIDO);
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
        public int GetTieneFactura(string connection, string LD_CODEMP, int LH_LSTPAQ)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
               return LtaEmpaqueBD.GetTieneFactura(oSessionManager, LD_CODEMP, LH_LSTPAQ);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                oSessionManager = null;
            }
        }
        public int GetAnulalistaEmpaque(string connection, string LD_CODEMP, int LH_LSTPAQ,string LH_USUARIO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBL ObjM = new MovimientosBL();
            MovimientosBD Obj = new MovimientosBD();
            //int ln_nromov = 0;
            try {
                oSessionManager.BeginTransaction();
                LtaEmpaqueBD.AnularEmpaqueHD(oSessionManager, LD_CODEMP, LH_LSTPAQ, LH_USUARIO);
                LtaEmpaqueBD.AnularEmpaqueDT(oSessionManager, LD_CODEMP, LH_LSTPAQ, LH_USUARIO);
                //ln_nromov = LtaEmpaqueBD.GetLtaNroMov(oSessionManager, LD_CODEMP, LH_LSTPAQ);                
                foreach (DataRow rp in LtaEmpaqueBD.GetLtaEmpaqueDT(oSessionManager,LD_CODEMP,LH_LSTPAQ).Rows)
                {
                    //foreach (DataRow rw in Obj.CargarMovimiento(oSessionManager, Convert.ToString(LD_CODEMP), ln_nromov).Rows)
                    foreach (DataRow rw in Obj.CargarMovimiento(oSessionManager, Convert.ToString(LD_CODEMP), Convert.ToInt32(rp["LD_NRMOV"])).Rows)                    
                        ObjM.AnularMovimiento(oSessionManager, LD_CODEMP, Convert.ToInt32(rw["MBIDMOVI"]), Convert.ToInt32(rw["MBIDITEM"]), LH_USUARIO);                    
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
        public DataTable GetCajas(string connection, string LD_CODEMP, int LH_LSTPAQ, int LD_ITMPAQ)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LtaEmpaqueBD.GetCajas(oSessionManager, LD_CODEMP, LH_LSTPAQ, LD_ITMPAQ);
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

        //Evidencias
        public DataTable GetEvidencias(string connection, string LH_CODEMP, int LH_LSTPAQ)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return LtaEmpaqueBD.GetEvidencias(oSessionManager, LH_CODEMP, LH_LSTPAQ);
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
        public int InsertEvidencia(string connection, string LH_CODEMP, int LH_LSTPAQ, string EV_DESCRIPCION, string inRuta, string EV_USUARIO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            Stream ioArchivo = File.OpenRead(inRuta);
            byte[] result;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ioArchivo.CopyTo(ms);
                    result = ms.ToArray();
                }

                return LtaEmpaqueBD.InsertEvidencia(oSessionManager, LH_CODEMP, LH_LSTPAQ, EV_DESCRIPCION, result, EV_USUARIO);
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
        public DataTable GetEvidenciasFoto(string connection, int EV_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LtaEmpaqueBD.GetEvidenciasFoto(oSessionManager, EV_CODIGO);
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
