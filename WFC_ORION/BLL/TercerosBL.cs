using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.Models;

namespace WFC_ORION.BLL
{
    public class TercerosBL
    {
        public Terceros getTercerosItem(SqlDataReader Reader)
        {
            Terceros item = new Terceros();
            try
            {
                item.trcodemp = Convert.ToString(Reader["TRCODEMP"]);
                item.trcodter = Reader.IsDBNull(Reader.GetOrdinal("TRCODTER")) ? null : (int?)Convert.ToInt32(Reader["TRCODTER"]);
                item.trnombre = Convert.ToString(Reader["TRNOMBRE"]);
                item.trnombr2 = Convert.ToString(Reader["TRNOMBR2"]);
                item.trcontac = Convert.ToString(Reader["TRCONTAC"]);
                item.trcodedi = Reader.IsDBNull(Reader.GetOrdinal("TRCODEDI")) ? null : (int?)Convert.ToInt32(Reader["TRCODEDI"]);
                item.trcodnit = Convert.ToString(Reader["TRCODNIT"]);
                item.trdigver = Convert.ToString(Reader["TRDIGVER"]);
                item.trdirecc = Convert.ToString(Reader["TRDIRECC"]);
                item.trdirec2 = Convert.ToString(Reader["TRDIREC2"]);
                item.trdelega = Convert.ToString(Reader["TRDELEGA"]);
                item.trcoloni = Convert.ToString(Reader["TRCOLONI"]);
                item.trnrotel = Convert.ToString(Reader["TRNROTEL"]);
                item.trnrofax = Convert.ToString(Reader["TRNROFAX"]);
                item.trpostal = Convert.ToString(Reader["TRPOSTAL"]);
                item.trcorreo = Convert.ToString(Reader["TRCORREO"]);
                item.trciudad = Convert.ToString(Reader["TRCIUDAD"]);
                item.trciuda2 = Convert.ToString(Reader["TRCIUDA2"]);
                item.trcdpais = Convert.ToString(Reader["TRCDPAIS"]);
                item.trmoneda = Convert.ToString(Reader["TRMONEDA"]);
                item.tridioma = Convert.ToString(Reader["TRIDIOMA"]);
                item.trbodega = Convert.ToString(Reader["TRBODEGA"]);
                item.trterpag = Convert.ToString(Reader["TRTERPAG"]);
                item.trmoddes = Convert.ToString(Reader["TRMODDES"]);
                item.trterdes = Convert.ToString(Reader["TRTERDES"]);
                item.trcatego = Convert.ToString(Reader["TRCATEGO"]);
                item.tragente = Reader.IsDBNull(Reader.GetOrdinal("TRAGENTE")) ? null : (int?)Convert.ToInt32(Reader["TRAGENTE"]);
                item.trlispre = Convert.ToString(Reader["TRLISPRE"]);
                item.trlispra = Convert.ToString(Reader["TRLISPRA"]);
                item.trdescue = Reader.IsDBNull(Reader.GetOrdinal("TRDESCUE")) ? null : (double?)Convert.ToDouble(Reader["TRDESCUE"]);
                item.trcupocr = Reader.IsDBNull(Reader.GetOrdinal("TRCUPOCR")) ? null : (double?)Convert.ToDouble(Reader["TRCUPOCR"]);
                item.trindcli = Convert.ToString(Reader["TRINDCLI"]);
                item.trindpro = Convert.ToString(Reader["TRINDPRO"]);
                item.trindsop = Convert.ToString(Reader["TRINDSOP"]);
                item.trindemp = Convert.ToString(Reader["TRINDEMP"]);
                item.trindsoc = Convert.ToString(Reader["TRINDSOC"]);
                item.trindven = Convert.ToString(Reader["TRINDVEN"]);
                item.trcdcla1 = Convert.ToString(Reader["TRCDCLA1"]);
                item.trcdcla2 = Convert.ToString(Reader["TRCDCLA2"]);
                item.trcdcla3 = Convert.ToString(Reader["TRCDCLA3"]);
                item.trcdcla4 = Convert.ToString(Reader["TRCDCLA4"]);
                item.trcdcla5 = Convert.ToString(Reader["TRCDCLA5"]);
                item.trcdcla6 = Convert.ToString(Reader["TRCDCLA6"]);
                item.trdttec1 = Convert.ToString(Reader["TRDTTEC1"]);
                item.trdttec2 = Convert.ToString(Reader["TRDTTEC2"]);
                item.trdttec3 = Convert.ToString(Reader["TRDTTEC3"]);
                item.trdttec4 = Convert.ToString(Reader["TRDTTEC4"]);
                item.trdttec5 = Reader.IsDBNull(Reader.GetOrdinal("TRDTTEC5")) ? null : (double?)Convert.ToDouble(Reader["TRDTTEC5"]);
                item.trdttec6 = Reader.IsDBNull(Reader.GetOrdinal("TRDTTEC6")) ? null : (double?)Convert.ToDouble(Reader["TRDTTEC6"]);
                item.trprogdt = Reader.IsDBNull(Reader.GetOrdinal("TRPROGDT")) ? null : (int?)Convert.ToInt32(Reader["TRPROGDT"]);
                item.trestado = Convert.ToString(Reader["TRESTADO"]);
                item.trcausae = Convert.ToString(Reader["TRCAUSAE"]);
                item.trnmuser = Convert.ToString(Reader["TRNMUSER"]);
                item.trfecing = Reader.IsDBNull(Reader.GetOrdinal("TRFECING")) ? null : (DateTime?)Convert.ToDateTime(Reader["TRFECING"]);
                item.trfecmod = Reader.IsDBNull(Reader.GetOrdinal("TRFECMOD")) ? null : (DateTime?)Convert.ToDateTime(Reader["TRFECMOD"]);
                item.trobserv = Convert.ToString(Reader["TROBSERV"]);
                item.trfecnac = Reader.IsDBNull(Reader.GetOrdinal("TRFECNAC")) ? null : (DateTime?)Convert.ToDateTime(Reader["TRFECNAC"]);
                item.trrespal = Convert.ToString(Reader["TRRESPAL"]);
                item.trrescup = Reader.IsDBNull(Reader.GetOrdinal("TRRESCUP")) ? null : (double?)Convert.ToDouble(Reader["TRRESCUP"]);
                item.trapelli = Convert.ToString(Reader["TRAPELLI"]);
                item.trnombr3 = Convert.ToString(Reader["TRNOMBR3"]);
                item.trtipdoc = Convert.ToString(Reader["TRTIPDOC"]);
                item.trdigchk = Convert.ToString(Reader["TRDIGCHK"]);
                item.trcodzona = Convert.ToString(Reader["TRCODZONA"]);
                item.trtipreg = Convert.ToString(Reader["TRTIPREG"]);
                item.trgranct = Convert.ToString(Reader["TRGRANCT"]);
                item.trautoret = Convert.ToString(Reader["TRAUTORE"]);
                item.trnomcomercial = Convert.ToString(Reader["TRNOMCOMERCIAL"]);
                item.trindfor = Convert.ToString(Reader["TRINDFOR"]);
                item.trsubzona = Convert.ToString(Reader["TRSUBZONA"]);
                item.trmiczona = Convert.ToString(Reader["TRMICZONA"]);

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                item = null;
            }
        }
    }
}
