using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XUSS.DAL.BE
{
	[Serializable]
	public class CtrlCalidadUsr
	{
		public string almacen { get; set; }
		public int nro { get; set; }
		public string identificacion { get; set; }
		public string nombre { get; set; }
		public DateTime fecha { get; set; }
		public string telefono { get; set; }
		public string tp { get; set; }
		public string referencia { get; set; }
		public string novedad { get; set; }
		public string pieza { get; set; }		
	}
}
