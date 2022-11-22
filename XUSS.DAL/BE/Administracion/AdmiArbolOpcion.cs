using System;
using Mapping;

namespace BE.Administracion
{
	[Table(LogicalName = "AdmiArbolOpcion", PhysicalName = "admi_tarbolOpcion", EntityId = 11, NumberColumns = 8)]
	[Serializable]
	public class AdmiArbolOpcion
	{
		private int intAropArbolOpcion;
		private int? intSistSistema;
		private int? intModuModulo;
		private int? intAropIdOriginal;
		private int? intAropIdUnicoPadre;
		private string srtAropEntidad;
		private string srtAropNombre;
		private bool blnIsChecked;
		public AdmiArbolOpcion(int aropArbolOpcion, int? sistSistema, int? moduModulo, int? aropIdOriginal, int? aropIdUnicoPadre, string aropEntidad, string aropNombre, bool isChecked)
		{
			this.intAropArbolOpcion = aropArbolOpcion;
			this.intSistSistema = sistSistema;
			this.intModuModulo = moduModulo;
			this.intAropIdOriginal = aropIdOriginal;
			this.intAropIdUnicoPadre = aropIdUnicoPadre;
			this.srtAropEntidad = aropEntidad;
			this.srtAropNombre = aropNombre;
			this.blnIsChecked = isChecked;
		}
		public AdmiArbolOpcion()
		{
		}
		[Field(LogicalName = "AropArbolOpcion", PhysicalName = "arop_arbolOpcion", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = true, IsDiscriminant = false, Alias = "ArbolOpcion")]
		public int AropArbolOpcion
		{
			get { return intAropArbolOpcion; }
			set { intAropArbolOpcion = value; }
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
		[Field(LogicalName = "AropIdOriginal", PhysicalName = "arop_idOriginal", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "IdOriginal")]
		public int? AropIdOriginal
		{
			get { return intAropIdOriginal; }
			set { intAropIdOriginal = value; }
		}
		[Field(LogicalName = "AropIdUnicoPadre", PhysicalName = "arop_idUnicoPadre", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "IdUnicoPadre")]
		public int? AropIdUnicoPadre
		{
			get { return intAropIdUnicoPadre; }
			set { intAropIdUnicoPadre = value; }
		}
		[Field(LogicalName = "AropEntidad", PhysicalName = "arop_entidad", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Entidad")]
		public string AropEntidad
		{
			get { return srtAropEntidad; }
			set { srtAropEntidad = value; }
		}
		[Field(LogicalName = "AropNombre", PhysicalName = "arop_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Nombre")]
		public string AropNombre
		{
			get { return srtAropNombre; }
			set { srtAropNombre = value; }
		}
		[Field(LogicalName = "IsChecked", PhysicalName = "checked", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Seleccionado")]
		public bool IsChecked
		{
			get { return blnIsChecked; }
			set { blnIsChecked = value; }
		}
	}
}