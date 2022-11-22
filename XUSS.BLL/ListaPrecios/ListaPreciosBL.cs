using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XUSS.DAL.ListaPrecios;
using DataAccess;

namespace XUSS.BLL.ListaPrecios
{
    public class ListaPreciosBL
    {
        public DataTable GetListaPrecioHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ListaPreciosBD.GetListaPrecioHD(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetListaPrecioDT(string connection, string P_RCODEMP, string P_RLISPRE)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ListaPreciosBD.GetListaPrecioDT(oSessionManager, P_RCODEMP, P_RLISPRE);
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
        public DataTable GetListaPrecioDTF(string connection, string P_RCODEMP, string P_RLISPRE,string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ListaPreciosBD.GetListaPrecioDTF(oSessionManager, P_RCODEMP, P_RLISPRE, filter);
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
        public int InsertListaPrecioDT(string connection, string P_RCODEMP, string P_RLISPRE, int? P_RCODTER, string P_RTIPPRO, string P_RCLAVE1, string P_RCLAVE2,
                                              string P_RCLAVE3, string P_RCLAVE4, string P_RCODCLA, string P_RCODCAL, string P_RUNDPRE, double P_RPRECIO, double P_RCANMIN, string P_RESTADO,
                                              string P_RCAUSAE, string P_RNMUSER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            
            try
            {
                return ListaPreciosBD.InsertListaPrecioDT(oSessionManager, P_RCODEMP, P_RLISPRE, P_RCODTER, P_RTIPPRO, P_RCLAVE1, P_RCLAVE2, P_RCLAVE3, P_RCLAVE4, P_RCODCLA, P_RCODCAL,
                                                   P_RUNDPRE, P_RPRECIO, P_RCANMIN, P_RESTADO, P_RCAUSAE, P_RNMUSER);
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

        public int UpdateListaPrecioDT(string connection, string P_RCODEMP, string P_RLISPRE, int? P_RCODTER, string P_RTIPPRO, string P_RCLAVE1, string P_RCLAVE2,
                                              string P_RCLAVE3, string P_RCLAVE4, string P_RCODCLA, string P_RCODCAL, string P_RUNDPRE, double P_RPRECIO, double P_RCANMIN, string P_RESTADO,
                                              string P_RCAUSAE, string P_RNMUSER)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return ListaPreciosBD.UpdateListaPrecioDT(oSessionManager, P_RCODEMP, P_RLISPRE, P_RCODTER, P_RTIPPRO, P_RCLAVE1, P_RCLAVE2, P_RCLAVE3, P_RCLAVE4, P_RCODCLA, P_RCODCAL,
                                                   P_RUNDPRE, P_RPRECIO, P_RCANMIN, P_RESTADO, P_RCAUSAE, P_RNMUSER);
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
        public int UpdateListaPrecioHD(string connection, string P_CCODEMP, DateTime P_CFECPRE, string P_CNOMBRE, string P_CDESCRI, string P_CMONEDA, string P_CCLIPRO, string P_CTIPLIS,
                                       double P_CREDOND, string P_CESTADO, string P_CCAUSAE, string P_CNMUSER, string original_P_CLISPRE)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                ListaPreciosBD.UpdateListaPrecioHD(oSessionManager, P_CCODEMP, original_P_CLISPRE, P_CFECPRE, P_CNOMBRE, P_CDESCRI, P_CMONEDA, P_CCLIPRO, P_CTIPLIS, P_CREDOND, P_CESTADO, P_CCAUSAE, P_CNMUSER);
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
        public int InsertListaPrecioHD(string connection, string P_CCODEMP, string P_CLISPRE, DateTime P_CFECPRE, string P_CNOMBRE, string P_CDESCRI, string P_CMONEDA, string P_CCLIPRO, string P_CTIPLIS,
                                       double P_CREDOND, string P_CESTADO, string P_CCAUSAE, string P_CNMUSER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                ListaPreciosBD.InsertListaPrecioHD(oSessionManager, P_CCODEMP, P_CLISPRE, P_CFECPRE, P_CNOMBRE, P_CDESCRI, P_CMONEDA, P_CCLIPRO, P_CTIPLIS, P_CREDOND, P_CESTADO, P_CCAUSAE, P_CNMUSER);
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
        public DataTable GetListaPrecioDT(string connection, string P_RCODEMP, string P_RTIPPRO,string P_RCLAVE1)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ListaPreciosBD.GetListaPrecioDT(oSessionManager, P_RCODEMP, P_RTIPPRO, P_RCLAVE1);                
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
        public double GetPrecio(string connection, string P_RCODEMP, string P_RTIPPRO, string P_RCLAVE1, string P_RCLAVE2, string P_RCLAVE3, string P_RCLAVE4, string P_RLISPRE)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ListaPreciosBD.GetPrecio(oSessionManager, P_RCODEMP, P_RTIPPRO, P_RCLAVE1,P_RCLAVE2,P_RCLAVE3,P_RCLAVE4,P_RLISPRE);
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
        public Boolean ExisteListaPrecioDT(string connection, string P_RCODEMP, string P_RLISPRE, string P_RTIPPRO, string P_RCLAVE1, string P_RCLAVE2, string P_RCLAVE3, string P_RCLAVE4)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                if (ListaPreciosBD.ExisteListaPrecioDT(oSessionManager, P_RCODEMP, P_RLISPRE, P_RTIPPRO, P_RCLAVE1, P_RCLAVE2, P_RCLAVE3, P_RCLAVE4) >= 1)
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
    }
}
