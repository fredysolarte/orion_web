using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.DAL;
using WFC_ORION.Models;

namespace WFC_ORION.BLL
{
    public class movimientosBL
    {        
        public movimele getItemMovimele(SqlDataReader Reader)
        {
            movimele item = new movimele();

            try
            {
                item.mecodemp = Convert.ToString(Reader["MECODEMP"]);
                item.meidmovi = Reader.IsDBNull(Reader.GetOrdinal("MEIDMOVI")) ? null : (int?)Convert.ToInt32(Reader["MEIDMOVI"]);
                item.meiditem = Reader.IsDBNull(Reader.GetOrdinal("MEIDITEM")) ? null : (int?)Convert.ToInt32(Reader["MEIDITEM"]);
                item.meidlote = Reader.IsDBNull(Reader.GetOrdinal("MEIDLOTE")) ? null : (int?)Convert.ToInt32(Reader["MEIDLOTE"]);
                item.meidelem = Reader.IsDBNull(Reader.GetOrdinal("MEIDELEM")) ? null : (int?)Convert.ToInt32(Reader["MEIDELEM"]);
                item.mefecmov = Convert.ToString(Reader["MEFECMOV"]);
                item.mebodega = Convert.ToString(Reader["MEBODEGA"]);
                item.metippro = Convert.ToString(Reader["METIPPRO"]);
                item.meclave1 = Convert.ToString(Reader["MECLAVE1"]);
                item.meclave2 = Convert.ToString(Reader["MECLAVE2"]);
                item.meclave3 = Convert.ToString(Reader["MECLAVE3"]);
                item.meclave4 = Convert.ToString(Reader["MECLAVE4"]);
                item.mecodcal = Convert.ToString(Reader["MECODCAL"]);
                item.mecdlote = Convert.ToString(Reader["MECDLOTE"]);
                item.mecdelem = Convert.ToString(Reader["MECDELEM"]);
                item.mecdtran = Convert.ToString(Reader["MECDTRAN"]);
                item.mecantid = Reader.IsDBNull(Reader.GetOrdinal("MECANTID")) ? null : (double?)Convert.ToDouble(Reader["MECANTID"]);
                item.mecanori = Reader.IsDBNull(Reader.GetOrdinal("MECANORI")) ? null : (double?)Convert.ToDouble(Reader["MECANORI"]);
                item.mebonifi = Reader.IsDBNull(Reader.GetOrdinal("MEBONIFI")) ? null : (double?)Convert.ToDouble(Reader["MEBONIFI"]);
                item.meumelem = Convert.ToString(Reader["MEUMELEM"]);
                item.menroele = Reader.IsDBNull(Reader.GetOrdinal("MENROELE")) ? null : (int?)Convert.ToInt32(Reader["MENROELE"]);
                item.melocali = Convert.ToString(Reader["MELOCALI"]);
                item.mepesobr = Reader.IsDBNull(Reader.GetOrdinal("MEPESOBR")) ? null : (double?)Convert.ToDouble(Reader["MEPESOBR"]);
                item.mepesont = Reader.IsDBNull(Reader.GetOrdinal("MEPESONT")) ? null : (double?)Convert.ToDouble(Reader["MEPESONT"]);
                item.meanchel = Reader.IsDBNull(Reader.GetOrdinal("MEANCHEL")) ? null : (double?)Convert.ToDouble(Reader["MEANCHEL"]);
                item.melargel = Reader.IsDBNull(Reader.GetOrdinal("MELARGEL")) ? null : (double?)Convert.ToDouble(Reader["MELARGEL"]);
                item.medttec1 = Convert.ToString(Reader["MEDTTEC1"]);
                item.medttec2 = Convert.ToString(Reader["MEDTTEC2"]);
                item.medttec3 = Convert.ToString(Reader["MEDTTEC3"]);
                item.medttec4 = Convert.ToString(Reader["MEDTTEC4"]);
                item.medttec5 = Reader.IsDBNull(Reader.GetOrdinal("MEDTTEC5")) ? null : (double?)Convert.ToDouble(Reader["MEDTTEC5"]);
                item.medttec6 = Reader.IsDBNull(Reader.GetOrdinal("MEDTTEC6")) ? null : (double?)Convert.ToDouble(Reader["MEDTTEC6"]);
                item.meestado = Convert.ToString(Reader["MEESTADO"]);
                item.mecausae = Convert.ToString(Reader["MECAUSAE"]);
                item.menmuser = Convert.ToString(Reader["MENMUSER"]);
                item.mefecing = Convert.ToString(Reader["MEFECING"]);
                item.mefecmod = Convert.ToString(Reader["MEFECMOD"]);
                item.mepdelem = Convert.ToString(Reader["MEPDELEM"]);

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
