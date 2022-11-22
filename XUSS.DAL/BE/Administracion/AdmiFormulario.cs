using System;
using Mapping;

namespace BE.Administracion
{

	[Table(LogicalName = "AdmiFormulario", PhysicalName = "admi_tformulario", EntityId = 5, NumberColumns = 10)]
	public class AdmiFormulario
	{
		private int intFormFormulario;
		private int? intBlobAyuda;
		private string srtFormNombre;
		private string srtFormDescripcion;
		private string srtFormTablabase;
		private int? intFormEstado;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		private string strBlobNombre;
		private int intOpciOpcion;
		private int? intSistSistema;
		private int? intModuModulo;
		public AdmiFormulario(int formFormulario, int? blobAyuda, string formNombre, string formDescripcion, string formTablabase, int? formEstado, int? logsUsuario, DateTime? logsFecha, string blobNombre, int opciOpcion, int? sistSistema, int? moduModulo)
		{
			this.intFormFormulario = formFormulario;
			this.intBlobAyuda = blobAyuda;
			this.srtFormNombre = formNombre;
			this.srtFormDescripcion = formDescripcion;
			this.srtFormTablabase = formTablabase;
			this.intFormEstado = formEstado;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
			this.strBlobNombre = blobNombre;
			this.intOpciOpcion = opciOpcion;
			this.intSistSistema = sistSistema;
			this.intModuModulo = moduModulo;
		}
		public AdmiFormulario()
		{
		}
		[Field(LogicalName = "FormFormulario", PhysicalName = "form_formulario", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = true, IsDiscriminant = false, Alias = "Formulario")]
		public int FormFormulario
		{
			get { return intFormFormulario; }
			set { intFormFormulario = value; }
		}
        [Field(LogicalName = "BlobAyuda", PhysicalName = "blob_ayuda", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Ayuda")]
        public int? BlobAyuda
        {
            get { return intBlobAyuda; }
            set { intBlobAyuda = value; }
        }
		[Field(LogicalName = "FormNombre", PhysicalName = "form_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Nombre", IsFilterable = true)]
		public string FormNombre
		{
			get { return srtFormNombre; }
			set { srtFormNombre = value; }
		}
		[Field(LogicalName = "FormDescripcion", PhysicalName = "form_descripcion", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Descripcion", IsFilterable = true)]
		public string FormDescripcion
		{
			get { return srtFormDescripcion; }
			set { srtFormDescripcion = value; }
		}
		[Field(LogicalName = "FormTablabase", PhysicalName = "form_tablabase", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Tablabase", IsFilterable = true)]
		public string FormTablabase
		{
			get { return srtFormTablabase; }
			set { srtFormTablabase = value; }
		}
		[Field(LogicalName = "FormEstado", PhysicalName = "form_estado", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Estado")]
		public bool FormEstado
		{
			get { return intFormEstado == 1; }
			set { intFormEstado = value ? 1 : 0; }
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
        [Field(LogicalName = "BlobNombre", PhysicalName = "blob_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "NombreBlob")]
        public string BlobNombre
        {
            get { return strBlobNombre; }
            set { strBlobNombre = value; }
        }
		[Field(LogicalName = "OpciOpcion", PhysicalName = "opci_opcion", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Formulario", IsFilterable = true)]
		public int OpciOpcion
		{
			get { return intOpciOpcion; }
			set { intOpciOpcion = value; }
		}

		[Field(LogicalName = "SistSistema", PhysicalName = "sist_sistema", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Sistema")]
		public int? SistSistema
		{
			get { return intSistSistema; }
			set { intSistSistema = value; }
		}
		[Field(LogicalName = "ModuModulo", PhysicalName = "modu_modulo", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Modulo")]
		public int? ModuModulo
		{
			get { return intModuModulo; }
			set { intModuModulo = value; }
		}
	}
}