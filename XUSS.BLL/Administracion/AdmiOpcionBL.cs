using System;
using DAL.Administracion;
using DataAccess;
using System.ComponentModel;
using BE.Administracion;
using System.Collections.Generic;
using System.Data;

namespace BLL.Administracion
{
	public class AdmiOpcionBL
	{
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AdmiOpcion> GetList(string connection, string filter, int startRowIndex, int maximumRows)
        {
            AdmiOpcionDB objDB = new AdmiOpcionDB();
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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AdmiOpcion> GetListByModulo(string connection, string filter, int startRowIndex, int maximumRows, string modulo)
        {
            AdmiOpcionDB objDB = new AdmiOpcionDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                if (string.IsNullOrWhiteSpace(modulo))
                {
                    return new List<AdmiOpcion>();
                }
                else
                {
                    return objDB.GetAllList(oSessionManager, " admi_topcion.modu_modulo =" + modulo, startRowIndex, maximumRows);
                }
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
        public List<AdmiOpcion> GetListByIdModuloAndIdSistema(string connection, string filter, int startRowIndex, int maximumRows, int idModulo, int idSistema)
        {
            AdmiOpcionDB objDB = new AdmiOpcionDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return objDB.GetListByIdModuloAndIdSistema(oSessionManager, filter, startRowIndex, maximumRows, idModulo, idSistema);
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
        public int Insert(AdmiOpcion item)
        {
            int result = -1;
            int ln_formulario = 0, ln_control = 0, aropunicopadre=0;
            AdmiOpcionDB objDB = new AdmiOpcionDB();
            SessionManager oSessionManager = new SessionManager(null);
            AdmiArbolOpcionDB admiArbolOpcionDB = new AdmiArbolOpcionDB();
            AdmiArbolOpcion admiArbolOpcion = new AdmiArbolOpcion();
            AdmiFormulario admiFormulario = new AdmiFormulario();
            AdmiFormularioDB admiFormularioDB = new AdmiFormularioDB();
            AdmiControlDB admiControlDB = new AdmiControlDB();
            AdmiControl admiControl = new AdmiControl();
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                item.LogsFecha = DateTime.Now;
                item.LogsUsuario = (int)System.Web.HttpContext.Current.Session["UserId"];
                result = objDB.Add(oSessionManager, item);
                admiArbolOpcion.AropEntidad = "O";
                admiArbolOpcion.AropIdOriginal = result;
                if (item.OpciPadre != null)
                {
                    admiArbolOpcion.AropIdUnicoPadre = admiArbolOpcionDB.GetByIdAndEntidadAndSystem(oSessionManager, "", 0, 0, (int)item.OpciPadre, "O", item.SistSistema).AropArbolOpcion;
                }
                else
                {
                    admiArbolOpcion.AropIdUnicoPadre = admiArbolOpcionDB.GetByIdAndEntidadAndSystem(oSessionManager, "", 0, 0, (int)item.ModuModulo, "M", item.SistSistema).AropArbolOpcion;
                }

                admiArbolOpcion.AropNombre = item.OpciNombre;
                admiArbolOpcion.ModuModulo = item.ModuModulo;
                admiArbolOpcion.SistSistema = item.SistSistema;
                aropunicopadre = admiArbolOpcionDB.Add(oSessionManager, admiArbolOpcion);

                // fse formularios
                admiFormulario.SistSistema = item.SistSistema;
                admiFormulario.ModuModulo = item.ModuModulo;
                admiFormulario.OpciOpcion = result;
                admiFormulario.FormDescripcion = item.OpciNombre;                
                admiFormulario.FormNombre = item.NombreRuta;
                admiFormulario.FormEstado = item.OpciEstado;
                ln_formulario = admiFormularioDB.Add(oSessionManager, admiFormulario);

                // fse controles /Insert/Editar/Eliminar/Imprimir/Buscar
                admiControl.FormFormulario = ln_formulario;
                admiControl.CtrlNombre = "INS";
                admiControl.CtrlDescripcion = "Insertar";
                admiControl.CtrlEstado = true;
                ln_control = admiControlDB.Add(oSessionManager, admiControl);

                admiArbolOpcion.AropEntidad = "C";
                admiArbolOpcion.AropIdOriginal = ln_control;
                admiArbolOpcion.AropIdUnicoPadre = aropunicopadre;
                admiArbolOpcion.AropNombre = admiControl.CtrlDescripcion;
                admiArbolOpcion.ModuModulo = item.ModuModulo;
                admiArbolOpcion.SistSistema = item.SistSistema;
                admiArbolOpcionDB.Add(oSessionManager, admiArbolOpcion);

                //Editar
                admiControl.FormFormulario = ln_formulario;
                admiControl.CtrlNombre = "EDT";
                admiControl.CtrlDescripcion = "Editar";
                admiControl.CtrlEstado = true;
                ln_control = admiControlDB.Add(oSessionManager, admiControl);
                
                admiArbolOpcion.AropEntidad = "C";
                admiArbolOpcion.AropIdOriginal = ln_control;
                admiArbolOpcion.AropIdUnicoPadre = aropunicopadre;
                admiArbolOpcion.AropNombre = admiControl.CtrlDescripcion;
                admiArbolOpcion.ModuModulo = item.ModuModulo;
                admiArbolOpcion.SistSistema = item.SistSistema;
                admiArbolOpcionDB.Add(oSessionManager, admiArbolOpcion);

                //Eliminar
                admiControl.FormFormulario = ln_formulario;
                admiControl.CtrlNombre = "DEL";
                admiControl.CtrlDescripcion = "Eliminar";
                admiControl.CtrlEstado = true;
                ln_control = admiControlDB.Add(oSessionManager, admiControl);

                admiArbolOpcion.AropEntidad = "C";
                admiArbolOpcion.AropIdOriginal = ln_control;
                admiArbolOpcion.AropIdUnicoPadre = aropunicopadre;
                admiArbolOpcion.AropNombre = admiControl.CtrlDescripcion;
                admiArbolOpcion.ModuModulo = item.ModuModulo;
                admiArbolOpcion.SistSistema = item.SistSistema;
                admiArbolOpcionDB.Add(oSessionManager, admiArbolOpcion);

                //Imprimir
                admiControl.FormFormulario = ln_formulario;
                admiControl.CtrlNombre = "PRN";
                admiControl.CtrlDescripcion = "Imprimir";
                admiControl.CtrlEstado = true;
                ln_control = admiControlDB.Add(oSessionManager, admiControl);

                admiArbolOpcion.AropEntidad = "C";
                admiArbolOpcion.AropIdOriginal = ln_control;
                admiArbolOpcion.AropIdUnicoPadre = aropunicopadre;
                admiArbolOpcion.AropNombre = admiControl.CtrlDescripcion;
                admiArbolOpcion.ModuModulo = item.ModuModulo;
                admiArbolOpcion.SistSistema = item.SistSistema;
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
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Update(AdmiOpcion objEntity)
        {
            AdmiOpcionDB objDB = new AdmiOpcionDB();
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
                admiArbolOpcion = admiArbolOpcionDB.GetByIdAndEntidadAndSystem(oSessionManager, "", 0, 0, objEntity.OpciOpcion, "O", objEntity.SistSistema);
                if (admiArbolOpcion != null)
                {
                    if (objEntity.OpciPadre != null)
                    {
                        admiArbolOpcion.AropIdUnicoPadre = admiArbolOpcionDB.GetByIdAndEntidadAndSystem(oSessionManager, "", 0, 0, (int)objEntity.OpciPadre, "O", objEntity.SistSistema).AropArbolOpcion;
                    }
                    else
                    {
                        admiArbolOpcion.AropIdUnicoPadre = admiArbolOpcionDB.GetByIdAndEntidadAndSystem(oSessionManager, "", 0, 0, (int)objEntity.ModuModulo, "M", objEntity.SistSistema).AropArbolOpcion;
                    }
                    admiArbolOpcion.SistSistema = objEntity.SistSistema;
                    admiArbolOpcion.AropNombre = objEntity.OpciNombre;
                    admiArbolOpcion.ModuModulo = objEntity.ModuModulo;
                    admiArbolOpcionDB.Update(oSessionManager, admiArbolOpcion);
                }
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

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Delete(AdmiOpcion objEntity)
        {
            AdmiOpcionDB objDB = new AdmiOpcionDB();
            SessionManager oSessionManager = new SessionManager(null);
            AdmiArbolOpcionDB admiArbolOpcionDB = new AdmiArbolOpcionDB();
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                admiArbolOpcionDB.DeleteByIdOriginalAndEntidad(oSessionManager, objEntity.OpciOpcion, "O");
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
		public static string GetUrlPrincipalPorIdModulo(string connection, int idModulo)
		{
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				return AdmiOpcionDB.GetUrlPrincipalPorIdModulo(oSessionManager, idModulo);
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
        public DataTable GetlstReportes(string connection)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return AdmiOpcionDB.GetlstReportes(oSessionManager);
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
        public DataTable GetPermisos(string connection, string inUrl, string inUsuario)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return AdmiOpcionDB.GetlstReportes(oSessionManager);
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