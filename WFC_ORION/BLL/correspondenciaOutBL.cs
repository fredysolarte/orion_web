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
    public class correspondenciaOutBL
    {
        public List<tb_correspondenciahdout> getCorrespondenciaHDIN(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<tb_correspondenciahdout> lst = new List<tb_correspondenciahdout>();
            try
            {
                Obj.ConnectionString = inConnection;
                using (SqlDataReader reader = correspondenciaOutBD.getCorrespondenciaOutHD(Obj, inFiltro))
                {
                    while (reader.Read())
                    {
                        lst.Add(getItemCorrespondenciaHDOUT(reader));
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

        public tb_correspondenciahdout getItemCorrespondenciaHDOUT(SqlDataReader Reader)
        {
            tb_correspondenciahdout item = new tb_correspondenciahdout();

            try
            {
                item.coh_codigo = Convert.ToInt32(Reader["COH_CODIGO"]);
                item.coh_codemp = Convert.ToString(Reader["COH_CODEMP"]);
                item.coh_descripcion = Convert.ToString(Reader["COH_DESCRIPCION"]);
                item.coh_fecha = Convert.ToString(Reader["COH_FECHA"]);
                item.coh_usuario = Convert.ToString(Reader["COH_USUARIO"]);
                item.coh_estado = Convert.ToString(Reader["COH_ESTADO"]);
                item.coh_fecing = Convert.ToString(Reader["COH_FECING"]);
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

        public List<tb_correspondenciadtout> getCorrespondenciaDTIN(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<tb_correspondenciadtout> lst = new List<tb_correspondenciadtout>();
            try
            {
                Obj.ConnectionString = inConnection;
                using (SqlDataReader reader = correspondenciaOutBD.getCorrespondenciaOutDT(Obj, inFiltro))
                {
                    while (reader.Read())
                    {
                        lst.Add(getItemCorrespondenciaDTOUT(reader));
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

        public tb_correspondenciadtout getItemCorrespondenciaDTOUT(SqlDataReader Reader)
        {
            tb_correspondenciadtout item = new tb_correspondenciadtout();

            try
            {
                item.coh_pk = Reader.IsDBNull(Reader.GetOrdinal("COH_PK")) ? null : (int?)Convert.ToInt32(Reader["COH_PK"]);
                item.coh_codigo = Reader.IsDBNull(Reader.GetOrdinal("COH_CODIGO")) ? null : (int?)Convert.ToInt32(Reader["COH_CODIGO"]);
                item.cod_item = Reader.IsDBNull(Reader.GetOrdinal("COD_ITEM")) ? null : (int?)Convert.ToInt32(Reader["COD_ITEM"]);
                item.ph_codigo = Convert.ToInt32(Reader["PH_CODIGO"]);                
                item.cod_usuario = Convert.ToString(Reader["COD_USUARIO"]);
                item.cod_estado = Convert.ToString(Reader["COD_ESTADO"]);
                item.cod_causae = Convert.ToString(Reader["COD_CAUSAE"]);
                item.cod_fecing = Convert.ToString(Reader["COD_FECING"]);
                item.cod_fecmod = Convert.ToString(Reader["COD_FECMOD"]);

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
