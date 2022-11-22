using System;
using Mapping;

namespace BE.Administracion
{
	[Table(LogicalName = "AdmiRol", PhysicalName = "admi_trol", EntityId = 9, NumberColumns = 6)]
	public class AdmiRol
	{
		private string srtRolmRolm;
		private string srtRolmNombre;
		private string srtRolmDescripcion;
		private int? intRolmEstado;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		public AdmiRol(string rolmRolm,string rolmNombre,string rolmDescripcion,int? rolmEstado,int? logsUsuario,DateTime? logsFecha)
		{
			this.srtRolmRolm = rolmRolm;
			this.srtRolmNombre = rolmNombre;
			this.srtRolmDescripcion = rolmDescripcion;
			this.intRolmEstado = rolmEstado;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
		}
		public AdmiRol()
		{
		}
		[Field(LogicalName = "RolmRolm", PhysicalName = "rolm_rolm", FieldType = System.Data.DbType.String, PrimaryKey = true, IsSequence = true, IsDiscriminant = false, Alias="Id", IsFilterable=true)]
		public string RolmRolm
		{
			get { return srtRolmRolm; }
			set { srtRolmRolm=value; }
		}
        [Field(LogicalName = "RolmNombre", PhysicalName = "rolm_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Nombre", IsFilterable = true)]
		public string RolmNombre
		{
			get { return srtRolmNombre; }
			set { srtRolmNombre=value; }
		}
		[Field(LogicalName = "RolmDescripcion", PhysicalName = "rolm_descripcion", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias="Descripcion", IsFilterable = true)]
		public string RolmDescripcion
		{
			get { return srtRolmDescripcion; }
			set { srtRolmDescripcion=value; }
		}
        [Field(LogicalName = "RolmEstado", PhysicalName = "rolm_estado", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Estado", IsFilterable = false)]
		public bool RolmEstado
		{
			get { return intRolmEstado == 1; }
			set { intRolmEstado=value ? 1:0; }
		}
        [Field(LogicalName = "LogsUsuario", PhysicalName = "logs_usuario", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Usuario", IsFilterable = false)]
		public int? LogsUsuario
		{
			get { return intLogsUsuario; }
			set { intLogsUsuario=value; }
		}
        [Field(LogicalName = "LogsFecha", PhysicalName = "logs_fecha", FieldType = System.Data.DbType.DateTime, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Fecha", IsFilterable = false)]
		public DateTime? LogsFecha
		{
			get { return datLogsFecha; }
			set { datLogsFecha=value; }
		}
	}
}
