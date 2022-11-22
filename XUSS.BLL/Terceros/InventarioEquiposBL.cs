using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using DataAccess;
using XUSS.DAL.Terceros;

namespace XUSS.BLL.Terceros
{
    [DataObject(true)]
    public class InventarioEquiposBL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetEquipos(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return InventarioEquiposBD.GetEquipos(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetEquipos(string connection, int CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return InventarioEquiposBD.GetEquipos(oSessionManager, CODIGO);
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
        public DataTable GetTbTablas(string TTCODTAB)
        {
            SessionManager oSessionManager = new SessionManager(null); 
            try
            {
                return InventarioEquiposBD.GetTbTablas(oSessionManager,TTCODTAB);
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
        public DataTable GetHadware(int Codigo)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return InventarioEquiposBD.GetHadware(oSessionManager,Codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetSoftware(int Codigo)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return InventarioEquiposBD.GetSoftware(oSessionManager, Codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        public static string GetNombreTbTablas(string TTCODTAB, string TTCODCLA)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return InventarioEquiposBD.GetNombreTbTablas(oSessionManager, TTCODTAB, TTCODCLA);
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
        public int InsertEquipo(string connection, int CODIGO, string T_EQUIPO, string UBICACION, string IP1, string IP2,
                                       string IP3, string IP4, string IP5, string IP6, string USUARIO, string REFERENCIA, string NOMBRE)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                InventarioEquiposBD.InsertEquipo(oSessionManager, CODIGO, T_EQUIPO, UBICACION, IP1, IP2, IP3, IP4, IP5, IP6, USUARIO, REFERENCIA, NOMBRE);
                return CODIGO;
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
        public int UpdateEquipo(string connection, string T_EQUIPO, string UBICACION, string IP1, string IP2,
                                       string IP3, string IP4, string IP5, string IP6, string USUARIO, string REFERENCIA, string NOMBRE, int original_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                InventarioEquiposBD.UpdateEquipo(oSessionManager, original_CODIGO, T_EQUIPO, UBICACION, IP1, IP2, IP3, IP4, IP5, IP6, USUARIO, REFERENCIA, NOMBRE);
                return original_CODIGO;
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

        public static int InsertSoftware(string connection, int CODIGO, int CODINT, string NOMBRE, string LICENCIA, DateTime? FECVEN,
                                         string DESCRIPCION)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                InventarioEquiposBD.InsertSoftware(oSessionManager, CODIGO, CODINT, NOMBRE, LICENCIA, FECVEN, DESCRIPCION);
                return CODIGO;
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
        public static int InsertHadware(string connection,  int CODIGO, int CODINT, string MARCA, string TIPO, string DESCRIPCION,
                                                                       string ESTADO, string CAUSAE, DateTime? FECCOMPRA, string PROVEEDOR)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                InventarioEquiposBD.InsertHadware(oSessionManager, CODIGO, CODINT, MARCA, TIPO, DESCRIPCION, ESTADO, CAUSAE, FECCOMPRA, PROVEEDOR);
                return CODIGO;
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
        public static int DeleteHadware(string connection, int CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                InventarioEquiposBD.DeleteHadware(oSessionManager, CODIGO);
                return CODIGO;
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
        public static int DeleteSoftware(string connection, int CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                InventarioEquiposBD.DeleteSoftware(oSessionManager, CODIGO);
                return CODIGO;
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
