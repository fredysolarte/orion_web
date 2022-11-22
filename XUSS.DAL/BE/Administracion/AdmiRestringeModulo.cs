using System;
using Mapping;

namespace BE.Administracion
{
	[Serializable]
	[Table(LogicalName = "AdmiRestringemodulo", PhysicalName = "admi_trestringemodulo", EntityId = 20, NumberColumns = 5)]
	public class AdmiRestringeModulo
	{
		private int intSistSistema;
		private int intModuModulo;
		private string srtUsuaUsuario;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		public AdmiRestringeModulo(int sistSistema,int moduModulo,string usuaUsuario,int? logsUsuario,DateTime? logsFecha)
		{
			this.intSistSistema = sistSistema;
			this.intModuModulo = moduModulo;
			this.srtUsuaUsuario = usuaUsuario;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
		}
		public AdmiRestringeModulo()
		{
		}
		[Field(LogicalName = "SistSistema", PhysicalName = "sist_sistema", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias="Sistema")]
		public int SistSistema
		{
			get { return intSistSistema; }
			set { intSistSistema=value; }
		}
		[Field(LogicalName = "ModuModulo", PhysicalName = "modu_modulo", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias="Modulo")]
		public int ModuModulo
		{
			get { return intModuModulo; }
			set { intModuModulo=value; }
		}
		[Field(LogicalName = "UsuaUsuario", PhysicalName = "usua_usuario", FieldType = System.Data.DbType.String, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias="Usuario")]
		public string UsuaUsuario
		{
			get { return srtUsuaUsuario; }
			set { srtUsuaUsuario=value; }
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
