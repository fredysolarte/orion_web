using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XUSS.DAL.Inventarios;
using System.Data;
using DataAccess;
using XUSS.BLL.Comun;
using XUSS.DAL.Comun;
using XUSS.DAL.Parametros;
using XUSS.DAL.Costos;
using System.IO;
using XUSS.DAL.Compras;

namespace XUSS.BLL.Inventarios
{
    public class TrasladosBL
    {
        public DataTable GetTraslados(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TrasladosBD Obj = new TrasladosBD();
            try
            {
                return Obj.GetTraslados(oSessionManager, filter, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                Obj = null;
            }
        }
        public int InsertTraslado(string connection,string TSCODEMP, int TSNROTRA, DateTime TSFECTRA, string TSBODEGA, string TSOTBODE, string TSCDTRAN, string TSOTTRAN,
                                  int TSMOVENT, int TSMOVSAL, string TSCOMENT, string TSESTADO, string P_CLISPRE, string TSCAUSAE, string TSNMUSER, int TRCODTER,object tbitems, object tbCostos, object tbSoportes, 
                                  object tbBLHD, object tbBLDT,object tbSegregacion, object tbTrasladoWrIn)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TrasladosBD Obj = new TrasladosBD();
            MovimientosBL ObjM = new MovimientosBL();
           
            double ln_cantidad = 0;
            Boolean lb_lote = false, lb_elem = false;

            int MIOTMOVI = 0,MIIDMOVI=0,BLH_CODIGO = 0; 
            
            try {
                oSessionManager.BeginTransaction();
                //Genera Nro Traslado
                TSNROTRA = ComunBD.GeneraConsecutivo(oSessionManager,"NROTRA",TSCODEMP);
                //MIOTMOVI = ComunBD.GeneraConsecutivo(oSessionManager, "MOVINV", TSCODEMP);

                //Genera Movimiento Salida
                MIIDMOVI=ObjM.InsertMovimiento(oSessionManager, TSCODEMP, MIOTMOVI, TSBODEGA, TSOTBODE, "99", 0, 0, 0, null, TSFECTRA, null, "AC", ".", TSNMUSER, 0, 0, TSNROTRA, null, 0,null,null,null);
                foreach (DataRow rw in (tbitems as DataTable).Rows)
                {
                    lb_lote = false;
                    lb_elem = false;
                    ln_cantidad = 0;

                    using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, TSCODEMP, TSBODEGA, Convert.ToString(rw["TP"])))
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                                lb_lote = true;
                            if (Convert.ToString(reader["ABMNELEM"]) == "S")
                                lb_elem = true;
                        }
                    }

