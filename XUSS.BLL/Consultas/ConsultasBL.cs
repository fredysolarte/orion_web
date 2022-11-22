using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using XUSS.DAL.Consultas;
using DataAccess;

namespace XUSS.BLL.Consultas
{
	[DataObject(true)]
	public class ConsultasBL
    {
        //Genericas
        #region
        [DataObjectMethod(DataObjectMethodType.Select, false)]
		public DataSet GetInvVsVtas(string connection, string filter, int startRowIndex, int maximumRows, int servicio)
		{
			ConsultasBD ObjDB = new ConsultasBD();
			SessionManager oSessionManager = new SessionManager(null);
			try
			{
				return ObjDB.GetInvVsVtas(oSessionManager, filter, startRowIndex, maximumRows, servicio);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				ObjDB = null;
				oSessionManager = null;
			}
		}
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public DataTable GetTipPro(string connection, string filter, int startRowIndex, int maximumRows)
		{			
			SessionManager oSessionManager = new SessionManager(null);
			try
			{
				return ConsultasBD.GetTipPro(oSessionManager, filter, startRowIndex, maximumRows);
					//ObjDB.GetTipPro(oSessionManager, filter, startRowIndex, maximumRows);
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
		public DataTable GetBodegas(string connection, string filter, int startRowIndex, int maximumRows)
		{
			SessionManager oSessionManager = new SessionManager(null);
			try
			{
				return ConsultasBD.GetBodegas(oSessionManager, filter, startRowIndex, maximumRows);
				//ObjDB.GetTipPro(oSessionManager, filter, startRowIndex, maximumRows);
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
		public DataTable GetArticulos(string connection, string filter, int startRowIndex, int maximumRows)
		{
			SessionManager oSessionManager = new SessionManager(null);
			try
			{
				return ConsultasBD.GetArticulos(oSessionManager, filter, startRowIndex, maximumRows);
				//ObjDB.GetTipPro(oSessionManager, filter, startRowIndex, maximumRows);
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
		public DataTable GetFoto(string connection, string filter)
		{
			SessionManager oSessionManager = new SessionManager(null);
			try
			{
				return ConsultasBD.GetFoto(oSessionManager, filter);
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
		public DataTable GetTrazaInventarios(string connection,string CODEMP,string TP,string C1,string C2,string C3,string C4,string BD)
		{
			SessionManager oSessionManager = new SessionManager(null);
			try
			{
                return ConsultasBD.GetTrazaInventarios(oSessionManager, CODEMP,TP,C1,C2,C3,C4,BD);
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
        #endregion
        //Consulta de Inventarios
        #region
        public DataTable GetConsulatInventario(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetConsulatInventario(oSessionManager, filter);
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
        public DataTable GetConsulatInventarioLote(string connection, string BLCODEMP, string BLBODEGA, string BLTIPPRO, string BLCLAVE1, string BLCLAVE2, string BLCLAVE3, string BLCLAVE4)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetConsulatInventarioLote(oSessionManager, BLCODEMP, BLBODEGA, BLTIPPRO, BLCLAVE1, BLCLAVE2, BLCLAVE3, BLCLAVE4);
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
        public DataTable GetConsulatInventarioElemento(string connection, string inLlave)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetConsulatInventarioElemento(oSessionManager, inLlave);
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
        public DataTable GetConsultaMovimientos(string connection, string filter, DateTime inFecIni, DateTime inFecFin)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetConsultaMovimientos(oSessionManager, filter, inFecIni, inFecFin);
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
        #endregion
        //Consulta de Ventas 
        #region
        public static DataTable GetVentasDetalle(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetVentasDetalle(oSessionManager, filter);
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
        public static DataTable GetCartera(string connection, string filter,string inMes,string inAno)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetCartera(oSessionManager, filter, inMes, inAno);
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
        public static DataTable GetRecaudo(string connection, string filter, string inMes, string inAno)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetRecaudo(oSessionManager, filter, inMes, inAno);
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
        public static DataTable GetVentasGrafica(string connection, string filter, string inMes, string inAno)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetVentasGrafica(oSessionManager, filter, inMes, inAno);
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
        public static DataTable GetClientesxVendedor(string connection, string filter,int inMes, int inAno)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetClientesxVendedor(oSessionManager, filter,inMes,inAno);
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
        public static DataTable GetConsultaPagos(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetConsultaPagos(oSessionManager, filter);
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
        #endregion
        //DashBoard
        #region
        public DataTable GetVentasHDAgrupadasxMes(string connection, int inYear)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetVentasHDAgrupadasxMes(oSessionManager, inYear);
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
        public DataTable GetVentasTipoxMes(string connection, int inMonth, int inYear)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetVentasTipoxMes(oSessionManager, inMonth, inYear);
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
        public DataTable GetVentasVendedorxMes(string connection, int inMonth, int inYear)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetVentasVendedorxMes(oSessionManager, inMonth, inYear);
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
        #endregion
        //Hoja Kardex
        #region
        public DataTable GetAnosMov(string connection)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {                
                return ConsultasBD.GetAnosMov(oSessionManager);
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
        public DataTable GetHojaKardex(string connection,string inFiltro,string inBodega,DateTime inFechaIni, DateTime inFechaFin)
        {            
            SessionManager oSessionManager = new SessionManager(null);
            try
            {                                                
                return ConsultasBD.GetHojaKardex(oSessionManager, inFiltro, inBodega,inFechaIni,inFechaFin);
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
        #endregion
        //Cartera
        #region
        public DataTable GetCartera(string connection, DateTime inFecha,string inFiltro)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetCartera(oSessionManager, inFecha, inFiltro);
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
        public DataTable GetCarteraxCliente(string connection, DateTime inFecha, string inFiltro)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetCarteraxCliente(oSessionManager, inFecha, inFiltro);
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
        public DataTable GetRecaudoxFactura(string connection, int HDNROFAC, string HDTIPFAC, string HDCODEMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ConsultasBD.GetRecaudoxFactura(oSessionManager, HDNROFAC, HDTIPFAC, HDCODEMP);
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
        #endregion
        //Recaduos
        #region
        public DataTable GetConsultaRecaudos(string connection, string filter, params object[] paramtervalues)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetConsultaRecaudos(oSessionManager, filter, paramtervalues);
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
        public DataTable GetConsultaDetalleRecaudos(string connection, int RC_NRORECIBO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetConsultaDetalleRecaudos(oSessionManager, RC_NRORECIBO);
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

        #endregion
        //Pedidos
        #region
        public DataTable GetTrazePedidos(string connection, string inFilter)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetTrazePedidos(oSessionManager, inFilter);
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
        #endregion
        //Contabilidad
        #region
        public DataTable GetMovimientosMes(string connection, string inFilter)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultasBD.GetMovimientosMes(oSessionManager, inFilter);
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
        #endregion
    }
}
