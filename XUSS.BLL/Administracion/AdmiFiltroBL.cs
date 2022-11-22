using System;
using System.Collections.Generic;
using System.ComponentModel;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;

namespace BLL.Administracion
{
	[DataObject(true)]
	public class AdmiFiltroBL
	{
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<AdmiFiltro> GetByURLForm(string connection, string filter, int startRowIndex, int maximumRows, string urlForm)
		{
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				return AdmiFiltroDB.GetByURLForm(oSessionManager, filter, startRowIndex, maximumRows, urlForm);
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
	}
}