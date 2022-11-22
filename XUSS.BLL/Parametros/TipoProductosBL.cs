using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Parametros
{
    public class TipoProductosBL
    {
        public DataTable GetTipoProducto(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TipoProductosBD.GetTipoProducto(oSessionManager, filter, startRowIndex, maximumRows);
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
        public IDataReader GetTipoProductoxBodegaR(string connection, string CODEMP, string BODEGA, string TP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager,  CODEMP,  BODEGA,  TP);
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
        public IDataReader GetTipoProductoxBodegaTFR(string connection, string CODEMP, string TFTIPFAC, string TP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TipoProductosBD.GetTipoProductoxBodegaTFR(oSessionManager, CODEMP, TFTIPFAC, TP);
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
        public int InsertTipoProducto(string connection, string TACODEMP, string TATIPPRO, string TANOMBRE, int TACLAVES, string TACLAPRO, string TADSCLA1, string TADSCLA2, string TACTLSE2, string TADSCLA3, string TACTLSE3, string TADSCLA4, string TACTLSE4,
                                                                            string TACDCLA1, string TACDCLA2, string TACDCLA3, string TACDCLA4, string TACDCLA5, string TACDCLA6, string TACDCLA7, string TACDCLA8, string TACDCLA9, string TACDCLA10, string TADTTEC1, string TADTTEC2, string TADTTEC3, string TADTTEC4, string TADTTEC5, string TADTTEC6, string TADTTEC7, string TADTTEC8, string TADTTEC9, string TADTTEC10,
                                                                            string TADTLOT1, string TADTLOT2, string TADTLOT3, string TADTLOT4, string TADTLOT5, string TADTLOT6, string TAUMPESO, string TAUMANCH, string TAUMLARG, string TADTELE1, string TADTELE2, string TADTELE3,
                                                                            string TADTELE4, string TADTELE5, string TADTELE6, string TAESTADO, string TACAUSAE, string TANMUSER, int? TACLAFLO, string TAINDALT, int? TACLATEC, int? TANROBAR, string TAFRMBAR, string TACONSEC,
                                                                            string TACONCAT, string TACALIFI, string TASUFCON, int? TACNTCLA, string TAREFINI, string TAVENTA, string TAAUTINC, double TATOLERA, string TAREPORTE)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TipoProductosBD.InsertTipoProducto(oSessionManager, TACODEMP, TATIPPRO, TANOMBRE, TACLAVES, TACLAPRO, TADSCLA1, TADSCLA2, TACTLSE2, TADSCLA3, TACTLSE3, TADSCLA4, TACTLSE4,
                                                                           TACDCLA1, TACDCLA2, TACDCLA3, TACDCLA4, TACDCLA5, TACDCLA6, TACDCLA7, TACDCLA8, TACDCLA9, TACDCLA10, TADTTEC1, TADTTEC2, TADTTEC3, TADTTEC4, TADTTEC5, TADTTEC6, TADTTEC7, TADTTEC8, TADTTEC9, TADTTEC10,
                                                                           TADTLOT1, TADTLOT2, TADTLOT3, TADTLOT4, TADTLOT5, TADTLOT6, TAUMPESO, TAUMANCH, TAUMLARG, TADTELE1, TADTELE2, TADTELE3,
                                                                           TADTELE4, TADTELE5, TADTELE6, TAESTADO, TACAUSAE, TANMUSER, TACLAFLO, TAINDALT, TACLATEC, TANROBAR, TAFRMBAR, TACONSEC,
                                                                           TACONCAT, TACALIFI, TASUFCON, TACNTCLA, TAREFINI, TAVENTA, TAAUTINC,  TATOLERA, TAREPORTE);
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
        public int UpdateTipoProducto(string connection, string TACODEMP, string TATIPPRO, string TANOMBRE, int TACLAVES, string TACLAPRO, string TADSCLA1, string TADSCLA2, string TACTLSE2, string TADSCLA3, string TACTLSE3, string TADSCLA4, string TACTLSE4,
                                                                            string TACDCLA1, string TACDCLA2, string TACDCLA3, string TACDCLA4, string TACDCLA5, string TACDCLA6, string TACDCLA7, string TACDCLA8, string TACDCLA9, string TACDCLA10, string TADTTEC1, string TADTTEC2, string TADTTEC3, string TADTTEC4, string TADTTEC5, string TADTTEC6, string TADTTEC7, string TADTTEC8, string TADTTEC9, string TADTTEC10,
                                                                            string TADTLOT1, string TADTLOT2, string TADTLOT3, string TADTLOT4, string TADTLOT5, string TADTLOT6, string TAUMPESO, string TAUMANCH, string TAUMLARG, string TADTELE1, string TADTELE2, string TADTELE3,
                                                                            string TADTELE4, string TADTELE5, string TADTELE6, string TAESTADO, string TACAUSAE, string TANMUSER, int? TACLAFLO, string TAINDALT, int? TACLATEC, int? TANROBAR, string TAFRMBAR, string TACONSEC,
                                                                            string TACONCAT, string TACALIFI, string TASUFCON, int? TACNTCLA, string TAREFINI, string TAVENTA, string TAAUTINC, double TATOLERA, string TAREPORTE)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TipoProductosBD.UpdateTipoProducto(oSessionManager, TACODEMP, TATIPPRO, TANOMBRE, TACLAVES, TACLAPRO, TADSCLA1, TADSCLA2, TACTLSE2, TADSCLA3, TACTLSE3, TADSCLA4, TACTLSE4,
                                                                           TACDCLA1, TACDCLA2, TACDCLA3, TACDCLA4, TACDCLA5, TACDCLA6, TADTTEC1, TADTTEC2, TADTTEC3, TADTTEC4, TADTTEC5, TADTTEC6,
                                                                           TADTLOT1, TADTLOT2, TADTLOT3, TADTLOT4, TADTLOT5, TADTLOT6, TAUMPESO, TAUMANCH, TAUMLARG, TADTELE1, TADTELE2, TADTELE3,
                                                                           TADTELE4, TADTELE5, TADTELE6, TAESTADO, TACAUSAE, TANMUSER, TACLAFLO, TAINDALT, TACLATEC, TANROBAR, TAFRMBAR, TACONSEC,
                                                                           TACONCAT, TACALIFI, TASUFCON, TACNTCLA, TAREFINI, TAVENTA, TAAUTINC, TATOLERA, TAREPORTE,
                                                                           TACDCLA7, TACDCLA8, TACDCLA9, TACDCLA10, TADTTEC7, TADTTEC8, TADTTEC9, TADTTEC10);
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
        public DataTable GetNivelesTP(string connection, string TATIPPRO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TipoProductosBD.GetNivelesTP(oSessionManager, TATIPPRO);
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
