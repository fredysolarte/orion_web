using System;
using Mapping;

namespace BE.Administracion
{
	[Table(LogicalName = "AdmiOpcion", PhysicalName = "admi_topcion", EntityId = 8, NumberColumns = 17)]
	public class AdmiOpcion
	{
		private int intOpciOpcion;
		private int intSistSistema;
		private int? intModuModulo;
		private int? intIconIcono;
		private int? intBlobAyuda;
		private string srtOpciNombre;
		private string srtOpciEtiqueta;
		private int? intParaClase2;
		private string srtOpciComando;
		private int? intOpciPadre;
		private int? intOpciOrden;
		private string srtOpciHint;
		private int? intOpciEstado;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		private string strNombreAyuda;
		private string strNombreIcono;
		private int? intPrincipal;
        private string strNombreRuta;
        private int? intopci_reporte;
        private string strformformulario;
        public AdmiOpcion(int opciOpcion, int sistSistema, int? moduModulo, int? iconIcono, int? blobAyuda, string opciNombre, string opciEtiqueta, int? paraClase2, string opciComando, int? opciPadre, int? opciOrden, string opciHint, int? opciEstado, int? logsUsuario, DateTime? logsFecha, string nombreAyuda, string nombreIcono, int? opciPrincipal, string NombreRuta,int? opciReporte,string formformulario)
		{
			this.intOpciOpcion = opciOpcion;
			this.intSistSistema = sistSistema;
			this.intModuModulo = moduModulo;
			this.intIconIcono = iconIcono;
			this.intBlobAyuda = blobAyuda;
			this.srtOpciNombre = opciNombre;
			this.srtOpciEtiqueta = opciEtiqueta;
			this.intParaClase2 = paraClase2;
			this.srtOpciComando = opciComando;
			this.intOpciPadre = opciPadre;
			this.intOpciOrden = opciOrden;
			this.srtOpciHint = opciHint;
			this.intOpciEstado = opciEstado;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
			this.strNombreAyuda = nombreAyuda;
			this.strNombreIcono = nombreIcono;
			this.intPrincipal = opciPrincipal;
            this.strNombreRuta = NombreRuta;
            this.intopci_reporte = opciReporte;
            this.strformformulario = formformulario;
		}
		public AdmiOpcion()
		{
		}

		[Field(LogicalName = "OpciPrincipal", PhysicalName = "opci_principal", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Principal")]
		public bool OpciPrincipal
		{
			get { return intPrincipal == 1; }
			set { intPrincipal = value ? 1 : 0; }
		}

		[Field(LogicalName = "OpciOpcion", PhysicalName = "opci_opcion", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = true, IsDiscriminant = false, Alias = "Opcion")]
		public int OpciOpcion
		{
			get { return intOpciOpcion; }
			set { intOpciOpcion = value; }
		}
		[Field(LogicalName = "SistSistema", PhysicalName = "sist_sistema", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Sistema", IsFilterable = true)]
		public int SistSistema
		{
			get { return intSistSistema; }
			set { intSistSistema = value; }
		}
		[Field(LogicalName = "ModuModulo", PhysicalName = "modu_modulo", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Módulo", IsFilterable = true)]
		public int? ModuModulo
		{
			get { return intModuModulo; }
			set { intModuModulo = value; }
		}
		[Field(LogicalName = "OpciNombre", PhysicalName = "opci_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Nombre", IsFilterable = true)]
		public string OpciNombre
		{
			get { return srtOpciNombre; }
			set { srtOpciNombre = value; }
		}
		[Field(LogicalName = "OpciEtiqueta", PhysicalName = "opci_etiqueta", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Etiqueta", IsFilterable = true)]
		public string OpciEtiqueta
		{
			get { return srtOpciEtiqueta; }
			set { srtOpciEtiqueta = value; }
		}
		[Field(LogicalName = "OpciComando", PhysicalName = "opci_comando", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Comando", IsFilterable = true)]
		public string OpciComando
		{
			get { return srtOpciComando; }
			set { srtOpciComando = value; }
		}
		[Field(LogicalName = "OpciOrden", PhysicalName = "opci_orden", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Orden", IsFilterable = true)]
		public int? OpciOrden
		{
			get { return intOpciOrden; }
			set { intOpciOrden = value; }
		}
		[Field(LogicalName = "OpciHint", PhysicalName = "opci_hint", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Hint", IsFilterable = true)]
		public string OpciHint
		{
			get { return srtOpciHint; }
			set { srtOpciHint = value; }
		}
		[Field(LogicalName = "ParaClase2", PhysicalName = "para_clase2", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Tipo", IsFilterable = true)]
		public int? ParaClase2
		{
			get { return intParaClase2; }
			set { intParaClase2 = value; }
		}
		[Field(LogicalName = "BlobAyuda", PhysicalName = "blob_ayuda", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Ayuda", IsFilterable = true)]
		public int? BlobAyuda
		{
			get { return intBlobAyuda; }
			set { intBlobAyuda = value; }
		}
		[Field(LogicalName = "IconIcono", PhysicalName = "icon_icono", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Icono", IsFilterable = true)]
		public int? IconIcono
		{
			get { return intIconIcono; }
			set { intIconIcono = value; }
		}
		[Field(LogicalName = "OpciPadre", PhysicalName = "opci_padre", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Padre", IsFilterable = true)]
		public int? OpciPadre
		{
			get { return intOpciPadre; }
			set { intOpciPadre = value; }
		}
		[Field(LogicalName = "OpciEstado", PhysicalName = "opci_estado", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Estado")]
		public bool OpciEstado
		{
			get { return intOpciEstado == 1; }
			set { intOpciEstado = value ? 1 : 0; }
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
        [Field(LogicalName = "NombreRuta", PhysicalName = "form_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Comando", IsFilterable = true)]
        public string NombreRuta
        {
            get { return strNombreRuta; }
            set { strNombreRuta = value; }
        }

        [Field(LogicalName = "opciReporte", PhysicalName = "opci_reporte", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Reporte")]
        public int? opciReporte
        {
            get { return intopci_reporte ; }
            set { intopci_reporte = value; }
        }
        [Field(LogicalName = "formformulario", PhysicalName = "form_formulario", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Comando", IsFilterable = true)]
        public string formformulario
        {
            get { return strformformulario; }
            set { strformformulario = value; }
        }
	}
}