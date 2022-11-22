using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.BLL;
using WFC_ORION.DAL;
using WFC_ORION.Models;

namespace WFC_ORION.Repository
{
    public class RPInspeccion
    {        
        
        public int InsertInspeccion(string inConecction, inInspeccion tb_inspeccion)
        {
            DBAccess ObjDB = new DBAccess();
            ObjDB.ConnectionString = inConecction;
            int ln_consecutivo = 0,ln_codter=0,at_codigo=0;

            try
            {
                ObjDB.BeginTransaction();
                ln_consecutivo = tbTablasBD.GeneraConsecutivo(ObjDB, "INSPEC");
                
                //Valida Tercero
                if (TercerosBD.ExisteTercero(ObjDB, tb_inspeccion.terceros[0].trcodnit) == 0)
                {
                    ln_codter = tbTablasBD.GeneraConsecutivo(ObjDB, "CODTER");
                    TercerosBD.InsertTercero(ObjDB, tb_inspeccion.terceros[0].trcodemp, ln_codter, tb_inspeccion.terceros[0].trnombre, tb_inspeccion.terceros[0].trnombr2, tb_inspeccion.terceros[0].trcontac,
                                             tb_inspeccion.terceros[0].trcodedi, tb_inspeccion.terceros[0].trcodnit, tb_inspeccion.terceros[0].trdigver, tb_inspeccion.terceros[0].trdirecc, tb_inspeccion.terceros[0].trdirec2, tb_inspeccion.terceros[0].trdelega,
                                             tb_inspeccion.terceros[0].trcoloni, tb_inspeccion.terceros[0].trnrotel, tb_inspeccion.terceros[0].trnrofax, tb_inspeccion.terceros[0].trpostal, tb_inspeccion.terceros[0].trcorreo, tb_inspeccion.terceros[0].trciudad,
                                             tb_inspeccion.terceros[0].trciuda2, tb_inspeccion.terceros[0].trcdpais, tb_inspeccion.terceros[0].trmoneda, tb_inspeccion.terceros[0].tridioma, tb_inspeccion.terceros[0].trbodega, tb_inspeccion.terceros[0].trterpag,
                                             tb_inspeccion.terceros[0].trmoddes, tb_inspeccion.terceros[0].trterdes, tb_inspeccion.terceros[0].trcatego, tb_inspeccion.terceros[0].tragente, tb_inspeccion.terceros[0].trlispre, tb_inspeccion.terceros[0].trlispra,
                                             tb_inspeccion.terceros[0].trdescue, tb_inspeccion.terceros[0].trcupocr, tb_inspeccion.terceros[0].trindcli, tb_inspeccion.terceros[0].trindpro, tb_inspeccion.terceros[0].trindsop, tb_inspeccion.terceros[0].trindemp,
                                             tb_inspeccion.terceros[0].trindsoc, tb_inspeccion.terceros[0].trindven, tb_inspeccion.terceros[0].trindfor, tb_inspeccion.terceros[0].trcdcla1, tb_inspeccion.terceros[0].trcdcla2, tb_inspeccion.terceros[0].trcdcla3,
                                             tb_inspeccion.terceros[0].trcdcla4, tb_inspeccion.terceros[0].trcdcla5, tb_inspeccion.terceros[0].trcdcla6, tb_inspeccion.terceros[0].trdttec1, tb_inspeccion.terceros[0].trdttec2, tb_inspeccion.terceros[0].trdttec3,
                                             tb_inspeccion.terceros[0].trdttec4, tb_inspeccion.terceros[0].trdttec5, tb_inspeccion.terceros[0].trdttec6, tb_inspeccion.terceros[0].trprogdt, tb_inspeccion.terceros[0].trestado, tb_inspeccion.terceros[0].trcausae,
                                             tb_inspeccion.terceros[0].trnmuser, tb_inspeccion.terceros[0].trobserv, tb_inspeccion.terceros[0].trfecnac, tb_inspeccion.terceros[0].trrespal, tb_inspeccion.terceros[0].trrescup, tb_inspeccion.terceros[0].trapelli,
                                             tb_inspeccion.terceros[0].trnombre, tb_inspeccion.terceros[0].trtipdoc, tb_inspeccion.terceros[0].trdigchk, tb_inspeccion.terceros[0].trcodzona, tb_inspeccion.terceros[0].trtipreg, tb_inspeccion.terceros[0].trgranct,
                                             tb_inspeccion.terceros[0].trautoret, tb_inspeccion.terceros[0].trnomcomercial);
                }
                else
                {
                    ln_codter = TercerosBD.getCodTer(ObjDB, tb_inspeccion.terceros[0].trcodnit);
                }

                if (Convert.ToInt32(tb_inspeccion.inspeccion[0].at_codigo) < 0)
                {
                    at_codigo = tbTablasBD.GeneraConsecutivo(ObjDB, "CODQAS");
                    tbInspeccionBD.insertAtencionCliente(ObjDB, at_codigo, tb_inspeccion.terceros[0].trcodemp, ln_codter, tb_inspeccion.atencioncliente[0].at_tipoinspeccion, tb_inspeccion.atencioncliente[0].at_tipopredio, tb_inspeccion.atencioncliente[0].at_tipo,
                                                        "CE", tb_inspeccion.atencioncliente[0].at_usuario, System.DateTime.Today, tb_inspeccion.atencioncliente[0].at_usuario, tb_inspeccion.atencioncliente[0].at_ctacontrato, tb_inspeccion.atencioncliente[0].at_disisometrico,
                                                        tb_inspeccion.atencioncliente[0].at_planoaprobado, tb_inspeccion.atencioncliente[0].at_certlaboral, tb_inspeccion.atencioncliente[0].at_competencias, tb_inspeccion.atencioncliente[0].at_administrador,
                                                        tb_inspeccion.atencioncliente[0].at_contacto, tb_inspeccion.atencioncliente[0].at_idconstructor, tb_inspeccion.atencioncliente[0].at_nomconstructor, tb_inspeccion.atencioncliente[0].at_newusd,
                                                        tb_inspeccion.atencioncliente[0].at_almatriz, tb_inspeccion.atencioncliente[0].at_clmatriz, tb_inspeccion.atencioncliente[0].at_cuinspeccion, Convert.ToDateTime(tb_inspeccion.atencioncliente[0].at_fecuinspeccion),
                                                        null, tb_inspeccion.atencioncliente[0].at_tipogas);
                    tb_inspeccion.inspeccion[0].at_codigo = Convert.ToString(at_codigo);
                }
                else { 
                    
                }
                
                tbInspeccionBD.InsertInspeccion(ObjDB, ln_consecutivo, "001", ln_codter, tb_inspeccion.inspeccion[0].ip_tipo, tb_inspeccion.inspeccion[0].ip_predio, tb_inspeccion.inspeccion[0].ip_servicio,
                    Convert.ToDateTime(tb_inspeccion.inspeccion[0].ip_fecha), tb_inspeccion.inspeccion[0].ip_observaciones, tb_inspeccion.inspeccion[0].ip_estado, tb_inspeccion.inspeccion[0].ip_usuario,Convert.ToInt32(tb_inspeccion.inspeccion[0].at_codigo));


                Tickets.updateTicket(ObjDB, Convert.ToInt32(tb_inspeccion.inspeccion[0].at_codigo));
                
                ObjDB.Commit();
                return ln_consecutivo;
            }
            catch (Exception ex)
            {
                ObjDB.Rollback();
                throw ex;
            }
            finally
            { 
            
            }
        }

