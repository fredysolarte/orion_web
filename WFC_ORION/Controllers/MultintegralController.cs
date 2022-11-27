using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using WFC_ORION.Models;
using WFC_ORION.Repository;

namespace WFC_ORION.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MultintegralController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public MultintegralController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        [HttpGet("getDetallePH/{inFiltro}")]
        public ActionResult<IEnumerable<consulta_terceros_ph>> GetTercerosMultintegralPH(string inFiltro)
        {            
            RPTerceros Obj = new RPTerceros();            
            return Obj.GetTercerosMultintegralPH(Configuration.GetValue<string>("Settings:DefaultConnection"),inFiltro);
        }

        [HttpGet("getDetallePropiedadHorizontal/{inFiltro}")]
        public ActionResult<IEnumerable<inDetallePropiedadHorizontal>> getDetallePropiedadHorizontal(string inFiltro)
        {
            RPMultintegral Obj = new RPMultintegral();
            return Obj.getPropiedadHorizontalDetalle(Configuration.GetValue<string>("Settings:DefaultConnection"), inFiltro);
        }

        [HttpPost("insertPropiedadHorizontal")]
        public ActionResult<lst_resultado> insertPropiedadHorizontal(inDetallePropiedadHorizontal inDatos)
        {
            RPMultintegral Obj = new RPMultintegral();

            lst_resultado itm = new lst_resultado();
            int ln_retorno = 0;
            try
            {
                ln_retorno = Obj.insertPropiedadHorizontal(Configuration.GetValue<string>("Settings:DefaultConnection"), inDatos);
                itm.id = ln_retorno;
                itm.msg = "Ok";

                return itm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;                
                itm = null;
            }
        }
    }
}
