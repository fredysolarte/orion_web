using System;
using Mapping;

namespace BE.Administracion
{
	[Serializable]
	[Table(LogicalName = "AdmiControl", PhysicalName = "admi_tcontrol", EntityId = 6, NumberColumns = 7)]
	public class AdmiControl
	{
		private int intCtrlControl;
		private int intFormFormulario;
		private string srtCtrlNombre;
		private string srtCtrlDescripcion;
		private int? intCtrlPadre;
		private int? intCtrlEstado;
		private int? intCtrlExisteforma;
		public AdmiControl(int ctrlControl, int formFormulario, string ctrlNombre, string ctrlDescripcion, int? ctrlPadre, int? ctrlEstado, int? ctrlExisteforma)
		{
			this.intCtrlControl = ctrlControl;
			this.intFormFormulario = formFormulario;
			this.srtCtrlNombre = ctrlNombre;
			this.srtCtrlDescripcion = ctrlDescripcion;
			this.intCtrlPadre = ctrlPadre;
			this.intCtrlEstado = ctrlEstado;
			this.intCtrlExisteforma = ctrlExisteforma;
		}
		public AdmiControl()
		{
		}
		[Field(LogicalName = "CtrlControl", PhysicalName = "ctrl_control", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = true, IsDiscriminant = false, Alias = "Control")]
		public int CtrlControl
		{
			get { return intCtrlControl; }
			set { intCtrlControl = value; }
		}
		[Field(LogicalName = "FormFormulario", PhysicalName = "form_formulario", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Formulario")]
		public int FormFormulario
		{
			get { return intFormFormulario; }
			set { intFormFormulario = value; }
		}
		[Field(LogicalName = "CtrlNombre", PhysicalName = "ctrl_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Nombre")]
		public string CtrlNombre
		{
			get { return srtCtrlNombre; }
			set { srtCtrlNombre = value; }
		}
		[Field(LogicalName = "CtrlDescripcion", PhysicalName = "ctrl_descripcion", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Descripcion")]
		public string CtrlDescripcion
		{
			get { return srtCtrlDescripcion; }
			set { srtCtrlDescripcion = value; }
		}
		[Field(LogicalName = "CtrlPadre", PhysicalName = "ctrl_padre", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Padre")]
		public int? CtrlPadre
		{
			get { return intCtrlPadre; }
			set { intCtrlPadre = value; }
		}
		[Field(LogicalName = "CtrlEstado", PhysicalName = "ctrl_estado", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "Estado")]
		public bool CtrlEstado
		{
			get { return intCtrlEstado == 1; }
			set { intCtrlEstado = value ? 1 : 0; }
		}

		[Field(LogicalName = "CtrlExisteforma", PhysicalName = "ctrl_existeforma", FieldType = System.Data.DbType.Boolean, PrimaryKey = false, IsSequence = false, IsDiscriminant = false, Alias = "ExisteForma")]
		public bool CtrlExisteforma
		{
			get { return intCtrlExisteforma == 1; }
			set { intCtrlExisteforma = value ? 1 : 0; }
		}
	}
}