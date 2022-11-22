using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Parametros
{
    public class TipoFacturaBL
    {
        public DataTable GetTiposFactura(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TipoFacturaBD Obj = new TipoFacturaBD();
            try
            {
                return Obj.GetTiposFactura(oSessionManager, filter, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                Obj = null;
            }

        }
        public DataTable GetResolucion(string connection, string RFCODEMP, string RFTIPFAC)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TipoFacturaBD.GetResolucion(oSessionManager, RFCODEMP,RFTIPFAC);
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
        public int InsertTipoFactura(string connection, string TFCODEMP, string TFTIPFAC, string TFCLAFAC, string TFNOMBRE, int TFNROFAC, DateTime? TFFECFAC, string TFBODEGA,
                                     string TFCDTRAN, string TFEXPORT, string TFESTADO, string TFCAUSAE, string TFNMUSER, string TFLSTPRE, string TFPREFIJ, string TFREPORT,
                                     string TFFORFAC,int? TFMAXITM)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                TipoFacturaBD.InsertTipoFactura(oSessionManager,TFCODEMP, TFTIPFAC, TFCLAFAC, TFNOMBRE, TFNROFAC, TFFECFAC, TFBODEGA,
                                     TFCDTRAN, TFEXPORT, TFESTADO, TFCAUSAE, TFNMUSER, TFLSTPRE, TFPREFIJ, TFREPORT,
                                     TFFORFAC, TFMAXITM);
                return 0;
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

        public int UpdateTipoFactura(string connection, string TFCODEMP , string TFCLAFAC, string TFNOMBRE, int TFNROFAC, DateTime? TFFECFAC, string TFBODEGA,
                                     string TFCDTRAN, string TFEXPORT, string TFESTADO, string TFCAUSAE, string TFNMUSER, string TFLSTPRE, string TFPREFIJ, 
                                     string TFFORFAC, int? TFMAXITM, object tbUsuarios, object tbResolucion,string original_TFTIPFAC)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                TipoFacturaBD.UpdateTipoFactura(oSessionManager, TFCODEMP, original_TFTIPFAC, TFCLAFAC, TFNOMBRE, TFNROFAC, TFFECFAC, TFBODEGA,
                                     TFCDTRAN, TFEXPORT, TFESTADO, TFCAUSAE, TFNMUSER, TFLSTPRE, TFPREFIJ, TFFORFAC, TFMAXITM);
                foreach (DataRow rw in (tbUsuarios as DataTable).Rows)
                {
                    if (TipoFacturaBD.ExisteUsuarioxTF(oSessionManager, TFCODEMP, Convert.ToString(rw["FUUSUARIO"]), Convert.ToString(rw["FUTIPFAC"]), TFNMUSER) != 0)
                        TipoFacturaBD.DeleteUsuarioxTF(oSessionManager, TFCODEMP, Convert.ToString(rw["FUUSUARIO"]), Convert.ToString(rw["FUTIPFAC"]), TFNMUSER);
                    
                    if (Convert.ToString(rw["FUESTADO"])=="AC")
                        TipoFacturaBD.InsertUsuarioxTF(oSessionManager, TFCODEMP, Convert.ToString(rw["FUUSUARIO"]), Convert.ToString(rw["FUTIPFAC"]), TFNMUSER);
                }

                //Resoluciones
                TipoFacturaBD.DeleteResolucion(oSessionManager, TFCODEMP, original_TFTIPFAC);
                foreach (DataRow rw in (tbResolucion as DataTable).Rows)
                {
                    //if (TipoFacturaBD.ExisteResolucion(oSessionManager, TFCODEMP, original_TFTIPFAC, Convert.ToString(rw["RFNRORES"])) == 0)
                    TipoFacturaBD.InsertResolucion(oSessionManager, TFCODEMP, original_TFTIPFAC, Convert.ToString(rw["RFNRORES"]), Convert.ToDateTime(rw["RFFECRES"]), Convert.ToString(rw["RFTIPRES"]), Convert.ToInt32(rw["RFFACINI"]), Convert.ToInt32(rw["RFFACFIN"]), Convert.ToString(rw["RFESTADO"]), ".", TFNMUSER);
                    //else
                }

                oSessionManager.CommitTranstaction();
                return 0;
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
        public DataTable GetUsuarioxTF(string connection, string TFTIPFAC)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TipoFacturaBD.GetUsuarioxTF(oSessionManager, TFTIPFAC);   
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
        public DataTable GetTFxUsuario(string connection, string filter ,string usua_usuario)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TipoFacturaBD.GetTFxUsuario(oSessionManager, filter, usua_usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
    }
}
