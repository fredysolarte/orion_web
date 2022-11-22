using System;
using Mapping;

namespace BE.Administracion
{
	[Serializable]
	[Table(LogicalName = "AdmiRestringecontrol", PhysicalName = "admi_trestringecontrol", EntityId = 20, NumberColumns = 4)]
	public class AdmiRestringeControl
	{
		private string srtUsuaUsuario;
		private int intCtrlControl;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		public AdmiRestringeControl(string usuaUsuario,int ctrlControl,int? logsUsuario,DateTime? logsFecha)
		{
			this.srtUsuaUsuario = usuaUsuario;
			this.intCtrlControl = ctrlControl;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
		}
		public AdmiRestringeControl()
		{
		}
		[Field(LogicalName = "UsuaUsuario", PhysicalName = "usua_usuario", FieldType = System.Data.DbType.String, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias="Usuario")]
		public string UsuaUsuario
		{
			get { return srtUsuaUsuario; }
			set { srtUsuaUsuario=value; }
		}
		[Field(LogicalName = "CtrlControl", PhysicalName = "ctrl_control", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias="Control")]
		public int CtrlControl
		{
			get { return intCtrlControl; }
			set { intCtrlControl=value; }
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
