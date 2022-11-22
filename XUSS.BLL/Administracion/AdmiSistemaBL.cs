using System;
using System.Collections.Generic;
using System.ComponentModel;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;

namespace BLL.Administracion
{
	[DataObject(true)]
	public class AdmiSistemaBL
	{
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<AdmiSistema> GetList(string connection, string filter, int startRowIndex, int maximumRows)
		{
			AdmiSistemaDB objDB = new AdmiSistemaDB();
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				return objDB.GetAllList(oSessionManager, filter, startRowIndex, maximumRows);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				objDB = null;
				oSessionManager = null;
			}
		}

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Insert(AdmiSistema objEntity)
        {
            AdmiSistemaDB objDB = new AdmiSistemaDB();
            SessionManager oSessionManager = new SessionManager(null);
            int retorno = -1;
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                retorno = objDB.Add(oSessionManager, objEntity);
                oSessionManager.CommitTranstaction();
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
            return retorno;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Delete(AdmiSistema objEntity)
        {
            AdmiSistemaDB objDB = new AdmiSistemaDB();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                objDB.Delete(oSessionManager, objEntity);
                oSessionManager.CommitTranstaction();
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Update(AdmiSistema objEntity)
        {
            AdmiSistemaDB objDB = new AdmiSistemaDB();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                objDB.Update(oSessionManager, objEntity);
                oSessionManager.CommitTranstaction();
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
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