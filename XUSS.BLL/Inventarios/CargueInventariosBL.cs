using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Comun;
using XUSS.DAL.Inventarios;

namespace XUSS.BLL.Inventarios
{
    public class CargueInventariosBL
    {
        public DataTable GetTomaFisicaHD(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            try {
                return Obj.GetFisicoInv(oSessionManager, filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                oSessionManager = null;
            }
        }
        public DataTable GetTomaFisicaDT(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            try
            {
                return Obj.GetFisicoBod(oSessionManager, filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                oSessionManager = null;
            }
        }
        public int InsertTomaFisica(string connection,string IICODEMP,string IIBODEGA, string IICOMENT,string IIESTADO,string IICAUSAE, string IINMUSER, Object inDT)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            int i = 0;
            try
            {
                oSessionManager.BeginTransaction();
                //Genera Foto
                int ln_nrofoto = ComunBD.GeneraConsecutivo(oSessionManager, "FOTOIN", IICODEMP);
                Obj.InsertFotoBod(oSessionManager, IICODEMP, ln_nrofoto, IIBODEGA, "N", IINMUSER);
                
                //Insert Cabcecera Toma Fisica
                Obj.InsertFisicoInv(oSessionManager, IICODEMP, ln_nrofoto, 1, 1, System.DateTime.Today, IIBODEGA, IICOMENT, IIESTADO, IICAUSAE, IINMUSER);
                //Detalle
                foreach(DataRow rw in (inDT as DataTable).Rows)
                {
                    i++;
                    Obj.InsertFisicoBod(oSessionManager, IICODEMP, ln_nrofoto, 1, 1, i, System.DateTime.Today, IIBODEGA, Convert.ToString(rw["IBTIPPRO"]), 
                        Convert.ToString(rw["IBCLAVE1"]), Convert.ToString(rw["IBCLAVE2"]), Convert.ToString(rw["IBCLAVE3"]), Convert.ToString(rw["IBCLAVE4"]), ".", Convert.ToDouble(rw["IBCANTID"]), "AC", ".", IINMUSER);
                }

                //Carga Inventario
                Obj.DeteleBalanBod(oSessionManager, IICODEMP, IIBODEGA);
                foreach (DataRow rw in (inDT as DataTable).Rows)
                {
                    Obj.InsertBalanBod(oSessionManager, IICODEMP, IIBODEGA, Convert.ToString(rw["IBTIPPRO"]),
                        Convert.ToString(rw["IBCLAVE1"]), Convert.ToString(rw["IBCLAVE2"]), Convert.ToString(rw["IBCLAVE3"]), Convert.ToString(rw["IBCLAVE4"]), ".", Convert.ToDouble(rw["IBCANTID"]), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, System.DateTime.Today, System.DateTime.Today, IINMUSER, 0, 0);
                }

                // Genera Foto Inv Inicial
                ln_nrofoto = ComunBD.GeneraConsecutivo(oSessionManager, "FOTOIN", IICODEMP);
                Obj.InsertFotoBod(oSessionManager, IICODEMP, ln_nrofoto, IIBODEGA, "S", IINMUSER);

                oSessionManager.CommitTranstaction();

                return ln_nrofoto;
            }
            catch(Exception ex)
            {
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
