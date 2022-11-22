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
    public class tb_comericialBL
    {
        public List<tb_comercial> geDesistalacion(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<tb_comercial> lst = new List<tb_comercial>();
            try
            {
                Obj.ConnectionString = inConnection;
                using (SqlDataReader reader = tb_comercialBD.getComercial(Obj, inFiltro))
                {
                    while (reader.Read())
                    {
                        lst.Add(getItemComercial(reader));
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

        public static tb_comercial getItemComercial(SqlDataReader Reader)
        {
            tb_comercial item = new tb_comercial();

            try
            {
                item.co_codigo = Convert.ToInt32(Reader["CO_CODIGO"]);
                item.co_codemp = Convert.ToString(Reader["CO_CODEMP"]);
                item.ph_codigo = Convert.ToInt32(Reader["PH_CODIGO"]);
                item.co_cuotas = Convert.ToInt32(Reader["CO_CUOTAS"]);
                item.co_precio = Convert.ToDouble(Reader["CO_PRECIO"]);
                item.trcodter = Convert.ToInt32(Reader["TRCODTER"]);
                item.co_fecha = Convert.ToString(Reader["CO_FECHA"]);
                item.co_estado = Convert.ToString(Reader["CO_ESTADO"]);
                item.co_usuario = Convert.ToString(Reader["CO_USUARIO"]);
                item.co_fecing = Convert.ToString(Reader["CO_FECING"]);
                item.co_feccomodato = Convert.ToString(Reader["CO_FECCOMODATO"]);
                item.co_fecpagare = Convert.ToString(Reader["CO_FECPAGARE"]);
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
