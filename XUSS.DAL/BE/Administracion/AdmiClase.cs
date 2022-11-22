using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mapping;

namespace BE.Administracion
{
	[Serializable]
	[Table(LogicalName = "AdmiClase", PhysicalName = "admi_tclase", EntityId = 1, NumberColumns = 6)]
	public class AdmiClase
	{
		private int intClasClase;
		private string srtClasNombre;
		private string srtClasDescripcion;
		private int? intClasEditable;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		public AdmiClase(int clasClase,string clasNombre,string clasDescripcion,int? clasEditable,int? logsUsuario,DateTime? logsFecha)
		{
			this.intClasClase = clasClase;
			this.srtClasNombre = clasNombre;
			this.srtClasDescripcion = clasDescripcion;
			this.intClasEditable = clasEditable;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
		}
		public AdmiClase()
		{
		}
		[Field(LogicalName = "ClasClase", PhysicalName = "clas_clase", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = true, IsDiscriminant = false, Alias="Clase")]
		public int ClasClase
		{
			get { return intClasClase; }
			set { intClasClase=value; }
		}
		[Field(LogicalName = "ClasNombre", PhysicalName = "clas_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias="Nombre",IsFilterable=true)]
		public string ClasNombre
		{
			get { return srtClasNombre; }
			set { srtClasNombre=value; }
		}
		[Field(LogicalName = "ClasDescripcion", PhysicalName = "clas_descripcion", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias="Descripcion",IsFilterable=true)]
		public string ClasDescripcion
		{
			get { return srtClasDescripcion; }
			set { srtClasDescripcion=value; }
		}
		[Field(LogicalName = "ClasEditable", PhysicalName = "clas_editable", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias="Editable")]
        public bool ClasEditable
		{
			get { return intClasEditable == 1; }
			set { intClasEditable=value ? 1 : 0; }
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
