using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.Models;

namespace WFC_ORION.BLL
{
    public class imagenesBL
    {
        public static imagenes getImagenesItem(SqlDataReader Reader)
        {
            imagenes item = new imagenes();
            try {
                item.im_consecutivo = Convert.ToInt32(Reader["IM_CONSECUTIVO"]);
                item.im_codemp = Convert.ToString(Reader["IM_CODEMP"]);
                item.im_tippro = Convert.ToString(Reader["IM_TIPPRO"]);
                item.im_clave1 = Convert.ToString(Reader["IM_CLAVE1"]);
                item.im_clave2 = Convert.ToString(Reader["IM_CLAVE2"]);
                item.im_clave3 = Convert.ToString(Reader["IM_CLAVE3"]);
                item.im_clave4 = Convert.ToString(Reader["IM_CLAVE4"]);
                item.im_tipima = Convert.ToString(Reader["IM_TIPIMA"]);
                item.im_imagen = Convert.ToString(Reader["IM_IMAGEN"]);
                item.im_nmuser = Convert.ToString(Reader["IM_NMUSER"]);
                item.im_fecing = Convert.ToString(Reader["IM_FECING"]);
                item.im_fecmod = Convert.ToString(Reader["IM_FECMOD"]);

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
        public static imagenes getImagenesItem(DataRow row)
        {
            imagenes item = new imagenes();
            try
            {
                item.im_consecutivo = Convert.ToInt32(row["IM_CONSECUTIVO"]);
                item.im_codemp = Convert.ToString(row["IM_CODEMP"]);
                item.im_tippro = Convert.ToString(row["IM_TIPPRO"]);
                item.im_clave1 = Convert.ToString(row["IM_CLAVE1"]);
                item.im_clave2 = Convert.ToString(row["IM_CLAVE2"]);
                item.im_clave3 = Convert.ToString(row["IM_CLAVE3"]);
                item.im_clave4 = Convert.ToString(row["IM_CLAVE4"]);
                item.im_tipima = Convert.ToString(row["IM_TIPIMA"]);
                //item.im_imagen = BytesToString(row["IM_IMAGEN"]);
                item.im_imagen = row["IM_IMAGEN"];
                item.im_nmuser = Convert.ToString(row["IM_NMUSER"]);
                item.im_fecing = Convert.ToString(row["IM_FECING"]);
                item.im_fecmod = Convert.ToString(row["IM_FECMOD"]);

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

        static string BytesToString(object bytes)
        {
            using (MemoryStream stream = new MemoryStream((byte[])bytes))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
