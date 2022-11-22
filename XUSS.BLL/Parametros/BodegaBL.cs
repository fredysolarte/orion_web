using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Parametros
{
    public class BodegaBL
    {
        public DataTable GetBodegas(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return BodegaBD.GetBodegas(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetLineaXBodega(string connection, string filter, string CODEMP, string ABBODEGA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return BodegaBD.GetLineaXBodega(oSessionManager, filter, CODEMP, ABBODEGA);
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
        public DataTable GetUsuariosXBodega(string connection, string filter, string BUBODEGA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return BodegaBD.GetUsuariosXBodega(oSessionManager, filter, BUBODEGA);
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
        public DataTable GetBodegasXUsuario(string connection, string usua_usuario)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return BodegaBD.GetBodegasXUsuario(oSessionManager, usua_usuario);
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
        public DataTable GetBodegasXUsuarioDef(string connection, string usua_usuario)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return BodegaBD.GetBodegasXUsuarioDef(oSessionManager, usua_usuario);
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
        public int InsertBodega(string connection, string BDCODEMP, string BDBODEGA, string BDNOMBRE, string BDDIRECC, string BDRESPON,
                                                                      string BDBODCON, DateTime? BDCIERRE, string BDCONSIG, string BDESTADO, string BDCAUSAE,
                                                                      string BDNMUSER, string BDMNCAJA, string BDBODSOC, int? BDCAJADF, string BDCENCOS,
                                                                      string BDPORMAX, string BDCAJAOP, string BDTELEFO, string BDALMACE, string BDPAIS, string CDCIUDAD,
                                                                      string BDCDTEC1, string BDDTTEC1,
                                                                      object tbArtXBod, object tbUsrXBod)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                oSessionManager.BeginTransaction();
                BodegaBD.InsertBodega(oSessionManager, BDCODEMP, BDBODEGA, BDNOMBRE, BDDIRECC, BDRESPON, BDBODCON, BDCIERRE, BDCONSIG, BDESTADO, BDCAUSAE,
                                                      BDNMUSER, BDMNCAJA, BDBODSOC, BDCAJADF, BDCENCOS, BDPORMAX, BDCAJAOP, BDTELEFO, BDALMACE, BDPAIS, CDCIUDAD, BDCDTEC1, BDDTTEC1);
                //Lineas X Bodega
                foreach (DataRow rw in (tbArtXBod as DataTable).Rows)
                {
                    if (BodegaBD.ExisteLineaXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["ABTIPPRO"])) > 0)
                        BodegaBD.DeleteLineaXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["ABTIPPRO"]));

                    if (!string.IsNullOrEmpty(Convert.ToString(rw["ABTIPPRO"])))
                    {
                        BodegaBD.InsertLineaXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["ABTIPPRO"]), Convert.ToString(rw["ABNOMBRE"]), Convert.ToString(rw["ABMNLOTE"]), Convert.ToString(rw["ABMNELEM"]), Convert.ToString(rw["ABMNCONT"]), Convert.ToString(rw["ABMNBONI"]), Convert.ToString(rw["ABMNNREL"]), Convert.ToString(rw["ABFRMUBI"]), Convert.ToString(rw["ABESTADO"]), Convert.ToString(rw["ABCAUSAE"]),
                            Convert.ToString(rw["ABNMUSER"]), Convert.ToString(rw["ABCTACON"]), rw.IsNull("ABENTLIM") ? null : (double?)Convert.ToDouble(rw["ABENTLIM"]), rw.IsNull("ABSALLIM") ? null : (double?)Convert.ToDouble(rw["ABSALLIM"]), Convert.ToString(rw["ABELEMUAT"]));
                    }
                }

                //Usuarios X Bodega
                foreach (DataRow rw in (tbUsrXBod as DataTable).Rows)
                {
                    if (BodegaBD.ExisteUsuarioXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["BUCDUSER"])) > 0)
                        BodegaBD.DeleteUsuarioXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["BUCDUSER"]));

                    if (!string.IsNullOrEmpty(Convert.ToString(rw["BUCDUSER"])))
                    {
                        BodegaBD.InsertUsuarioXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["BUCDUSER"]), "AC", ".", BDNMUSER);
                    }
                }
                oSessionManager.CommitTranstaction();
                return 1;
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
        public int UpdateBodega(string connection, string BDCODEMP, string original_BDBODEGA, string BDNOMBRE, string BDDIRECC, string BDRESPON,
                                                                      string BDBODCON, DateTime? BDCIERRE, string BDCONSIG, string BDESTADO, string BDCAUSAE,
                                                                      string BDNMUSER, string BDMNCAJA, string BDBODSOC, int? BDCAJADF, string BDCENCOS,
                                                                      string BDPORMAX, string BDCAJAOP, string BDTELEFO, string BDALMACE, string BDPAIS, string CDCIUDAD,
                                                                      string BDCDTEC1, string BDDTTEC1,
                                                                      object tbArtXBod, object tbUsrXBod)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                string BDBODEGA = original_BDBODEGA;
                oSessionManager.BeginTransaction();
                BodegaBD.UpdateBodega(oSessionManager, BDCODEMP, BDBODEGA, BDNOMBRE, BDDIRECC, BDRESPON, BDBODCON, BDCIERRE, BDCONSIG, BDESTADO, BDCAUSAE,
                                                      BDNMUSER, BDMNCAJA, BDBODSOC, BDCAJADF, BDCENCOS, BDPORMAX, BDCAJAOP, BDTELEFO, BDALMACE, BDPAIS, CDCIUDAD, BDCDTEC1, BDDTTEC1);               
                //Lineas X Bodega
                BodegaBD.UpdateLineaXBodega(oSessionManager, BDCODEMP, BDBODEGA, "");
                foreach (DataRow rw in (tbArtXBod as DataTable).Rows)
                {                    
                    if (BodegaBD.ExisteLineaXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["ABTIPPRO"])) > 0)
                    {    //BodegaBD.DeleteLineaXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["ABTIPPRO"]));
                        BodegaBD.UpdateLineaXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["ABTIPPRO"]), Convert.ToString(rw["ABNOMBRE"]),
                                                    Convert.ToString(rw["ABMNLOTE"]),Convert.ToString(rw["ABMNELEM"]),Convert.ToString(rw["ABMNCONT"]),Convert.ToString(rw["ABMNBONI"]),
                                                    Convert.ToString(rw["ABMNNREL"]),Convert.ToString(rw["ABFRMUBI"]),Convert.ToString(rw["ABESTADO"]),"'",Convert.ToString(rw["ABNMUSER"]),Convert.ToString(rw["ABCTACON"]),
                                                    0, 0, Convert.ToString(rw["ABELEMUAT"]));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(rw["ABTIPPRO"])))
                        {
                            BodegaBD.InsertLineaXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["ABTIPPRO"]), Convert.ToString(rw["ABNOMBRE"]), Convert.ToString(rw["ABMNLOTE"]), Convert.ToString(rw["ABMNELEM"]), Convert.ToString(rw["ABMNCONT"]), Convert.ToString(rw["ABMNBONI"]), Convert.ToString(rw["ABMNNREL"]), Convert.ToString(rw["ABFRMUBI"]), Convert.ToString(rw["ABESTADO"]), Convert.ToString(rw["ABCAUSAE"]),
                                Convert.ToString(rw["ABNMUSER"]), Convert.ToString(rw["ABCTACON"]), rw.IsNull("ABENTLIM") ? null : (double?)Convert.ToDouble(rw["ABENTLIM"]), rw.IsNull("ABSALLIM") ? null : (double?)Convert.ToDouble(rw["ABSALLIM"]), Convert.ToString(rw["ABELEMUAT"]));
                        }
                    }
                }

                
                BodegaBD.DeleteUsuarioXBodega(oSessionManager, BDCODEMP, BDBODEGA);
                //Usuarios X Bodega
                foreach (DataRow rw in (tbUsrXBod as DataTable).Rows)
                {
                    //if (BodegaBD.ExisteUsuarioXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["BUCDUSER"])) > 0)
                    //    BodegaBD.DeleteUsuarioXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["BUCDUSER"]));

                    if (!string.IsNullOrEmpty(Convert.ToString(rw["BUCDUSER"])))
                    {
                        BodegaBD.InsertUsuarioXBodega(oSessionManager, BDCODEMP, BDBODEGA, Convert.ToString(rw["BUCDUSER"]), "AC", ".", BDNMUSER);
                    }
                }
                oSessionManager.CommitTranstaction();
                return 1;
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

    }
}
