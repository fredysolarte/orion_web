using System.Collections.Generic;
using System.Data;
using System.Text;
using BE.Administracion;
using DataAccess;
using Mapping;
using System;
using XUSS.DAL.Genericas;

namespace DAL.Administracion
{
	public class AdmiSistemaDB : GenericBaseDB<AdmiSistema>
	{
		public AdmiSistemaDB()
			: base("admi_pGeneraSecuencia")
		{
		}

		override public List<AdmiSistema> GetAllList(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();
			sSql.AppendLine("SELECT admi_tsistema.sist_sistema, admi_tsistema.sist_nombre, admi_tsistema.sist_descripcion, admi_tsistema.icon_logo,  ");
            sSql.AppendLine(" admi_tsistema.sist_identifica  FROM admi_tsistema "); //,admi_tsistema.sistema_url
            //sSql.AppendLine(" LEFT OUTER JOIN admi_tblob ON admi_tsistema.icon_logo = admi_tblob.blob_blob");
			if (!string.IsNullOrWhiteSpace(filter))
			{
				sSql.AppendLine(" WHERE " + filter);
			}
			return MappingData<AdmiSistema>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text));
		}

        override public int Add(SessionManager oSessionManager, AdmiSistema objEntity)
        {
            StringBuilder sSql = new StringBuilder();
            object[] parametersSequence = new object[2];
            int seq = -1;
            try
            {
                parametersSequence[0] = ((Table)objEntity.GetType().GetCustomAttributes(typeof(Table), true)[0]).EntityId;
                parametersSequence[1] = 0;
                //parametersSequence[2] = 0;
                //seq = DBAccess.GetSequence(oSessionManager, "admi_pGeneraSecuencia", 4,0,true);
                seq = GenericaBD.GetSecuencia(oSessionManager, 4, 0);
                sSql.AppendLine("INSERT INTO admi_tsistema");
                sSql.AppendLine("(sist_sistema, sist_nombre, sist_descripcion, icon_logo, sist_identifica)");
                sSql.AppendLine("VALUES        (@p0,@p1,@p2,@p3,@p4)");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, seq, objEntity.SistNombre, objEntity.SistDescripcion, objEntity.IconLogo, objEntity.SistIdentifica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {                
                sSql = null;
            }
            return seq;
        }

        override public void Update(SessionManager oSessionManager, AdmiSistema objEntity)
        {            
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE admi_tsistema");
                sSql.AppendLine("SET sist_nombre = @p0, sist_descripcion = @p1, icon_logo = @p2, sist_identifica = @p3");
                sSql.AppendLine("WHERE (sist_sistema = @p4)");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, objEntity.SistNombre, objEntity.SistDescripcion, objEntity.IconLogo, objEntity.SistIdentifica, objEntity.SistSistema);
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