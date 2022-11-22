using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WFC_ORION.Models
{
    public class TB_INSPECCION
    {
        public int? ip_codigo { get; set; }
        public string ip_codemp { get; set; }
        public int? trcodter { get; set; }
        public string ip_tipo { get; set; }
        public string ip_predio { get; set; }
        public string ip_servicio { get; set; }

        //[JsonProperty("ip_fecha")]
        public string ip_fecha { get; set; }
        //[JsonIgnore]
        //public DateTime? IP_FECHA { get { return DateTime.Parse(ip_fecha, new CultureInfo("en-US")); } }
        public string ip_observaciones { get; set; }
        public string ip_estado { get; set; }
        public string ip_usuario { get; set; }
        public string ip_fecing { get; set; }
        public string at_codigo { get; set; }

    }
}
