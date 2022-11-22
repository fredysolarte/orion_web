using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using System.Web;
using System.Data;
using DAL.Administracion;
using DataAccess;
using BE.Administracion;
using Utils;


namespace BLL.Administracion
{
    [DataObject(true)]
    public class AdmiUsuarioBL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetUserPerson(string connection, string filter, int startRowIndex, int maximumRows)
        {
            AdmiUsuarioDB objDB = new AdmiUsuarioDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                //oSessionManager.CreateConnection();
                return objDB.GetUserPerson(oSessionManager, filter, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetUserPerson(string userLogon)
        {
            AdmiUsuarioDB objDB = new AdmiUsuarioDB();
            SessionManager oSessionManager = new SessionManager("");
            try
            {
                //oSessionManager.CreateConnection();
                return objDB.GetUserPerson(oSessionManager, userLogon);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        public IDataReader GetDataUser(string filter)
        {
            AdmiUsuarioDB objDB = new AdmiUsuarioDB();
            SessionManager oSessionManager = new SessionManager("");
            try
            {
                //oSessionManager.CreateConnection();
                return objDB.GetDataUser(oSessionManager,filter,0,0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }
        public DataTable GetDataUser(string connection,string filter)
        {
            AdmiUsuarioDB objDB = new AdmiUsuarioDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                //oSessionManager.CreateConnection();
                return objDB.GetDataUser(oSessionManager, filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }
        public List<AdmiUsuario> GetList(string connection, string filter, int startRowIndex, int maximumRows)
        {
            AdmiUsuarioDB objDB = new AdmiUsuarioDB();
            SessionManager oSessionManager = new SessionManager(connection);
            List<AdmiUsuario> olUsuario;
            Encryption encryptor = new Encryption();

            try
            {
                //oSessionManager.CreateConnection();
                olUsuario = objDB.GetAllList(oSessionManager, filter, startRowIndex, maximumRows);
                //foreach (AdmiUsuario oUsuario in olUsuario)
                //    oUsuario.UsuaClave = encryptor.Decrypt(oUsuario.UsuaClave);
                return olUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objEntity"></param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public string Insert(AdmiUsuario objEntity)
        {
            string id = string.Empty;
            AdmiUsuarioDB objDB = new AdmiUsuarioDB();
            PersonasDB objDBPersonas = new PersonasDB();
            SessionManager oSessionManager = new SessionManager("");
            try
            {
                objEntity.LogsUsuario = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                objEntity.LogsFecha = DateTime.Now;
                //if (String.IsNullOrEmpty(objEntity.UsuaClave))
                //{
                //    objEntity.UsuaClave = new Encryption().Encrypt(objEntity.UsuaUsuario.Substring(0, 3) + objEntity.UsuaIdentifica.Substring(0, 5));
                //    objEntity.UsuaFechavence = DateTime.Now.AddDays(-1);
                //}
                //else
                //{
                //    objEntity.UsuaClave = new Encryption().Encrypt(objEntity.UsuaClave);
                //    objEntity.UsuaFechavence = DateTime.Now.AddMonths(3);
                //}

                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                id = objDB.Add(oSessionManager, objEntity);
                //List<Personas> tmp = objDBPersonas.GetAllList(oSessionManager, " personas.pers_nrodoc = " + objEntity.UsuaIdentifica, 0, 0);
                //if (tmp == null || tmp.Count == 0)
                //{
                //    Personas objPersonas = new Personas();
                //    objPersonas.PersTipoDoc = 1;
                //    objPersonas.PersTelefono = objEntity.UsuaTelefonos;
                //    objPersonas.PersNroDoc = long.Parse(objEntity.UsuaIdentifica);
                //    objPersonas.PersNombre = objEntity.UsuaNombres;
                //    objPersonas.PersEmail = objEntity.UsuaEmail;
                //    objPersonas.PersDireccion = objEntity.UsuaDireccion;
                //    objPersonas.ParmTipoDoc = 1;
                //    objPersonas.DiviCodigo = "1690410001";
                //    objDBPersonas.Add(oSessionManager, objPersonas);
                //}
                oSessionManager.CommitTranstaction();
                return id;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objEntity"></param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Update(AdmiUsuario objEntity)
        {
            AdmiUsuarioDB objDB = new AdmiUsuarioDB();
            SessionManager oSessionManager = new SessionManager("");
            try
            {
                objEntity.LogsUsuario = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                objEntity.LogsFecha = DateTime.Now;
                objEntity.UsuaFechavence = DateTime.Now;
                //if (String.IsNullOrEmpty(objEntity.UsuaClave))
                //{
                //    objEntity.UsuaClave = new Encryption().Encrypt(objEntity.UsuaUsuario.Substring(0, 3) + objEntity.UsuaIdentifica.Substring(0, 5));
                //    objEntity.UsuaFechavence = DateTime.Now.AddDays(-1);
                //}
                //else
                //{
                //    objEntity.UsuaClave = new Encryption().Encrypt(objEntity.UsuaClave);
                //    objEntity.UsuaFechavence = DateTime.Now.AddMonths(3);
                //}

                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                objDB.Update(oSessionManager, objEntity);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objEntity"></param>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Delete(AdmiUsuario objEntity)
        {
            AdmiUsuarioDB objDB = new AdmiUsuarioDB();
            SessionManager oSessionManager = new SessionManager("");
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                objDB.Delete(oSessionManager, objEntity);
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

        public void SetRolesByUser(string connection, List<AdmiRolxUsuario> olEntity, List<AdmiArbolOpcion> olRestictions, string userLogon)
        {
            AdmiUsuarioDB objDB = new AdmiUsuarioDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                objDB.SetRolesByUser(oSessionManager, olEntity, olRestictions, userLogon, userId);
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

        public void ResetPassword(string connection, string userLogon, string defaultPassword)
        {
            AdmiUsuarioDB objDB = new AdmiUsuarioDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                Encryption encrypter = new Encryption();
                //defaultPassword = encrypter.Encrypt(defaultPassword);

                int userId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                objDB.ResetPassword(oSessionManager, userLogon, defaultPassword, userId);
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

        public AdmiUsuario UserLogin(string connection, string userLogon, string password, int system)
        {
            AdmiUsuarioDB objDB = new AdmiUsuarioDB();

            SessionManager oSessionManager = new SessionManager(connection);
            Encryption encrypter = new Encryption();
            try
            {
                //oSessionManager.CreateConnection();
                //string pwd = encrypter.Encrypt(password);
                string pwd = password;
                AdmiUsuario oEntity = objDB.UserLogin(oSessionManager, userLogon, pwd, system);
                if (oEntity != null)
                {
                    string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ip == null)
                        ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    oEntity.UsuaUltimaip = ip;
                    oEntity.UsuaUltimoacceso = DateTime.Now;
                    oEntity.UsuaUltimoequipo = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
                    objDB.Update(oSessionManager, oEntity);
                }
                return oEntity;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        public void ChangePassword(string connection, string userLogon, string password)
        {
            AdmiUsuarioDB objDB = new AdmiUsuarioDB();

            SessionManager oSessionManager = new SessionManager(connection);
            Encryption encrypter = new Encryption();
            try
            {
                //oSessionManager.CreateConnection();
                //string pwd = encrypter.Encrypt(password);
                string pwd = password;
                objDB.ChangePassword(oSessionManager, userLogon, pwd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }
    }
}
