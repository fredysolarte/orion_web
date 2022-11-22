using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFC_ORION.Models
{
    public class TicketHD
    {
        public int TK_NUMERO { get; set; }
        public string TK_RESPONSABLE { get; set; }
        public string TK_PROPIETARIO { get; set; }
        public string TK_PRIORIDAD { get; set; }
        public string TK_ASUNTO { get; set; }
        public string TK_OBSERVACIONES { get; set; }
        public string TK_ESTADO { get; set; }
        public DateTime TK_FECING { get; set; }
        public DateTime? TK_FECVEN { get; set; }
        public DateTime? TK_FECFIN { get; set; }
        public int? PH_CODIGO { get; set; }
        public string TK_TIPO { get; set; }
        public int? AT_CODIGO { get; set; }
    }
}
