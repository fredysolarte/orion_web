using Microsoft.AspNetCore.Http;
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
    public class RPMultintegral
    {
        public List<inDetallePropiedadHorizontal> getPropiedadHorizontalDetalle(string inConecction,string infiltro)
        {
            DBAccess Obj = new DBAccess();
            DBAccess ObjA = new DBAccess();
            tb_propiedahorizontalBL ObjP = new tb_propiedahorizontalBL();
            movimientosBL ObjM = new movimientosBL();
            TercerosBL ObjT = new TercerosBL();            
            correspondenciaInBL ObjCI = new correspondenciaInBL();
            correspondenciaOutBL ObjCO = new correspondenciaOutBL();
            tb_desistalacionBL ObjD = new tb_desistalacionBL();
            tb_instalacionlBL ObjI = new tb_instalacionlBL();
            tb_comericialBL ObjC = new tb_comericialBL();
            articuloBL ObjAR = new articuloBL();
            List<inDetallePropiedadHorizontal> retorno = new List<inDetallePropiedadHorizontal>();

            try {
                Obj.ConnectionString = inConecction;
                ObjA.ConnectionString = inConecction;
                using (SqlDataReader reader = ObjP.getPropiedadHorizontalDetalle(inConecction, infiltro)) {
                    while (reader.Read()) {
                        inDetallePropiedadHorizontal item = new inDetallePropiedadHorizontal();

                        item.id = retorno.Count + 1;
                        item.estado_co = Convert.ToString(reader["ESTADO_CO"]);
                        item.estado_t = Convert.ToString(reader["ESTADO_T"]);
                        item.propiedadhorizontal.Add(ObjP.getItemPropiedahorizontal(reader));
                        if (!reader.IsDBNull(reader.GetOrdinal("CO_CODIGO")))
                        {
                            item.comercial.Add(ObjC.getItemComercial(reader));
                            using (SqlDataReader reader_tercero = TercerosBD.GetTerceros(ObjA, Convert.ToInt32(reader["COD_CLIENTE"])))
                            {
                                while (reader_tercero.Read())
                                    item.clientes.Add(ObjT.getTercerosItem(reader_tercero));
                            }
                        }
                        else {
                            tb_comercial lstc = new tb_comercial();
                            item.comercial.Add(lstc);

                        }
                        item.articulo.Add(ObjAR.getArticulo(reader));
                        item.clientes.Add(ObjT.getTercerosItem(reader));
                        item.desistalacion.Add(ObjD.getItemDesistalacion(reader));
                        item.correspondenciaindt.Add(ObjCI.getItemCorrespondenciaDTIN(reader));
                        item.correspondenciaoutdt.Add(ObjCO.getItemCorrespondenciaDTOUT(reader));
                        item.instalacion.Add(ObjI.getItemInstalacion(reader));
                        item.movimele.Add(ObjM.getItemMovimele(reader));
                        retorno.Add(item);
                        item = null;

                    }
                }

                    return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                Obj = null;
                ObjM = null;
                ObjT = null;
                ObjD = null;
                ObjCI = null;
                ObjCO = null;
                ObjI = null;
                ObjA = null;
                ObjAR = null;
            }

        }

        public int insertPropiedadHorizontal(string inConecction, inDetallePropiedadHorizontal inDatos) {
            
            DBAccess ObjDB = new DBAccess();
            tb_comercialBD ObjC = new tb_comercialBD();
            ObjDB.ConnectionString = inConecction;
            int ln_codter = 0;

            try {
                ObjDB.BeginTransaction();

                if (TercerosBD.ExisteTercero(ObjDB, inDatos.clientes[0].trcodnit) == 0)
                {
                    ln_codter = tbTablasBD.GeneraConsecutivo(ObjDB, "CODTER");
                    TercerosBD.InsertTercero(ObjDB, inDatos.clientes[0].trcodemp, ln_codter, inDatos.clientes[0].trnombre, inDatos.clientes[0].trnombr2, inDatos.clientes[0].trcontac,
                                             inDatos.clientes[0].trcodedi, inDatos.clientes[0].trcodnit, inDatos.clientes[0].trdigver, inDatos.clientes[0].trdirecc, inDatos.clientes[0].trdirec2, inDatos.clientes[0].trdelega,
                                             inDatos.clientes[0].trcoloni, inDatos.clientes[0].trnrotel, inDatos.clientes[0].trnrofax, inDatos.clientes[0].trpostal, inDatos.clientes[0].trcorreo, inDatos.clientes[0].trciudad,
                                             inDatos.clientes[0].trciuda2, inDatos.clientes[0].trcdpais, inDatos.clientes[0].trmoneda, inDatos.clientes[0].tridioma, inDatos.clientes[0].trbodega, inDatos.clientes[0].trterpag,
                                             inDatos.clientes[0].trmoddes, inDatos.clientes[0].trterdes, inDatos.clientes[0].trcatego, inDatos.clientes[0].tragente, inDatos.clientes[0].trlispre, inDatos.clientes[0].trlispra,
                                             inDatos.clientes[0].trdescue, inDatos.clientes[0].trcupocr, inDatos.clientes[0].trindcli, inDatos.clientes[0].trindpro, inDatos.clientes[0].trindsop, inDatos.clientes[0].trindemp,
                                             inDatos.clientes[0].trindsoc, inDatos.clientes[0].trindven, inDatos.clientes[0].trindfor, inDatos.clientes[0].trcdcla1, inDatos.clientes[0].trcdcla2, inDatos.clientes[0].trcdcla3,
                                             inDatos.clientes[0].trcdcla4, inDatos.clientes[0].trcdcla5, inDatos.clientes[0].trcdcla6, inDatos.clientes[0].trdttec1, inDatos.clientes[0].trdttec2, inDatos.clientes[0].trdttec3,
                                             inDatos.clientes[0].trdttec4, inDatos.clientes[0].trdttec5, inDatos.clientes[0].trdttec6, inDatos.clientes[0].trprogdt, inDatos.clientes[0].trestado, inDatos.clientes[0].trcausae,
                                             inDatos.clientes[0].trnmuser, inDatos.clientes[0].trobserv, inDatos.clientes[0].trfecnac, inDatos.clientes[0].trrespal, inDatos.clientes[0].trrescup, inDatos.clientes[0].trapelli,
                                             inDatos.clientes[0].trnombr3, inDatos.clientes[0].trtipdoc, inDatos.clientes[0].trdigchk, inDatos.clientes[0].trcodzona, inDatos.clientes[0].trtipreg, inDatos.clientes[0].trgranct,
                                             inDatos.clientes[0].trautoret, inDatos.clientes[0].trnomcomercial);
                }
                else
                {
                    ln_codter = TercerosBD.getCodTer(ObjDB, inDatos.clientes[0].trcodnit);
                }

                if (ObjC.ExisteComercial(ObjDB, inDatos.comercial[0].ph_codigo) == 0)
                {
                    ObjC.InsertComercial(ObjDB, inDatos.comercial[0].co_codemp, inDatos.comercial[0].ph_codigo, inDatos.comercial[0].co_cuotas, inDatos.comercial[0].co_precio, ln_codter,
                    Convert.ToDateTime(inDatos.comercial[0].co_fecha), "CE", inDatos.comercial[0].co_usuario, Convert.ToDateTime(inDatos.comercial[0].co_feccomodato), Convert.ToDateTime(inDatos.comercial[0].co_fecpagare));
                }


                ObjDB.Commit();
                return Convert.ToInt32(inDatos.comercial[0].ph_codigo);
            }
            catch (Exception ex)
            {
                ObjDB.Rollback();
                throw ex;
            }
            finally {
                ObjDB = null;
                ObjC = null;
            }
        }

        public int InsertEvidenciaComercial(string inConecction, string EC_CODEMP,int PH_CODIGO, int EC_TIPO, string EC_USUARIO, List<IFormFile> image)
        {
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
                        tb_comercialBD.InsertEvidenciaComercial(Obj, EC_CODEMP, PH_CODIGO, EC_TIPO, EC_USUARIO,result);
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
    }

    public class inDetallePropiedadHorizontal { 
        public int id { get; set; }
        public string estado_co { get; set; }
        public string estado_t { get; set; }
        public string nombre_ph { get; set; }
        public List<tb_propiedahorizontal> propiedadhorizontal { get; set; } = new List<tb_propiedahorizontal>();
        public List<Terceros> clientes { get; set; } = new List<Terceros>();
        public List<tb_instalacion>? instalacion { get; set; } = new List<tb_instalacion>();
        public List<tb_desistalacion>? desistalacion { get; set; } = new List<tb_desistalacion>();
        public List<tb_comercial>? comercial { get; set; } = new List<tb_comercial>();
        public List<tb_correspondenciadtin>? correspondenciaindt { get; set; } = new List<tb_correspondenciadtin>();
        public List<tb_correspondenciadtout>? correspondenciaoutdt { get; set; } = new List<tb_correspondenciadtout>();
        public List<movimele>? movimele { get; set; } = new List<movimele>();
        public List<articulo>? articulo { get; set; } = new List<articulo>();
    }
}
