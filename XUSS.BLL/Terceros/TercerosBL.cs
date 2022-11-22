using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using XUSS.DAL.Terceros;
using System.Data;
using DataAccess;
using XUSS.BLL.Comun;
using XUSS.DAL.Contabilidad;
using System.IO;
using XUSS.DAL.Nomina;

namespace XUSS.BLL.Terceros
{
    [DataObject(true)]
    public class TercerosBL
    {
        //Terceros
        #region
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetTerceros(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return TercerosBD.GetTerceros(oSessionManager, filter, startRowIndex, maximumRows);
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
        public IDataReader GetTercerosR(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return TercerosBD.GetTercerosR(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetEstudios(string connection, int CODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return TercerosBD.GetEstudios(oSessionManager,CODTER);
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
        public DataTable GetExperiencia(string connection, int CODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return TercerosBD.GetExperiencia(oSessionManager, CODTER);
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
        public DataTable GetInfomormacionLaboral(string connection, int CODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return TercerosBD.GetInfomormacionLaboral(oSessionManager, CODTER);
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
        public Boolean ExisteTercero(string connection, string TRCODNIT)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                if (TercerosBD.ExisteTercero(oSessionManager, TRCODNIT) != 0)
                    return true;
                else
                    return false;
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
        public static DataTable GetTerminosPago(string connection, string inCodEmp)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.GetTerminosPago(oSessionManager, inCodEmp);
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
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int InsertTercero(string connection, string TRCODEMP, int TRCODTER, string TRNOMBRE, string TRNOMBR2, string TRCONTAC, int? TRCODEDI,
                                 string TRCODNIT, string TRDIGVER, string TRDIRECC, string TRDIREC2, string TRDELEGA, string TRCOLONI, string TRNROTEL, string TRNROFAX,
                                 string TRPOSTAL, string TRCORREO, string TRCIUDAD, string TRCIUDA2, string TRCDPAIS, string TRMONEDA, string TRIDIOMA, string TRBODEGA,
                                 string TRTERPAG, string TRMODDES, string TRTERDES, string TRCATEGO, int? TRAGENTE, string TRLISPRE, string TRLISPRA, double? TRDESCUE,
                                 double? TRCUPOCR, string TRINDCLI, string TRINDPRO, string TRINDSOP, string TRINDEMP, string TRINDSOC, string TRINDVEN, string TRINDFOR, string TRCDCLA1,
                                 string TRCDCLA2, string TRCDCLA3, string TRCDCLA4, string TRCDCLA5, string TRCDCLA6, string TRDTTEC1, string TRDTTEC2, string TRDTTEC3,
                                 string TRDTTEC4, double? TRDTTEC5, double? TRDTTEC6, int? TRPROGDT, string TRESTADO, string TRCAUSAE, string TRNMUSER, string TROBSERV,
                                 DateTime? TRFECNAC, string TRRESPAL, double? TRRESCUP, string TRAPELLI, string TRNOMBR3, string TRTIPDOC, string TRDIGCHK,
                                 string TRCODZONA, string TRTIPREG, string TRGRANCT, string TRAUTORE, string TRNOMCOMERCIAL, object tbFamilia, object tbTitulos,object tbContratos,object tbPlanillaNM, object tbHorizontal)
        {
            int ln_consecutivo = 0;
            PlanillaBD Obj = new PlanillaBD();
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                oSessionManager.BeginTransaction();

                ln_consecutivo = ComunBL.GeneraConsecutivo(connection, "CODTER", TRCODEMP);

                foreach (DataRow dt in Obj.GetPuc(oSessionManager, " AND PC_CODIGO='" + TRAUTORE + "'").Rows)
                    TRAUTORE = Convert.ToString(dt["PC_ID"]);

                TercerosBD.InsertTercero(oSessionManager,TRCODEMP,ln_consecutivo,TRNOMBRE,TRNOMBR2,TRCONTAC,TRCODEDI,TRCODNIT,TRDIGVER,TRDIRECC,TRDIREC2,TRDELEGA,TRCOLONI,TRNROTEL,
                                                TRNROFAX,TRPOSTAL,TRCORREO,TRCIUDAD,TRCIUDA2,TRCDPAIS,TRMONEDA,TRIDIOMA,TRBODEGA,TRTERPAG,TRMODDES,TRTERDES,TRCATEGO,TRAGENTE,TRLISPRE,
                                                TRLISPRA,TRDESCUE,TRCUPOCR,TRINDCLI,TRINDPRO,TRINDSOP,TRINDEMP,TRINDSOC,TRINDVEN, TRINDFOR, TRCDCLA1,TRCDCLA2,TRCDCLA3,TRCDCLA4,TRCDCLA5,TRCDCLA6,
                                                TRDTTEC1,TRDTTEC2,TRDTTEC3,TRDTTEC4,TRDTTEC5,TRDTTEC6,TRPROGDT,TRESTADO,TRCAUSAE,TRNMUSER,TROBSERV,TRFECNAC,TRRESPAL,TRRESCUP,TRAPELLI,
                                                TRNOMBR3,TRTIPDOC,TRDIGCHK,TRCODZONA,TRTIPREG,TRGRANCT,TRAUTORE, TRNOMCOMERCIAL);

                //Insert Familia
                foreach (DataRow rw in (tbFamilia as DataTable).Rows)
                    TercerosBD.InsertFamilia(oSessionManager, TRCODEMP, ln_consecutivo, Convert.ToString(rw["FM_TIPDOC"]), Convert.ToString(rw["FM_IDENTIFICACION"]), Convert.ToString(rw["FM_PNOMBRE"]), Convert.ToString(rw["FM_SNOMBRE"]), Convert.ToString(rw["FM_PAPELLIDO"]),
                        Convert.ToString(rw["FM_SAPELLIDO"]), Convert.ToDateTime(rw["FM_FNACIMIENTO"]), Convert.ToString(rw["FM_PARENTESCO"]), Convert.ToString(rw["FM_DIRECCION"]), Convert.ToString(rw["FM_EMAIL"]), Convert.ToString(rw["FM_TELEFONO"]), Convert.ToString(rw["FM_TIPO"]), TRNMUSER);

                foreach (DataRow rw in (tbTitulos as DataTable).Rows)
                    TercerosBD.InserTitulos(oSessionManager, TRCODEMP, ln_consecutivo, Convert.ToString(rw["TT_TIPO"]), Convert.ToString(rw["TT_PROFESION"]), Convert.ToString(rw["TT_DESCRIPCION"]), Convert.ToDateTime(rw["TT_FECHA"]), Convert.ToDateTime(rw["TT_FECHAVEN"]),
                        Convert.ToString(rw["TT_ALERTA"]), null,TRNMUSER);

                foreach (DataRow rw in (tbContratos as DataTable).Rows)
                    TercerosBD.InsertContratos(oSessionManager, TRCODEMP, ln_consecutivo, Convert.ToString(rw["CT_TNOVEDAD"]), Convert.ToString(rw["CT_TCONTRATO"]), Convert.ToString(rw["CT_CARGO"]), Convert.ToDateTime(rw["CT_FINGRESO"]), Convert.ToDouble(rw["CT_SALARIO"]), TRNMUSER, "AC");

                foreach (DataRow rw in (tbPlanillaNM as DataTable).Rows)
                    PlanillaConceptosNMBD.InsertPlanillaNMTercero(oSessionManager, Convert.ToInt32(rw["PH_CODIGO"]), ln_consecutivo);

                foreach(DataRow rw in (tbHorizontal as DataTable).Rows)
                    TercerosBD.InsertPropiedadHorizontal(oSessionManager, Convert.ToString(rw["PH_CTACONTRATO"]), Convert.ToString(rw["PH_POLIZA"]), TRCODEMP, ln_consecutivo, Convert.ToString(rw["PH_EDIFICIO"]), Convert.ToString(rw["PH_PORTAL"]), Convert.ToString(rw["PH_PISO"]), Convert.ToString(rw["PH_ESCALERA"]), 
                        Convert.ToString(rw["PH_OBJCONEXION"]), Convert.ToString(rw["PH_PTOSUMINISTRO"]), Convert.ToString(rw["PH_INSTALACION"]), Convert.ToString(rw["PH_UBCAPARATO"]),TRNMUSER);

                oSessionManager.CommitTranstaction();
                return ln_consecutivo;
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
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public int UpdteTercero(string connection, string TRCODEMP, string TRNOMBRE, string TRNOMBR2, string TRCONTAC, int? TRCODEDI,
                                 string TRCODNIT, string TRDIGVER, string TRDIRECC, string TRDIREC2, string TRDELEGA, string TRCOLONI, string TRNROTEL, string TRNROFAX,
                                 string TRPOSTAL, string TRCORREO, string TRCIUDAD, string TRCIUDA2, string TRCDPAIS, string TRMONEDA, string TRIDIOMA, string TRBODEGA,
                                 string TRTERPAG, string TRMODDES, string TRTERDES, string TRCATEGO, int? TRAGENTE, string TRLISPRE, string TRLISPRA, double? TRDESCUE,
                                 double? TRCUPOCR, string TRINDCLI, string TRINDPRO, string TRINDSOP, string TRINDEMP, string TRINDSOC, string TRINDVEN,string TRINDFOR, string TRCDCLA1,
                                 string TRCDCLA2, string TRCDCLA3, string TRCDCLA4, string TRCDCLA5, string TRCDCLA6, string TRDTTEC1, string TRDTTEC2, string TRDTTEC3,
                                 string TRDTTEC4, double? TRDTTEC5, double? TRDTTEC6, int? TRPROGDT, string TRESTADO, string TRCAUSAE, string TRNMUSER, string TROBSERV,
                                 DateTime? TRFECNAC, string TRRESPAL, double? TRRESCUP, string TRAPELLI, string TRNOMBR3, string TRTIPDOC, string TRDIGCHK,
                                 string TRCODZONA, string TRTIPREG, string TRGRANCT, string TRAUTORE, string TRNOMCOMERCIAL, object tbCuentas, object tbHorizontal,int original_TRCODTER)
        {
            PlanillaBD Obj = new PlanillaBD();
            SessionManager oSessionManager = new SessionManager(connection);
            int pc_id = 0;
            try
            {
                oSessionManager.BeginTransaction();                

                TercerosBD.UpdateTercero(oSessionManager, TRCODEMP, original_TRCODTER, TRNOMBRE, TRNOMBR2, TRCONTAC, TRCODEDI, TRCODNIT, TRDIGVER, TRDIRECC, TRDIREC2, TRDELEGA, TRCOLONI, TRNROTEL,
                                                TRNROFAX, TRPOSTAL, TRCORREO, TRCIUDAD, TRCIUDA2, TRCDPAIS, TRMONEDA, TRIDIOMA, TRBODEGA, TRTERPAG, TRMODDES, TRTERDES, TRCATEGO, TRAGENTE, TRLISPRE,
                                                TRLISPRA, TRDESCUE, TRCUPOCR, TRINDCLI, TRINDPRO, TRINDSOP, TRINDEMP, TRINDSOC, TRINDVEN, TRINDFOR, TRCDCLA1, TRCDCLA2, TRCDCLA3, TRCDCLA4, TRCDCLA5, TRCDCLA6,
                                                TRDTTEC1, TRDTTEC2, TRDTTEC3, TRDTTEC4, TRDTTEC5, TRDTTEC6, TRPROGDT, TRESTADO, TRCAUSAE, TRNMUSER, TROBSERV, TRFECNAC, TRRESPAL, TRRESCUP, TRAPELLI,
                                                TRNOMBR3, TRTIPDOC, TRDIGCHK, TRCODZONA, TRTIPREG, TRGRANCT, TRAUTORE, TRNOMCOMERCIAL);

                foreach (DataRow rw in (tbCuentas as DataTable).Rows)
                {
                    foreach (DataRow dt in Obj.GetPuc(oSessionManager, " AND PC_CODIGO='" + Convert.ToString(rw["PC_CODIGO"]) + "'").Rows)
                        pc_id = Convert.ToInt32(dt["PC_ID"]);

                    if (TercerosBD.ExisteCuentaXTercero(oSessionManager, Convert.ToInt32(rw["CTT_ID"])) == 0)                                           
                        TercerosBD.InsertCuentasxTercero(oSessionManager, pc_id, original_TRCODTER, Convert.ToString(rw["CTT_NATURALEZA"]), Convert.ToString(rw["CTT_BASE"]), Convert.ToString(rw["CTT_IMPUESTO"]), 0);
                    else
                        TercerosBD.UpdateCuentasxTercero(oSessionManager, Convert.ToInt32(rw["CTT_ID"]), pc_id, Convert.ToString(rw["CTT_NATURALEZA"]), Convert.ToString(rw["CTT_BASE"]), Convert.ToString(rw["CTT_IMPUESTO"]), 0);

                }

                foreach (DataRow rw in (tbHorizontal as DataTable).Rows)
                {
                    if (TercerosBD.ExistePropiedaHorizontal(oSessionManager, TRCODEMP, original_TRCODTER, Convert.ToString(rw["PH_EDIFICIO"]), Convert.ToString(rw["PH_ESCALERA"])) == 0)
                    {
                        TercerosBD.InsertPropiedadHorizontal(oSessionManager, Convert.ToString(rw["PH_CTACONTRATO"]), Convert.ToString(rw["PH_POLIZA"]), TRCODEMP, original_TRCODTER, Convert.ToString(rw["PH_EDIFICIO"]), Convert.ToString(rw["PH_PORTAL"]), Convert.ToString(rw["PH_PISO"]), Convert.ToString(rw["PH_ESCALERA"]),
                        Convert.ToString(rw["PH_OBJCONEXION"]), Convert.ToString(rw["PH_PTOSUMINISTRO"]), Convert.ToString(rw["PH_INSTALACION"]), Convert.ToString(rw["PH_UBCAPARATO"]), TRNMUSER);
                    }
                    else
                    {
                        TercerosBD.UpdatePropiedadHorizontal(oSessionManager, Convert.ToString(rw["PH_CTACONTRATO"]), Convert.ToString(rw["PH_POLIZA"]), Convert.ToString(rw["PH_OBJCONEXION"]), Convert.ToString(rw["PH_PTOSUMINISTRO"]), Convert.ToString(rw["PH_INSTALACION"]),
                                                             Convert.ToString(rw["PH_UBCAPARATO"]), TRNMUSER, Convert.ToInt32(rw["PH_CODIGO"]));
                        
                        TercerosBD.UpdateCampana(oSessionManager, Convert.ToInt32(rw["PH_CODIGO"]), rw["CP_ID"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(rw["CP_ID"]));
                    }
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
                Obj = null;
            }
        }
        #endregion
        //Sucursales
        #region
        public DataTable GetSucursales(string connection, string SC_CODEMP, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.GetSucursales(oSessionManager, SC_CODEMP,TRCODTER);
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
        public int InsertSucursales(string connection, string SC_CODEMP, int TRCODTER, string SC_NOMBRE, string SC_TELEFONO, string SC_DIRECCION,
                                                string SC_DIRECCION2, string SC_PAIS, string SC_CIUDAD, string SC_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return TercerosBD.InsertSucursales(oSessionManager, SC_CODEMP, TRCODTER, SC_NOMBRE, SC_TELEFONO, SC_DIRECCION,
                                                   SC_DIRECCION2, SC_PAIS, SC_CIUDAD, SC_ESTADO);
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
        public IDataReader GetSucursalesID(string connection, string SC_CODEMP, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.GetSucursalesID(oSessionManager, SC_CODEMP, TRCODTER);
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
        #endregion
        //Impuestos & Contabilidad
        #region 
        public DataTable GetImpuestosxTercero(string connection, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.GetImpuestosxTercero(oSessionManager, TRCODTER);
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
        public int InsertImpuestosxTercero(string connection, int PH_CODIGO, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.InsertImpuestosxTercero(oSessionManager, PH_CODIGO, TRCODTER);
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
        public int DeleteImpuestosxTercero(string connection, int PH_CODIGO, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.DeleteImpuestosxTercero(oSessionManager, PH_CODIGO, TRCODTER);
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

        public DataTable GetCuentasxTercero(string connection, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.GetCuentasxTercero(oSessionManager, TRCODTER);
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
        #endregion
        //Familia & Titulos
        #region
        public DataTable GetOtrosAnexos(string connection, string TRCODEMP, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return TercerosBD.GetOtrosAnexos(oSessionManager, TRCODEMP, TRCODTER);
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
        public DataTable GetFamilia(string connection, string TRCODEMP,int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return TercerosBD.GetFamilia(oSessionManager, TRCODEMP, TRCODTER);
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
        public DataTable GetTitulos(string connection, string TRCODEMP, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.GetTitulos(oSessionManager, TRCODEMP, TRCODTER);
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
        public int insertTitulo(string connection, string TT_CODEMP, int TRCODTER, string TT_TIPO, string TT_PROFESION, string TT_DESCRIPCION, DateTime? TT_FECHA, DateTime? TT_FECHAVEN, string TT_ALERTA, string TT_RUTA,string TT_USUARIO)
        {
            Stream ioArchivo = File.OpenRead(TT_RUTA);
            byte[] result;
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                using (MemoryStream ms = new MemoryStream())
                {
                    ioArchivo.CopyTo(ms);
                    result = ms.ToArray();
                }

                return TercerosBD.InserTitulos(oSessionManager, TT_CODEMP, TRCODTER, TT_TIPO, TT_PROFESION, TT_DESCRIPCION, TT_FECHA, TT_FECHAVEN, TT_ALERTA, result, TT_USUARIO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                result = null;
            }
        }
        public int InsertFamilia(string connection, string FM_CODEMP, int TRCODTER, string FM_TIPDOC, string FM_IDENTIFICACION, string FM_PNOMBRE, string FM_SNOMBRE, string FM_PAPELLIDO, string FM_SAPELLIDO, DateTime? FM_FNACIMIENTO,
                                string FM_PARENTESCO, string FM_DIRECCION, string FM_EMAIL, string FM_TELEFONO, string FM_TIPO, string FM_USUARIO)
        {
            
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {

                return TercerosBD.InsertFamilia(oSessionManager, FM_CODEMP, TRCODTER, FM_TIPDOC, FM_IDENTIFICACION, FM_PNOMBRE, FM_SNOMBRE, FM_PAPELLIDO, FM_SAPELLIDO, FM_FNACIMIENTO, FM_PARENTESCO, FM_DIRECCION, FM_EMAIL, FM_TELEFONO, FM_TIPO, FM_USUARIO);
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
        public int InsertOtrosAnexos(string connection, string TRCODEMP, int TRCODTER, string OA_DESCRIPCION, string OA_ALERTA, DateTime? OA_FECVENCIMINTO, string OA_RUTA, string OA_USUARIO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            Stream ioArchivo = File.OpenRead(OA_RUTA);
            byte[] result;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ioArchivo.CopyTo(ms);
                    result = ms.ToArray();
                }
                return TercerosBD.InsertOtrosAnexos(oSessionManager, TRCODEMP, TRCODTER, OA_DESCRIPCION, OA_ALERTA, OA_FECVENCIMINTO, result, OA_USUARIO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                result = null;
            }
        }
        public int DeleteFamilia(string connection, int FM_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.DeleteFamilia(oSessionManager, FM_CODIGO);
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
        public int DeleteTitulos(string connection, int TT_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.DeleteTitulos(oSessionManager, TT_CODIGO);
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
        public int DeleteOtrosAnexos(string connection, int OA_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.DeleteOtrosAnexos(oSessionManager, OA_CONSECUTIVO);
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
        public object GetImagenOtros(string connection, int OA_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(null);            
            try
            {
                return TercerosBD.GetImagenOtros(oSessionManager, OA_CONSECUTIVO);
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
        public object GetImagenAcademico(string connection, int TT_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return TercerosBD.GetImagenAcademico(oSessionManager, TT_CODIGO);
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
        #endregion
        //Contratos & Nomina
        #region
        public DataTable GetContratos(string connection, string TRCODEMP, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return TercerosBD.GetContratos(oSessionManager, TRCODEMP,TRCODTER);
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
        public int InsertContratos(string connection, string TRCODEMP, int TRCODTER, string CT_TNOVEDAD, string CT_TCONTRATO, string CT_CARGO, DateTime CT_FINGRESO, double CT_SALARIO, string CT_USUARIO, string CT_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return TercerosBD.InsertContratos(oSessionManager, TRCODEMP, TRCODTER, CT_TNOVEDAD, CT_TCONTRATO, CT_CARGO, CT_FINGRESO, CT_SALARIO, CT_USUARIO, CT_ESTADO);
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
        public int UpdateContratos(string connection, int CT_ID, string CT_TNOVEDAD, string CT_TCONTRATO, string CT_CARGO, DateTime CT_FINGRESO, DateTime? CT_FTERMINACION, double CT_SALARIO, string CT_USUARIO, string CT_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return TercerosBD.UpdateContratos(oSessionManager, CT_ID, CT_TNOVEDAD, CT_TCONTRATO, CT_CARGO, CT_FINGRESO, CT_FTERMINACION, CT_SALARIO, CT_USUARIO, CT_ESTADO);
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
        public int DeleteContrato(string connection, int CT_ID)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try {
                TercerosBD.DeleteContratos(oSessionManager, CT_ID);
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
        public DataTable GetTercerosPlanillaNomina(string connection,string filter)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return TercerosBD.GetTercerosPlanillaNomina(oSessionManager,filter);
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
        #endregion
        //Propiedad Horizontal
        #region
        public DataTable GetPropiedadHorizontal(string connection, string PH_CODEMP, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return TercerosBD.GetPropiedadHorizontal(oSessionManager, PH_CODEMP, TRCODTER);
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
        public DataTable GetImagenesInstalacion(string connection, int PH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.GetImagenesInstalacion(oSessionManager, PH_CODIGO);
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
        public DataTable GetImagenesComercial(string connection, int PH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.GetImagenesComercial(oSessionManager, PH_CODIGO);
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
        public object GetImageneComercial(string connection, int EC_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.GetImageneComercial(oSessionManager, EC_CODIGO);
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
        public DataTable GetImageneDesmonte(string connection, int PH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TercerosBD.GetImageneDesmonte(oSessionManager, PH_CODIGO);
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
        public DataTable GetgestionP(string connection, string SV_CODEMP, int? PH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return TercerosBD.GetgestionP(oSessionManager, SV_CODEMP, PH_CODIGO);
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
        public DataTable GetServicios(string connection, string SV_CODEMP, int? PH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return TercerosBD.GetgestionP(oSessionManager, SV_CODEMP, PH_CODIGO);
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
        public DataTable GetCampanas(string connection)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return TercerosBD.GetCampanas(oSessionManager);
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
        public int InsertDesmontaje(string connection, int PH_CODIGO, string DI_CODEMP, DateTime DI_FECHA, string DI_USUARIO,string DI_NRODOC)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return TercerosBD.InsertDesmontaje(oSessionManager, PH_CODIGO, DI_CODEMP, DI_FECHA, DI_USUARIO, DI_NRODOC);
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
        public int DeleteComercial(string connection, int PH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                TercerosBD.DeleteEvidenciaComercial(oSessionManager,PH_CODIGO);
                TercerosBD.DeleteComercial(oSessionManager, PH_CODIGO);
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
        public int DeleteDesmontaje(string connection, int PH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                TercerosBD.DeleteDesmontaje(oSessionManager, PH_CODIGO);                
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
        #endregion
    }
}
