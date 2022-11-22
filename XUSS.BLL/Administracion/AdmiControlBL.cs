using System;
using System.Collections.Generic;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;
using System.ComponentModel;
using System.Data;

namespace BLL.Administracion
{
	public class AdmiControlBL
	{
		public static List<AdmiControl> GetListControlAllowed(string connection, string user, string nombreForm)
		{
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				return AdmiControlDB.GetListControlAllowed(oSessionManager, user, nombreForm);
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
        public List<AdmiControl> GetByIdFormaEstadoForma(string connection, string filter, int startRowIndex, int maximumRows, int idTipo, bool presenteForma)
        {
            AdmiControlDB objDB = new AdmiControlDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return objDB.GetByIdFormaEstadoForma(oSessionManager, filter, startRowIndex, maximumRows, idTipo, presenteForma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDB = null;
                oSessionManager = null;
            }
        }

        public static DataTable GetControles(string connection, int opci_opcion)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return AdmiControlDB.GetControles(oSessionManager, opci_opcion);
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
        public int Add(string connection, string nombre,string descripcion, int formulario,int sistema, int modulo, int opcion)
        {
            AdmiControl admiControl = new AdmiControl();
            SessionManager oSessionManager = new SessionManager(null);            
            AdmiControlDB admiControlDB = new AdmiControlDB();
            AdmiArbolOpcionDB admiArbolOpcionDB = new AdmiArbolOpcionDB();
            AdmiArbolOpcion admiArbolOpcion = new AdmiArbolOpcion();
            int rformulario = 0,aropunicopadre=0;
            try {               
                admiControl.CtrlNombre = nombre;
                admiControl.CtrlDescripcion = descripcion;
                admiControl.FormFormulario = formulario;
                admiControl.CtrlEstado = true;
                rformulario = admiControlDB.Add(oSessionManager, admiControl);

                aropunicopadre = admiArbolOpcionDB.GetByIdAndEntidadAndSystem(oSessionManager, "", 0, 0, opcion, "O", sistema).AropArbolOpcion;

                admiArbolOpcion.AropEntidad = "C";
                admiArbolOpcion.AropIdOriginal = rformulario;
                admiArbolOpcion.AropIdUnicoPadre = aropunicopadre;
                admiArbolOpcion.AropNombre = descripcion;
                admiArbolOpcion.ModuModulo = modulo;
                admiArbolOpcion.SistSistema = sistema;
                admiArbolOpcionDB.Add(oSessionManager, admiArbolOpcion);

                return rformulario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                admiControl = null;
                admiControlDB = null;
                oSessionManager = null;
            }
        }
        public DataTable GetControlesPermisos(string connection, int opci_opcion, string usua_usuario)
        {            
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return AdmiControlDB.GetControlesPermisos(oSessionManager, opci_opcion, usua_usuario);
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