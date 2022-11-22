using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Contabilidad;
using System.ComponentModel;
using XUSS.BLL.Comun;
using XUSS.DAL.Parametros;
using System.IO;

namespace XUSS.BLL.Contabilidad
{
    [DataObject(true)]
    public class CausacionProveedoresBL
    {
        public DataTable GetTiposDocumentos(string connection)
        {
            SessionManager oSessionManager = new SessionManager(null);
            CausacionProveedoresBD Obj = new CausacionProveedoresBD();
            try
            {
                return Obj.GetTiposDocumentos(oSessionManager);
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
        public DataTable GetMovimientosHD(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(null);
            CausacionProveedoresBD Obj = new CausacionProveedoresBD();
            try
            {
                return Obj.GetMovimientosHD(oSessionManager, filter);
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
        public DataTable GetMovimientosDT(string connection, string MVTH_CODEMP, int MVTH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            CausacionProveedoresBD Obj = new CausacionProveedoresBD();
            try
            {
                return Obj.GetMovimientosDT(oSessionManager, MVTH_CODEMP, MVTH_CODIGO);
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
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int InsertMovimiento(string connection, string MVTH_CODEMP, string TFTIPFAC,DateTime MVTH_FECMOV, string MVTH_CDUSER, string MVTH_ESTADO,
                                    Object dtDetalle)            
        {
            int ln_consecutivo = 0,ln_nrofac=0;
            string lc_documento = "",lc_prefijo="";
            SessionManager oSessionManager = new SessionManager(null);
            CausacionProveedoresBD Obj = new CausacionProveedoresBD();
            TipoFacturaBD ObjF = new TipoFacturaBD();

            try
            {                                
                
                oSessionManager.BeginTransaction();
                ln_consecutivo = ComunBL.GeneraConsecutivo(connection, "CAUDOC", MVTH_CODEMP);

                ObjF.UpdateTipoFactura(oSessionManager, MVTH_CODEMP, TFTIPFAC);
                //ln_consecutivo = ObjF.GetUltimoNroFac(oSessionManager, MVTH_CODEMP, TFTIPFAC);
                foreach (DataRow rw in (ObjF.GetTiposFactura(oSessionManager, "", 0, 0)).Rows)
                {
                    ln_nrofac = Convert.ToInt32(rw["TFNROFAC"]);
                    lc_prefijo = Convert.ToString(rw["TFPREFIJ"]);
                }

                lc_documento = Convert.ToString(MVTH_FECMOV.Day) + "-" + Convert.ToString(MVTH_FECMOV.Month) + "-" + Convert.ToString(MVTH_FECMOV.Year) + "-" + lc_prefijo + Convert.ToString(ln_nrofac);
                Obj.InsertMovimientoHD(oSessionManager, ln_consecutivo, MVTH_CODEMP, TFTIPFAC, ln_nrofac,lc_documento, MVTH_FECMOV.Day, MVTH_FECMOV.Month, MVTH_FECMOV.Year, MVTH_FECMOV, MVTH_CDUSER, MVTH_ESTADO);
                foreach (DataRow row in (dtDetalle as DataTable).Rows)
                {
                    Obj.InsertMovimientoDT(oSessionManager,MVTH_CODEMP, ln_consecutivo,Convert.ToInt32(row["PC_ID"]), Convert.ToInt32(row["TRCODTER"]),Convert.ToString(row["MVTD_TIPDOC"]), 
                        Convert.ToString(row["MVTD_NRODOC"]),Convert.ToDateTime(row["MVTD_FECDOC"]), Convert.ToString(row["MVTD_DESCRIPCION"]),Convert.ToDouble(row["MVTD_CREDITO"]), Convert.ToDouble(row["MVTD_DEBITO"]),
                        MVTH_CDUSER, MVTH_ESTADO);
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
                Obj = null;
                ObjF = null;
            }
        
        }
        public int ExisteDocumento(string connection, int TRCODTER, string MVTD_TIPDOC, string MVTD_NRODOC)
        {
            CausacionProveedoresBD Obj = new CausacionProveedoresBD();
            SessionManager oSessionManager = new SessionManager(null);
            try {

                return Obj.ExisteDocumento(oSessionManager, TRCODTER, MVTD_TIPDOC, MVTD_NRODOC);
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
        public DataTable GetEvidencias(string connection,  string MVEV_CODEMP, int MVTH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return CausacionProveedoresBD.GetEvidencias(oSessionManager, MVEV_CODEMP, MVTH_CODIGO);
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
        public int InsertEvidencia(string connection, string MVEV_CODEMP, int MVTH_CODIGO, string MVEV_DESCRIPCION, string MVEV_USUARIO, string inRuta)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            Stream ioArchivo = File.OpenRead(inRuta);
            byte[] result;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ioArchivo.CopyTo(ms);
                    result = ms.ToArray();
                }

                return CausacionProveedoresBD.InsertEvidencia(oSessionManager, MVEV_CODEMP, MVTH_CODIGO, MVEV_DESCRIPCION, result, MVEV_USUARIO);
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
        public DataTable GetEvidenciasAnexo(string connection, int MVEV_ID)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return CausacionProveedoresBD.GetEvidenciasFoto(oSessionManager, MVEV_ID);
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
