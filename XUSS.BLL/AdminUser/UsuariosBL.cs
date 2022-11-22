using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using TESIS.DAL.AdminUser;
using System.Data;
using DataAccess;


namespace TESIS.BLL.AdminUser
{
	[DataObject(true)]
	public class UsuariosBL
	{
		public static Boolean GetUsuarioExiste(string connection, string usuario, string password)
		{
			SessionManager oSessionManager = new SessionManager(connection);

			try
			{
				if (UsuariosBD.GetUsuarioExiste(oSessionManager, usuario, password) > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
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
        public static DataTable GetUsuarioExiste(string connection, string usuario, string password, int sist_sistema)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return UsuariosBD.GetUsuarioExiste(oSessionManager, usuario, password,sist_sistema);
        
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
		public DataTable GetListByUserAndSystem(string connection, string filter, int startRowIndex, int maximumRows, string userLogon, int systemId)
		{
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				oSessionManager.CreateConnection();
				return UsuariosBD.GetListByUserAndSystem(oSessionManager, filter, startRowIndex, maximumRows, userLogon, systemId);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				oSessionManager.CloseConnection();
				oSessionManager = null;
			}
		}
		public DataTable GetOptionsByUserAndModule(string connection, string userLogon, int moduleId, int sistemaId, int parentId)
		{
			
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				oSessionManager.CreateConnection();
				return UsuariosBD.GetOptionsByUserAndModule(oSessionManager, userLogon, moduleId, sistemaId, parentId);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				oSessionManager.CloseConnection();
				oSessionManager = null;
			}
		}
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public DataTable GetOptionsByURLForm(string connection, string filter, int startRowIndex, int maximumRows, string urlForm)
		{
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				oSessionManager.CreateConnection();
				return UsuariosBD.GetOptionsByURLForm(oSessionManager, filter, startRowIndex, maximumRows, urlForm);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				oSessionManager.CloseConnection();
				oSessionManager = null;
			}
		}
		public static string GetUrlPrincipalPorIdModulo(string connection, int idModulo)
		{

			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				//oSessionManager.CreateConnection();
				return UsuariosBD.GetUrlPrincipalPorIdModulo(oSessionManager, idModulo);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				//oSessionManager.CloseConnection();
				oSessionManager = null;
			}
		}
		public int ChangePassword(string connection, string usuario, string password)
		{
			SessionManager oSessionManager = new SessionManager(connection);
			try 
			{
				return UsuariosBD.ChangePassword(oSessionManager, usuario, password);
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
        public DataTable GetPerimosFormulario(string connection, string usuario,int sistema)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return UsuariosBD.GetPerimosFormulario(oSessionManager, usuario, sistema);
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