using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;

namespace BLL.Administracion
{
    /// <summary>
    /// 
    /// </summary>
    [DataObject(true)]
    public class AdmiArbolOpcionBL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="filter"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AdmiArbolOpcion> GetListBySystemAndModule(string connection, string filter, int startRowIndex, int maximumRows, int systemId, int moduleId)
        {
            AdmiArbolOpcionDB objDB = new AdmiArbolOpcionDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.CreateConnection();
                return objDB.GetListBySystemAndModule(oSessionManager, filter, startRowIndex, maximumRows, systemId, moduleId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AdmiArbolOpcion> GetListCheckedBySystemModuleAndRol(string connection, string filter, int startRowIndex, int maximumRows, int systemId, int moduleId, int rolId)
        {
            AdmiArbolOpcionDB objDB = new AdmiArbolOpcionDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.CreateConnection();
                return objDB.GetListCheckedBySystemModuleAndRol(oSessionManager, filter, startRowIndex, maximumRows, systemId, moduleId, rolId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AdmiArbolOpcion> GetListCheckedByRolesUserAndSystem(string connection, string filter, int startRowIndex, int maximumRows, string rolesId, string userId, int systemId)
        {
            AdmiArbolOpcionDB objDB = new AdmiArbolOpcionDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.CreateConnection();
                return objDB.GetListCheckedByRoles(oSessionManager, filter, startRowIndex, maximumRows, rolesId, userId, systemId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetOptionsByUserAndModule(string connection, string filter, int startRowIndex, int maximumRows, string userLogon, int moduleId, int sistemaId)
        {
            AdmiArbolOpcionDB objDB = new AdmiArbolOpcionDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.CreateConnection();
                return objDB.GetOptionsByUserAndModule(oSessionManager, filter, startRowIndex, maximumRows, userLogon, moduleId, sistemaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetOptionsByUserAndModule(string connection, string userLogon, int moduleId, int sistemaId, int parentId)
        {
            AdmiArbolOpcionDB objDB = new AdmiArbolOpcionDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.CreateConnection();
                return objDB.GetOptionsByUserAndModule(oSessionManager, userLogon, moduleId, sistemaId, parentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetOptionsByURLForm(string connection, string filter, int startRowIndex, int maximumRows, string urlForm)
        {
            AdmiArbolOpcionDB objDB = new AdmiArbolOpcionDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.CreateConnection();
                return objDB.GetOptionsByURLForm(oSessionManager, filter, startRowIndex, maximumRows, urlForm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }
    }
}
