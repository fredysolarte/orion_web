using System;
using Mapping;

namespace BE.Administracion
{
	[Serializable]
	[Table(LogicalName = "AdmiRolxUsuario", PhysicalName = "admi_trolxusuario", EntityId = 23, NumberColumns = 4)]
	public class AdmiRolxUsuario
	{
		private string srtUsuaUsuario;
		private int intRolmRolm;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		public AdmiRolxUsuario(string usuaUsuario,int rolmRolm,int? logsUsuario,DateTime? logsFecha)
		{
			this.srtUsuaUsuario = usuaUsuario;
			this.intRolmRolm = rolmRolm;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
		}
		public AdmiRolxUsuario()
		{
		}
		[Field(LogicalName = "UsuaUsuario", PhysicalName = "usua_usuario", FieldType = System.Data.DbType.String, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias="Usuario")]
		public string UsuaUsuario
		{
			get { return srtUsuaUsuario; }
			set { srtUsuaUsuario=value; }
		}
		[Field(LogicalName = "RolmRolm", PhysicalName = "rolm_rolm", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias="Rolm")]
		public int RolmRolm
		{
			get { return intRolmRolm; }
			set { intRolmRolm=value; }
		}
		[Field(LogicalName = "LogsUsuario", PhysicalName = "logs_usuario", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias="Usuario")]
		public int? LogsUsuario
		{
			get { return intLogsUsuario; }
			set { intLogsUsuario=value; }
		}
		[Field(LogicalName = "LogsFecha", PhysicalName = "logs_fecha", FieldType = System.Data.DbType.DateTime, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias="Fecha")]
		public DateTime? LogsFecha
		{
			get { return datLogsFecha; }
			set { datLogsFecha=value; }
		}
	}
}
