using System;
using Mapping;


namespace BE.Administracion
{
	[Table(LogicalName = "AdmiOpcionxrol", PhysicalName = "admi_topcionxrol", EntityId = 13, NumberColumns = 4)]
	public class AdmiOpcionxrol
	{
		private int intRolmRolm;
		private int intOpciOpcion;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		public AdmiOpcionxrol(int rolmRolm,int opciOpcion,int? logsUsuario,DateTime? logsFecha)
		{
			this.intRolmRolm = rolmRolm;
			this.intOpciOpcion = opciOpcion;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
		}
		public AdmiOpcionxrol()
		{
		}
		[Field(LogicalName = "RolmRolm", PhysicalName = "rolm_rolm", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias="Rolm")]
		public int RolmRolm
		{
			get { return intRolmRolm; }
			set { intRolmRolm=value; }
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
