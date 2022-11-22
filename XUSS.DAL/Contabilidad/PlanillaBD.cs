using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Contabilidad
{
    public class PlanillaBD
    {
        public DataTable GetPuc(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder(); 
            try
            {
                sSql.AppendLine("SELECT PC_ID,PC_PARENT,PC_EMPRESA,PC_CODIGO,(PC_CODIGO +'-'+ PC_NOMBRE) PC_NOMBRE,PC_NATURALEZA,PC_ESTADO,PC_USUARIO,PC_FECING,PC_FECMOD,PC_TIPO ");
                sSql.AppendLine("  FROM TB_PUC WITH(NOLOCK) WHERE 1= 1 " + filter);
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
        public int InsertPUC(SessionManager oSessionManager, int PC_PARENT, string PC_EMPRESA, string PC_CODIGO, string PC_NOMBRE, string PC_NATURALEZA, string PC_TIPO, string PC_ESTADO, string PC_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_PUC (PC_PARENT,PC_EMPRESA,PC_CODIGO,PC_NOMBRE,PC_NATURALEZA,PC_TIPO,PC_ESTADO,PC_USUARIO,PC_FECING,PC_FECMOD) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PC_PARENT, PC_EMPRESA, PC_CODIGO, PC_NOMBRE, PC_NATURALEZA, PC_TIPO, PC_ESTADO, PC_USUARIO);
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

        public int UpdatePUC(SessionManager oSessionManager, int PC_ID, string PC_EMPRESA, string PC_CODIGO, string PC_NOMBRE, string PC_NATURALEZA, string PC_TIPO, string PC_ESTADO, string PC_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_PUC SET PC_EMPRESA=@p1,PC_CODIGO=@p2,PC_NOMBRE=@p3,PC_NATURALEZA=@p4,PC_TIPO=@p5,PC_ESTADO=@p6,PC_USUARIO=@p7,PC_FECMOD=GETDATE() WHERE PC_ID=@p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PC_ID, PC_EMPRESA, PC_CODIGO, PC_NOMBRE, PC_NATURALEZA, PC_TIPO, PC_ESTADO, PC_USUARIO);
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
        public DataTable GetAutoComplete(SessionManager oSessionManager, string filtro)
        {            
            try
            {
                return DBAccess.GetDataTable(oSessionManager, filtro, CommandType.Text);    
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        public DataTable GetPuc(SessionManager oSessionManager)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT PUC_CUENTA,PUC_DESCRIPCION FROM CONT_PUC WITH(NOLOCK) ");
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
    }
}
