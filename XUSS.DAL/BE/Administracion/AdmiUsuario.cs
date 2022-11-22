using System;
using Mapping;

namespace BE.Administracion
{
	[Table(LogicalName = "AdmiUsuario", PhysicalName = "admi_tusuario", EntityId = 14, NumberColumns = 23)]
	[Serializable]
	public class AdmiUsuario
	{
		private string srtUsuaUsuario;
		private int? intUsuaSecuencia;
		private string srtUsuaIdentifica;
		private string srtUsuaNombres;
		private string srtUsuaDireccion;
		private string srtUsuaTelefonos;
		private string srtUsuaEmail;
		private string srtUsuaClave;
		private DateTime datUsuaFechavence;
		private int? intUsuaEncripta;
		private int? intUsuaClavecaduca;
		private int? intUsuaPublicareporte;
		private int? intUsuaRestringeip;
		private int? intUsuaRestringehora;
		private DateTime? datUsuaUltimoacceso;
		private string srtUsuaUltimoequipo;
		private string srtUsuaUltimaip;
		private int? intUsuaCambiaClave;
		private int intUsuaFallos;
		private int? intUsuaEstado;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
        private int? intusuaadministrador;
        public AdmiUsuario(string usuaUsuario, int? usuaSecuencia, string usuaIdentifica, string usuaNombres, string usuaDireccion, string usuaTelefonos, string usuaEmail, string usuaClave, DateTime usuaFechavence, int? usuaEncripta, int? usuaClavecaduca, int? usuaPublicareporte, int? usuaRestringeip, int? usuaRestringehora, DateTime? usuaUltimoacceso, string usuaUltimoequipo, int? usuaCambiaClave, int usuaFallos, string usuaUltimaip, int? usuaEstado, int? logsUsuario, DateTime? logsFecha, int? usuaadministrador)
		{
			this.srtUsuaUsuario = usuaUsuario;
			this.intUsuaSecuencia = usuaSecuencia;
			this.srtUsuaIdentifica = usuaIdentifica;
			this.srtUsuaNombres = usuaNombres;
			this.srtUsuaDireccion = usuaDireccion;
			this.srtUsuaTelefonos = usuaTelefonos;
			this.srtUsuaEmail = usuaEmail;
			this.srtUsuaClave = usuaClave;
			this.datUsuaFechavence = usuaFechavence;
			this.intUsuaEncripta = usuaEncripta;
			this.intUsuaClavecaduca = usuaClavecaduca;
			this.intUsuaPublicareporte = usuaPublicareporte;
			this.intUsuaRestringeip = usuaRestringeip;
			this.intUsuaRestringehora = usuaRestringehora;
			this.datUsuaUltimoacceso = usuaUltimoacceso;
			this.srtUsuaUltimoequipo = usuaUltimoequipo;
			this.intUsuaCambiaClave = usuaCambiaClave;
			this.intUsuaFallos = usuaFallos;
			this.srtUsuaUltimaip = usuaUltimaip;
			this.intUsuaEstado = usuaEstado;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
            this.intusuaadministrador = usuaadministrador;
		}
		public AdmiUsuario()
		{
		}
		[Field(LogicalName = "UsuaUsuario", PhysicalName = "usua_usuario", FieldType = System.Data.DbType.String, PrimaryKey = true, IsSequence = false, IsDiscriminant = false, Alias = "Usuario", IsFilterable = true)]
		public string UsuaUsuario
		{
			get { return srtUsuaUsuario; }
			set { srtUsuaUsuario = value; }
		}
		[Field(LogicalName = "UsuaSecuencia", PhysicalName = "usua_secuencia", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = true, IsDiscriminant = false, Alias = "Secuencia", IsFilterable = false)]
		public int? UsuaSecuencia
		{
			get { return intUsuaSecuencia; }
			set { intUsuaSecuencia = value; }
		}
		[Field(LogicalName = "UsuaIdentifica", PhysicalName = "usua_identifica", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Identificacion", IsFilterable = true)]
		public string UsuaIdentifica
		{
			get { return srtUsuaIdentifica; }
			set { srtUsuaIdentifica = value; }
		}
		[Field(LogicalName = "UsuaNombres", PhysicalName = "usua_nombres", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Nombres", IsFilterable = true)]
		public string UsuaNombres
		{
			get { return srtUsuaNombres; }
			set { srtUsuaNombres = value; }
		}
		[Field(LogicalName = "UsuaDireccion", PhysicalName = "usua_direccion", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Direccion", IsFilterable = false)]
		public string UsuaDireccion
		{
			get { return srtUsuaDireccion; }
			set { srtUsuaDireccion = value; }
		}
		[Field(LogicalName = "UsuaTelefonos", PhysicalName = "usua_telefonos", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Telefonos", IsFilterable = false)]
		public string UsuaTelefonos
		{
			get { return srtUsuaTelefonos; }
			set { srtUsuaTelefonos = value; }
		}
		[Field(LogicalName = "UsuaEmail", PhysicalName = "usua_email", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Email", IsFilterable = true)]
		public string UsuaEmail
		{
			get { return srtUsuaEmail; }
			set { srtUsuaEmail = value; }
		}
		[Field(LogicalName = "UsuaClave", PhysicalName = "usua_clave", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Clave", IsFilterable = false)]
		public string UsuaClave
		{
			get { return srtUsuaClave; }
			set { srtUsuaClave = value; }
		}
		[Field(LogicalName = "UsuaFechavence", PhysicalName = "usua_fechavence", FieldType = System.Data.DbType.DateTime, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Fechavence", IsFilterable = false)]
		public DateTime UsuaFechavence
		{
			get { return datUsuaFechavence; }
			set { datUsuaFechavence = value; }
		}
		[Field(LogicalName = "UsuaEncripta", PhysicalName = "usua_encripta", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Encripta", IsFilterable = false)]
		public bool UsuaEncripta
		{
			get { return intUsuaEncripta == 1; }
			set { intUsuaEncripta = value ? 1 : 0; }
		}
		[Field(LogicalName = "UsuaClavecaduca", PhysicalName = "usua_clavecaduca", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Clavecaduca")]
		public bool UsuaClavecaduca
		{
			get { return intUsuaClavecaduca == 1; }
			set { intUsuaClavecaduca = value ? 1 : 0; }
		}
		[Field(LogicalName = "UsuaPublicareporte", PhysicalName = "usua_publicareporte", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Publicareporte", IsFilterable = false)]
		public bool UsuaPublicareporte
		{
			get { return intUsuaPublicareporte == 1; }
			set { intUsuaPublicareporte = value ? 1 : 0; }
		}
		[Field(LogicalName = "UsuaRestringeip", PhysicalName = "usua_restringeip", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Restringeip", IsFilterable = false)]
		public bool UsuaRestringeip
		{
			get { return intUsuaRestringeip == 1; }
			set { intUsuaRestringeip = value ? 1 : 0; }
		}
		[Field(LogicalName = "UsuaRestringehora", PhysicalName = "usua_restringehora", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Restringehora", IsFilterable = false)]
		public bool UsuaRestringehora
		{
			get { return intUsuaRestringehora == 1; }
			set { intUsuaRestringehora = value ? 1 : 0; }
		}
		[Field(LogicalName = "UsuaUltimoacceso", PhysicalName = "usua_ultimoacceso", FieldType = System.Data.DbType.DateTime, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Ultimoacceso", IsFilterable = false)]
		public DateTime? UsuaUltimoacceso
		{
			get { return datUsuaUltimoacceso; }
			set { datUsuaUltimoacceso = value; }
		}
		[Field(LogicalName = "UsuaUltimoequipo", PhysicalName = "usua_ultimoequipo", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Ultimoequipo", IsFilterable = false)]
		public string UsuaUltimoequipo
		{
			get { return srtUsuaUltimoequipo; }
			set { srtUsuaUltimoequipo = value; }
		}
		[Field(LogicalName = "UsuaCambiaClave", PhysicalName = "usua_cambiaclave", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Cambiar Contraseña", IsFilterable = false)]
		public bool UsuaCambiaClave
		{
			get { return intUsuaCambiaClave == 1; }
			set { intUsuaCambiaClave = value ? 1 : 0; }
		}
		[Field(LogicalName = "UsuaFallos", PhysicalName = "usua_fallos", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Fallos", IsFilterable = false)]
		public int UsuaFallos
		{
			get { return intUsuaFallos; }
			set { intUsuaFallos = value; }
		}
		[Field(LogicalName = "UsuaUltimaip", PhysicalName = "usua_ultimaip", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Ultimaip", IsFilterable = false)]
		public string UsuaUltimaip
		{
			get { return srtUsuaUltimaip; }
			set { srtUsuaUltimaip = value; }
		}
		[Field(LogicalName = "UsuaEstado", PhysicalName = "usua_estado", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Estado", IsFilterable = false)]
		public bool UsuaEstado
		{
			get { return intUsuaEstado == 1; }
			set { intUsuaEstado = value ? 1 : 0; }
		}
		[Field(LogicalName = "LogsUsuario", PhysicalName = "logs_usuario", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Usuario", IsFilterable = false)]
		public int? LogsUsuario
		{
			get { return intLogsUsuario; }
			set { intLogsUsuario = value; }
		}
		[Field(LogicalName = "LogsFecha", PhysicalName = "logs_fecha", FieldType = System.Data.DbType.DateTime, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Fecha", IsFilterable = false)]
		public DateTime? LogsFecha
		{
			get { return datLogsFecha; }
			set { datLogsFecha = value; }
		}
        [Field(LogicalName = "usuaadministrador", PhysicalName = "usua_administrador", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Administrador", IsFilterable = false)]
        public bool usuaadministrador
        {
            get { return intusuaadministrador == 1; }
            set { intusuaadministrador = value ? 1 : 0; }
        }
	}
}