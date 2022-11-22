using System;
using Mapping;

namespace BE.Administracion
{
	[Table(LogicalName = "AdmiModuloxrol", PhysicalName = "admi_tmoduloxrol", EntityId = 12, NumberColumns = 5)]
	public class AdmiModuloxrol
	{
		private int intRolmRolm;
		private int intSistSistema;
		private int intModuModulo;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		public AdmiModuloxrol(int rolmRolm,int sistSistema,int moduModulo,int? logsUsuario,DateTime? logsFecha)
		{
			this.intRolmRolm = rolmRolm;
			this.intSistSistema = sistSistema;
			this.intModuModulo = moduModulo;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
		}
		public AdmiModuloxrol()
		{
		}
		[Field(LogicalName = "RolmRolm", PhysicalName = "rolm_rolm", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias="Rolm")]
		public int RolmRolm
		{
			get { return intRolmRolm; }
			set { intRolmRolm=value; }
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
