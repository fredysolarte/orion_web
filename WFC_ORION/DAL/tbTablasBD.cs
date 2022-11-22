using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFC_ORION.DAL
{
    public class tbTablasBD
    {
        public static int GeneraConsecutivo(DBAccess ObjDB, string TTCODCLA)
        {
            StringBuilder sql = new StringBuilder();
            

            try
            {
                sql.AppendLine("UPDATE TBTABLAS         ");
                sql.AppendLine("SET TTVALORN = TTVALORN + 1 ");
                sql.AppendLine("WHERE TTCODEMP = '001'  ");
                sql.AppendLine("AND TTCODTAB = 'CONT'   ");
                sql.AppendLine("AND TTCODCLA = @p0 ");//NROTCK

                ObjDB.ExecuteNonQuery(sql.ToString(), TTCODCLA);

                sql.Clear();
                sql.AppendLine("SELECT TTVALORN ");
                sql.AppendLine(" FROM TBTABLAS  ");
                sql.AppendLine("WHERE TTCODEMP = '001' ");
                sql.AppendLine("  AND TTCODTAB = 'CONT' ");
                sql.AppendLine("  AND TTCODCLA = @p0 ");
                return Convert.ToInt32(ObjDB.ExecuteScalar( sql.ToString(), TTCODCLA));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sql = null;
            }
        }
    }
}
