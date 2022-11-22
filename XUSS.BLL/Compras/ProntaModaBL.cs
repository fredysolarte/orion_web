using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.BLL.Comun;
using XUSS.DAL.Compras;

namespace XUSS.BLL.Compras
{
    public class ProntaModaBL
    {
        public DataTable GetProntaModa(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            ProntaModaBD Obj = new ProntaModaBD();
            try
            {
                return Obj.GetProntaModa(oSessionManager, filter);
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
        public int InsertProntaModa(string connection,string ICCODEMP,string ICTIPPRO,string ICCLAVE1,int ICPROVEE,string ICREFERENCIA,int ICCANTIDAD,string ICMONEDA,double ICCOSTO,DateTime ICFECHA,string ICOBSERVACION,string ICCDUSER,string ICESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            ProntaModaBD Obj = new ProntaModaBD();
            int ICCONSE = 0;
            try
            {
                oSessionManager.BeginTransaction();
                ICCONSE = ComunBL.GeneraConsecutivo(null, "CCST",ICCODEMP);
                Obj.InsertProntaModa(oSessionManager, ICCONSE,ICCODEMP,ICTIPPRO,ICCLAVE1,ICPROVEE,ICREFERENCIA,ICCANTIDAD,ICMONEDA,ICCOSTO,ICFECHA,ICOBSERVACION,ICCDUSER,ICESTADO);
                oSessionManager.CommitTranstaction();
                return ICCONSE;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        public DataTable GetProntaModaDT(string connection, int ICCONSE)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            ProntaModaBD Obj = new ProntaModaBD();
            try
            {
                return Obj.GetProntaModaDT(oSessionManager, ICCONSE);
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
        public DataTable GetProntaModaInv(string connection, int ICCONSE)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            ProntaModaBD Obj = new ProntaModaBD();
            try
            {
                return Obj.GetProntaModaInv(oSessionManager, ICCONSE);
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
        public int UpdateProntaModa(string connection, string ICCODEMP, string ICTIPPRO, string ICCLAVE1, int ICPROVEE, string ICREFERENCIA, int ICCANTIDAD, string ICMONEDA, double ICCOSTO, DateTime ICFECHA, string ICOBSERVACION, string ICCDUSER, string ICESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            ProntaModaBD Obj = new ProntaModaBD();
            int ICCONSE = 0;
            try
            {
                oSessionManager.BeginTransaction();
                ICCONSE = ComunBL.GeneraConsecutivo(null, "CCST", ICCODEMP);
                Obj.InsertProntaModa(oSessionManager, ICCONSE, ICCODEMP, ICTIPPRO, ICCLAVE1, ICPROVEE, ICREFERENCIA, ICCANTIDAD, ICMONEDA, ICCOSTO, ICFECHA, ICOBSERVACION, ICCDUSER, ICESTADO);
                oSessionManager.CommitTranstaction();
                return ICCONSE;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }

    }
}
