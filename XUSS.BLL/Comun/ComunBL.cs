using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Comun;

namespace XUSS.BLL.Comun
{
    public class ComunBL
    {
        public DataTable GetProveedores(string connection, string filter)
        { 
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return ComunBD.GetProveedores(oSessionManager, filter);
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
        public DataTable GetLstBodegas(string connection, string ALMACEN)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GetLstBodegas(oSessionManager, ALMACEN);
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
        public DataTable GetTiposProducto(string connection)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GetTiposProducto(oSessionManager);
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
        public IDataReader GetCaracteristicaTP(string connection, string CODEMP ,string TP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GetCaracteristicaTP(oSessionManager,CODEMP,TP);
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
        public static int InsertTercero(string connection, string TRCODEMP, int TRCODTER, string TRNOMBRE, string TRNOMBR2, string TRCONTAC, int? TRCODEDI, string TRCODNIT,
                                                        string TRDIGVER, string TRDIRECC, string TRDIREC2, string TRDELEGA, string TRCOLONI, string TRNROTEL, string TRNROFAX,
                                                        string TRPOSTAL, string TRCORREO, string TRCIUDAD, string TRCIUDA2, string TRCDPAIS, string TRMONEDA, string TRIDIOMA,
                                                        string TRBODEGA, string TRTERPAG, string TRMODDES, string TRTERDES, string TRCATEGO, int? TRAGENTE, string TRLISPRE,
                                                        string TRLISPRA, double? TRDESCUE, double? TRCUPOCR, string TRINDCLI, string TRINDPRO, string TRINDSOP, string TRINDEMP,
                                                        string TRINDSOC, string TRINDVEN, string TRCDCLA1, string TRCDCLA2, string TRCDCLA3, string TRCDCLA4, string TRCDCLA5,
                                                        string TRCDCLA6, string TRDTTEC1, string TRDTTEC2, string TRDTTEC3, string TRDTTEC4, double? TRDTTEC5, double? TRDTTEC6,
                                                        int? TRPROGDT, double? TRSALCAR,
                                                        string TRCONVEN, string TROBSERV, string TRREQCLI, string TRRETAUT, string TRRETIVA, string TRRETICA, string TRRETFUE,
                                                        string TRCODALT, double? TRSALVEN, string TRCENCOS, string TRTIPCART, DateTime? TRFECNAC, string TRRESPAL, double? TRRESCUP,
                                                        string TRAPELLI, string TRNOMBR3, Boolean INDCUM = false)
        {            
            SessionManager oSessionManager = new SessionManager(null);
            oSessionManager.BeginTransaction();
            int ln_ter = 0;
            try
            {
                if (!ComunBD.ExisteTercero(oSessionManager, TRCODNIT))
                {
                    ln_ter = ComunBD.GeneraConsecutivo(oSessionManager, "CODTER");
                    ComunBD.InsertTercero(oSessionManager, TRCODEMP, ln_ter, TRNOMBRE, TRNOMBR2, TRCONTAC, TRCODEDI, TRCODNIT, TRDIGVER, TRDIRECC, TRDIREC2, TRDELEGA, TRCOLONI, TRNROTEL, TRNROFAX,
                                        TRPOSTAL, TRCORREO, TRCIUDAD, TRCIUDA2, TRCDPAIS, TRMONEDA, TRIDIOMA, TRBODEGA, TRTERPAG, TRMODDES, TRTERDES, TRCATEGO, TRAGENTE, TRLISPRE,
                                        TRLISPRA, TRDESCUE, TRCUPOCR, TRINDCLI, TRINDPRO, TRINDSOP, TRINDEMP, TRINDSOC, TRINDVEN, TRCDCLA1, TRCDCLA2, TRCDCLA3, TRCDCLA4, TRCDCLA5,
                                        TRCDCLA6, TRDTTEC1, TRDTTEC2, TRDTTEC3, TRDTTEC4, TRDTTEC5, TRDTTEC6, TRPROGDT, TRSALCAR, TRCONVEN, TROBSERV, TRREQCLI, TRRETAUT, TRRETIVA,
                                        TRRETICA, TRRETFUE, TRCODALT, TRSALVEN, TRCENCOS, TRTIPCART, TRFECNAC, TRRESPAL, TRRESCUP, TRAPELLI, TRNOMBR3);
                }
                else
                {
                    if (INDCUM)
                    {
                        ComunBD.UpdateTerceros(oSessionManager, TRCODEMP, TRCODNIT, TRFECNAC, TRAPELLI, TRNOMBRE, TRCORREO, TRDTTEC4, TRNROTEL);
                    }
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
        public static Boolean GetExisteTercero(string TRCODNIT)
        {
            SessionManager oSessionManager = new SessionManager(null);            
            try
            {

                return (ComunBD.ExisteTercero(oSessionManager, TRCODNIT) );                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IDataReader GetInformacionTercero(string TRCODNIT)
        {
            SessionManager oSessionManager = new SessionManager(null);            
            try
            {
                return ComunBD.GetInformacionTercero(oSessionManager, TRCODNIT);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static string GetNombreProveedor(string connection, string codigo)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return ComunBD.GetNombreProveedor(oSessionManager, codigo);
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
        public static Boolean ExisteDescuentoHappy(string TRCODNIT)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {

                return (ComunBD.ExisteDescuentoHappy(oSessionManager, TRCODNIT));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IDataReader GetCiudades(string connection, string InCodemp, string InPais)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GetCiudades(oSessionManager,InPais,InCodemp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
        public DataTable GetTerminosPago(string connection, string InCodemp)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GetTerminosPago(oSessionManager, connection);                
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
        public static string GetMoneda(string connection, string InCodemp)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GetMoneda(oSessionManager, InCodemp);
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
        public static Boolean ExisteTasaCambio(string connection, string TC_CODEMP, string TC_MONEDA, DateTime TC_FECHA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                if (ComunBD.ExisteTasaCambio(oSessionManager, TC_CODEMP, TC_MONEDA, TC_FECHA) > 0)
                    return true;
                else
                    return false;
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
        //TBTABLAS
        #region        
        public static int GeneraConsecutivo(string connection, string TTCODCLA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GeneraConsecutivo(oSessionManager, TTCODCLA);
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
        public static int GeneraConsecutivo(string connection, string TTCODCLA, string TTCODEMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GeneraConsecutivo(oSessionManager, TTCODCLA, TTCODEMP);
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
        public DataTable GetTbTablaLista(string connection, string TTCODTAB)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GetTbTablaLista(oSessionManager, TTCODTAB);
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
        public DataTable GetTbTablaLista(string connection, string TTCODEMP, string TTCODTAB)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GetTbTablaLista(oSessionManager, TTCODEMP, TTCODTAB);
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
        public static string GetValorc(string connection, string TTCODEMP, string TTCODTAB, string TTCODCLA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GetValorc(oSessionManager, TTCODEMP, TTCODTAB, TTCODCLA);
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
        public static double GetValorN(string connection, string TTCODEMP, string TTCODTAB, string TTCODCLA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GetValorN(oSessionManager, TTCODEMP, TTCODTAB, TTCODCLA);
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
        public DataTable GetClasesParametros(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ComunBD.GetClasesParametros(oSessionManager, filter); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager =null;
            }
        }
        public List<TBTABLAS> GetTbTablaListaS(string connection, string TTCODEMP, string TTCODTAB)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            ComunBD Obj = new ComunBD();
            try
            {
                List<TBTABLAS> lst = new List<TBTABLAS>();
                using (IDataReader reader = Obj.GetTbTablaListaR(oSessionManager,TTCODEMP,TTCODTAB))
                {
                    while (reader.Read())
                    {
                        TBTABLAS item = new TBTABLAS();
                        item.TTCODCLA = Convert.ToString(reader["TTCODCLA"]);
                        item.TTDESCRI = Convert.ToString(reader["TTDESCRI"]);
                        lst.Add(item);
                        item = null;
                    }
                }
                return lst;
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
        public int InsertTBTABLAS(string connection, string TTCODEMP, string TTCODTAB, string TTCODCLA, string TTVALORC, int? TTVALORN, double? TTVALORF, DateTime? TTVALORD, string TTDESCRI, string TTCDUSER, string TTESTADO,double? TTVLRFL2, string TTDESLAR, string TTPREGUN)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                ComunBD.InsertTBTABLAS(oSessionManager, TTCODEMP, TTCODTAB, TTCODCLA, TTVALORC, TTVALORN, TTVALORF, TTVALORD, TTDESCRI, TTCDUSER, TTESTADO, TTVLRFL2, TTDESLAR, TTPREGUN);
                return 1;
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
        public int UpdateTBTABLAS(string connection, string TTCODEMP, string TTCODTAB, string TTCODCLA, string TTVALORC, int? TTVALORN, double? TTVALORF, DateTime? TTVALORD, string TTDESCRI, string TTCDUSER, string TTESTADO, double? TTVLRFL2, string TTDESLAR, string TTPREGUN)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                ComunBD.UpdateTBTABLAS(oSessionManager, TTCODEMP, TTCODTAB, TTCODCLA, TTVALORC, TTVALORN, TTVALORF, TTVALORD, TTDESCRI, TTCDUSER, TTESTADO, TTVLRFL2, TTDESLAR, TTPREGUN);
                return 1;
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
        #region
        public Boolean isInt32(string num)
        {
            try
            {
                Int32.Parse(num);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Boolean isDateTime(string num)
        {
            try
            {
                DateTime.Parse(num);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Boolean isDouble(string num)
        {
            try
            {
                Double.Parse(num);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string GetHttpHttps(string inUrl)
        {
            if (inUrl.Substring(4, 1) == ":")
                return "http:";
            else
                return "https:";
        }
        #endregion
    }
}
