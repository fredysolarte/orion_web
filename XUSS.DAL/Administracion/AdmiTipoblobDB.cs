using System.Data;
using BE.Administracion;
using DataAccess;
using Mapping;
using BE.Administracion;

namespace DAL.Administracion
{
	public class AdmiTipoblobDB : GenericBaseDB<AdmiTipoblob>
	{
		public AdmiTipoblobDB()
			: base("admi_pGeneraSecuencia")
		{
		}

		public AdmiTipoblob GetById(SessionManager oSessionManager, int idTipoBlob)
		{
			return MappingData<AdmiTipoblob>.Mapping(DBAccess.GetDataReader(oSessionManager, "SELECT * FROM admi_ttipoblob WHERE (tipb_tipoblob = @p0)", CommandType.Text, idTipoBlob));
		}
	}
}