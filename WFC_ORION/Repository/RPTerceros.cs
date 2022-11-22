using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.BLL;
using WFC_ORION.DAL;
using WFC_ORION.Models;

namespace WFC_ORION.Repository
{
    public class RPTerceros
    {        

        public List<consulta_terceros_ph> GetTercerosMultintegralPH(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();            
            try {
                Obj.ConnectionString = inConnection;

                return GetTercerosMultintegralPH(Obj, inFiltro);
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
        private static List<consulta_terceros_ph> GetTercerosMultintegralPH(DBAccess ObjDB, string inFiltro)
        {
            List<consulta_terceros_ph> lst = new List<consulta_terceros_ph>();
            try
            {
                using (SqlDataReader reader = TercerosBD.GetTercerosMultintegralPH(ObjDB, inFiltro))
                {
                    while (reader.Read())
                    {
                        consulta_terceros_ph item = new consulta_terceros_ph();
                        try
                        {
                            item.terceros.Add(TercerosBL.GetTercerosItem(reader));
                            item.id = Convert.ToInt32(reader["TRCODTER"]);
                            item.POTENCIAL = reader.IsDBNull(reader.GetOrdinal("POTENCIAL")) ? null : (int?)Convert.ToInt32(reader["POTENCIAL"]);
                            item.INSTALADOS = reader.IsDBNull(reader.GetOrdinal("INSTALADOS")) ? null : (int?)Convert.ToInt32(reader["INSTALADOS"]);
                            item.PEN_COMERCIALIZAR = reader.IsDBNull(reader.GetOrdinal("PEN_COMERCIALIZAR")) ? null : (int?)Convert.ToInt32(reader["PEN_COMERCIALIZAR"]);
                            item.PEN_RADOFF = reader.IsDBNull(reader.GetOrdinal("PEN_RADOFF")) ? null : (int?)Convert.ToInt32(reader["PEN_RADOFF"]);
                            item.VANTI = reader.IsDBNull(reader.GetOrdinal("VANTI")) ? null : (int?)Convert.ToInt32(reader["VANTI"]);
                            item.OFICINA = reader.IsDBNull(reader.GetOrdinal("OFICINA")) ? null : (int?)Convert.ToInt32(reader["OFICINA"]);
                            item.P_DESMONTE = reader.IsDBNull(reader.GetOrdinal("P_DESMONTE")) ? null : (int?)Convert.ToInt32(reader["P_DESMONTE"]);
                            item.HURTADOS = reader.IsDBNull(reader.GetOrdinal("HURTADOS")) ? null : (int?)Convert.ToInt32(reader["HURTADOS"]);
                            item.DESINSTALADOS = reader.IsDBNull(reader.GetOrdinal("DESINSTALADOS")) ? null : (int?)Convert.ToInt32(reader["DESINSTALADOS"]);

                            lst.Add(item);
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
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lst = null;
            }
        }
    }
    public class consulta_terceros_ph    {
        public int id { get; set; }
        public List<Terceros> terceros { get; set; } = new List<Terceros>();
        public int? POTENCIAL { get; set; }
        public int? INSTALADOS { get; set; }
        public int? PEN_COMERCIALIZAR { get; set; }
        public int? PEN_RADOFF { get; set; }
        public int? VANTI { get; set; }
        public int? OFICINA { get; set; }
        public int? P_DESMONTE { get; set; }
        public int? HURTADOS { get; set; }
        public int? DESINSTALADOS { get; set; }
    }
}
