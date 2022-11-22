using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Parametros
{
    public class CarguePlanosBD
    {
        public static DataTable GetTablas(SessionManager oSessionManager)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("select name,object_id from sys.tables with(nolock)");                
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
        }
        public static DataTable GetCampos(SessionManager oSessionManager, int object_id)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("select sys.columns.object_id,sys.columns.name nom_campo,sys.columns.precision,sys.columns.max_length,sys.columns.is_nullable,");
                sSql.AppendLine("sys.types.name nom_tipo,case when sys.indexes.is_primary_key=1 then 0 else 1 end is_primary_key,'                                                         ' campo");
                sSql.AppendLine("from sys.columns");
                sSql.AppendLine("inner join sys.types on (sys.types.system_type_id = sys.columns.system_type_id)");
                sSql.AppendLine("left outer join sys.index_columns on(sys.index_columns.object_id = sys.columns.object_id and sys.columns.column_id = sys.index_columns.column_id)");
                sSql.AppendLine("left outer join sys.indexes on(sys.indexes.object_id = sys.index_columns.object_id)");
                sSql.AppendLine("where sys.columns.object_id = @p0");
                sSql.AppendLine("order by sys.columns.column_id");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, object_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
        }

        public static int EjecucionInsert(SessionManager oSessionManager, string inSql, object[] inValues)
        {
            try
            {                
                return DBAccess.ExecuteNonQuery(oSessionManager, inSql, CommandType.Text, inValues);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }
    }
}
