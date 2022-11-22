using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BE.Administracion;
using DataAccess;


namespace DAL.Administracion
{
    public class AdmiColumnaDB : GenericBaseDB<AdmiColumna>
    {
        public AdmiColumnaDB()
            : base("admi_pGeneraSecuencia")
        {
        }

        //public DataTable GetDataTableByTableId(SessionManager oSessionManager, int tableId)
        //{
        //    StringBuilder sSql = new StringBuilder();
        //    DBAccess doObject = new DBAccess();
        //    try
        //    {
        //        sSql.AppendLine("SELECT tabl_tabla, colu_columna, colu_nombre, colu_alias, colu_descripcion, colu_tipoDato");
        //        sSql.AppendLine("FROM Admi_tColumna");
        //        sSql.AppendLine("WHERE tabl_tabla = " + tableId);
        //        return doObject.GetDataTable(oSessionManager, sSql.ToString().Trim(), CommandType.Text, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        sSql = null;
        //        doObject = null;
        //    }
        //}
    }
}
