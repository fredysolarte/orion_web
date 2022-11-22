using System;
using Mapping;

namespace BE.Administracion
{
	[Table(LogicalName = "AdmiBlob", PhysicalName = "admi_tblob", EntityId = 7, NumberColumns = 9)]
	public class AdmiBlob
	{
		private int intBlobBlob;
		private int? intTipbTipoblob;
		private string srtBlobNombre;
		private string srtBlobOrigen;
		private string srtBlobDescripcion;
		private byte[] strBlobBinario;
		private string srtBlobVersion;
		private int? intLogsUsuario;
		private DateTime? datLogsFecha;
		public AdmiBlob(int blobBlob, int? tipbTipoblob, string blobNombre, string blobOrigen, string blobDescripcion, byte[] blobBinario, string blobVersion, int? logsUsuario, DateTime? logsFecha)
		{
			this.intBlobBlob = blobBlob;
			this.intTipbTipoblob = tipbTipoblob;
			this.srtBlobNombre = blobNombre;
			this.srtBlobOrigen = blobOrigen;
			this.srtBlobDescripcion = blobDescripcion;
			this.strBlobBinario = blobBinario;
			this.srtBlobVersion = blobVersion;
			this.intLogsUsuario = logsUsuario;
			this.datLogsFecha = logsFecha;
		}
		public AdmiBlob()
		{
		}
		[Field(LogicalName = "BlobBlob", PhysicalName = "blob_blob", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = true, IsDiscriminant = false, Alias = "Blob")]
		public int BlobBlob
		{
			get { return intBlobBlob; }
			set { intBlobBlob = value; }
		}
		[Field(LogicalName = "TipbTipoblob", PhysicalName = "tipb_tipoblob", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Tipoblob")]
		public int? TipbTipoblob
		{
			get { return intTipbTipoblob; }
			set { intTipbTipoblob = value; }
		}
		[Field(LogicalName = "BlobNombre", PhysicalName = "blob_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Nombre")]
		public string BlobNombre
		{
			get { return srtBlobNombre; }
			set { srtBlobNombre = value; }
		}
		[Field(LogicalName = "BlobOrigen", PhysicalName = "blob_origen", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Origen")]
		public string BlobOrigen
		{
			get { return srtBlobOrigen; }
			set { srtBlobOrigen = value; }
		}
		[Field(LogicalName = "BlobDescripcion", PhysicalName = "blob_descripcion", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Descripcion")]
		public string BlobDescripcion
		{
			get { return srtBlobDescripcion; }
			set { srtBlobDescripcion = value; }
		}
		[Field(LogicalName = "BlobBinario", PhysicalName = "blob_binario", FieldType = System.Data.DbType.Binary, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Binario")]
		public byte[] BlobBinario
		{
			get { return strBlobBinario; }
			set { strBlobBinario = value; }
		}
		[Field(LogicalName = "BlobVersion", PhysicalName = "blob_version", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Version")]
		public string BlobVersion
		{
			get { return srtBlobVersion; }
			set { srtBlobVersion = value; }
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