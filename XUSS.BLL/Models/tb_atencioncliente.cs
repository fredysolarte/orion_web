using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFC_ORION.Models
{
    public class tb_atencioncliente
    {
        public int? at_codigo { get; set; }
        public string at_codemp { get; set; }
        public int? trcodter { get; set; }
        public string at_tipoinspeccion { get; set; }
        public string at_tipopredio { get; set; }
        public string at_tipo{ get; set; }
        public string at_tipogas { get; set; }
        public string at_ctacontrato { get; set; }
        public DateTime? at_fecprogramacion { get; set; }
        public string at_responsable { get; set; }
        public string at_disisometrico { get; set; }
        public string at_planoaprobado{ get; set; }
        public string at_certlaboral { get; set; }
        public string at_competencias { get; set; }
        public string at_administrador { get; set; }
        public string at_contacto { get; set; }
        public string at_idconstructor{ get; set; }
        public string at_nomconstructor{ get; set; }
        public string at_newusd{ get; set; }
        public string at_almatriz { get; set; }
        public string at_clmatriz{ get; set; }
        public string at_cuinspeccion { get; set; }
        public string at_fecuinspeccion { get; set; }
        public string at_estado { get; set; }
        public string at_usuario { get; set; }
        public DateTime? at_fecing{ get; set; }
        public DateTime? at_fecmod { get; set; }
    }
}
