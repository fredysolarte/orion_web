using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.DAL;
using WFC_ORION.Models;

namespace WFC_ORION.BLL
{
    public class facturasBL
    {
        public List<facturahd> getFacturasHD(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<facturahd> lst = new List<facturahd>();
            try
            {
                Obj.ConnectionString = inConnection;
                using (SqlDataReader reader = facturasBD.getFacturasHD(Obj, "001", inFiltro))
                {
                    while (reader.Read())
                    {
                        lst.Add(getFacturaHD(reader));
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
                Obj = null;
            }
        }

        public facturahd getFacturaHD(SqlDataReader Reader) {

            facturahd item = new facturahd();
            try
            {
                item.hdcodemp = Convert.ToString(Reader["HDCODEMP"]);
                item.hdtipfac = Convert.ToString(Reader["HDTIPFAC"]);
                item.hdnrofac = Reader.IsDBNull(Reader.GetOrdinal("HDNROFAC")) ? null : (int?)Convert.ToInt32(Reader["HDNROFAC"]);
                item.hdfecfac = Convert.ToString(Reader["LH_CODEMP"]);
                item.hdcodcli = Reader.IsDBNull(Reader.GetOrdinal("HDCODCLI")) ? null : (int?)Convert.ToInt32(Reader["HDCODCLI"]);
                item.hdcodsuc = Reader.IsDBNull(Reader.GetOrdinal("HDCODSUC")) ? null : (int?)Convert.ToInt32(Reader["HDCODSUC"]);
                item.hdfecven = Convert.ToString(Reader["LH_CODEMP"]);
                item.hdsubtot = Reader.IsDBNull(Reader.GetOrdinal("HDSUBTOT")) ? null : (double?)Convert.ToDouble(Reader["HDSUBTOT"]);
                item.hdtotdes = Reader.IsDBNull(Reader.GetOrdinal("HDTOTDES")) ? null : (double?)Convert.ToDouble(Reader["HDTOTDES"]);
                item.hdtotiva = Reader.IsDBNull(Reader.GetOrdinal("HDTOTIVA")) ? null : (double?)Convert.ToDouble(Reader["HDTOTIVA"]);
                item.hdtotfac = Reader.IsDBNull(Reader.GetOrdinal("HDTOTFAC")) ? null : (double?)Convert.ToDouble(Reader["HDTOTFAC"]);
                item.hdmoneda = Convert.ToString(Reader["HDMONEDA"]);
                item.hdsubttl = Reader.IsDBNull(Reader.GetOrdinal("HDSUBTTL")) ? null : (double?)Convert.ToDouble(Reader["HDSUBTTL"]);
                item.hdtotdsl = Reader.IsDBNull(Reader.GetOrdinal("HDTOTDSL")) ? null : (double?)Convert.ToDouble(Reader["HDTOTDSL"]);
                item.hdtotivl = Reader.IsDBNull(Reader.GetOrdinal("HDTOTIVL")) ? null : (double?)Convert.ToDouble(Reader["HDTOTIVL"]);
                item.hdtotfcl = Reader.IsDBNull(Reader.GetOrdinal("HDTOTFCL")) ? null : (double?)Convert.ToDouble(Reader["HDTOTFCL"]);
                item.hdsubttd = Reader.IsDBNull(Reader.GetOrdinal("HDSUBTTD")) ? null : (double?)Convert.ToDouble(Reader["HDSUBTTD"]);
                item.hdtotdsd = Reader.IsDBNull(Reader.GetOrdinal("HDTOTDSD")) ? null : (double?)Convert.ToDouble(Reader["HDTOTDSD"]);
                item.hdtotivd = Reader.IsDBNull(Reader.GetOrdinal("HDTOTIVD")) ? null : (double?)Convert.ToDouble(Reader["HDTOTIVD"]);
                item.hdtotfcd = Reader.IsDBNull(Reader.GetOrdinal("HDTOTFCD")) ? null : (double?)Convert.ToDouble(Reader["HDTOTFCD"]);
                item.hdcodnit = Convert.ToString(Reader["HDCODNIT"]);
                item.hdcdpais = Convert.ToString(Reader["HDCDPAIS"]);
                item.hdciudad = Convert.ToString(Reader["HDCIUDAD"]);
                item.hdmoddes = Convert.ToString(Reader["HDMODDES"]);
                item.hdterdes = Convert.ToString(Reader["HDTERDES"]);
                item.hdterpag = Convert.ToString(Reader["HDTERPAG"]);
                item.hdagente = Reader.IsDBNull(Reader.GetOrdinal("HDAGENTE")) ? null : (int?)Convert.ToInt32(Reader["HDAGENTE"]);
                item.hdresdian = Convert.ToString(Reader["HDRESDIAN"]);
                item.hdcatego = Convert.ToString(Reader["HDCATEGO"]);
                item.hdcajcom = Convert.ToString(Reader["HDCAJCOM"]);
                item.hdnroalj = Convert.ToString(Reader["HDNROALJ"]);
                item.hdtipalj = Convert.ToString(Reader["HDTIPALJ"]);
                item.hddivisi = Convert.ToString(Reader["HDDIVISI"]);
                item.hdtototr = Reader.IsDBNull(Reader.GetOrdinal("HDTOTOTR")) ? null : (double?)Convert.ToDouble(Reader["HDTOTOTR"]);
                item.hdtotseg = Reader.IsDBNull(Reader.GetOrdinal("HDTOTSEG")) ? null : (double?)Convert.ToDouble(Reader["HDTOTSEG"]);
                item.hdtotfle = Reader.IsDBNull(Reader.GetOrdinal("HDTOTFLE")) ? null : (double?)Convert.ToDouble(Reader["HDTOTFLE"]);
                item.hdcntfis = Convert.ToString(Reader["HDCNTFIS"]);
                item.hdobserv = Convert.ToString(Reader["HDOBSERV"]);
                item.hdtotica = Reader.IsDBNull(Reader.GetOrdinal("HDTOTOTR")) ? null : (double?)Convert.ToDouble(Reader["HDTOTOTR"]);
                item.hdtotfte = Reader.IsDBNull(Reader.GetOrdinal("HDTOTOTR")) ? null : (double?)Convert.ToDouble(Reader["HDTOTOTR"]);
                item.hdtotfiv = Reader.IsDBNull(Reader.GetOrdinal("HDTOTOTR")) ? null : (double?)Convert.ToDouble(Reader["HDTOTOTR"]);
                item.hdestado = Convert.ToString(Reader["HDESTADO"]);
                item.hdcausae = Convert.ToString(Reader["HDCAUSAE"]);
                item.hdnmuser = Convert.ToString(Reader["HDNMUSER"]);
                item.hdfecing = Convert.ToString(Reader["HDFECING"]);
                item.hdfecmod = Convert.ToString(Reader["HDFECMOD"]);
                item.hdtrasmi = Convert.ToString(Reader["HDTRASMI"]);
                item.hdfeccie = Convert.ToString(Reader["HDFECCIE"]);
                item.hdtipdev = Convert.ToString(Reader["HDTIPDEV"]);
                item.hdnrodev = Reader.IsDBNull(Reader.GetOrdinal("HDNRODEV")) ? null : (int?)Convert.ToInt32(Reader["HDNRODEV"]);
                item.hdtfcdev = Convert.ToString(Reader["HDTFCDEV"]);
                item.hdfacdev = Reader.IsDBNull(Reader.GetOrdinal("HDFACDEV")) ? null : (int?)Convert.ToInt32(Reader["HDFACDEV"]);
                item.hdnrocaja = Convert.ToString(Reader["HDNROCAJA"]);
                item.lh_lstpaq = Reader.IsDBNull(Reader.GetOrdinal("LH_LSTPAQ")) ? null : (int?)Convert.ToInt32(Reader["LH_LSTPAQ"]);
                item.hd_tiprem = Convert.ToString(Reader["HD_TIPREM"]);
                item.hd_nrorem = Reader.IsDBNull(Reader.GetOrdinal("HD_NROREM")) ? null : (int?)Convert.ToInt32(Reader["HD_NROREM"]);

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
