using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using XUSS.DAL.Costos;
using DataAccess;
using System.Web;

namespace XUSS.BLL.Costos
{
    [DataObject(true)]
    public class ObsequiosBL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetDescuentosCedula(string connection, string filter)
        {
            ObsequiosBD ObjDB = new ObsequiosBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetObsequiosCedula(oSessionManager, filter);
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
        public static DataTable GetAlmacenes(string connection)
        {
            ObsequiosBD ObjDB = new ObsequiosBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetAlmacenes(oSessionManager);
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

        public string GetNombreTerceros(string connection, string filter)
        {
            ObsequiosBD ObjDB = new ObsequiosBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetNombreTerceros(oSessionManager, filter);
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

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int InsertDecuento(string connection, string BODEGA, double VALOR, string CONDICION_1, DateTime FECHAINI, string TRNOMBRE)
        {
            ObsequiosBD ObjDB = new ObsequiosBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.InsertObsequioCedula(oSessionManager, BODEGA, VALOR, CONDICION_1, FECHAINI, HttpContext.Current.Session["UserLogon"].ToString());
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

        public static int InsertTercero(string connection, string TRCODEMP, int TRCODTER, string TRNOMBRE, string TRNOMBR2, string TRCONTAC, int? TRCODEDI, string TRCODNIT,
                                                        string TRDIGVER, string TRDIRECC, string TRDIREC2, string TRDELEGA, string TRCOLONI, string TRNROTEL, string TRNROFAX,
                                                        string TRPOSTAL, string TRCORREO, string TRCIUDAD, string TRCIUDA2, string TRCDPAIS, string TRMONEDA, string TRIDIOMA,
                                                        string TRBODEGA, string TRTERPAG, string TRMODDES, string TRTERDES, string TRCATEGO, int? TRAGENTE, string TRLISPRE,
                                                        string TRLISPRA, double? TRDESCUE, double? TRCUPOCR, string TRINDCLI, string TRINDPRO, string TRINDSOP, string TRINDEMP,
                                                        string TRINDSOC, string TRINDVEN, string TRCDCLA1, string TRCDCLA2, string TRCDCLA3, string TRCDCLA4, string TRCDCLA5,
                                                        string TRCDCLA6, string TRDTTEC1, string TRDTTEC2, string TRDTTEC3, string TRDTTEC4, double? TRDTTEC5, double? TRDTTEC6,
                                                        int? TRPROGDT, double? TRSALCAR,
                                                        string TRCONVEN, string TROBSERV, string TRREQCLI, string TRRETAUT, string TRRETIVA, string TRRETICA, string TRRETFUE,
                                                        string TRCODALT, double? TRSALVEN, string TRCENCOS, string TRTIPCART, DateTime TRFECNAC, string TRRESPAL, double? TRRESCUP,
                                                        string TRAPELLI, string TRNOMBR3)
        {
            ObsequiosBD ObjDB = new ObsequiosBD();
            SessionManager oSessionManager = new SessionManager(null);
            int ln_ter = 0;
            try
            {
                if (!ObjDB.ExisteTercero(oSessionManager, TRCODNIT))
                {
                    ln_ter = ObjDB.GetContador(oSessionManager, "001", "CODTER");
                    ObjDB.InsertTercero(oSessionManager, TRCODEMP, ln_ter, TRNOMBRE, TRNOMBR2, TRCONTAC, TRCODEDI, TRCODNIT, TRDIGVER, TRDIRECC, TRDIREC2, TRDELEGA, TRCOLONI, TRNROTEL, TRNROFAX,
                                        TRPOSTAL, TRCORREO, TRCIUDAD, TRCIUDA2, TRCDPAIS, TRMONEDA, TRIDIOMA, TRBODEGA, TRTERPAG, TRMODDES, TRTERDES, TRCATEGO, TRAGENTE, TRLISPRE,
                                        TRLISPRA, TRDESCUE, TRCUPOCR, TRINDCLI, TRINDPRO, TRINDSOP, TRINDEMP, TRINDSOC, TRINDVEN, TRCDCLA1, TRCDCLA2, TRCDCLA3, TRCDCLA4, TRCDCLA5,
                                        TRCDCLA6, TRDTTEC1, TRDTTEC2, TRDTTEC3, TRDTTEC4, TRDTTEC5, TRDTTEC6, TRPROGDT, TRSALCAR, TRCONVEN, TROBSERV, TRREQCLI, TRRETAUT, TRRETIVA,
                                        TRRETICA, TRRETFUE, TRCODALT, TRSALVEN, TRCENCOS, TRTIPCART, TRFECNAC, TRRESPAL, TRRESCUP, TRAPELLI, TRNOMBR3);
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                ObjDB = null;
            }
        }
    }
}
