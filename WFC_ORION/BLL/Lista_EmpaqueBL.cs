using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WFC_ORION.DAL;
using WFC_ORION.Models;

namespace WFC_ORION.BLL
{
    public class Lista_EmpaqueBL
    {
        public List<tb_empaquehd> getEmpaquesHD(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<tb_empaquehd> lst = new List<tb_empaquehd>();
            try
            {
                Obj.ConnectionString = inConnection;
                using (SqlDataReader reader = Lista_EmpaqueBD.getEmpaquesHD(Obj, "001",inFiltro))
                {
                    while (reader.Read())
                    {
                        lst.Add(getEmpaqueHD(reader));
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
        public tb_empaquehd getEmpaqueHD(SqlDataReader Reader) {
            tb_empaquehd item = new tb_empaquehd();
            try {
                item.lh_codemp = Convert.ToString(Reader["LH_CODEMP"]);
                item.lh_lstpaq = Reader.IsDBNull(Reader.GetOrdinal("LH_LSTPAQ")) ? null : (int?)Convert.ToInt32(Reader["LH_LSTPAQ"]); 
                item.lh_feclst = Convert.ToString(Reader["LH_FECLST"]);
                item.lh_pedido = Reader.IsDBNull(Reader.GetOrdinal("LH_PEDIDO")) ? null : (int?)Convert.ToInt32(Reader["LH_PEDIDO"]);
                item.lh_bodega = Convert.ToString(Reader["LH_BODEGA"]);
                item.lh_estado = Convert.ToString(Reader["LH_ESTADO"]);
                item.lh_causae = Convert.ToString(Reader["LH_CAUSAE"]);
                item.lh_nmuser = Convert.ToString(Reader["LH_NMUSER"]);
                item.lh_fecing = Convert.ToString(Reader["LH_FECING"]);
                item.lh_fecmod = Convert.ToString(Reader["LH_FECMOD"]);
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
        public List<tb_empaquedt> getEmpaquesDT(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<tb_empaquedt> lst = new List<tb_empaquedt>();
            try
            {
                Obj.ConnectionString = inConnection;
                using (SqlDataReader reader = Lista_EmpaqueBD.getEmpaquesDT(Obj, "001", inFiltro))
                {
                    while (reader.Read())
                    {
                        lst.Add(getEmpaqueDT(reader));
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
        public tb_empaquedt getEmpaqueDT(SqlDataReader Reader)
        {
            tb_empaquedt item = new tb_empaquedt();
            try
            {
                item.ld_codemp = Convert.ToString(Reader["LD_CODEMP"]);
                item.lh_lstpaq = Reader.IsDBNull(Reader.GetOrdinal("LH_LSTPAQ")) ? null : (int?)Convert.ToInt32(Reader["LH_LSTPAQ"]);
                item.ld_itmpaq = Reader.IsDBNull(Reader.GetOrdinal("LD_ITMPAQ")) ? null : (int?)Convert.ToInt32(Reader["LD_ITMPAQ"]);
                item.ld_tippro = Convert.ToString(Reader["LD_TIPPRO"]);
                item.ld_clave1 = Convert.ToString(Reader["LD_CLAVE1"]);
                item.ld_clave2 = Convert.ToString(Reader["LD_CLAVE2"]);
                item.ld_clave3 = Convert.ToString(Reader["LD_CLAVE3"]);
                item.ld_clave4 = Convert.ToString(Reader["LD_CLAVE4"]);
                item.ld_cantid = Reader.IsDBNull(Reader.GetOrdinal("LD_CANTID")) ? null : (double?)Convert.ToDouble(Reader["LD_CANTID"]);
                item.ld_pesont = Reader.IsDBNull(Reader.GetOrdinal("LD_PESONT")) ? null : (double?)Convert.ToDouble(Reader["LD_PESONT"]);
                item.ld_bodega = Convert.ToString(Reader["LD_BODEGA"]);
                item.ld_cdlote = Convert.ToString(Reader["LD_CDLOTE"]);
                item.ld_cdelemt = Reader.IsDBNull(Reader.GetOrdinal("LD_CDELEMT")) ? null : (int?)Convert.ToInt32(Reader["LD_CDELEMT"]);
                item.ld_nrmov = Reader.IsDBNull(Reader.GetOrdinal("LD_NRMOV")) ? null : (int?)Convert.ToInt32(Reader["LD_NRMOV"]);
                item.ld_estado = Convert.ToString(Reader["LD_ESTADO"]);
                item.ld_causae = Convert.ToString(Reader["LD_CAUSAE"]);
                item.ld_nmuser = Convert.ToString(Reader["LD_NMUSER"]);
                item.ld_fecing = Convert.ToString(Reader["LD_FECING"]);
                item.ld_fecmod = Convert.ToString(Reader["LD_FECMOD"]);
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
