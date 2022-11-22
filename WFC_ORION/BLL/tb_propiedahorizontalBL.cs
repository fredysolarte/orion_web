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
    public class tb_propiedahorizontalBL
    {
        public List<tb_propiedahorizontal> getPropiedadHorizontal(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<tb_propiedahorizontal> lst = new List<tb_propiedahorizontal>();
            try
            {
                Obj.ConnectionString = inConnection;
                using (SqlDataReader reader = tb_propiedahorizontalBD.getPropiedahorizontal(Obj, inFiltro))
                {
                    while (reader.Read())
                    {
                        lst.Add(getItemPropiedahorizontal(reader));
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

        public SqlDataReader getPropiedadHorizontalDetalle(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            
            try
            {
                Obj.ConnectionString = inConnection;
                               
                return tb_propiedahorizontalBD.getPropiedadHorizontalDetalle(Obj, inFiltro);
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

        public tb_propiedahorizontal getItemPropiedahorizontal(SqlDataReader Reader)
        {
            tb_propiedahorizontal item = new tb_propiedahorizontal();

            try
            {
                item.ph_codigo = Convert.ToInt32(Reader["DI_CODIGO"]);
                item.ph_ctacontrato = Convert.ToString(Reader["DI_CODEMP"]);
                item.ph_codemp = Convert.ToString(Reader["DI_CODEMP"]);                
                item.trcodter = Convert.ToInt32(Reader["DI_CODIGO"]);
                item.ph_edificio = Convert.ToString(Reader["DI_FECHA"]);
                item.ph_portal = Convert.ToString(Reader["DI_USUARIO"]);
                item.ph_piso = Convert.ToString(Reader["DI_FECING"]);
                item.ph_escalera = Convert.ToString(Reader["DI_NRODOC"]);
                item.ph_objconexion = Convert.ToString(Reader["DI_NRODOC"]);
                item.ph_ptosuministro = Convert.ToString(Reader["DI_NRODOC"]);
                item.ph_instalacion = Convert.ToString(Reader["DI_NRODOC"]);
                item.ph_ubcaparato = Convert.ToString(Reader["DI_NRODOC"]);
                item.ph_usuario = Convert.ToString(Reader["DI_NRODOC"]);
                item.ph_fecing = Convert.ToString(Reader["DI_NRODOC"]);
                item.ph_poliza = Convert.ToString(Reader["DI_NRODOC"]);
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
