using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using BE.Web;
using System.Web.UI;
using BE.Administracion;
using DataAccess;
using DAL.Administracion;
using Telerik.Web.UI;

namespace XUSS.BLL.Administracion
{
    public class PageBase : System.Web.UI.Page
    {
        protected bool bloqueo = false;
        protected string keyBloqueo;

        private List<FormControls> _lista;

        public List<FormControls> Lista
        {
            get { return _lista; }
        }

        public StateBag ViewStateBase
        {
            get { return ViewState; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (bloqueo && Convert.ToBoolean(Session["Authenticated"]))
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringBuilder js = new StringBuilder();
                //js.AppendLine("     var h_isPostBack = false;");
                js.AppendLine("     function UnlockJs () {");
                //js.AppendLine("         if (h_isPostBack) {");
                //js.AppendLine("             return;");
                //js.AppendLine("         } else {");
                js.AppendLine("             $.ajax({ ");
                js.AppendLine("                 type: 'POST', ");
                js.AppendLine("                 url: '" + ResolveUrl("~/Service/WebServiceClose.asmx") + "/Unlock',");
                js.AppendLine("                 async:false,");
                js.AppendLine("                 data: 'pagina=" + this.keyBloqueo + "', ");
                js.AppendLine("                 cache: false");
                js.AppendLine("             });");
                //js.AppendLine("         }");
                js.AppendLine("     }");
                js.AppendLine("     window.onbeforeunload= UnlockJs;");

                //if (!ClientScript.IsOnSubmitStatementRegistered(typeof(Page), "onSubmitScript"))
                //{
                //    ClientScript.RegisterOnSubmitStatement(typeof(Page), "onSubmitScript", "h_isPostBack = true;");
                //}

                if (!ClientScript.IsStartupScriptRegistered(typeof(Page), "scriptUnlock"))
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "scriptUnlock", js.ToString(), true);
                }

                List<string> bloqueos;
                List<string> bloqueosSession;
                if (Session["bloqueos"] != null)
                {
                    bloqueosSession = Session["bloqueos"] as List<string>;
                }
                else
                {
                    bloqueosSession = new List<string>();
                }

                if (IsPostBack)
                {
                    if (Application["bloqueos"] != null)
                    {
                        bloqueos = Application["bloqueos"] as List<string>;
                        if (bloqueos.Find(t => t == keyBloqueo) == null)
                        {
                            bloqueos.Add(keyBloqueo);
                            Application[keyBloqueo] = Session["UserLogon"];
                            Application["bloqueos"] = bloqueos;
                        }
                    }
                    else
                    {
                        bloqueos = new List<string>();
                        bloqueos.Add(keyBloqueo);
                        Application[keyBloqueo] = Session["UserLogon"];
                        Application["bloqueos"] = bloqueos;
                    }
                }
                else
                {
                    if (bloqueosSession.Find(t => t == keyBloqueo) != null)
                    {
                        Session["bloqueoMensaje"] = keyBloqueo;
                        Response.Redirect("~/Default.aspx");
                        return;
                    }
                    else
                    {
                        if (Application["bloqueos"] != null)
                        {
                            bloqueos = Application["bloqueos"] as List<string>;
                            if (bloqueos.Find(t => t == keyBloqueo) == null)
                            {
                                bloqueos.Add(keyBloqueo);
                                Application["bloqueos"] = bloqueos;
                                bloqueosSession.Add(keyBloqueo);
                                Session["bloqueos"] = bloqueosSession;
                                Application[keyBloqueo] = Session["UserLogon"];
                            }
                            else
                            {
                                Session["bloqueoMensaje"] = keyBloqueo;
                                Response.Redirect("~/Default.aspx");
                                return;
                            }
                        }
                        else
                        {
                            bloqueos = new List<string>();
                            bloqueos.Add(keyBloqueo);
                            Application["bloqueos"] = bloqueos;
                            bloqueosSession.Add(keyBloqueo);
                            Session["bloqueos"] = bloqueosSession;
                            Application[keyBloqueo] = Session["UserLogon"];
                        }
                    }
                }
            }

            if (_lista == null)
            {
                _lista = new List<FormControls>();
            }
            AddControls();
        }

        private List<AdmiControl> Security()
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                AdmiUsuario a = Session["User"] as AdmiUsuario;
                if (a == null)
                    return new List<AdmiControl>();
                else
                    return AdmiControlDB.GetListControlAllowed(oSessionManager, a.UsuaUsuario, Request.Url.PathAndQuery.Substring(1, Request.Url.PathAndQuery.Length - 1));

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

        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);

            Control tmp, tmpPadre;
            foreach (AdmiControl AdControl in Security())
            {
                FormControls control = _lista.Find(t => t.Id == AdControl.CtrlNombre);
                if (control != null)
                {
                    if (string.IsNullOrEmpty(control.IdPadre))
                    {
                        tmp = FindControl(control.Id);
                        if (tmp == null)
                        {
                            tmp = PageBase.EncuentraControl(this, control.Id);
                        }
                    }
                    else
                    {
                        tmpPadre = FindControl(control.IdPadre);
                        if (tmpPadre == null)
                        {
                            tmpPadre = PageBase.EncuentraControl(this, control.IdPadre);
                        }
                        tmp = PageBase.EncuentraControl(tmpPadre, control.Id);
                    }
                    if (tmp != null)
                    {
                        tmp.Visible = false;
                    }
                }
            }
        }

        virtual public void AddControls()
        {
            if (_lista == null)
            {
                _lista = new List<FormControls>();
            }
        }

        public static Control EncuentraControl(Control Root, string Id)
        {
            if (Root.ID == Id)
            {
                return Root;
            }
            foreach (Control Ctl in Root.Controls)
            {
                Control FoundControl;
                FoundControl = EncuentraControl(Ctl, Id);
                if (FoundControl != null)
                    return FoundControl;
            }
            return null;
        }

        public bool GetBoolean(object value)
        {
            bool retValue;
            if (value == null || value == System.DBNull.Value)
                retValue = false;
            else if (Convert.ToInt32(value) == 1)
                retValue = true;
            else
                retValue = false;

            return retValue;
        }

        protected void AnalizarCommand(string comando)
        {
            if (string.IsNullOrEmpty(comando) || comando.Equals("Cancel"))
            {
                ViewState["toolbars"] = true;
            }
            else
            {
                ViewState["toolbars"] = false;
            }
        }

        protected void OcultarPaginador(RadListView rlv, string idPaginador, string idPanelBotones)
        {
            if (rlv != null)
            {
                Control paginador = rlv.FindControl(idPaginador);
                if (paginador != null)
                {
                    paginador.Visible = (bool)ViewState["toolbars"];
                }
                if (rlv.Items.Count > 0 && rlv.Items[0].ItemType == RadListViewItemType.DataItem)
                {
                    (rlv.Items[0].FindControl(idPanelBotones) as Control).Visible = (bool)ViewState["toolbars"];
                }
            }
        }

        public string GetMessage(int id, string text)
        {
            switch (id)
            {
                case 1:
                    return "No se encontraron registros " + text;
                case 2:
                    return "Ver " + text;
                case 3:
                    return "Adicionar " + text;
                case 4:
                    return "Modificar " + text;
                default:
                    return text;
            }
        }
    }
}
