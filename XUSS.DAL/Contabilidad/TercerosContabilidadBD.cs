using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Contabilidad
{
    public class TercerosContabilidadBD
    {
        public DataTable GetTerceros(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM CONT_TERCEROS WITH(NOLOCK) WHERE 1=1 ");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine(" " + filter);
                }
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
        public int InsertTerceros(SessionManager oSessionManager, int TR_CONSE, string TR_CODEMP, string TR_TIPDOC, string TR_NUMDOC, string TR_PRIMERNOMBRE,
                                  string TR_PRIMERAPELLI, string TR_SEGUNDNOMBRE, string TR_SEGUNDAPELLI, string TR_DIRECCION, string TR_TELEFONO, string TR_USUARIO, string TR_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CONT_TERCEROS (TR_CONSE,TR_CODEMP,TR_TIPDOC,TR_NUMDOC,TR_PRIMERNOMBRE,TR_PRIMERAPELLI,TR_SEGUNDNOMBRE,TR_SEGUNDAPELLI,");
                sSql.AppendLine("TR_DIRECCION, TR_TELEFONO, TR_USUARIO,TR_ESTADO, TR_FECING, TR_FECMOD) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,GETDATE(),GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TR_CONSE, TR_CODEMP, TR_TIPDOC, TR_NUMDOC, TR_PRIMERNOMBRE,
                                  TR_PRIMERAPELLI, TR_SEGUNDNOMBRE, TR_SEGUNDAPELLI, TR_DIRECCION, TR_TELEFONO, TR_USUARIO, TR_ESTADO);
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

        public int UpdateTerceros(SessionManager oSessionManager, int TR_CONSE, string TR_TIPDOC, string TR_NUMDOC, string TR_PRIMERNOMBRE,
                                  string TR_PRIMERAPELLI, string TR_SEGUNDNOMBRE, string TR_SEGUNDAPELLI, string TR_DIRECCION, string TR_TELEFONO, string TR_USUARIO, string TR_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE CONT_TERCEROS SET TR_TIPDOC=@p0,TR_NUMDOC=@p1,TR_PRIMERNOMBRE=@p2,TR_PRIMERAPELLI=@p3,TR_SEGUNDNOMBRE=@p4,TR_SEGUNDAPELLI=@p5,");
                sSql.AppendLine("TR_DIRECCION=@p6, TR_TELEFONO=@p7, TR_USUARIO=@p8,TR_ESTADO=@p9, TR_FECMOD=GETDATE() ");
                sSql.AppendLine(" WHERE TR_CONSE=@p10");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TR_TIPDOC, TR_NUMDOC, TR_PRIMERNOMBRE,
                                  TR_PRIMERAPELLI, TR_SEGUNDNOMBRE, TR_SEGUNDAPELLI, TR_DIRECCION, TR_TELEFONO, TR_USUARIO, TR_ESTADO, TR_CONSE);
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
