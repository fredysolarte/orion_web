using System;
using System.Collections.Generic;
using System.ComponentModel;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;

namespace BLL.Administracion
{
	[DataObject(true)]
	public class AdmiModuloBL
	{		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<AdmiModulo> GetListByUserAndSystem(string connection, string filter, int startRowIndex, int maximumRows, string userLogon, int systemId)
		{
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				return AdmiModuloDB.GetListByUserAndSystem(oSessionManager, filter, startRowIndex, maximumRows, userLogon, systemId);
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
        public List<AdmiModulo> GetListBySystem(string connection, string filter, int startRowIndex, int maximumRows, int systemId)
        {
            AdmiModuloDB objDB = new AdmiModuloDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return objDB.GetListBySystem(oSessionManager, filter, startRowIndex, maximumRows, systemId);
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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AdmiModulo> GetList(string connection, string filter, int startRowIndex, int maximumRows)
        {
            AdmiModuloDB objDB = new AdmiModuloDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return objDB.GetAllList(oSessionManager, filter, startRowIndex, maximumRows);
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

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Insert(AdmiModulo objEntity)
        {
            AdmiModuloDB objDB = new AdmiModuloDB();
            SessionManager oSessionManager = new SessionManager(null);
            
            AdmiOpcion AdmiOpcion = new AdmiOpcion();
            AdmiOpcionDB AdmiOpcionDB = new AdmiOpcionDB();

            AdmiFormulario AdmiFormulario = new AdmiFormulario();
            AdmiFormularioDB AdmiFormularioDB = new AdmiFormularioDB();

            AdmiArbolOpcion admiArbolOpcion = new AdmiArbolOpcion();
            AdmiArbolOpcionDB admiArbolOpcionDB = new AdmiArbolOpcionDB();

            int retorno = -1;
            int ln_rtnop = -1;
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                objEntity.LogsFecha = DateTime.Now;
                objEntity.LogsUsuario = (int?)System.Web.HttpContext.Current.Session["UserId"];
                retorno = objDB.Add(oSessionManager, objEntity);
                //--fse opcion
                AdmiOpcion.SistSistema = objEntity.SistSistema;
                AdmiOpcion.ModuModulo = retorno;
                AdmiOpcion.OpciNombre = "Inicio";
                AdmiOpcion.OpciEtiqueta = "Inicio";
                AdmiOpcion.ParaClase2 = 2;
                AdmiOpcion.OpciHint = "Inicio";
                AdmiOpcion.OpciEstado = true;
                AdmiOpcion.LogsUsuario = 1;
                AdmiOpcion.LogsFecha = System.DateTime.Now;
                AdmiOpcion.OpciPrincipal = true;
                ln_rtnop = AdmiOpcionDB.Add(oSessionManager, AdmiOpcion);
                //--fse admi _formulario
                AdmiFormulario.OpciOpcion = ln_rtnop;
                AdmiFormulario.FormNombre = objEntity.Formulario;
                AdmiFormulario.FormDescripcion = "Inicio";
                AdmiFormulario.FormEstado = true;
                AdmiFormulario.LogsUsuario = 1;
                AdmiFormulario.LogsFecha = System.DateTime.Now;
                AdmiFormularioDB.Add(oSessionManager, AdmiFormulario);
                //--fse arbol opcion
                admiArbolOpcion.AropEntidad = "M";
                admiArbolOpcion.AropIdOriginal = retorno;
                //admiArbolOpcion.AropIdUnicoPadre = 0;
                admiArbolOpcion.AropNombre = objEntity.ModuNombre;
                admiArbolOpcion.ModuModulo = retorno;
                admiArbolOpcion.SistSistema = objEntity.SistSistema;
                admiArbolOpcionDB.Add(oSessionManager, admiArbolOpcion);

                oSessionManager.CommitTranstaction();
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
            return retorno;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Delete(AdmiModulo objEntity)
        {
            AdmiModuloDB objDB = new AdmiModuloDB();
            SessionManager oSessionManager = new SessionManager(null);
            AdmiArbolOpcionDB admiArbolOpcionDB = new AdmiArbolOpcionDB();
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                admiArbolOpcionDB.DeleteByIdOriginalAndEntidad(oSessionManager, objEntity.ModuModulo, "M");
                //objDB.Delete(oSessionManager, objEntity);
                oSessionManager.CommitTranstaction();
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Update(AdmiModulo objEntity)
        {
            AdmiModuloDB objDB = new AdmiModuloDB();
            SessionManager oSessionManager = new SessionManager(null);
            AdmiArbolOpcion admiArbolOpcion;
            AdmiArbolOpcionDB admiArbolOpcionDB = new AdmiArbolOpcionDB();
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                objEntity.LogsFecha = DateTime.Now;
                objEntity.LogsUsuario = (int)System.Web.HttpContext.Current.Session["UserId"];
                objDB.Update(oSessionManager, objEntity);
                admiArbolOpcion = admiArbolOpcionDB.GetByIdAndEntidadAndSystem(oSessionManager, "", 0, 0, objEntity.ModuModulo, "M", objEntity.SistSistema);
                admiArbolOpcion.AropNombre = objEntity.ModuNombre;
                admiArbolOpcionDB.Update(oSessionManager, admiArbolOpcion);
                oSessionManager.CommitTranstaction();
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }
	}
}
