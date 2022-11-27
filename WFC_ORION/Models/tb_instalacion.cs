using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFC_ORION.Models
{
    public class tb_instalacion
    {
        public int? it_codigo { get; set; }
        public string it_codemp { get; set; }
        public int? ph_codigo { get; set; }
        public int? meidmovi { get; set; }
        public int? meiditem { get; set; }
        public int? meidlote { get; set; }
        public int? meidelem { get; set; }
        public string it_observaciones { get; set; }
        public string it_fecha { get; set; }
        public string it_estado { get; set; }
        public string it_usuario { get; set; }
        public string it_fecing { get; set; }        
    }
}
