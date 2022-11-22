using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using XUSS.DAL.Comun;
using XUSS.DAL.Tareas;
using XUSS.DAL.Terceros;

namespace XUSS.BLL.Parametros
{
    public class QuejasApelacionesBL
    {
        public DataTable GetQuejaSolicitudeApelacion(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return QuejasApelacionesBD.GetQuejaSolicitudeApelacion(oSessionManager, filter, startRowIndex, maximumRows);
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

        public int InsertQuejaSolicitudApelacion(string connection, string TQ_CODEMP, string TQ_TRATAMIENTO, int TRCODTER, string TQ_TIPO, DateTime TQ_FECHA, string TQ_DESCRIPCION,
                                                 string TRNOMBRE, string TRNOMBR2, string TRCODNIT, string TRDIRECC,string TRNROTEL,string TRCORREO,string TRAPELLI,string TRNOMBR3,
                                                 string TRTIPDOC, string TRDIGCHK, string TRDTTEC2, string TQ_ESTADO, string TQ_USUARIO)
        {
            int TQ_NUMERO = 0;
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                TQ_NUMERO = ComunBD.GeneraConsecutivo(oSessionManager, "CODQAS", TQ_CODEMP);
                if (TRCODTER == 0)
                {
                    TRCODTER = ComunBD.GeneraConsecutivo(oSessionManager, "CODTER", TQ_CODEMP);
                    TercerosBD.InsertTercero(oSessionManager, TQ_CODEMP, TRCODTER, TRNOMBRE, TRNOMBR2, null, null, TRCODNIT, null, TRDIRECC, null, null, null, TRNROTEL, null, null, TRCORREO, null, null, null, null, null, null, null, null, null, null, null, null, null,
                        null, null, "S", "N", "N", "N", "N", "N", "N", null, "Cta Contrato", null, null, null, null, null, TRDTTEC2, null, null, null, null, null, "AC", ".", TQ_USUARIO, null, null, null, null, TRAPELLI, TRNOMBR3, TRTIPDOC, TRDIGCHK, null, null, null, null, null);
                }

                QuejasApelacionesBD.InsertQuejaSolicitudApelacion(oSessionManager, TQ_CODEMP, TQ_NUMERO, TQ_TRATAMIENTO, TRCODTER, TQ_TIPO, TQ_FECHA, TQ_DESCRIPCION, TQ_ESTADO, TQ_USUARIO);

                return TQ_NUMERO;
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
        public int UpdateQuejaSolicitudApelacion(string connection, string TQ_CODEMP, int TQ_NUMERO, string TQ_TRATAMIENTO, int TRCODTER, string TQ_TIPO, DateTime TQ_FECHA, string TQ_DESCRIPCION,
                                                 string TRNOMBRE, string TRNOMBR2, string TRCODNIT, string TRDIRECC, string TRNROTEL, string TRCORREO, string TRAPELLI, string TRNOMBR3,
                                                 string TRTIPDOC, string TRDIGCHK, string TQ_ESTADO, string TQ_USUARIO, string TR_RESPONSABLE,string TR_CARGO,string TR_VALIDA,string TR_PRIORIDAD,string TR_PROCESO, string TR_DESICION)
        {            
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                QuejasApelacionesBD.UpdateQuejaSolicitudApelacion(oSessionManager, TQ_CODEMP, TQ_NUMERO, TQ_TIPO);
                QuejasApelacionesBD.DeleteDetalleRespuestaQUejaApelacion(oSessionManager, TQ_CODEMP, TQ_NUMERO);
                QuejasApelacionesBD.InsertDetalleQuejaSolicitudApelacion(oSessionManager, TQ_CODEMP, TQ_NUMERO, TR_RESPONSABLE,TR_CARGO,TR_VALIDA,TR_PRIORIDAD,TR_PROCESO,TR_DESICION,TQ_USUARIO);
                oSessionManager.CommitTranstaction();

                return TQ_NUMERO;
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
        public DataTable GetImagenes(string connection, string IE_CODEMP, int TQ_NUMERO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return QuejasApelacionesBD.GetImagenes(oSessionManager, IE_CODEMP, TQ_NUMERO);
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
        public DataTable GetImagenesAntencionCliente(string connection, int AT_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return QuejasApelacionesBD.GetImagenesAntencionCliente(oSessionManager, AT_CODIGO);
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
        public DataTable GetImagenAntencionCliente(string connection, int EI_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return QuejasApelacionesBD.GetImagenAtencionCliente(oSessionManager, EI_CODIGO);
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
        public int InsertImagen(string connection, string IE_CODEMP, int TQ_NUMERO, string IE_TIPIMA, string inRuta, string IE_DESCRIPCION, string IE_USUARIO)
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
                QuejasApelacionesBD.InsertImagen(oSessionManager,IE_CODEMP, TQ_NUMERO, IE_TIPIMA, result, IE_DESCRIPCION, IE_USUARIO);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                ioArchivo = null;
                result = null;
            }
        }

        public DataTable getAtencionCLiente(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return QuejasApelacionesBD.getAtencionCLiente(oSessionManager, filter, startRowIndex, maximumRows);
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
        
        public int insertAtencionCliente(string connection, string AT_CODEMP,  int TRCODTER, string TRNOMBRE, string TRNOMBR2, string TRCODNIT, string TRDIRECC, string TRNROTEL, string TRCORREO, string TRAPELLI, string TRNOMBR3,
                                                 string TRTIPDOC, string TRDIGCHK, string AT_ESTADO, string AT_USUARIO, string AT_TIPOINSPECCION, string AT_TIPOPREDIO, string AT_TIPO,string TRNOMCOMERCIAL,DateTime AT_FECPROGRAMACION, 
                                                 string AT_RESPONSABLE,string AT_CTACONTRATO, string AT_DISISOMETRICO,string AT_PLANOAPROBADO,string AT_CERTLABORAL,string AT_COMPETENCIAS,
                                                 string AT_ADMINISTRADOR,string AT_CONTACTO,string AT_IDCONSTRUCTOR,string AT_NOMCONSTRUCTOR,string AT_NEWUSD, string AT_ALMATRIZ,string AT_CLMATRIZ,string AT_CUINSPECCION, DateTime? AT_FECUINSPECCION,
                                                 string AT_ZONA,string AT_TIPOGAS)
        {
            int AT_CODIGO = 0, TK_NUMERO=0, TD_NUMERO=0;
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                AT_CODIGO = ComunBD.GeneraConsecutivo(oSessionManager, "CODQAS", AT_CODEMP);
                TK_NUMERO = LstTareasBD.GeneraConsecutivo(oSessionManager, "NROTCK");
                TD_NUMERO = LstTareasBD.GeneraConsecutivo(oSessionManager, "NRODTCK");

                if (TRCODTER == 0)
                {
                    TRCODTER = ComunBD.GeneraConsecutivo(oSessionManager, "CODTER", AT_CODEMP);
                    TercerosBD.InsertTercero(oSessionManager, AT_CODEMP, TRCODTER, TRNOMBRE, TRNOMBR2, null, null, TRCODNIT, null, TRDIRECC, null, null, null, TRNROTEL, null, null, TRCORREO, null, null, null, null, null, null, null, null, null, null, null, null, null,
                        null, null, "S", "N", "N", "N", "N", "N", "N", null, "Cta Contrato", null, null, null, null, null, AT_CTACONTRATO, null, null, null, null, null, "AC", ".", AT_USUARIO, null, null, null, null, TRAPELLI, TRNOMBR3, TRTIPDOC, TRDIGCHK, null, null, null, null, null);
                }
                else
                {
                    TercerosBD.UpdateTercero(oSessionManager, AT_CODEMP, TRCODTER, TRNOMBRE, TRNOMBR2, null, null, TRCODNIT, null, TRDIRECC, null, null, null, TRNROTEL, null, null, TRCORREO, null, null, null, null, null, null, null, null, null, null, null, null, null,
                            null, null, "S", "N", "N", "N", "N", "N", "N", null, "Cta Contrato", null, null, null, null, null, AT_CTACONTRATO, null, null, null, null, null, "AC", ".", AT_USUARIO, null, null, null, null, TRAPELLI, TRNOMBR3, TRTIPDOC, TRDIGCHK, null, null, null, null, null);
                }

                QuejasApelacionesBD.insertAtencionCliente(oSessionManager, AT_CODIGO, AT_CODEMP, TRCODTER, AT_TIPOINSPECCION, AT_TIPOPREDIO, AT_TIPO, AT_ESTADO, AT_USUARIO, AT_FECPROGRAMACION,
                                                 AT_RESPONSABLE, AT_CTACONTRATO, AT_DISISOMETRICO, AT_PLANOAPROBADO, AT_CERTLABORAL, AT_COMPETENCIAS, AT_ADMINISTRADOR, AT_CONTACTO, AT_IDCONSTRUCTOR, AT_NOMCONSTRUCTOR, 
                                                 AT_NEWUSD, AT_ALMATRIZ, AT_CLMATRIZ, AT_CUINSPECCION, AT_FECUINSPECCION, AT_ZONA, AT_TIPOGAS);

                string lc_descripcion = "Inspeccion - Direccion:" + TRDIRECC;
                string lc_asunto = " Inspeccion";
                switch (AT_TIPOINSPECCION)
                { 
                    case "1":
                        lc_asunto += " Nueva";
                        break;
                    case "2":
                        lc_asunto += " Periodica";
                        break;
                }
                switch (AT_TIPOPREDIO)
                {
                    case "1":
                        lc_asunto += " Residencial";
                        break;
                    case "2":
                        lc_asunto += " Comercial";
                        break;
                }
                switch (AT_TIPO)
                {
                    case "1":
                        lc_asunto += " Linea Matriz";
                        break;
                    case "2":
                        lc_asunto += " Instalacion Interna";
                        break;
                    case "3":
                        lc_asunto += " GLP";
                        break;
                }

                LstTareasBD.InsertTicket(oSessionManager, TK_NUMERO, AT_RESPONSABLE, AT_USUARIO, "02", "Inspeccion ", lc_asunto, "5",AT_CODIGO, "AC", System.DateTime.Today);
                LstTareasBD.InsertDetalleTicket(oSessionManager, TD_NUMERO, TK_NUMERO, AT_USUARIO, lc_asunto, null);

                LstTareasBD.InsertAppoiment(oSessionManager, lc_descripcion, AT_FECPROGRAMACION, AT_FECPROGRAMACION.AddHours(3), 0, AT_RESPONSABLE, null, null, 
                                            1, AT_USUARIO, TK_NUMERO, TRCODTER, 0, "5");

                oSessionManager.CommitTranstaction();

                return AT_CODIGO;
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

        public int updateAtencionCliente(string connection, int AT_CODIGO, string AT_CODEMP, int TRCODTER, string TRNOMBRE, string TRNOMBR2, string TRCODNIT, string TRDIRECC, string TRNROTEL, string TRCORREO, string TRAPELLI, string TRNOMBR3,
                                                 string TRTIPDOC, string TRDIGCHK, string AT_ESTADO, string AT_USUARIO, string AT_TIPOINSPECCION, string AT_TIPOPREDIO, string AT_TIPO, string TRNOMCOMERCIAL, DateTime AT_FECPROGRAMACION,
                                                 string AT_RESPONSABLE, string AT_CTACONTRATO, string AT_DISISOMETRICO, string AT_PLANOAPROBADO, string AT_CERTLABORAL, string AT_COMPETENCIAS,
                                                 string AT_ADMINISTRADOR, string AT_CONTACTO, string AT_IDCONSTRUCTOR, string AT_NOMCONSTRUCTOR, string AT_NEWUSD, string AT_ALMATRIZ, string AT_CLMATRIZ, string AT_CUINSPECCION, DateTime? AT_FECUINSPECCION,
                                                 string AT_ZONA, string AT_TIPOGAS)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                QuejasApelacionesBD.updateAtencionCliente(oSessionManager, AT_CODIGO, AT_CODEMP, TRCODTER, AT_TIPOINSPECCION, AT_TIPOPREDIO, AT_TIPO, AT_ESTADO, AT_USUARIO, AT_FECPROGRAMACION,
                                                 AT_RESPONSABLE, AT_CTACONTRATO, AT_DISISOMETRICO, AT_PLANOAPROBADO, AT_CERTLABORAL, AT_COMPETENCIAS, AT_ADMINISTRADOR, AT_CONTACTO, AT_IDCONSTRUCTOR, AT_NOMCONSTRUCTOR,
                                                 AT_NEWUSD, AT_ALMATRIZ, AT_CLMATRIZ, AT_CUINSPECCION, AT_FECUINSPECCION, AT_ZONA, AT_TIPOGAS);

                return 1;
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

        public int deleteImagenAntencionCliente(string connection, int EI_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return QuejasApelacionesBD.deleteImagenAntencionCliente(oSessionManager, EI_CODIGO);
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
        public int insertImagenAntencionCliente(string connection, int IP_CODIGO, int EI_TIPO, string ruta)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                Stream ioArchivo = File.OpenRead(ruta);
                byte[] result;
                using (MemoryStream ms = new MemoryStream())
                {
                    ioArchivo.CopyTo(ms);
                    result = ms.ToArray();
                }
                return QuejasApelacionesBD.insertImagenAntencionCliente(oSessionManager, IP_CODIGO, EI_TIPO, result);
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
