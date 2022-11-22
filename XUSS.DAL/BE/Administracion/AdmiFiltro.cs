using System;
using Mapping;

namespace BE.Administracion
{
	[Serializable]
	[Table(LogicalName = "AdmiFiltro", PhysicalName = "admi_tfiltro", EntityId = 50, NumberColumns = 5)]
	public class AdmiFiltro
	{
		private int intFiltFiltro;
		private int? intFormFormulario;
		private string srtFiltSql;
		private string strFiltPadre;
		private string srtFiltLogicalname;
		private string srtFiltNombre;
		public AdmiFiltro(int filtFiltro, int? formFormulario, string filtSql, string filtPadre, string filtLogicalname, string filtNombre)
		{
			this.intFiltFiltro = filtFiltro;
			this.intFormFormulario = formFormulario;
			this.srtFiltSql = filtSql;
			this.strFiltPadre = filtPadre;
			this.srtFiltLogicalname = filtLogicalname;
			this.srtFiltNombre = filtNombre;
		}
		public AdmiFiltro()
		{
		}
		[Field(LogicalName = "FiltFiltro", PhysicalName = "filt_filtro", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = true, IsDiscriminant = false, Alias = "Filtro")]
		public int FiltFiltro
		{
			get { return intFiltFiltro; }
			set { intFiltFiltro = value; }
		}
		[Field(LogicalName = "FormFormulario", PhysicalName = "form_formulario", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Formulario")]
		public int? FormFormulario
		{
			get { return intFormFormulario; }
			set { intFormFormulario = value; }
		}
		[Field(LogicalName = "FiltSql", PhysicalName = "filt_sql", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Sql")]
		public string FiltSql
		{
			get { return srtFiltSql; }
			set { srtFiltSql = value; }
		}
		[Field(LogicalName = "FiltPadre", PhysicalName = "filt_padre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Padre")]
		public string FiltPadre
		{
			get { return strFiltPadre; }
			set { strFiltPadre = value; }
		}
		[Field(LogicalName = "FiltLogicalname", PhysicalName = "filt_logicalname", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Logicalname")]
		public string FiltLogicalname
		{
			get { return srtFiltLogicalname; }
			set { srtFiltLogicalname = value; }
		}
		[Field(LogicalName = "FiltNombre", PhysicalName = "filt_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Logicalname")]
		public string FiltNombre
		{
			get { return srtFiltNombre; }
			set { srtFiltNombre = value; }
		}
	}
}