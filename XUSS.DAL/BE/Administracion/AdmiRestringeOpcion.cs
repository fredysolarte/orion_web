using System;
using Mapping;

namespace BE.Administracion
{
	[Serializable]
	[Table(LogicalName = "AdmiRestringeopcion", PhysicalName = "admi_trestringeopcion", EntityId = 22, NumberColumns = 4)]
	public class AdmiRestringeOpcion
	{
		private string srtUsuaUsuario;
		private int intOpciOpcion;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		public AdmiRestringeOpcion(string usuaUsuario,int opciOpcion,int? logsUsuario,DateTime? logsFecha)
		{
			this.srtUsuaUsuario = usuaUsuario;
			this.intOpciOpcion = opciOpcion;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
		}
		public AdmiRestringeOpcion()
		{
		}
		[Field(LogicalName = "UsuaUsuario", PhysicalName = "usua_usuario", FieldType = System.Data.DbType.String, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias="Usuario")]
		public string UsuaUsuario
		{
			get { return srtUsuaUsuario; }
			set { srtUsuaUsuario=value; }
		}
		[Field(LogicalName = "OpciOpcion", PhysicalName = "opci_opcion", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias="Opcion")]
		public int OpciOpcion
		{
			get { return intOpciOpcion; }
			set { intOpciOpcion=value; }
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