        public int InsertEvidenciaInspeccion(string inConecction, int IP_CODIGO, int EI_TIPO, DateTime? EI_FECHA,List<IFormFile> image) {
            DBAccess Obj = new DBAccess();
            Obj.ConnectionString = inConecction;
            try
            {
                Obj.BeginTransaction();
                foreach (var file in image)
                {
                    if (file.Length > 0)
                    {
                        byte[] result;
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            result = ms.ToArray();
                        }
                        tbInspeccionBD.InsertEvidenciaInspeccion(Obj, IP_CODIGO, EI_TIPO, result, EI_FECHA);
                    }

                }
                Obj.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                Obj.Rollback();
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        public List<inAtencionClienteInspeccion> getInpeccionAtencionCliente(string inConnection, string inFiltro) {
            DBAccess Obj = new DBAccess();
            try
            {
                Obj.ConnectionString = inConnection;

                return _getInpeccionAtencionCliente(Obj, inFiltro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        public List<inAtencionClienteInspeccion> _getInpeccionAtencionCliente(DBAccess ObjDB, string inFiltro) {
            
            List<inAtencionClienteInspeccion> retorno = new List<inAtencionClienteInspeccion>();
            TercerosBL ObjT = new TercerosBL();
            try
            {
                using (SqlDataReader reader = tbInspeccionBD.getAtencionClienteInspeccion(ObjDB, inFiltro))
                {
                    while (reader.Read())
                    {
                        inAtencionClienteInspeccion item = new inAtencionClienteInspeccion();
                        tb_atencioncliente item_ = new tb_atencioncliente();

                        item.id = retorno.Count + 1;
                        
                        item_.at_codigo = Convert.ToInt32(reader["AT_CODIGO"]);
                        item_.at_codemp = Convert.ToString(reader["AT_CODEMP"]);
                        item_.trcodter = Convert.ToInt32(reader["TRCODTER"]);
                        item_.at_tipoinspeccion = Convert.ToString(reader["AT_TIPOINSPECCION"]);
                        item_.at_tipopredio = Convert.ToString(reader["AT_TIPOPREDIO"]);
                        item_.at_tipo = Convert.ToString(reader["AT_TIPO"]);
                        item_.at_tipogas = Convert.ToString(reader["AT_TIPOGAS"]);
                        item_.at_ctacontrato = Convert.ToString(reader["AT_CTACONTRATO"]);
                        item_.at_fecprogramacion = reader.IsDBNull(reader.GetOrdinal("AT_FECPROGRAMACION")) ? null : (DateTime?)Convert.ToDateTime(reader["AT_FECPROGRAMACION"]);
                        item_.at_responsable = Convert.ToString(reader["AT_RESPONSABLE"]);
                        item_.at_disisometrico  = Convert.ToString(reader["AT_DISISOMETRICO"]);
                        item_.at_planoaprobado = Convert.ToString(reader["AT_PLANOAPROBADO"]);
                        item_.at_certlaboral = Convert.ToString(reader["AT_CERTLABORAL"]);
                        item_.at_competencias = Convert.ToString(reader["AT_COMPETENCIAS"]);
                        item_.at_administrador = Convert.ToString(reader["AT_ADMINISTRADOR"]);
                        item_.at_contacto = Convert.ToString(reader["AT_CONTACTO"]);
                        item_.at_idconstructor = Convert.ToString(reader["AT_IDCONSTRUCTOR"]);
                        item_.at_nomconstructor = Convert.ToString(reader["AT_NOMCONSTRUCTOR"]);
                        item_.at_newusd = Convert.ToString(reader["AT_NEWUSD"]);
                        item_.at_almatriz = Convert.ToString(reader["AT_ALMATRIZ"]);
                        item_.at_clmatriz = Convert.ToString(reader["AT_CLMATRIZ"]);
                        item_.at_cuinspeccion = Convert.ToString(reader["AT_CUINSPECCION"]);
                        //item_.at_fecuinspeccion = reader.IsDBNull(reader.GetOrdinal("AT_FECUINSPECCION")) ? null : (DateTime?)Convert.ToDateTime(reader["AT_FECUINSPECCION"]);
                        item_.at_fecuinspeccion = Convert.ToString(Convert.ToDateTime(reader["AT_FECUINSPECCION"]).Year.ToString()+"-"+ Convert.ToDateTime(reader["AT_FECUINSPECCION"]).Month.ToString()+"-"+ Convert.ToDateTime(reader["AT_FECUINSPECCION"]).Day.ToString());
                        item_.at_estado = Convert.ToString(reader["AT_ESTADO"]);
                        item_.at_usuario = Convert.ToString(reader["AT_USUARIO"]);
                        item_.at_fecing = reader.IsDBNull(reader.GetOrdinal("AT_FECING")) ? null : (DateTime?)Convert.ToDateTime(reader["AT_FECING"]);
                        item_.at_fecmod = reader.IsDBNull(reader.GetOrdinal("AT_FECMOD")) ? null : (DateTime?)Convert.ToDateTime(reader["AT_FECMOD"]);

                        item.inatencioncliente.Add(item_);
                        item.tercero.Add(ObjT.getTercerosItem(reader));

                        retorno.Add(item);

                        item = null;
                        item_ = null;
                    }
                }
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                ObjT = null;
                retorno = null;                
            }
        }
    }

    public class inInspeccion {
        public IList<TB_INSPECCION> inspeccion { get; set; } = new List<TB_INSPECCION>();
        public List<Terceros> terceros { get; set; } = new List<Terceros>();
        public List<tb_atencioncliente> atencioncliente { get; set; } = new List<tb_atencioncliente>();
    }

    public class inAtencionClienteInspeccion {
        public int id { get; set; }
        public List<tb_atencioncliente> inatencioncliente { get; set; } = new List<tb_atencioncliente>();
        public List<Terceros> tercero { get; set; } = new List<Terceros>();
    }
}
