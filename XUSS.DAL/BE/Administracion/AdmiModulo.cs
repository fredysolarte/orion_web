using System;
using Mapping;

namespace BE.Administracion
{
	[Table(LogicalName = "AdmiModulo", PhysicalName = "admi_tmodulo", EntityId = 19, NumberColumns = 14)]
	public class AdmiModulo
	{
		private int intSistSistema;
		private int intModuModulo;
		private string srtModuNombre;
		private string srtModuDescripcion;
		private int? intBlobAyuda;
		private string srtModuParametros;
		private int? intIconIcono;
		private int? intModuEstado;
		private int? intModuApp;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		private string strNombreAyuda;
		private string strNombreIcono;
		private int? intModuOrden;
        private string strFormulario;
        public AdmiModulo(int sistSistema, int moduModulo, string moduNombre, string moduDescripcion, int? blobAyuda, string moduParametros, int? iconIcono, int? moduEstado, int? logsUsuario, DateTime? logsFecha, string nombreAyuda, string nombreIcono, int? moduOrden, string Formulario, int? moduApp)
		{
			this.intSistSistema = sistSistema;
			this.intModuModulo = moduModulo;
			this.srtModuNombre = moduNombre;
			this.srtModuDescripcion = moduDescripcion;
			this.intBlobAyuda = blobAyuda;
			this.srtModuParametros = moduParametros;
			this.intIconIcono = iconIcono;
			this.intModuEstado = moduEstado;
			this.intModuApp = moduApp;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
			this.strNombreAyuda = nombreAyuda;
			this.strNombreIcono = nombreIcono;
			this.intModuOrden = moduOrden;
            this.strFormulario = Formulario;
		}
		public AdmiModulo()
		{
		}
		[Field(LogicalName = "SistSistema", PhysicalName = "sist_sistema", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias = "Sistema", IsFilterable = true)]
		public int SistSistema
		{
			get { return intSistSistema; }
			set { intSistSistema = value; }
		}
		[Field(LogicalName = "ModuModulo", PhysicalName = "modu_modulo", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = true, IsDiscriminant = false, Alias = "M�dulo", IsFilterable = false)]
		public int ModuModulo
		{
			get { return intModuModulo; }
			set { intModuModulo = value; }
		}
		[Field(LogicalName = "ModuNombre", PhysicalName = "modu_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Nombre", IsFilterable = true)]
		public string ModuNombre
		{
			get { return srtModuNombre; }
			set { srtModuNombre = value; }
		}
		[Field(LogicalName = "ModuDescripcion", PhysicalName = "modu_descripcion", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Descripci�n", IsFilterable = true)]
		public string ModuDescripcion
		{
			get { return srtModuDescripcion; }
			set { srtModuDescripcion = value; }
		}
		[Field(LogicalName = "BlobAyuda", PhysicalName = "blob_ayuda", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Ayuda")]
		public int? BlobAyuda
		{
			get { return intBlobAyuda; }
			set { intBlobAyuda = value; }
		}
		[Field(LogicalName = "ModuParametros", PhysicalName = "modu_parametros", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "M�dulos Par�metros", IsFilterable = true)]
		public string ModuParametros
		{
			get { return srtModuParametros; }
			set { srtModuParametros = value; }
		}
		[Field(LogicalName = "IconIcono", PhysicalName = "icon_icono", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Icono")]
		public int? IconIcono
		{
			get { return intIconIcono; }
			set { intIconIcono = value; }
		}
		[Field(LogicalName = "ModuEstado", PhysicalName = "modu_estado", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Estado")]
		public bool ModuEstado
		{
			get { return intModuEstado == 1; }
			set { intModuEstado = value ? 1 : 0; }
		}
		[Field(LogicalName = "ModuApp", PhysicalName = "modu_app", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "App")]
		public bool ModuApp
		{
			get { return intModuApp == 1; }
			set { intModuApp = value ? 1 : 0; }
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
        //[Field(LogicalName = "NombreAyuda", PhysicalName = "nombre_ayuda", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Fecha")]
        //public string NombreAyuda
        //{
        //    get { return strNombreAyuda; }
        //    set { strNombreAyuda = value; }
        //}

        //[Field(LogicalName = "NombreIcono", PhysicalName = "nombre_icono", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Fecha")]
        //public string NombreIcono
        //{
        //    get { return strNombreIcono; }
        //    set { strNombreIcono = value; }
        //}

		[Field(LogicalName = "ModuOrden", PhysicalName = "modu_orden", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Orden")]
		public int? ModuOrden
		{
			get { return intModuOrden; }
			set { intModuOrden = value; }
		}

        [Field(LogicalName = "Formulario", PhysicalName = "form_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Formulario")]
        public string Formulario
        {
            get { return strFormulario; }
            set { strFormulario = value; }
        }
	}
}