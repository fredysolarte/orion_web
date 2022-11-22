using System;
using Mapping;

namespace BE.Administracion
{
	[Table(LogicalName = "AdmiControlxrol", PhysicalName = "admi_tcontrolxrol", EntityId = 18, NumberColumns = 4)]
	public class AdmiControlxrol
	{
		private int intCtrlControl;
		private int intRolmRolm;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		public AdmiControlxrol(int ctrlControl,int rolmRolm,int? logsUsuario,DateTime? logsFecha)
		{
			this.intCtrlControl = ctrlControl;
			this.intRolmRolm = rolmRolm;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
		}
		public AdmiControlxrol()
		{
		}
		[Field(LogicalName = "CtrlControl", PhysicalName = "ctrl_control", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias="Control")]
		public int CtrlControl
		{
			get { return intCtrlControl; }
			set { intCtrlControl=value; }
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
