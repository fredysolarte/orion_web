using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;
namespace BLL.Administracion
{
	/// <summary>
	/// 
	/// </summary>
	[DataObject(true)]
	public class AdmiRolBL
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="filter"></param>
		/// <param name="startRowIndex"></param>
		/// <param name="maximumRows"></param>
		/// <returns></returns>
		public List<AdmiRol> GetList(string connection, string filter, int startRowIndex, int maximumRows)
		{
			AdmiRolDB objDB = new AdmiRolDB();
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="objEntity"></param>
		[DataObjectMethod(DataObjectMethodType.Insert, false)]
		public int Insert(AdmiRol objEntity)
		{
			int id = 0;
			AdmiRolDB objDB = new AdmiRolDB();
			SessionManager oSessionManager = new SessionManager("");
			try
			{
				objEntity.LogsUsuario = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
				objEntity.LogsFecha = DateTime.Now;
				oSessionManager.CreateConnection();
				oSessionManager.BeginTransaction();
				id = objDB.Add(oSessionManager, objEntity);
				oSessionManager.CommitTranstaction();
				return id;
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="objEntity"></param>
		[DataObjectMethod(DataObjectMethodType.Update, false)]
		public void Update(AdmiRol objEntity)
		{
			AdmiRolDB objDB = new AdmiRolDB();
			SessionManager oSessionManager = new SessionManager("");
			try
			{
				objEntity.LogsUsuario = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
				objEntity.LogsFecha = DateTime.Now;
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="objEntity"></param>
		[DataObjectMethod(DataObjectMethodType.Delete, false)]
		public void Delete(AdmiRol objEntity)
		{
			AdmiRolDB objDB = new AdmiRolDB();
			SessionManager oSessionManager = new SessionManager("");
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="olEntity"></param>
		public void UpdateConfiguration(string connection, List<AdmiArbolOpcion> olEntity, int idRol)
		{
			AdmiRolDB objDB = new AdmiRolDB();
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				int userId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
				oSessionManager.CreateConnection();
				oSessionManager.BeginTransaction();
				objDB.UpdateConfiguration(oSessionManager, olEntity, idRol, userId);
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

		public DataTable GetDataTableByUserId(string connection, string filter, int startRowIndex, int maximumRows, string userId)
		{
			AdmiRolDB objDB = new AdmiRolDB();
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				return objDB.GetDataTableByUserId(oSessionManager, filter, startRowIndex, maximumRows, userId);
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
	}
}
