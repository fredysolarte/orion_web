using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using XUSS.DAL.Costos;
using System.Globalization;
using DataAccess;

namespace XUSS.BLL.Costos
{
	[DataObject(true)]
	public class CostosBL
	{
		public DataTable GetCostosIng(string connection, string filter, int startRowIndex, int maximumRows)
		{
			SessionManager oSessionManager = new SessionManager(connection);

			try
			{
				return CostosBD.GetCostosIng(oSessionManager, filter, startRowIndex, maximumRows);
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
		public DataTable GetProveedor(string connection, string filter, int startRowIndex, int maximumRows)
		{
			SessionManager oSessionManager = new SessionManager(connection);

			try
			{
				return CostosBD.GetProveedor(oSessionManager, filter, startRowIndex, maximumRows);
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
				return CostosBD.GetTipPro(oSessionManager, filter, startRowIndex, maximumRows);
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
		public DataTable GetMarca(string connection, string filter, int startRowIndex, int maximumRows)
		{
			SessionManager oSessionManager = new SessionManager(connection);

			try
			{
				return CostosBD.GetMarca(oSessionManager, filter, startRowIndex, maximumRows);
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
		[DataObjectMethod(DataObjectMethodType.Update, false)]
		public static int UpdateCostosIng(string connection, string ICCODEMP, int ICCONSE, int ICCONSEINT, int ICPROVEE, string ICMARCA,
										  string ICTIPPRO, string ICREFERENCIA, string ICCLAVE1, int ICCANTIDAD, double ICTASA, double ICCOSTOUUSD,
										  double ICTOTALUSD, double ICCOSTOUND, double ICCOSTOTOT, double ICCOSTOSVT, DateTime ICFECHA, DateTime ICFECING,
										  string ICCDUSER, string ICESTADO)
		{
			SessionManager oSessionManager = new SessionManager(connection);
			int retorno = -1;
			DateTime Fecha = System.DateTime.Today;
			oSessionManager.CreateConnection();
			oSessionManager.BeginTransaction();
			try
			{

				retorno = CostosBD.UpdateCostosIng(oSessionManager, ICCODEMP, ICCONSE, ICCONSEINT, ICPROVEE, ICMARCA,
										   ICTIPPRO, ICREFERENCIA, ICCLAVE1, ICCANTIDAD, ICTASA, ICCOSTOUUSD,
										   ICTOTALUSD, ICCOSTOUND, ICCOSTOTOT, ICCOSTOSVT, ICFECHA, ICFECING,
										   ICCDUSER, ICESTADO);

				if (CostosBD.GetExistePrecioDT(oSessionManager, "001", "LBASE07", ICTIPPRO, ICCLAVE1, ".", ".", ".") == 0)
				{
					retorno = CostosBD.InsertListaPreciosDT(oSessionManager, "001", "LBASE07", Fecha, ICTIPPRO, ICCLAVE1, ".", ".", ".", ".", "1", "UN", ICCOSTOSVT, 0, "AC", ".",
															ICCDUSER, Fecha, Fecha);
				}
				else {
					retorno = CostosBD.UpdateListaPreciosDT(oSessionManager, ICCODEMP, "LBASE07", ICTIPPRO, ICCLAVE1, ".", ".", ".", ICCDUSER, ICCOSTOSVT);
				}

				retorno = CostosBD.UpdateEstadoCosto(oSessionManager, ICCONSE, ICCONSEINT);


				oSessionManager.CommitTranstaction();

				return retorno;
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
		public DataTable GetEstados()
		{
			try {
				return CostosBD.GetEstados();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally { }

		}
        public static DataTable GetDatosTotales(string connection, int ICCONSE)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {                 
                return CostosBD.GetDatosTotales(oSessionManager, ICCONSE);
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
        public static DataTable GetDatosPrecosteo(string connection)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return CostosBD.GetDatosPrecosteo(oSessionManager);
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
