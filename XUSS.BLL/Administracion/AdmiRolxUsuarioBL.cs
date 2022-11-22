using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;
namespace BLL.Administracion
{
	public class AdmiRolxUsuarioBL
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objEntity"></param>
		[DataObjectMethod(DataObjectMethodType.Insert, true)]
		public int Insert(AdmiRolxUsuario objEntity)
		{
			int id = 0;
			AdmiRolxUsuarioDB objDB = new AdmiRolxUsuarioDB();
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
	}
}
