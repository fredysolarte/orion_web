using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.Repository;

namespace WFC_ORION.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TercerosController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public TercerosController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        [HttpGet("GetTercerosMultintegralPH/{inFiltro}")]
        public ActionResult<IEnumerable<consulta_terceros_ph>> GetTercerosMultintegralPH(string inFiltro)
        {            
            RPTerceros Obj = new RPTerceros();            
            return Obj.GetTercerosMultintegralPH(Configuration.GetValue<string>("Settings:DefaultConnection"),inFiltro);
        }
    }
}