                    ln_cantidad = Convert.ToDouble(rw["MBCANTID"]);
                    if (lb_lote)
                        ln_cantidad = Convert.ToDouble(rw["MLCANTID"]);
                    if (lb_elem)
                        ln_cantidad = Convert.ToDouble(rw["MECANTID"]);

                    ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSBODEGA, TSOTBODE, TSFECTRA, "99", Convert.ToString(rw["TP"]), Convert.ToString(rw["C1"]), Convert.ToString(rw["C2"]), Convert.ToString(rw["C3"]),
                                          Convert.ToString(rw["C4"]), ".", ln_cantidad, ln_cantidad, "UN", MIIDMOVI, MIOTMOVI, Convert.ToInt32(rw["IT"]), Convert.ToString(rw["MLCDLOTE"]), Convert.ToString(rw["MECDELEM"]), null, null,
                                          null, null, 0, 0, "AC", "AC", ".", TSNMUSER, Convert.ToInt32(rw["IDLOTE"]), Convert.ToInt32(rw["IDELEM"]));                   
                }

                //Movimiento de Entrada
                MIOTMOVI = ObjM.InsertMovimiento(oSessionManager, TSCODEMP, MIOTMOVI, TSBODEGA, TSOTBODE, "98", 0, 0, 0, null, TSFECTRA, null, "AC", ".", TSNMUSER, 0, 0, TSNROTRA, null, 0,null,null,null);
                foreach (DataRow rw in (tbitems as DataTable).Rows)
                {
                    lb_lote = false;
                    lb_elem = false;
                    ln_cantidad = 0;

                    using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, TSCODEMP, TSBODEGA, Convert.ToString(rw["TP"])))
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                                lb_lote = true;
                            if (Convert.ToString(reader["ABMNELEM"]) == "S")
                                lb_elem = true;
                        }
                    }

                    ln_cantidad = Convert.ToDouble(rw["MBCANTID"]);
                    if (lb_lote)
                        ln_cantidad = Convert.ToDouble(rw["MLCANTID"]);
                    if (lb_elem)
                        ln_cantidad = Convert.ToDouble(rw["MECANTID"]);

                    ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSOTBODE, TSBODEGA, System.DateTime.Today, "98", Convert.ToString(rw["TP"]), Convert.ToString(rw["C1"]), Convert.ToString(rw["C2"]), Convert.ToString(rw["C3"]),
                                          Convert.ToString(rw["C4"]), ".", ln_cantidad, ln_cantidad, "UN", MIOTMOVI, MIIDMOVI, Convert.ToInt32(rw["IT"]), Convert.ToString(rw["MLCDLOTE"]), Convert.ToString(rw["MECDELEM"]), null, null,
                                          null, null, 0, 0, "AC", "AC", ".", TSNMUSER, Convert.ToInt32(rw["IDLOTE"]), Convert.ToInt32(rw["IDELEM"]));

                    //ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSOTBODE, TSBODEGA, System.DateTime.Today, "98", Convert.ToString(rw["TP"]), Convert.ToString(rw["C1"]), Convert.ToString(rw["C2"]), Convert.ToString(rw["C3"]),
                    //                      Convert.ToString(rw["C4"]), ".", Convert.ToDouble(rw["MBCANTID"]), Convert.ToDouble(rw["MBCANTID"]), "UN", MIOTMOVI, MIIDMOVI, Convert.ToInt32(rw["IT"]), Convert.ToString(rw["MLCDLOTE"]), Convert.ToString(rw["MECDELEM"]), null, null,
                    //                      null, null, 0, 0, "AC", "AC", ".", TSNMUSER, Convert.ToInt32(rw["IDLOTE"]), Convert.ToInt32(rw["IDELEM"]));
                }

                //Inserta Traslado
                Obj.InsertTraslado(oSessionManager, TSCODEMP, TSNROTRA, TSFECTRA, TSBODEGA, TSOTBODE, TSCDTRAN, TSOTTRAN, MIOTMOVI, MIIDMOVI, TSCOMENT, P_CLISPRE, TSESTADO, TSCAUSAE, TSNMUSER, null, null, null,TRCODTER);

                //Inserta BL para WR OUT
                foreach (DataRow rw in ((DataTable)tbBLHD).Rows)
                {
                    BLH_CODIGO = ComunBL.GeneraConsecutivo(connection, "CNBL");

                    BillofLadingBD.InsertBLHD(oSessionManager, BLH_CODIGO, TSCODEMP, Convert.ToDateTime(rw["BHL_FECHA"]), Convert.ToInt32(rw["BLH_CODEXPORTER"]), Convert.ToInt32(rw["BLH_CODRECEPTOR"]), Convert.ToInt32(rw["BLH_CODNOTIFY"]), Convert.ToString(rw["BLH_MODTRANS"]), Convert.ToString(rw["BLH_CIUREC"]), Convert.ToString(rw["BLH_NROVIAJE"]),
                        Convert.ToString(rw["BLH_PURORIGEN"]), Convert.ToString(rw["BLH_PURDESTINO"]), Convert.ToString(rw["BLH_CIUDESTI"]), Convert.ToString(rw["BLH_BOOKINGNO"]), Convert.ToString(rw["BLH_NROBILLOFL"]), Convert.ToString(rw["BLH_EXPORTREF"]), Convert.ToString(rw["BLH_PTOPAISORI"]),
                        Convert.ToString(rw["BLH_TIPOENVIO"]), TSNMUSER);

                    foreach (DataRow rx in (tbBLDT as DataTable).Rows)
                    {
                        BillofLadingBD.InsertBLDT(oSessionManager, BLH_CODIGO, Convert.ToString(rx["BLD_NROCONTAINER"]), Convert.ToDouble(rx["BLD_NROPACK"]), Convert.ToString(rx["BLD_DESCRIPTION"]), Convert.ToDouble(rx["BLD_GROSSWEIGHT"]), Convert.ToString(rx["BLD_GROSSUN"]),
                            Convert.ToDouble(rx["BLD_DIMESION"]), Convert.ToString(rx["BLD_DIMESIONUN"]));
                    }

                    BillofLadingBD.InsertBL_Traslado(oSessionManager, TSCODEMP, TSNROTRA, BLH_CODIGO);
                }
                //Inserta Costos
                foreach (DataRow rw in (tbCostos as DataTable).Rows)
                {
                    CargarCostosBD.InsertCosto(oSessionManager, TSCODEMP, TSNROTRA, Convert.ToString(rw["CT_TIPPRO"]), Convert.ToString(rw["CT_CLAVE1"]), Convert.ToString(rw["CT_CLAVE2"]),
                                               Convert.ToString(rw["CT_CLAVE3"]), Convert.ToString(rw["CT_CLAVE4"]), Convert.ToInt32(rw["TRCODTER"]), Convert.ToString(rw["CT_TIPDOC"]), Convert.ToString(rw["CT_NUMDOC"]),
                                               Convert.ToDateTime(rw["CT_FECDOC"]), Convert.ToString(rw["CT_MONEDA"]), Convert.ToDouble(rw["CT_PRECIO"]), Convert.ToString(rw["CT_OBSERVACIONES"]), TSNMUSER, "AC", null);
                }                
                //Inserta Soportes
                foreach (DataRow rw in (tbSoportes as DataTable).Rows)
                {
                    if (SoportesBD.ExisteImagen(oSessionManager, Convert.ToInt32(rw["SP_CONSECUTIVO"]), TSNROTRA) == 0)
                    {
                        Stream ioArchivo = File.OpenRead(Convert.ToString(rw["RUTA"]));
                        byte[] result;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ioArchivo.CopyTo(ms);
                            result = ms.ToArray();
                        }

                        SoportesBD.InsertSoporte(oSessionManager, Convert.ToString(rw["SP_TIPO"]), TSNROTRA, Convert.ToString(rw["SP_DESCRIPCION"]), Convert.ToString(rw["SP_EXTENCION"]), result, TSNMUSER,null,null);

                    }
                }
                //Inserta Segregacion x Traslado
                foreach (DataRow rw in (tbSegregacion as DataTable).Rows)
                {
                    SegregacionBD.InsertSegregacionxTraslado(oSessionManager, TSCODEMP, TSNROTRA, Convert.ToInt32(rw["SGH_CODIGO"]));
                }
                //Detalle WrIn Traslado
                foreach (DataRow rw in (tbTrasladoWrIn as DataTable).Rows) {
                    OrdenesComprasBD.InsertTrasladoWrIn(oSessionManager, Convert.ToInt32(rw["WIH_CONSECUTIVO"]), Convert.ToInt32(rw["WID_ITEM"]), TSCODEMP, MIIDMOVI, Convert.ToInt32(rw["MBIDITEM"]));
                }                

                oSessionManager.CommitTranstaction();
                //oSessionManager.RollBackTransaction();
                return TSNROTRA;
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

        public int UpdateTraslado(string connection, string TSCODEMP, int TSNROTRA, DateTime TSFECTRA, string TSBODEGA, string TSOTBODE, string TSCDTRAN, string TSOTTRAN,
                                  int TSMOVENT, int TSMOVSAL, string TSCOMENT, string TSESTADO, string TSCAUSAE, string TSNMUSER, object tbitems, object tbCostos, object tbSoportes,
                                  object tbBLHD,object tbBLDT)
        {
            TrasladosBD Obj = new TrasladosBD();
            MovimientosBL ObjM = new MovimientosBL();
            MovimientosBD ObjMD = new MovimientosBD();
            DataTable dt = new DataTable();
            DataTable dtL = new DataTable();
            DataTable dtE = new DataTable();
            SessionManager oSessionManager = new SessionManager(connection);
            Boolean lb_lote = false, lb_elem = false;
            int BLH_CODIGO = 0;
            try
            {
                oSessionManager.BeginTransaction();

                if (TSESTADO == "CE")
                {
                    // Movimiento Entrada
                    dt = ObjMD.CargarMovimiento(oSessionManager, TSCODEMP, TSMOVENT);
                    foreach (DataRow rw in dt.Rows)
                    {
                        //Bodegas
                        using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, TSCODEMP, TSBODEGA, Convert.ToString(rw["MBTIPPRO"])))
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                                    lb_lote = true;
                                if (Convert.ToString(reader["ABMNELEM"]) == "S")
                                    lb_elem = true;
                            }
                        }

                        if (lb_lote)
                        {                            
                            dtL = ObjM.CargarMovimientoLot(null, TSCODEMP, TSMOVENT, Convert.ToInt32(rw["MBIDITEM"]));
                            foreach (DataRow rl in dtL.Rows)
                            {
                                if (lb_elem)
                                {
                                    dtE = ObjM.CargarMovimientoEle(null, TSCODEMP, TSMOVENT, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToInt32(rl["MLIDLOTE"]));
                                    foreach(DataRow re in dtE.Rows)
                                        ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSOTBODE, TSBODEGA, System.DateTime.Today, "98", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(re["MECANTID"]), Convert.ToDouble(re["MECANTID"]), "UN", TSMOVENT, TSMOVSAL, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToString(rl["MLCDLOTE"]), Convert.ToString(re["MECDELEM"]), "", "", "", "", 0, 0, "AC", "CE", ".", TSNMUSER, Convert.ToInt32(re["MEIDLOTE"]), Convert.ToInt32(re["MEIDELEM"]));
                                }
                                else
                                    ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSOTBODE, TSBODEGA, System.DateTime.Today, "98", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(rl["MLCANTID"]), Convert.ToDouble(rl["MLCANTID"]), "UN", TSMOVENT, TSMOVSAL, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToString(rl["MLCDLOTE"]), "", "", "", "", "", 0, 0, "AC", "CE", ".", TSNMUSER, 0, 0);
                            }
                        }
                        else                        
                            ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSOTBODE, TSBODEGA, System.DateTime.Today, "98", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(rw["MBCANTID"]), Convert.ToDouble(rw["MBCANTID"]), "UN", TSMOVENT, TSMOVSAL, Convert.ToInt32(rw["MBIDITEM"]), "", "", "", "", "", "", 0, 0, "AC", "CE", ".", TSNMUSER, 0, 0);                        
                    }
                    //Movimiento Salida                
                    dt = ObjMD.CargarMovimiento(oSessionManager, TSCODEMP, TSMOVSAL);
                    foreach (DataRow rw in dt.Rows)
                    {
                        lb_lote = false;
                        lb_elem = false;
                        //Bodegas
                        using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, TSCODEMP, TSBODEGA, Convert.ToString(rw["MBTIPPRO"])))
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                                    lb_lote = true;
                                if (Convert.ToString(reader["ABMNELEM"]) == "S")
                                    lb_elem = true;
                            }
                        }

                        if (lb_lote)
                        {
                            dtL = ObjMD.CargarMovimientoLot(oSessionManager, TSCODEMP, TSMOVSAL, Convert.ToInt32(rw["MBIDITEM"]));
                            foreach (DataRow rl in dtL.Rows)
                            {
                                if (lb_elem)
                                {
                                    dtE = ObjM.CargarMovimientoEle(null, TSCODEMP, TSMOVENT, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToInt32(rl["MLIDLOTE"]));
                                    foreach (DataRow re in dtE.Rows)
                                        ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSBODEGA, TSOTBODE, System.DateTime.Today, "99", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(re["MECANTID"]), Convert.ToDouble(re["MECANTID"]), "UN", TSMOVSAL, TSMOVENT, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToString(rl["MLCDLOTE"]), Convert.ToString(re["MECDELEM"]), "", "", "", "", 0, 0, "AC", "CE", ".", TSNMUSER, Convert.ToInt32(re["MEIDLOTE"]), Convert.ToInt32(re["MEIDELEM"]));
                                }
                                else
                                    ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSBODEGA, TSOTBODE, System.DateTime.Today, "99", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(rl["MLCANTID"]), Convert.ToDouble(rl["MLCANTID"]), "UN", TSMOVSAL, TSMOVENT, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToString(rl["MLCDLOTE"]), "", "", "", "", "", 0, 0, "AC", "CE", ".", TSNMUSER, 0, 0);
                            }
                        }
                        else
                        {
                            ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSBODEGA, TSOTBODE, System.DateTime.Today, "99", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(rw["MBCANTID"]), Convert.ToDouble(rw["MBCANTID"]), "UN", TSMOVSAL, TSMOVENT, Convert.ToInt32(rw["MBIDITEM"]), "", "", "", "", "", "", 0, 0, "AC", "CE", ".", TSNMUSER, 0, 0);
                        }
                    }

                    //Cierra Traslado
                    Obj.UpdateTraslado(oSessionManager, TSCODEMP, TSNROTRA, TSFECTRA, TSBODEGA, TSOTBODE, TSCDTRAN, TSOTTRAN, TSMOVENT, TSMOVSAL, TSCOMENT, TSCAUSAE, TSESTADO, null, TSNMUSER);
                }

                //Inserta BL para WR OUT
                BillofLadingBD.DeleteBL_Traslado(oSessionManager, TSCODEMP, TSNROTRA);
                foreach (DataRow rw in ((DataTable)tbBLHD).Rows)
                {
                    BLH_CODIGO = ComunBL.GeneraConsecutivo(connection, "CNBL");

                    BillofLadingBD.InsertBLHD(oSessionManager, BLH_CODIGO, TSCODEMP, Convert.ToDateTime(rw["BLH_FECHA"]), Convert.ToInt32(rw["BLH_CODEXPORTER"]), Convert.ToInt32(rw["BLH_CODRECEPTOR"]), Convert.ToInt32(rw["BLH_CODNOTIFY"]), Convert.ToString(rw["BLH_MODTRANS"]), Convert.ToString(rw["BLH_CIUREC"]), Convert.ToString(rw["BLH_NROVIAJE"]),
                        Convert.ToString(rw["BLH_PURORIGEN"]), Convert.ToString(rw["BLH_PURDESTINO"]), Convert.ToString(rw["BLH_CIUDESTI"]), Convert.ToString(rw["BLH_BOOKINGNO"]), Convert.ToString(rw["BLH_NROBILLOFL"]), Convert.ToString(rw["BLH_EXPORTREF"]), Convert.ToString(rw["BLH_PTOPAISORI"]),
                        Convert.ToString(rw["BLH_TIPOENVIO"]), TSNMUSER);

                    foreach (DataRow rx in (tbBLDT as DataTable).Rows)
                    {
                        BillofLadingBD.InsertBLDT(oSessionManager, BLH_CODIGO, Convert.ToString(rx["BLD_NROCONTAINER"]), Convert.ToDouble(rx["BLD_NROPACK"]), Convert.ToString(rx["BLD_DESCRIPTION"]), Convert.ToDouble(rx["BLD_GROSSWEIGHT"]), Convert.ToString(rx["BLD_GROSSUN"]),
                            Convert.ToDouble(rx["BLD_DIMESION"]), Convert.ToString(rx["BLD_DIMESIONUN"]));
                    }

                    BillofLadingBD.InsertBL_Traslado(oSessionManager, TSCODEMP,TSNROTRA, BLH_CODIGO);
                }
                //Inserta Costos
                CargarCostosBD.DeleteCosto(oSessionManager, TSNROTRA);
                foreach (DataRow rw in (tbCostos as DataTable).Rows)
                {                    
                    CargarCostosBD.InsertCosto(oSessionManager, TSCODEMP, TSNROTRA, Convert.ToString(rw["CT_TIPPRO"]), Convert.ToString(rw["CT_CLAVE1"]), Convert.ToString(rw["CT_CLAVE2"]),
                                               Convert.ToString(rw["CT_CLAVE3"]), Convert.ToString(rw["CT_CLAVE4"]), Convert.ToInt32(rw["TRCODTER"]), Convert.ToString(rw["CT_TIPDOC"]), Convert.ToString(rw["CT_NUMDOC"]),
                                               Convert.ToDateTime(rw["CT_FECDOC"]), Convert.ToString(rw["CT_MONEDA"]), Convert.ToDouble(rw["CT_PRECIO"]), Convert.ToString(rw["CT_OBSERVACIONES"]), TSNMUSER, "AC", null);
                }

                //Inserta Soportes
                foreach (DataRow rw in (tbSoportes as DataTable).Rows)
                {
                    if (SoportesBD.ExisteImagen(oSessionManager, Convert.ToInt32(rw["SP_CONSECUTIVO"]), TSNROTRA) == 0)
                    {
                        Stream ioArchivo = File.OpenRead(Convert.ToString(rw["RUTA"]));
                        byte[] result;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ioArchivo.CopyTo(ms);
                            result = ms.ToArray();
                        }

                        SoportesBD.InsertSoporte(oSessionManager, Convert.ToString(rw["SP_TIPO"]), TSNROTRA, Convert.ToString(rw["SP_DESCRIPCION"]), Convert.ToString(rw["SP_EXTENCION"]), result, TSNMUSER,null,null);

                    }
                }
                oSessionManager.CommitTranstaction();
                return TSNROTRA;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                ObjMD = null;
                ObjM = null;
                dtL = null;
                dt = null;
            }
        }
        public int confirmTraslado(string connection, string TSCODEMP, int TSNROTRA, DateTime TSFECTRA, string TSBODEGA, string TSOTBODE, string TSCDTRAN, string TSOTTRAN,
                                  int TSMOVENT, int TSMOVSAL, string TSCOMENT, string TSESTADO, string TSCAUSAE, string TSNMUSER)
        {
            TrasladosBD Obj = new TrasladosBD();
            MovimientosBL ObjM = new MovimientosBL();
            MovimientosBD ObjMD = new MovimientosBD();
            DataTable dt = new DataTable();
            DataTable dtL = new DataTable();
            DataTable dtE = new DataTable();
            SessionManager oSessionManager = new SessionManager(connection);
            Boolean lb_lote = false, lb_elem = false;            
            try
            {
                oSessionManager.BeginTransaction();

                if (TSESTADO == "CE")
                {
                    // Movimiento Entrada
                    dt = ObjMD.CargarMovimiento(oSessionManager, TSCODEMP, TSMOVENT);
                    foreach (DataRow rw in dt.Rows)
                    {
                        //Bodegas
                        using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, TSCODEMP, TSBODEGA, Convert.ToString(rw["MBTIPPRO"])))
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                                    lb_lote = true;
                                if (Convert.ToString(reader["ABMNELEM"]) == "S")
                                    lb_elem = true;
                            }
                        }

                        if (lb_lote)
                        {
                            dtL = ObjM.CargarMovimientoLot(null, TSCODEMP, TSMOVENT, Convert.ToInt32(rw["MBIDITEM"]));
                            foreach (DataRow rl in dtL.Rows)
                            {
                                if (lb_elem)
                                {
                                    dtE = ObjM.CargarMovimientoEle(null, TSCODEMP, TSMOVENT, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToInt32(rl["MLIDLOTE"]));
                                    foreach (DataRow re in dtE.Rows)
                                        ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSOTBODE, TSBODEGA, System.DateTime.Today, "98", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(re["MECANTID"]), Convert.ToDouble(re["MECANTID"]), "UN", TSMOVENT, TSMOVSAL, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToString(rl["MLCDLOTE"]), Convert.ToString(re["MECDELEM"]), "", "", "", "", 0, 0, "AC", "CE", ".", TSNMUSER, Convert.ToInt32(re["MEIDLOTE"]), Convert.ToInt32(re["MEIDELEM"]));
                                }
                                else
                                    ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSOTBODE, TSBODEGA, System.DateTime.Today, "98", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(rl["MLCANTID"]), Convert.ToDouble(rl["MLCANTID"]), "UN", TSMOVENT, TSMOVSAL, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToString(rl["MLCDLOTE"]), "", "", "", "", "", 0, 0, "AC", "CE", ".", TSNMUSER, 0, 0);
                            }
                        }
                        else
                            ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSOTBODE, TSBODEGA, System.DateTime.Today, "98", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(rw["MBCANTID"]), Convert.ToDouble(rw["MBCANTID"]), "UN", TSMOVENT, TSMOVSAL, Convert.ToInt32(rw["MBIDITEM"]), "", "", "", "", "", "", 0, 0, "AC", "CE", ".", TSNMUSER, 0, 0);
                    }
                    //Movimiento Salida                
                    dt = ObjMD.CargarMovimiento(oSessionManager, TSCODEMP, TSMOVSAL);
                    foreach (DataRow rw in dt.Rows)
                    {
                        lb_lote = false;
                        lb_elem = false;
                        //Bodegas
                        using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, TSCODEMP, TSBODEGA, Convert.ToString(rw["MBTIPPRO"])))
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                                    lb_lote = true;
                                if (Convert.ToString(reader["ABMNELEM"]) == "S")
                                    lb_elem = true;
                            }
                        }

                        if (lb_lote)
                        {
                            dtL = ObjMD.CargarMovimientoLot(oSessionManager, TSCODEMP, TSMOVSAL, Convert.ToInt32(rw["MBIDITEM"]));
                            foreach (DataRow rl in dtL.Rows)
                            {
                                if (lb_elem)
                                {
                                    dtE = ObjM.CargarMovimientoEle(null, TSCODEMP, TSMOVENT, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToInt32(rl["MLIDLOTE"]));
                                    foreach (DataRow re in dtE.Rows)
                                        ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSBODEGA, TSOTBODE, System.DateTime.Today, "99", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(re["MECANTID"]), Convert.ToDouble(re["MECANTID"]), "UN", TSMOVSAL, TSMOVENT, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToString(rl["MLCDLOTE"]), Convert.ToString(re["MECDELEM"]), "", "", "", "", 0, 0, "AC", "CE", ".", TSNMUSER, Convert.ToInt32(re["MEIDLOTE"]), Convert.ToInt32(re["MEIDELEM"]));
                                }
                                else
                                    ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSBODEGA, TSOTBODE, System.DateTime.Today, "99", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(rl["MLCANTID"]), Convert.ToDouble(rl["MLCANTID"]), "UN", TSMOVSAL, TSMOVENT, Convert.ToInt32(rw["MBIDITEM"]), Convert.ToString(rl["MLCDLOTE"]), "", "", "", "", "", 0, 0, "AC", "CE", ".", TSNMUSER, 0, 0);
                            }
                        }
                        else
                        {
                            ObjM.InsertMovimiento(oSessionManager, TSCODEMP, TSBODEGA, TSOTBODE, System.DateTime.Today, "99", Convert.ToString(rw["MBTIPPRO"]), Convert.ToString(rw["MBCLAVE1"]), Convert.ToString(rw["MBCLAVE2"]), Convert.ToString(rw["MBCLAVE3"]), Convert.ToString(rw["MBCLAVE4"]), ".", Convert.ToDouble(rw["MBCANTID"]), Convert.ToDouble(rw["MBCANTID"]), "UN", TSMOVSAL, TSMOVENT, Convert.ToInt32(rw["MBIDITEM"]), "", "", "", "", "", "", 0, 0, "AC", "CE", ".", TSNMUSER, 0, 0);
                        }
                    }

                    //Cierra Traslado
                    Obj.UpdateTraslado(oSessionManager, TSCODEMP, TSNROTRA, TSFECTRA, TSBODEGA, TSOTBODE, TSCDTRAN, TSOTTRAN, TSMOVENT, TSMOVSAL, TSCOMENT, TSCAUSAE, TSESTADO, null, TSNMUSER);
                }
                
                oSessionManager.CommitTranstaction();
                return TSNROTRA;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                ObjMD = null;
                ObjM = null;
                dtL = null;
                dt = null;
            }
        }
        public int AnularMovimientos(string connection, string TSCODEMP, int TSNROTRA, int TSMOVENT, int TSMOVSAL, string TSNMUSER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TrasladosBD Obj = new TrasladosBD();
            MovimientosBL ObjM = new MovimientosBL();
            try
            {
                oSessionManager.BeginTransaction();
                //Entrada
                foreach (DataRow rw in ObjM.CargarMovimiento(null, TSCODEMP, TSMOVENT).Rows)
                {
                    ObjM.AnularMovimiento(oSessionManager, TSCODEMP, Convert.ToInt32(rw["MBIDMOVI"]), Convert.ToInt32(rw["MBIDITEM"]), TSNMUSER);
                }
                //Salida
                foreach (DataRow rw in ObjM.CargarMovimiento(null, TSCODEMP, TSMOVSAL).Rows)
                {
                    ObjM.AnularMovimiento(oSessionManager, TSCODEMP, Convert.ToInt32(rw["MBIDMOVI"]), Convert.ToInt32(rw["MBIDITEM"]), TSNMUSER);
                }
                //Traslado
                Obj.AnulaTraslado(oSessionManager, TSCODEMP, TSNROTRA, "AN", TSNMUSER);

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
                Obj = null;
                ObjM = null;
            }
        }
    }
}
