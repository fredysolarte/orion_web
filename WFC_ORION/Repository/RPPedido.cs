using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using WFC_ORION.BLL;
using WFC_ORION.DAL;
using WFC_ORION.Models;

namespace WFC_ORION.Repository
{
    public class RPPedido
    {
        #region       
        public IEnumerable<PedidoHD> GetPedidoHD(string inConnection,DateTime PHPECPED)
        {
            DBAccess Obj = new DBAccess();
            //Obj.ConnectionString = "Data Source=190.94.251.11;Initial Catalog=orion_multintegral;User ID=sa;Password=Seguro1;";
            Obj.ConnectionString = inConnection;

            return _GetPedidoHD(Obj, "001", PHPECPED);
        }
        public IEnumerable<inPedidos> GetPedidoHD(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            //Obj.ConnectionString = "Data Source=190.94.251.11;Initial Catalog=orion_multintegral;User ID=sa;Password=Seguro1;";
            Obj.ConnectionString = inConnection;

            return _GetPedidoHD(Obj, "001", inFiltro);
        }
        private static List<PedidoHD> _GetPedidoHD(DBAccess ObjDB, string EMPRESA, DateTime P_FECHA)
        {
            List<PedidoHD> lst = new List<PedidoHD>();
            try
            {
                using (SqlDataReader reader = Pedido.GetPedidos(ObjDB, EMPRESA, P_FECHA))
                {
                    while (reader.Read())
                    {
                        lst.Add(GetPedidoHDItem(reader));
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lst = null;
            }            
        }
        private static List<inPedidos> _GetPedidoHD(DBAccess ObjDB, string EMPRESA, string inFiltro)
        {
            List<inPedidos> lst = new List<inPedidos>();
           
            try
            {
                using (SqlDataReader reader = Pedido.GetPedidos(ObjDB, EMPRESA, inFiltro))
                {
                    while (reader.Read())
                    {
                        inPedidos item = new inPedidos();
                        item.id = Convert.ToInt32(reader["PHPEDIDO"]);
                        item.inPedido.Add(GetPedidoHDItem(reader));
                        item.tercero.Add(TercerosBL.GetTercerosItem(reader));
                        lst.Add(item);
                        item = null;
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lst = null;
            }
        }
        private static PedidoHD GetPedidoHDItem(SqlDataReader Reader)
        {
            PedidoHD item = new PedidoHD();

            item.phcodemp = Convert.ToString(Reader["PHCODEMP"]);
            item.phpedido = Convert.ToInt32(Reader["PHPEDIDO"]);
            item.phfecped = Convert.ToString(Convert.ToDateTime(Reader["PHFECPED"]).Year.ToString() + "-" + Convert.ToDateTime(Reader["PHFECPED"]).Month.ToString() + "-" + Convert.ToDateTime(Reader["PHFECPED"]).Day.ToString());
            item.phcodcli = Convert.ToInt32(Reader["PHCODCLI"]);
            item.phcodsuc = Reader.IsDBNull(Reader.GetOrdinal("PHCODSUC")) ? null : (int?)Convert.ToInt32(Reader["PHCODSUC"]);
            item.phtipped = Convert.ToString(Reader["PHTIPPED"]);
            item.phbodega = Convert.ToString(Reader["PHBODEGA"]);
            item.phlispre = Convert.ToString(Reader["PHLISPRE"]);
            item.phfecpre = Reader.IsDBNull(Reader.GetOrdinal("PHFECPRE")) ? null : (DateTime?)Convert.ToDateTime(Reader["PHFECPRE"]);
            item.phidioma = Convert.ToString(Reader["PHIDIOMA"]);
            item.phmoneda = Convert.ToString(Reader["PHMONEDA"]);
            item.phtrmloc = Reader.IsDBNull(Reader.GetOrdinal("PHTRMLOC")) ? null : (double?)Convert.ToDouble(Reader["PHTRMLOC"]);
            item.phtrmusd = Reader.IsDBNull(Reader.GetOrdinal("PHTRMUSD")) ? null : (double?)Convert.ToDouble(Reader["PHTRMUSD"]);
            item.phtrmped = Reader.IsDBNull(Reader.GetOrdinal("PHTRMPED")) ? null : (double?)Convert.ToDouble(Reader["PHTRMPED"]);
            item.phagente = Reader.IsDBNull(Reader.GetOrdinal("PHAGENTE")) ? null : (int?)Convert.ToInt32(Reader["PHAGENTE"]);
            item.phterpag = Convert.ToString(Reader["PHTERPAG"]);
            item.phmoddes = Convert.ToString(Reader["PHMODDES"]);
            item.phterdes = Convert.ToString(Reader["PHTERDES"]);
            item.phptoent = Convert.ToString(Reader["PHPTOENT"]);
            item.phtipdes = Convert.ToString(Reader["PHTIPDES"]);
            item.phdescu1 = Reader.IsDBNull(Reader.GetOrdinal("PHDESCU1")) ? null : (double?)Convert.ToDouble(Reader["PHDESCU1"]);
            item.phdespag = Reader.IsDBNull(Reader.GetOrdinal("PHDESPAG")) ? null : (double?)Convert.ToDouble(Reader["PHDESPAG"]);
            item.phdesval = Reader.IsDBNull(Reader.GetOrdinal("PHDESVAL")) ? null : (double?)Convert.ToDouble(Reader["PHDESVAL"]);
            item.phtotdes = Reader.IsDBNull(Reader.GetOrdinal("PHTOTDES")) ? null : (double?)Convert.ToDouble(Reader["PHTOTDES"]);
            item.phtotped = Reader.IsDBNull(Reader.GetOrdinal("PHTOTPED")) ? null : (double?)Convert.ToDouble(Reader["PHTOTPED"]);
            item.phfecini = Reader.IsDBNull(Reader.GetOrdinal("PHFECINI")) ? null : (DateTime?)Convert.ToDateTime(Reader["PHFECINI"]);
            item.phfecfin = Reader.IsDBNull(Reader.GetOrdinal("PHFECFIN")) ? null : (DateTime?)Convert.ToDateTime(Reader["PHFECFIN"]);
            item.phciudes = Convert.ToString(Reader["PHCIUDES"]);
            item.phdirdes = Convert.ToString(Reader["PHDIRDES"]);
            item.phobserv = Convert.ToString(Reader["PHOBSERV"]);
            item.phcdcla1 = Convert.ToString(Reader["PHCDCLA1"]);
            item.phcdcla2 = Convert.ToString(Reader["PHCDCLA2"]);
            item.phcdcla3 = Convert.ToString(Reader["PHCDCLA3"]);
            item.phcdcla4 = Convert.ToString(Reader["PHCDCLA4"]);
            item.phcdcla5 = Convert.ToString(Reader["PHCDCLA5"]);
            item.phcdcla6 = Convert.ToString(Reader["PHCDCLA6"]);
            item.phdttec1 = Convert.ToString(Reader["PHDTTEC1"]);
            item.phdttec2 = Convert.ToString(Reader["PHDTTEC2"]);
            item.phdttec3 = Convert.ToString(Reader["PHDTTEC3"]);
            item.phdttec4 = Convert.ToString(Reader["PHDTTEC4"]);
            item.phdttec5 = Reader.IsDBNull(Reader.GetOrdinal("PHDTTEC5")) ? null : (double?)Convert.ToDouble(Reader["PHDTTEC5"]);
            item.phdttec6 = Reader.IsDBNull(Reader.GetOrdinal("PHDTTEC6")) ? null : (double?)Convert.ToDouble(Reader["PHDTTEC6"]);
            item.phprogdt = Reader.IsDBNull(Reader.GetOrdinal("PHPROGDT")) ? null : (int?)Convert.ToInt32(Reader["PHPROGDT"]);
            item.phestado = Convert.ToString(Reader["PHESTADO"]);
            item.phcausae = Convert.ToString(Reader["PHCAUSAE"]);
            item.phnmuser = Convert.ToString(Reader["PHNMUSER"]);
            item.phfecfin = Convert.ToDateTime(Reader["PHFECING"]);
            item.phfecmod = Convert.ToDateTime(Reader["PHFECMOD"]);
            item.phcantid = Reader.IsDBNull(Reader.GetOrdinal("PHCANTID")) ? null : (double?)Convert.ToDouble(Reader["PHCANTID"]);
            item.phconven = Convert.ToString(Reader["PHCONVEN"]);
            item.phdivisi = Convert.ToString(Reader["PHDIVISI"]);
            item.phptodes = Reader.IsDBNull(Reader.GetOrdinal("PHPTODES")) ? null : (int?)Convert.ToInt32(Reader["PHPTODES"]);
            item.phtiplin = Convert.ToString(Reader["PHTIPLIN"]);
            item.phsolcom = Reader.IsDBNull(Reader.GetOrdinal("PHSOLCOM")) ? null : (int?)Convert.ToDouble(Reader["PHSOLCOM"]);
            item.phpedano = Reader.IsDBNull(Reader.GetOrdinal("PHPEDANO")) ? null : (int?)Convert.ToDouble(Reader["PHPEDANO"]);
            item.phpedmes = Reader.IsDBNull(Reader.GetOrdinal("PHPEDMES")) ? null : (int?)Convert.ToDouble(Reader["PHPEDMES"]);
            item.phfecliq = Reader.IsDBNull(Reader.GetOrdinal("PHFECLIQ")) ? null : (DateTime?)Convert.ToDateTime(Reader["PHFECLIQ"]);


            return item;
        }

        public int InsertPedido(string inConnection, inPedidosFL inPedido)
        {
            DBAccess ObjDB = new DBAccess();
            ObjDB.ConnectionString = inConnection;
            int ln_consecutivo = 0, ln_codter= 0,ln_acu = 0;
            string phtipped = "PB",phbodega = "",phmoneda="",phidioma="";
            try {
                
                ObjDB.BeginTransaction();
                ln_consecutivo = tbTablasBD.GeneraConsecutivo(ObjDB, "PEDIDO");
                if (TercerosBD.ExisteTercero(ObjDB, inPedido.terceros[0].trcodnit) == 0)
                {
                    ln_codter = tbTablasBD.GeneraConsecutivo(ObjDB, "CODTER");
                    TercerosBD.InsertTercero(ObjDB, inPedido.terceros[0].trcodemp, ln_codter, inPedido.terceros[0].trnombre, inPedido.terceros[0].trnombr2, inPedido.terceros[0].trcontac,
                                             inPedido.terceros[0].trcodedi, inPedido.terceros[0].trcodnit, inPedido.terceros[0].trdigver, inPedido.terceros[0].trdirecc, inPedido.terceros[0].trdirec2, inPedido.terceros[0].trdelega,
                                             inPedido.terceros[0].trcoloni, inPedido.terceros[0].trnrotel, inPedido.terceros[0].trnrofax, inPedido.terceros[0].trpostal, inPedido.terceros[0].trcorreo, inPedido.terceros[0].trciudad,
                                             inPedido.terceros[0].trciuda2, inPedido.terceros[0].trcdpais, inPedido.terceros[0].trmoneda, inPedido.terceros[0].tridioma, inPedido.terceros[0].trbodega, inPedido.terceros[0].trterpag,
                                             inPedido.terceros[0].trmoddes, inPedido.terceros[0].trterdes, inPedido.terceros[0].trcatego, inPedido.terceros[0].tragente, inPedido.terceros[0].trlispre, inPedido.terceros[0].trlispra,
                                             inPedido.terceros[0].trdescue, inPedido.terceros[0].trcupocr, inPedido.terceros[0].trindcli, inPedido.terceros[0].trindpro, inPedido.terceros[0].trindsop, inPedido.terceros[0].trindemp,
                                             inPedido.terceros[0].trindsoc, inPedido.terceros[0].trindven, inPedido.terceros[0].trindfor, inPedido.terceros[0].trcdcla1, inPedido.terceros[0].trcdcla2, inPedido.terceros[0].trcdcla3,
                                             inPedido.terceros[0].trcdcla4, inPedido.terceros[0].trcdcla5, inPedido.terceros[0].trcdcla6, inPedido.terceros[0].trdttec1, inPedido.terceros[0].trdttec2, inPedido.terceros[0].trdttec3,
                                             inPedido.terceros[0].trdttec4, inPedido.terceros[0].trdttec5, inPedido.terceros[0].trdttec6, inPedido.terceros[0].trprogdt, inPedido.terceros[0].trestado, inPedido.terceros[0].trcausae,
                                             inPedido.terceros[0].trnmuser, inPedido.terceros[0].trobserv, inPedido.terceros[0].trfecnac, inPedido.terceros[0].trrespal, inPedido.terceros[0].trrescup, inPedido.terceros[0].trapelli,
                                             inPedido.terceros[0].trnombre, inPedido.terceros[0].trtipdoc, inPedido.terceros[0].trdigchk, inPedido.terceros[0].trcodzona, inPedido.terceros[0].trtipreg, inPedido.terceros[0].trgranct,
                                             inPedido.terceros[0].trautoret, inPedido.terceros[0].trnomcomercial);
                }
                else
                {
                    ln_codter = TercerosBD.getCodTer(ObjDB, inPedido.terceros[0].trcodnit);
                }

                using (SqlDataReader reader = tbtippedBD.GetTipPed(ObjDB," PTTIPPED = '" + phtipped + "'",0,0))
                {
                    while (reader.Read())
                    {
                        phbodega = Convert.ToString(reader["PTBODEGA"]);
                        phmoneda = Convert.ToString(reader["PTMONEDA"]);
                        phidioma = Convert.ToString(reader["PTIDIOMA"]);
                    }
                }

                Pedido.InsertPedidoHD(ObjDB, inPedido.inPedidoHD[0].phcodemp, ln_consecutivo, ln_codter, 0, inPedido.inPedidoHD[0].phagente, phtipped, phbodega,
                                      phidioma, phmoneda, inPedido.inPedidoHD[0].phtrmloc, 0, 0, "0", 0, 0, 0, 0, 0,
                                      inPedido.inPedidoHD[0].phestado, ".", inPedido.inPedidoHD[0].phnmuser, inPedido.inPedidoHD[0].phlispre,
                                                System.DateTime.Today.Month, System.DateTime.Today.Year, System.DateTime.Today, inPedido.inPedidoHD[0].phobserv,
                                                System.DateTime.Today);
                foreach (pedidodt row in inPedido.inPedidoDT)
                {
                    ln_acu++;
                    Pedido.InsertPedidoDT(ObjDB, inPedido.inPedidoHD[0].phcodemp, ln_consecutivo, ln_acu, phtipped, ln_codter, phbodega,
                                             inPedido.inPedidoDT[0].pdtippro, inPedido.inPedidoDT[0].pdclave1, inPedido.inPedidoDT[0].pdclave2, inPedido.inPedidoDT[0].pdclave3,
                                             inPedido.inPedidoDT[0].pdclave4, null, null, inPedido.inPedidoDT[0].pdundped,
                                             inPedido.inPedidoDT[0].pdcanped, inPedido.inPedidoDT[0].pdcantid, 0, inPedido.inPedidoDT[0].pdlispre, null, 0, 0, null,
                                             null, null, null, null, null, null, null, null,
                                             0, null, 0, null, 0, 0, 0, null,
                                             null, null, null, null, null, null, null, null, null,
                                             null, null, null, "AC", ".", inPedido.inPedidoHD[0].phnmuser, null,
                                             null, null, null, null, null, null, null, null, null, 0, 0);
                }
                ObjDB.Commit();
                

                return ln_consecutivo;
            }
            catch (Exception ex)
            {
                ObjDB.Rollback();
                throw ex;
            }
            finally {
                ObjDB = null;
            }

        }

        public int InsertEvidenciaPedido(string inConecction, string EP_CODEMP, int PHPEDIDO, string EP_USUARIO, List<IFormFile> image)
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
                        Pedido.InsertEvidenciaPedido(Obj, EP_CODEMP, PHPEDIDO, result, EP_USUARIO);
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
        #endregion
    }

    public class inPedidos
    {
        public int id { get; set; }
        public List<PedidoHD> inPedido { get; set; } = new List<PedidoHD>();
        public List<Terceros> tercero { get; set; } = new List<Terceros>();
    }

    public class inPedidosFL {
        public List<PedidoHD> inPedidoHD { get; set; } = new List<PedidoHD>();
        public List<pedidodt> inPedidoDT { get; set; } = new List<pedidodt>();
        public List<Terceros> terceros { get; set; } = new List<Terceros>();
    }
}
