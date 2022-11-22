using System;
using System.ComponentModel;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;

namespace BLL.Administracion
{
	[DataObject(true)]
	public class AdmiTipoblobBL
	{
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public AdmiTipoblob GetById(string connection, int idTipoBlob)
		{
			AdmiTipoblobDB objDB = new AdmiTipoblobDB();
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				return objDB.GetById(oSessionManager, idTipoBlob);
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
		public int Insert(AdmiTipoblob objEntity)
		{
			AdmiTipoblobDB objDB = new AdmiTipoblobDB();
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
				//connection = null;
			}
			return retorno;
		}
	}
}