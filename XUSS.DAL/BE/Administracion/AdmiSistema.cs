using Mapping;

namespace BE.Administracion
{
	[Table(LogicalName = "AdmiSistema", PhysicalName = "admi_tsistema", EntityId = 4, NumberColumns = 6)]
	public class AdmiSistema
	{
		private int intSistSistema;
		private string srtSistNombre;
		private string srtSistDescripcion;
		private int? intIconLogo;
		private string srtSistIdentifica;
		private string srtBlobNombre;
		private string sis_url;
		public AdmiSistema(int sistSistema, string sistNombre, string sistDescripcion, int? iconLogo, string sistIdentifica, string blobNombre, string pSis_Url)
		{
			this.intSistSistema = sistSistema;
			this.srtSistNombre = sistNombre;
			this.srtSistDescripcion = sistDescripcion;
			this.intIconLogo = iconLogo;
			this.srtSistIdentifica = sistIdentifica;
			this.srtBlobNombre = blobNombre;
			this.sis_url = pSis_Url;
		}
		public AdmiSistema()
		{
		}

        //[Field(LogicalName = "SistemaUrl", PhysicalName = "sistema_url", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "SistemaUrl")]
        //public string SistemaUrl
        //{
        //    get { return sis_url; }
        //    set { sis_url = value; }
        //}
		[Field(LogicalName = "SistSistema", PhysicalName = "sist_sistema", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = true, IsDiscriminant = false, Alias = "Sistema", IsFilterable = true)]
		public int SistSistema
		{
			get { return intSistSistema; }
			set { intSistSistema = value; }
		}
		[Field(LogicalName = "SistNombre", PhysicalName = "sist_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Nombre", IsFilterable = true)]
		public string SistNombre
		{
			get { return srtSistNombre; }
			set { srtSistNombre = value; }
		}
		[Field(LogicalName = "SistDescripcion", PhysicalName = "sist_descripcion", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Descripción", IsFilterable = true)]
		public string SistDescripcion
		{
			get { return srtSistDescripcion; }
			set { srtSistDescripcion = value; }
		}
		[Field(LogicalName = "IconLogo", PhysicalName = "icon_logo", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Logo")]
		public int? IconLogo
		{
			get { return intIconLogo; }
			set { intIconLogo = value; }
		}
		[Field(LogicalName = "SistIdentifica", PhysicalName = "sist_identifica", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Identificación", IsFilterable = true)]
		public string SistIdentifica
		{
			get { return srtSistIdentifica; }
			set { srtSistIdentifica = value; }
		}

        //[Field(LogicalName = "BlobNombre", PhysicalName = "blob_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "BlobNombre")]
        //public string BlobNombre
        //{
        //    get { return srtBlobNombre; }
        //    set { srtBlobNombre = value; }
        //}
	}
}