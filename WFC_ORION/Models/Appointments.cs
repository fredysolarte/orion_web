using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WFC_ORION.Models
{
    //private IFormatProvider yyyymmddFormat = new System.Globalization.CultureInfo(String.Empty, false);
    public class Appointments
    {        
        public int id { get; set; }
        public string descripcion { get; set; }        
        public DateTime? inicio { get; set; }
        public DateTime? final { get; set; }
        public int? RoomID { get; set; }
        public string usuario { get; set; }
        public string RecurrenceRule { get; set; }
        public int? RecurrenceParentID { get; set; }
        public int? tipo { get; set; }
        public string  usuario_responsable { get; set; }
        public int? TK_NUMERO { get; set; }
        public int? PH_CODIGO { get; set; }
        public int? TRCODTER { get; set; }
        public string  SERVICIO { get; set; }
    }
}
