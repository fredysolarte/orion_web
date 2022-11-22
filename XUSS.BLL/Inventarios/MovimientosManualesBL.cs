using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Inventarios;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Inventarios
{
    public class MovimientosManualesBL
    {
        public int InsertMovimiento(string connection, string MICODEMP, int MIOTMOVI, string MIBODEGA, string MIOTBODE, string MICDTRAN,
                                    int? MIPEDIDO, int? MICOMPRA, int? MICODTER, string MICDDOCU, DateTime? MIFECDOC, string MICOMENT, string MIESTADO, string MICAUSAE, string MINMUSER,
                                    int? MIORDPRO, int? MILINPRO, int? MINROTRA, string MICODMAQ, int? MIRECIBO, int? MISUCURSAL, string MIUSERSOL, string MIUSERAPR, object tbitems)
        {
            int MIIDMOVI = 0;
            double ln_cantidad = 0;
            Boolean lb_lote = false,lb_elem=false;
            
            MovimientosBL ObjM = new MovimientosBL();
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                oSessionManager.BeginTransaction();
                MIIDMOVI = ObjM.InsertMovimiento(oSessionManager, MICODEMP, MIOTMOVI, MIBODEGA, MIOTBODE, MICDTRAN, MIPEDIDO, MICOMPRA, MICODTER, MICDDOCU, MIFECDOC, 
                                                 MICOMENT, MIESTADO, MICAUSAE, MINMUSER, MIORDPRO, MILINPRO, MINROTRA, MICODMAQ, MIRECIBO, MISUCURSAL, MIUSERSOL, MIUSERAPR);                

                foreach (DataRow rw in (tbitems as DataTable).Rows)
                {
                    lb_lote = false;
                    lb_elem = false;
                    ln_cantidad = 0;

                    using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, MICODEMP, MIBODEGA, Convert.ToString(rw["TP"])))
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

                    ObjM.InsertMovimiento(oSessionManager, MICODEMP, MIBODEGA, MIOTBODE, System.DateTime.Today, MICDTRAN, Convert.ToString(rw["TP"]), Convert.ToString(rw["C1"]), Convert.ToString(rw["C2"]), Convert.ToString(rw["C3"]),
                                          Convert.ToString(rw["C4"]), ".", ln_cantidad, ln_cantidad, "UN", MIIDMOVI, MIOTMOVI, Convert.ToInt32(rw["IT"]), Convert.ToString(rw["MLCDLOTE"]), Convert.ToString(rw["MECDELEM"]), null, null,
                                          null, null, 0, 0, null, "CE", ".", MINMUSER, Convert.ToInt32(rw["IDLOTE"]), Convert.ToInt32(rw["IDELEM"]));

                    //ObjM.InsertMovimiento(oSessionManager, MICODEMP, MIBODEGA, MIOTBODE, System.DateTime.Today, MICDTRAN, Convert.ToString(rw["TP"]), Convert.ToString(rw["C1"]), Convert.ToString(rw["C2"]), Convert.ToString(rw["C3"]),
                    //                      Convert.ToString(rw["C4"]), ".", Convert.ToDouble(rw["MBCANTID"]), Convert.ToDouble(rw["MBCANTID"]), "UN", MIIDMOVI, MIOTMOVI, Convert.ToInt32(rw["IT"]), Convert.ToString(rw["MLCDLOTE"]), Convert.ToString(rw["MECDELEM"]), null, null,
                    //                      null, null, 0, 0, null, "CE", ".", MINMUSER, Convert.ToInt32(rw["IDLOTE"]), Convert.ToInt32(rw["IDELEM"]));
                }

                oSessionManager.CommitTranstaction();

                return MIIDMOVI;
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
        public DataTable GetMovimInv(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            try
            {
                return Obj.GetMovimInv(oSessionManager, filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
    }
}
