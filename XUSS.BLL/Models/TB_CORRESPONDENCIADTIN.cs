using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFC_ORION.Models
{
    public class TB_CORRESPONDENCIADTIN
    {
        public int? CIH_CODIGO { get; set; }
        public int? CID_ITEM { get; set; }
        public int? PH_CODIGO { get; set; }
        public string CID_ASESOR { get; set; }
        public string CID_USUARIO { get; set; }
        public string CID_ESTADO { get; set; }
        public string CID_CAUSAE { get; set; }
        public DateTime? CID_FECING { get; set; }
        public DateTime? CID_FECMOD { get; set; }

    }
}
