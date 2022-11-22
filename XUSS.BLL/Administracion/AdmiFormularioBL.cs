using System;
using System.Collections.Generic;
using System.ComponentModel;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;
using BE.Web;
using BE.General;

namespace BLL.Administracion
{
    [DataObject(true)]
    public class AdmiFormularioBL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AdmiFormulario> GetAllListByIdOpcion(string connection, string filter, int startRowIndex, int maximumRows, int idOpcion)
        {

            AdmiFormularioDB objDB = new AdmiFormularioDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {                
                return objDB.GetAllListByIdOpcion(oSessionManager, filter, startRowIndex, maximumRows, idOpcion);
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
        public List<AdmiFormulario> GetAllList(string connection, string filter, int startRowIndex, int maximumRows)
        {

            AdmiFormularioDB objDB = new AdmiFormularioDB();
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
        public int Insert(List<FormControls> ListaControlesForm, int OpciOpcion, string FormDescripcion, string FormTablabase, bool FormEstado, int ModuModulo, int? BlobAyuda, string FormNombre,int SistSistema)
        {
            int result = -1;
            AdmiFormularioDB objDB = new AdmiFormularioDB();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                AdmiFormulario objEntity = new AdmiFormulario();
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                objEntity.BlobAyuda = BlobAyuda;
                objEntity.FormNombre = FormNombre;
                objEntity.FormDescripcion = FormDescripcion;
                objEntity.FormEstado = FormEstado;
                objEntity.FormTablabase = FormTablabase;
                objEntity.LogsFecha = DateTime.Now;
                objEntity.LogsUsuario = (int)System.Web.HttpContext.Current.Session["UserId"];
                objEntity.OpciOpcion = OpciOpcion;
                result = objDB.Add(oSessionManager, objEntity);

                AdmiControlDB AControlDB = new AdmiControlDB();
                AdmiArbolOpcionDB admiArbolOpcionDB = new AdmiArbolOpcionDB();
                
                AdmiArbolOpcion admiArbolOpcion,admiArbol;
                admiArbol = admiArbolOpcionDB.GetByIdAndEntidadAndSystem(oSessionManager, "", 0, 0, OpciOpcion, "O", SistSistema);
                admiArbolOpcion = new AdmiArbolOpcion();
                admiArbolOpcion.AropEntidad = "F";
                admiArbolOpcion.AropIdOriginal = result;
                admiArbolOpcion.AropIdUnicoPadre = admiArbol.AropArbolOpcion;
                admiArbolOpcion.AropNombre = FormDescripcion;
                admiArbolOpcion.ModuModulo = ModuModulo;
                admiArbolOpcion.SistSistema = SistSistema;
                int idAO = admiArbolOpcionDB.Add(oSessionManager, admiArbolOpcion);

                //InsertArbol(ListaControlesForm, null, null, result, AControlDB, oSessionManager, idAO, admiArbolOpcionDB, SistSistema, ModuModulo);
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

        private void InsertArbol(List<FormControls> ListaControlesForm, string padre, int? idPadre, int idFormulario, AdmiControlDB AControlDB, SessionManager oSessionManager, int idArbolOpciones, AdmiArbolOpcionDB admiArbolOpcionDB, int idSistema, int idModulo)
        {
            AdmiControl AControl;
            int id=0;
            int idAO;
            AdmiArbolOpcion admiArbolOpcion;
            foreach (FormControls FControls in ListaControlesForm.FindAll(t => t.IdPadre == padre))
            {
                AControl = new AdmiControl();
                AControl.CtrlDescripcion = FControls.Descripcion;
                AControl.CtrlEstado = FControls.Estado;
                AControl.CtrlExisteforma = true;
                AControl.CtrlNombre = FControls.Id;
                AControl.CtrlPadre = idPadre;
                AControl.FormFormulario = idFormulario;
                //id = AControlDB.Add(oSessionManager, AControl);

                admiArbolOpcion = new AdmiArbolOpcion();
                admiArbolOpcion.AropEntidad = "C";
                admiArbolOpcion.AropIdOriginal = id;
                admiArbolOpcion.AropIdUnicoPadre = idArbolOpciones;
                admiArbolOpcion.AropNombre = FControls.Descripcion;
                admiArbolOpcion.ModuModulo = idModulo;
                admiArbolOpcion.SistSistema = idSistema;
                idAO = admiArbolOpcionDB.Add(oSessionManager, admiArbolOpcion);

                InsertArbol(ListaControlesForm, FControls.Id, id, idFormulario, AControlDB, oSessionManager, idAO, admiArbolOpcionDB, idSistema, idModulo);
            }
        }

        private void UpdateArbol(List<FormControls> ListaControlesForm, List<Campos> lista, string padre, int? idPadre, int idFormulario, AdmiControlDB AControlDB, SessionManager oSessionManager, int idArbolOpciones, AdmiArbolOpcionDB admiArbolOpcionDB, int idSistema, int idModulo)
        {
            AdmiControl AControl;
            int id=0;
            int idAO = idArbolOpciones;
            AdmiArbolOpcion admiArbolOpcion;
            foreach (FormControls FControls in ListaControlesForm.FindAll(t => t.IdPadre == padre))
            {
                if (FControls.ExisteEnBaseDatos)
                {
                    AControl = AControlDB.GetByNombreControlAndIdForma(oSessionManager, "", 0, 0, FControls.Id, idFormulario);
                    id = AControl.CtrlControl;

                    AControl = new AdmiControl();
                    AControl.CtrlControl = id;
                    AControl.CtrlDescripcion = FControls.Descripcion;
                    AControl.CtrlEstado = FControls.Estado;
                    AControl.CtrlExisteforma = true;
                    AControl.CtrlNombre = FControls.Id;
                    AControl.CtrlPadre = idPadre;
                    AControl.FormFormulario = idFormulario;
                    //AControlDB.Update(oSessionManager, AControl);

                    if (FControls.Estado)
                    {
                        admiArbolOpcion = new AdmiArbolOpcion();
                        admiArbolOpcion.AropEntidad = "C";
                        admiArbolOpcion.AropIdOriginal = id;
                        admiArbolOpcion.AropIdUnicoPadre = idArbolOpciones;
                        admiArbolOpcion.AropNombre = FControls.Descripcion;
                        admiArbolOpcion.ModuModulo = idModulo;
                        admiArbolOpcion.SistSistema = idSistema;
                        idAO = admiArbolOpcionDB.Add(oSessionManager, admiArbolOpcion);
                    }
                    UpdateArbol(ListaControlesForm, lista, FControls.Id, id, idFormulario, AControlDB, oSessionManager, idAO, admiArbolOpcionDB, idSistema, idModulo);
                }
                else
                {
                    Campos tmp = lista.Find(t => t.LogicalName == FControls.Id);
                    if (tmp != null)
                    {
                        AControl = AControlDB.GetByNombreControlAndIdForma(oSessionManager, "", 0, 0, tmp.FileColumnName, idFormulario);
                        id = AControl.CtrlControl;

                        AControl = new AdmiControl();
                        AControl.CtrlControl = id;
                        AControl.CtrlDescripcion = FControls.Descripcion;
                        AControl.CtrlEstado = FControls.Estado;
                        AControl.CtrlExisteforma = true;
                        AControl.CtrlNombre = FControls.Id;
                        AControl.CtrlPadre = idPadre;
                        AControl.FormFormulario = idFormulario;
                        //AControlDB.Update(oSessionManager, AControl);
                        if (FControls.Estado)
                        {
                            admiArbolOpcion = new AdmiArbolOpcion();
                            admiArbolOpcion.AropEntidad = "C";
                            admiArbolOpcion.AropIdOriginal = id;
                            admiArbolOpcion.AropIdUnicoPadre = idArbolOpciones;
                            admiArbolOpcion.AropNombre = FControls.Descripcion;
                            admiArbolOpcion.ModuModulo = idModulo;
                            admiArbolOpcion.SistSistema = idSistema;
                            idAO = admiArbolOpcionDB.Add(oSessionManager, admiArbolOpcion);
                        }
                        UpdateArbol(ListaControlesForm, lista, FControls.Id, id, idFormulario, AControlDB, oSessionManager, idAO, admiArbolOpcionDB, idSistema, idModulo);
                    }
                    else
                    {
                        AControl = new AdmiControl();
                        AControl.CtrlDescripcion = FControls.Descripcion;
                        AControl.CtrlEstado = FControls.Estado;
                        AControl.CtrlExisteforma = true;
                        AControl.CtrlNombre = FControls.Id;
                        AControl.CtrlPadre = idPadre;
                        AControl.FormFormulario = idFormulario;
                        //id = AControlDB.Add(oSessionManager, AControl);
                        if (FControls.Estado)
                        {
                            admiArbolOpcion = new AdmiArbolOpcion();
                            admiArbolOpcion.AropEntidad = "C";
                            admiArbolOpcion.AropIdOriginal = id;
                            admiArbolOpcion.AropIdUnicoPadre = idArbolOpciones;
                            admiArbolOpcion.AropNombre = FControls.Descripcion;
                            admiArbolOpcion.ModuModulo = idModulo;
                            admiArbolOpcion.SistSistema = idSistema;
                            idAO = admiArbolOpcionDB.Add(oSessionManager, admiArbolOpcion);
                        }
                        UpdateArbol(ListaControlesForm, lista, FControls.Id, id, idFormulario, AControlDB, oSessionManager, idAO, admiArbolOpcionDB, idSistema, idModulo);
                    }
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Update(List<FormControls> ListaControlesForm, List<Campos> lista,int OpciOpcion, string FormDescripcion,string FormTablabase,  bool FormEstado, int ModuModulo,int? BlobAyuda, string FormNombre,int SistSistema,int original_FormFormulario)
        {
            AdmiFormularioDB objDB = new AdmiFormularioDB();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                AdmiFormulario objEntity = new AdmiFormulario();
                objEntity.FormFormulario = original_FormFormulario;
                objEntity.BlobAyuda = BlobAyuda;
                objEntity.FormNombre = FormNombre;
                objEntity.FormDescripcion = FormDescripcion;
                objEntity.FormEstado = FormEstado;
                objEntity.FormTablabase = FormTablabase;
                objEntity.LogsFecha = DateTime.Now;
                objEntity.OpciOpcion = OpciOpcion;
                objEntity.LogsUsuario = (int)System.Web.HttpContext.Current.Session["UserId"];
                objDB.Update(oSessionManager, objEntity);

                AdmiControlDB AControlDB = new AdmiControlDB();
                AdmiArbolOpcionDB admiArbolOpcionDB = new AdmiArbolOpcionDB();
                //admiArbolOpcionDB.DeleteByIdOriginalAndEntidad();
                AdmiArbolOpcion admiArbolOpcionForma = admiArbolOpcionDB.GetByIdAndEntidadAndSystem(oSessionManager, "", 0, 0, objEntity.FormFormulario, "F", SistSistema);
                admiArbolOpcionForma.AropNombre = objEntity.FormDescripcion;
                admiArbolOpcionDB.Update(oSessionManager, admiArbolOpcionForma);
                //AControlDB.DisableControls(oSessionManager, objEntity.FormFormulario);
                //admiArbolOpcionDB.DeleteControlsByIdForma(oSessionManager, objEntity.FormFormulario);
                //UpdateArbol(ListaControlesForm, lista, null, null, objEntity.FormFormulario, AControlDB, oSessionManager, admiArbolOpcionForma.AropArbolOpcion, admiArbolOpcionDB, SistSistema, ModuModulo);
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
        public void Delete(int original_FormFormulario)
        {
            AdmiFormularioDB objDB = new AdmiFormularioDB();
            AdmiArbolOpcionDB admiArbolOpcionDB = new AdmiArbolOpcionDB();
            AdmiControlDB admiControlDB = new AdmiControlDB();
            SessionManager oSessionManager = new SessionManager(null);

            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                //admiArbolOpcionDB.DeleteControlsByIdForma(oSessionManager, original_FormFormulario);
                admiArbolOpcionDB.DeleteByIdOriginalAndEntidad(oSessionManager, original_FormFormulario, "F");
                //admiControlDB.DeleteByIdForma(oSessionManager, original_FormFormulario);
                objDB.Delete(oSessionManager, original_FormFormulario);
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