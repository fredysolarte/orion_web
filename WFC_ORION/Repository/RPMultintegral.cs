using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.BLL;
using WFC_ORION.DAL;
using WFC_ORION.Models;

namespace WFC_ORION.Repository
{
    public class RPMultintegral
    {
        public List<inDetallePropiedadHorizontal> getPropiedadHorizontalDetalle(string inConecction,string infiltro)
        {
            DBAccess Obj = new DBAccess();
            tb_propiedahorizontalBL ObjP = new tb_propiedahorizontalBL();
            movimientosBL ObjM = new movimientosBL();
            TercerosBL ObjT = new TercerosBL();
            correspondenciaInBL ObjCI = new correspondenciaInBL();
            correspondenciaOutBL ObjCO = new correspondenciaOutBL();
            tb_desistalacionBL ObjD = new tb_desistalacionBL();
            List<inDetallePropiedadHorizontal> retorno = new List<inDetallePropiedadHorizontal>();

            try {
                Obj.ConnectionString = inConecction;
                using (SqlDataReader reader = ObjP.getPropiedadHorizontalDetalle(inConecction, infiltro)) {
                    while (reader.Read()) {
                        inDetallePropiedadHorizontal item = new inDetallePropiedadHorizontal();

                        item.id = retorno.Count + 1;
                        item.propiedadhorizontal.Add(ObjP.getItemPropiedahorizontal(reader));
                        item.clientes.Add(ObjT.getTercerosItem(reader));
                        item.desistalacion.Add(ObjD.getItemDesistalacion(reader));
                        item.correspondenciaindt.Add(ObjCI.getItemCorrespondenciaDTIN(reader));
                        item.correspondenciaoutdt.Add(ObjCO.getItemCorrespondenciaDTOUT(reader));
                        item.movimele.Add(ObjM.getItemMovimele(reader));

                        retorno.Add(item);
                        item = null;

                    }
                }

                    return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                Obj = null;
                ObjM = null;
                ObjT = null;
                ObjD = null;
                ObjCI = null;
                ObjCO = null;
            }

        }        
    }

    public class inDetallePropiedadHorizontal { 
        public int id { get; set; }
        public string estado_co { get; set; }
        public string estado_t { get; set; }
        public List<tb_propiedahorizontal> propiedadhorizontal { get; set; } = new List<tb_propiedahorizontal>();
        public List<Terceros> clientes { get; set; } = new List<Terceros>();
        public List<tb_desistalacion> desistalacion { get; set; } = new List<tb_desistalacion>();
        public List<tb_comercial> comercial { get; set; } = new List<tb_comercial>();
        public List<tb_correspondenciadtin> correspondenciaindt { get; set; } = new List<tb_correspondenciadtin>();
        public List<tb_correspondenciadtout> correspondenciaoutdt { get; set; } = new List<tb_correspondenciadtout>();
        public List<movimele> movimele { get; set; } = new List<movimele>();
    }
}
