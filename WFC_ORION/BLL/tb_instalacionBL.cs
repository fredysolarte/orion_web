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
    public class tb_instalacionlBL
    {
        public List<tb_instalacion> getInstalacion(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<tb_instalacion> lst = new List<tb_instalacion>();
            try
            {
                Obj.ConnectionString = inConnection;
                using (SqlDataReader reader = tb_instalacionBD.getInstalacion(Obj, inFiltro))
                {
                    while (reader.Read())
                    {
                        lst.Add(getItemInstalacion(reader));
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

        public tb_instalacion getItemInstalacion(SqlDataReader Reader)
        {
            tb_instalacion item = new tb_instalacion();

            try
            {
                item.it_codigo = Reader.IsDBNull(Reader.GetOrdinal("IT_CODIGO")) ? null : (int?)Convert.ToInt32(Reader["IT_CODIGO"]);
                item.it_codemp = Convert.ToString(Reader["IT_CODEMP"]);
                item.ph_codigo = Reader.IsDBNull(Reader.GetOrdinal("PH_CODIGO")) ? null : (int?)Convert.ToInt32(Reader["PH_CODIGO"]);
                item.meidmovi = Reader.IsDBNull(Reader.GetOrdinal("MEIDMOVI")) ? null : (int?)Convert.ToInt32(Reader["MEIDMOVI"]);
                item.meiditem = Reader.IsDBNull(Reader.GetOrdinal("MEIDITEM")) ? null : (int?)Convert.ToInt32(Reader["MEIDITEM"]);
                item.meidlote = Reader.IsDBNull(Reader.GetOrdinal("MEIDLOTE")) ? null : (int?)Convert.ToInt32(Reader["MEIDLOTE"]);
                item.meidelem = Reader.IsDBNull(Reader.GetOrdinal("MEIDELEM")) ? null : (int?)Convert.ToInt32(Reader["MEIDELEM"]);
                item.it_observaciones = Convert.ToString(Reader["IT_OBSERVACIONES"]);                
                item.it_fecha = Convert.ToString(Reader["IT_FECHA"]);
                item.it_estado = Convert.ToString(Reader["IT_ESTADO"]);
                item.it_usuario = Convert.ToString(Reader["IT_USUARIO"]);
                item.it_fecing = Convert.ToString(Reader["IT_FECING"]);                
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
