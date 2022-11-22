using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Parametros
{
    public class TPedidoBL
    {
        public DataTable GetTipPed(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return TPedidoBD.GetTipPed(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetBodegasxPedido(string connection, string BDCODEMP, string PTTIPED)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return TPedidoBD.GetBodegasxPedido(oSessionManager, BDCODEMP, PTTIPED);
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
        public int InsertTipoPedido(string connection, string PTCODEMP, string PTTIPPED, string PTNOMBRE, string PTDESCRI, string PTBODEGA, string PTTERPAG, string PTMODDES, string PTTERDES, string PTMONEDA, string PTIDIOMA, string PTLISPRE,
                                          string PTCDIMPF, string PTCDTRAN, string PTTIPFAC, int? PTLSTSEP, string PTCDCLA1, string PTCDCLA2, string PTCDCLA3, string PTCDCLA4, string PTCDCLA5, string PTCDCLA6, string PTDTTEC1, string PTDTTEC2, string PTDTTEC3,
                                          string PTDTTEC4, string PTDTTEC5, string PTDTTEC6, string PTCDCLD1, string PTCDCLD2, string PTCDCLD3, string PTCDCLD4, string PTCDCLD5, string PTCDCLD6, string PTDTTCD1, string PTDTTCD2, string PTDTTCD3, string PTDTTCD4,
                                          string PTDTTCD5, string PTDTTCD6, string PTESTADO, string PTCAUSAE, string PTNMUSER, string PTCOTIZA, object tbBodegas)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {

                oSessionManager.BeginTransaction();

                TPedidoBD.InsertTipoPedido(oSessionManager, PTCODEMP, PTTIPPED, PTNOMBRE, PTDESCRI, PTBODEGA, PTTERPAG, PTMODDES, PTTERDES, PTMONEDA, PTIDIOMA, PTLISPRE,
                                          PTCDIMPF, PTCDTRAN, PTTIPFAC, PTLSTSEP, PTCDCLA1, PTCDCLA2, PTCDCLA3, PTCDCLA4, PTCDCLA5, PTCDCLA6, PTDTTEC1, PTDTTEC2, PTDTTEC3,
                                          PTDTTEC4, PTDTTEC5, PTDTTEC6, PTCDCLD1, PTCDCLD2, PTCDCLD3, PTCDCLD4, PTCDCLD5, PTCDCLD6, PTDTTCD1, PTDTTCD2, PTDTTCD3, PTDTTCD4,
                                          PTDTTCD5, PTDTTCD6, PTESTADO, PTCAUSAE, PTNMUSER, PTCOTIZA);
                
                TPedidoBD.DeleteBodegaxTPedido(oSessionManager, PTCODEMP, PTTIPPED);
                foreach (DataRow rw in ((DataTable)tbBodegas).Rows)
                {
                    if (Convert.ToString(rw["CHK"]) == "S")
                        TPedidoBD.InsertBodegaxTPedido(oSessionManager, PTCODEMP, Convert.ToString(rw["BDBODEGA"]), PTTIPPED, Convert.ToInt32(rw["BPORDEN"]));
                }
                oSessionManager.CommitTranstaction();
                return 0;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public int UpdateTipoPedido(string connection, string PTCODEMP, string original_PTTIPPED, string PTNOMBRE, string PTDESCRI, string PTBODEGA, string PTTERPAG, string PTMODDES, string PTTERDES, string PTMONEDA, string PTIDIOMA, string PTLISPRE,
                                          string PTCDIMPF, string PTCDTRAN, string PTTIPFAC, int? PTLSTSEP, string PTCDCLA1, string PTCDCLA2, string PTCDCLA3, string PTCDCLA4, string PTCDCLA5, string PTCDCLA6, string PTDTTEC1, string PTDTTEC2, string PTDTTEC3,
                                          string PTDTTEC4, string PTDTTEC5, string PTDTTEC6, string PTCDCLD1, string PTCDCLD2, string PTCDCLD3, string PTCDCLD4, string PTCDCLD5, string PTCDCLD6, string PTDTTCD1, string PTDTTCD2, string PTDTTCD3, string PTDTTCD4,
                                          string PTDTTCD5, string PTDTTCD6, string PTESTADO, string PTCAUSAE, string PTNMUSER, string PTCOTIZA, object tbBodegas)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();

                TPedidoBD.UpdateTipoPedido(oSessionManager, PTCODEMP, original_PTTIPPED, PTNOMBRE, PTDESCRI, PTBODEGA, PTTERPAG, PTMODDES, PTTERDES, PTMONEDA, PTIDIOMA, PTLISPRE,
                                          PTCDIMPF, PTCDTRAN, PTTIPFAC, PTLSTSEP, PTCDCLA1, PTCDCLA2, PTCDCLA3, PTCDCLA4, PTCDCLA5, PTCDCLA6, PTDTTEC1, PTDTTEC2, PTDTTEC3,
                                          PTDTTEC4, PTDTTEC5, PTDTTEC6, PTCDCLD1, PTCDCLD2, PTCDCLD3, PTCDCLD4, PTCDCLD5, PTCDCLD6, PTDTTCD1, PTDTTCD2, PTDTTCD3, PTDTTCD4,
                                          PTDTTCD5, PTDTTCD6, PTESTADO, PTCAUSAE, PTNMUSER, PTCOTIZA);

                TPedidoBD.DeleteBodegaxTPedido(oSessionManager, PTCODEMP, original_PTTIPPED);
                foreach (DataRow rw in ((DataTable)tbBodegas).Rows)
                {
                    if (Convert.ToString(rw["CHK"]) == "S")
                        TPedidoBD.InsertBodegaxTPedido(oSessionManager, PTCODEMP, Convert.ToString(rw["BDBODEGA"]), original_PTTIPPED, Convert.ToInt32(rw["BPORDEN"]));
                }

                oSessionManager.CommitTranstaction();
                return 1;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
    }
}
