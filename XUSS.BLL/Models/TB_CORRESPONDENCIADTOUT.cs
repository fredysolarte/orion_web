using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFC_ORION.Models
{
    public class TB_CORRESPONDENCIADTOUT
    {
        public int? COH_PK { get; set; }
        public int? COH_CODIGO { get; set; }
        public int? COD_ITEM { get; set; }
        public int? PH_CODIGO { get; set; }
        public string COD_USUARIO { get; set; }
        public string COD_ESTADO { get; set; }
        public string COD_CAUSAE { get; set; }
        public DateTime? COD_FECING { get; set; }
        public DateTime? COD_FECMOD { get; set; }

    }
}
