using System;
using Mapping;

namespace BE.Administracion
{
	[Serializable]
	[Table(LogicalName = "AdmiParametro", PhysicalName = "admi_tparametro", EntityId = 16, NumberColumns = 9)]
	public class AdmiParametro
	{
		private int intParaParametro;
		private int intClasClase;
		private string srtParaNombre;
		private string srtParaDescripcion;
		private string srtParaValor;
		private int? intParaEstado;
		private int? intParaEditable;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		public AdmiParametro(int paraParametro, int clasClase, string paraNombre, string paraDescripcion, string paraValor, int? paraEstado, int? paraEditable, int? logsUsuario, DateTime? logsFecha)
		{
			this.intParaParametro = paraParametro;
			this.intClasClase = clasClase;
			this.srtParaNombre = paraNombre;
			this.srtParaDescripcion = paraDescripcion;
			this.srtParaValor = paraValor;
			this.intParaEstado = paraEstado;
			this.intParaEditable = paraEditable;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
		}
		public AdmiParametro()
		{
		}
		[Field(LogicalName = "ParaParametro", PhysicalName = "para_parametro", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias = "Parametro")]
		public int ParaParametro
		{
			get { return intParaParametro; }
			set { intParaParametro = value; }
		}
		[Field(LogicalName = "ClasClase", PhysicalName = "clas_clase", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = false, IsDiscriminant = true, Alias = "Clase")]
		public int ClasClase
		{
			get { return intClasClase; }
			set { intClasClase = value; }
		}
		[Field(LogicalName = "ParaNombre", PhysicalName = "para_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Nombre", IsFilterable = true)]
		public string ParaNombre
		{
			get { return srtParaNombre; }
			set { srtParaNombre = value; }
		}
		[Field(LogicalName = "ParaDescripcion", PhysicalName = "para_descripcion", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Descripcion", IsFilterable = true)]
		public string ParaDescripcion
		{
			get { return srtParaDescripcion; }
			set { srtParaDescripcion = value; }
		}
		[Field(LogicalName = "ParaValor", PhysicalName = "para_valor", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Valor", IsFilterable = true)]
		public string ParaValor
		{
			get { return srtParaValor; }
			set { srtParaValor = value; }
		}
		[Field(LogicalName = "ParaEstado", PhysicalName = "para_estado", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Estado", IsFilterable = true)]
		public bool ParaEstado
		{
			get { return intParaEstado == 1; }
			set { intParaEstado = value ? 1 : 0; }
		}
		[Field(LogicalName = "ParaEditable", PhysicalName = "para_editable", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Editable")]
		public bool ParaEditable
		{
			get { return intParaEditable == 1; }
			set { intParaEditable = value ? 1 : 0; }
		}
		[Field(LogicalName = "LogsUsuario", PhysicalName = "logs_usuario", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Usuario")]
		public int? LogsUsuario
		{
			get { return intLogsUsuario; }
			set { intLogsUsuario = value; }
		}
		[Field(LogicalName = "LogsFecha", PhysicalName = "logs_fecha", FieldType = System.Data.DbType.DateTime, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Fecha")]
		public DateTime? LogsFecha
		{
			get { return datLogsFecha; }
			set { datLogsFecha = value; }
		}
	}
}