using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using XUSS.DAL.Calidad;
using System.Web;
using DataAccess;

namespace XUSS.BLL.Calidad
{
	[DataObject(true)]
	public class CtrlCalidadBL
	{
		public DataTable GetBodegas(string connection, string filter, int startRowIndex, int maximumRows)
		{
			SessionManager oSessionManager = new SessionManager(connection);
			try 
			{
				return CtrlCalidadBD.GetBodegas(oSessionManager, filter, startRowIndex, maximumRows);
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
		public DataTable GetTipPro(string connection, string filter, int startRowIndex, int maximumRows)
		{
			SessionManager oSessionManager = new SessionManager(connection);

			try
			{
				return CtrlCalidadBD.GetTipPro(oSessionManager, filter, startRowIndex, maximumRows);
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
		public DataTable GetTb_Calidad(string connection, string filter, int startRowIndex, int maximumRows)
		{
			SessionManager oSessionManager = new SessionManager(connection);

			try
			{

				return CtrlCalidadBD.GetTb_Calidad(oSessionManager, filter, startRowIndex, maximumRows);
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
		public DataTable GetTipError(string connection, string filter, int startRowIndex, int maximumRows)
		{
			SessionManager oSessionManager = new SessionManager(connection);

			try
			{
				return CtrlCalidadBD.GetTipError(oSessionManager, filter, startRowIndex, maximumRows);
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
		public DataTable GetParteP(string connection, string filter, int startRowIndex, int maximumRows)
		{
			SessionManager oSessionManager = new SessionManager(connection);

			try
			{
				return CtrlCalidadBD.GetParteP(oSessionManager, filter, startRowIndex, maximumRows);
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
		public DataTable GetTerceros(string connection, string filter, int startRowIndex, int maximumRows)
		{
			SessionManager oSessionManager = new SessionManager(connection);

			try
			{
				return CtrlCalidadBD.GetTerceros(oSessionManager, filter, startRowIndex, maximumRows);
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
		public static int InsertTb_Calidad(string connection, string CA_BODEGA, int CA_NCONSE, int CA_NRODOC,
										   DateTime CA_FECHA, string CA_NOMBRE, string CA_TELEFONO, string CA_CELULAR, string CA_TIPPRO,
										   string CA_CLAVE1,string CA_CLAVE2,string CA_CLAVE3,string CA_CLAVE4, string CA_URECIBE, string CA_NOVEDAD, 
										   string CA_PIEZA, string CA_OBSERVACIONES)
		{
			SessionManager oSessionManager = new SessionManager(connection);
			int i = 0;
			oSessionManager.CreateConnection();
			oSessionManager.BeginTransaction();
			try
			{

				i = CtrlCalidadBD.GeneraConsecutivo(oSessionManager);
				//CtrlCalidadBD.InsertTb_Calidad(oSessionManager, "001", CA_BODEGA, CA_NCONSE, CA_NRODOC, CA_FECHA, CA_NOMBRE, CA_TELEFONO, CA_CELULAR, 
				CtrlCalidadBD.InsertTb_Calidad(oSessionManager, "001", CA_BODEGA, i , CA_NRODOC, CA_FECHA, CA_NOMBRE, CA_TELEFONO, CA_CELULAR, 
												    //CA_TIPPRO, CA_CLAVE1, CA_CLAVE2, CA_CLAVE3, CA_CLAVE4, CA_URECIBE, CA_NOVEDAD, CA_PIEZA, CA_OBSERVACIONES,
													CA_TIPPRO, CA_CLAVE1, CA_CLAVE2, CA_CLAVE3, CA_CLAVE4, HttpContext.Current.Session["UserLogon"].ToString(), CA_NOVEDAD, CA_PIEZA, 
													CA_OBSERVACIONES,HttpContext.Current.Session["UserLogon"].ToString());

				oSessionManager.CommitTranstaction();
				return i; 
			}
			catch (Exception ee)
			{
				oSessionManager.RollBackTransaction();
				throw ee;
			}
			finally
			{
				oSessionManager.CloseConnection();
			}
		}
		public DataTable GetClave(string connection, string TP, int clave)
		{
			SessionManager oSessionManager = new SessionManager(connection);

			try
			{
				return CtrlCalidadBD.GetClave(oSessionManager, TP, clave);
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
		public static string GetNomTerceros(string connection, string nit)
		{
			SessionManager oSessionManager = new SessionManager(connection);

			try
			{
				return CtrlCalidadBD.GetNomTerceros(oSessionManager, nit);
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
		public static string GetTP(string connection, string clave1)
		{
			SessionManager oSessionManager = new SessionManager(connection);

			try
			{
				return CtrlCalidadBD.GetTP(oSessionManager, clave1);
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
