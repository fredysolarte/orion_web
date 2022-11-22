using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using BE.Administracion;
using Mapping;

namespace DAL.Administracion
{
	public class AdmiParametroDB : GenericBaseDB<AdmiParametro>
	{
        public AdmiParametroDB()
            : base("admi_pGeneraSecuencia")
        {
        }
        public List<AdmiParametro> GetListByIdClass(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, int idClass)
        {
            StringBuilder sSql = new StringBuilder();            
            try
            {
                sSql.AppendLine("SELECT para_parametro, clas_clase, para_nombre, para_descripcion, para_valor, para_estado, para_editable, logs_usuario, logs_fecha");
                sSql.AppendLine("FROM admi_tparametro");
                sSql.AppendLine("WHERE (clas_clase = @p0)");
                if (!string.IsNullOrEmpty(filter))
                {
                    sSql.AppendLine(" AND " + filter);
                }
                return MappingData<AdmiParametro>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString().Trim(), CommandType.Text, idClass));
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
