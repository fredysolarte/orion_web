using System;
using System.Collections.Generic;
using System.ComponentModel;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;

namespace BLL.Administracion
{
	[DataObject(true)]
	public class AdmiBlobBL
	{
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<AdmiBlob> GetByIdTipo(string connection, string filter, int startRowIndex, int maximumRows, int idTipo)
		{
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				return AdmiBlobDB.GetByIdTipo(oSessionManager, filter, startRowIndex, maximumRows, idTipo);
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
		public AdmiBlob GetById(string connection, string filter, int startRowIndex, int maximumRows, int id)
		{
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				return AdmiBlobDB.GetById(oSessionManager, filter, startRowIndex, maximumRows, id);
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
		public int Insert(AdmiBlob objEntity)
		{
			AdmiBlobDB objDB = new AdmiBlobDB();
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
	}
}