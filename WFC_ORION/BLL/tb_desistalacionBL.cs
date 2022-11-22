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
    public class tb_desistalacionBL
    {
        public List<tb_desistalacion> geDesistalacion(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<tb_desistalacion> lst = new List<tb_desistalacion>();
            try
            {
                Obj.ConnectionString = inConnection;
                using (SqlDataReader reader = tb_desistalacionBD.getDesistalacion(Obj, inFiltro))
                {
                    while (reader.Read())
                    {
                        lst.Add(getItemDesistalacion(reader));
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

        public tb_desistalacion getItemDesistalacion(SqlDataReader Reader)
        {
            tb_desistalacion item = new tb_desistalacion();

            try
            {
                item.di_codigo = Convert.ToInt32(Reader["DI_CODIGO"]);
                item.ph_codigo = Convert.ToInt32(Reader["PH_CODIGO"]);
                item.di_codemp = Convert.ToString(Reader["DI_CODEMP"]);
                item.di_fecha = Convert.ToString(Reader["DI_FECHA"]);
                item.di_usuario = Convert.ToString(Reader["DI_USUARIO"]);
                item.di_fecing = Convert.ToString(Reader["DI_FECING"]);
                item.di_nrodoc = Convert.ToString(Reader["DI_NRODOC"]);                
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
