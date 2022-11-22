using System.Collections.Generic;
using System.Data;
using System.Text;
using BE.Administracion;
using DataAccess;
using Mapping;

namespace DAL.Administracion
{
	public class AdmiBlobDB : GenericBaseDB<AdmiBlob>
	{
		public AdmiBlobDB()
			: base("admi_pGeneraSecuencia")
		{
		}

		override public int Add(SessionManager oSessionManager, AdmiBlob item)
		{
			int seqBlog = -1;
			seqBlog = DBAccess.GetSequence(oSessionManager, procedure, ((Table)new AdmiBlob().GetType().GetCustomAttributes(typeof(Table), true)[0]).EntityId, 0, false);
			DBAccess.ExecuteNonQuery(oSessionManager, "INSERT INTO admi_tblob (blob_blob,tipb_tipoblob, blob_nombre,blob_origen, blob_descripcion,  blob_binario, blob_version,logs_usuario,logs_fecha) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", CommandType.Text, seqBlog, item.TipbTipoblob, item.BlobNombre, item.BlobOrigen, item.BlobDescripcion, item.BlobBinario, item.BlobVersion, item.LogsUsuario, item.LogsFecha);
			return seqBlog;
		}

		public static List<AdmiBlob> GetByIdTipo(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, int idTipo)
		{
			StringBuilder sSql = new StringBuilder();
			sSql.AppendLine("SELECT * FROM admi_tblob WHERE (tipb_tipoblob = @p0)");
			if (!string.IsNullOrWhiteSpace(filter))
			{
				sSql.AppendLine(" and (" + filter + ")");
			}
			return MappingData<AdmiBlob>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, idTipo));
		}

		public static AdmiBlob GetById(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, int id)
		{
			return MappingData<AdmiBlob>.Mapping(DBAccess.GetDataReader(oSessionManager, "SELECT * FROM admi_tblob WHERE (blob_blob = @p0)", CommandType.Text, id));
		}
	}
}