using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using XUSS.DAL.Costos;
using DataAccess;
using System.Web;
using XUSS.BLL.Comun;

namespace XUSS.BLL.Costos
{
    [DataObject(true)]
    public class DecuentoCedulaBL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetDescuentosCedula(string connection, string filter)
        {
            DecuentoCedulaBD ObjDB = new DecuentoCedulaBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetDescuentosCedula(oSessionManager, filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjDB = null;
                oSessionManager = null;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetAlmacenes(string connection)
        {
            DecuentoCedulaBD ObjDB = new DecuentoCedulaBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetAlmacenes(oSessionManager);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjDB = null;
                oSessionManager = null;
            }
        }

        public IDataReader GetNombreTerceros(string connection,string filter)
        {
            DecuentoCedulaBD ObjDB = new DecuentoCedulaBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetNombreTerceros(oSessionManager,filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjDB = null;
                oSessionManager = null;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int InsertDecuento(string connection, string BODEGA, double VALOR, string CONDICION_1, DateTime FECHAINI, DateTime FECHAFIN, string TRNOMBRE, string TRAPELLI, string TRCONTAC, string CONDICION_2)
        {
            DecuentoCedulaBD ObjDB = new DecuentoCedulaBD();            
            SessionManager oSessionManager = new SessionManager(null);
            try
            {                                                      
                return ObjDB.InsertDecuento(oSessionManager, BODEGA, VALOR, CONDICION_1, FECHAINI, FECHAFIN , HttpContext.Current.Session["UserLogon"].ToString(), CONDICION_2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjDB = null;                
                oSessionManager = null;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public int UpdateDecuento(string connection, int ID,string BODEGA, double VALOR, string CONDICION_1, DateTime FECHAINI, DateTime FECHAFIN, string TRNOMBRE,string TRAPELLI, string TRCONTAC, string CONDICION_2,string ESTADO)
        {
            DecuentoCedulaBD ObjDB = new DecuentoCedulaBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.UpdateDecuento(oSessionManager, ID, BODEGA, VALOR, CONDICION_1, FECHAINI, FECHAFIN, HttpContext.Current.Session["UserLogon"].ToString(), CONDICION_2, ESTADO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjDB = null;
                oSessionManager = null;
            }
        }        
    }
}
