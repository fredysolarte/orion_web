using Mapping;

namespace BE.Administracion
{
	[Table(LogicalName = "AdmiTipoblob", PhysicalName = "admi_ttipoblob", EntityId = 10, NumberColumns = 4)]
	public class AdmiTipoblob
	{
		private int intTipbTipoblob;
		private string srtTipbNombre;
		private string srtTipbFiltro;
		private string srtTipbEditor;
		public AdmiTipoblob(int tipbTipoblob, string tipbNombre, string tipbFiltro, string tipbEditor)
		{
			this.intTipbTipoblob = tipbTipoblob;
			this.srtTipbNombre = tipbNombre;
			this.srtTipbFiltro = tipbFiltro;
			this.srtTipbEditor = tipbEditor;
		}
		public AdmiTipoblob()
		{
		}
		[Field(LogicalName = "TipbTipoblob", PhysicalName = "tipb_tipoblob", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = true, IsDiscriminant = false, Alias = "Tipoblob")]
		public int TipbTipoblob
		{
			get { return intTipbTipoblob; }
			set { intTipbTipoblob = value; }
		}
		[Field(LogicalName = "TipbNombre", PhysicalName = "tipb_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Nombre")]
		public string TipbNombre
		{
			get { return srtTipbNombre; }
			set { srtTipbNombre = value; }
		}
		[Field(LogicalName = "TipbFiltro", PhysicalName = "tipb_filtro", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Filtro")]
		public string TipbFiltro
		{
			get { return srtTipbFiltro; }
			set { srtTipbFiltro = value; }
		}
		[Field(LogicalName = "TipbEditor", PhysicalName = "tipb_editor", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Editor")]
		public string TipbEditor
		{
			get { return srtTipbEditor; }
			set { srtTipbEditor = value; }
		}
	}
}