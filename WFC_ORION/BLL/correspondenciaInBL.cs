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
    public class correspondenciaInBL
    {
        public List<tb_correspondenciahdin> getCorrespondenciaHDIN(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<tb_correspondenciahdin> lst = new List<tb_correspondenciahdin>();
            try
            {
                Obj.ConnectionString = inConnection;
                using (SqlDataReader reader = correspondenciaInBD.getCorrespondenciaInHD(Obj, inFiltro))
                {
                    while (reader.Read())
                    {
                        lst.Add(getItemCorrespondenciaHDIN(reader));
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

        public tb_correspondenciahdin getItemCorrespondenciaHDIN(SqlDataReader Reader)
        {
            tb_correspondenciahdin item = new tb_correspondenciahdin();

            try
            {
                item.cih_codigo = Convert.ToInt32(Reader["CIH_CODIGO"]);
                item.cih_codemp = Convert.ToString(Reader["CIH_CODEMP"]);
                item.cih_descripcion = Convert.ToString(Reader["CIH_DESCRIPCION"]);
                item.cih_fecha = Convert.ToString(Reader["CIH_FECHA"]);
                item.cih_usuario = Convert.ToString(Reader["CIH_USUARIO"]);
                item.cih_estado = Convert.ToString(Reader["CIH_ESTADO"]);
                item.cih_fecing = Convert.ToString(Reader["CIH_FECING"]);
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

        public List<tb_correspondenciadtin> getCorrespondenciaDTIN(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<tb_correspondenciadtin> lst = new List<tb_correspondenciadtin>();
            try
            {
                Obj.ConnectionString = inConnection;
                using (SqlDataReader reader = correspondenciaInBD.getCorrespondenciaInDT(Obj, inFiltro))
                {
                    while (reader.Read())
                    {
                        lst.Add(getItemCorrespondenciaDTIN(reader));
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

        public tb_correspondenciadtin getItemCorrespondenciaDTIN(SqlDataReader Reader)
        {
            tb_correspondenciadtin item = new tb_correspondenciadtin();

            try
            {
                item.cih_codigo = Reader.IsDBNull(Reader.GetOrdinal("CIH_CODIGO")) ? null : (int?)Convert.ToInt32(Reader["CIH_CODIGO"]);
                item.cid_item = Reader.IsDBNull(Reader.GetOrdinal("CID_ITEM")) ? null : (int?)Convert.ToInt32(Reader["CID_ITEM"]);
                item.ph_codigo = Convert.ToInt32(Reader["PH_CODIGO"]);
                item.cid_asesor = Convert.ToString(Reader["CID_ASESOR"]);
                item.cid_usuario = Convert.ToString(Reader["CID_USUARIO"]);
                item.cid_estado = Convert.ToString(Reader["CID_ESTADO"]);
                item.cid_causae = Convert.ToString(Reader["CID_CAUSAE"]);
                item.cid_fecing = Convert.ToString(Reader["CID_FECING"]);
                item.cid_fecmod = Convert.ToString(Reader["CID_FECMOD"]);

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
