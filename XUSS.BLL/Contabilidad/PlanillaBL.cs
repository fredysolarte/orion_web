using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using XUSS.DAL.Contabilidad;
using DataAccess;

namespace XUSS.BLL.Contabilidad
{
    [DataObject(true)]
    public class PlanillaBL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetPuc(string connection, string filter)
        {
            PlanillaBD Obj = new PlanillaBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return Obj.GetPuc(oSessionManager, filter);
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
       
        public int InsertPUC(string connection, int PC_PARENT, string PC_EMPRESA, string PC_CODIGO, string PC_NOMBRE, string PC_NATURALEZA, string PC_TIPO, string PC_ESTADO, string PC_USUARIO)
        {
            PlanillaBD Obj = new PlanillaBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return Obj.InsertPUC(oSessionManager, PC_PARENT, PC_EMPRESA, PC_CODIGO, PC_NOMBRE, PC_NATURALEZA, PC_TIPO, PC_ESTADO, PC_USUARIO);
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

        public int UpdatePUC(string connection, int PC_ID,string PC_EMPRESA, string PC_CODIGO, string PC_NOMBRE, string PC_NATURALEZA, string PC_TIPO, string PC_ESTADO, string PC_USUARIO)
        {
            PlanillaBD Obj = new PlanillaBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return Obj.UpdatePUC(oSessionManager, PC_ID, PC_EMPRESA, PC_CODIGO, PC_NOMBRE, PC_NATURALEZA, PC_TIPO, PC_ESTADO, PC_USUARIO);
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
        public static DataTable GetAutoComplete(string connection,string filtro)
        {
            SessionManager oSessionManager = new SessionManager(null);
            PlanillaBD Obj = new PlanillaBD();
            try
            {
                return Obj.GetAutoComplete(oSessionManager, filtro);
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
        public static DataTable GetPuc(string connection)
        {
            PlanillaBD Obj = new PlanillaBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return Obj.GetPuc(oSessionManager);
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
    }
}
