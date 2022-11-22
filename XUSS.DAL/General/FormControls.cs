using System;

namespace BE.Web
{
	[Serializable()]
	public class FormControls
	{
		public FormControls()
		{
		}

		public FormControls(string id, string descripcion, string idPadre)
		{
			this.id = id;
			this.descripcion = descripcion;
			this.idPadre = idPadre;
		}

		private bool estado;
		public bool Estado
		{
			get { return estado; }
			set { estado = value; }
		}

		private bool existeEnBaseDatos = false;
		public bool ExisteEnBaseDatos
		{
			get { return existeEnBaseDatos; }
			set { existeEnBaseDatos = value; }
		}

		private string id;
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		private string descripcion;
		public string Descripcion
		{
			get { return descripcion; }
			set { descripcion = value; }
		}

		private string idPadre;
		public string IdPadre
		{
			get { return idPadre; }
			set { idPadre = value; }
		}
	}
}