using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.BLL;
using WFC_ORION.Models;

namespace WFC_ORION.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ArticuloController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public ArticuloController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpGet("getArticulos")]
        public ActionResult<IEnumerable<articulo>> getArticulos(string inFiltro)
        {
            articuloBL Obj = new articuloBL();
            return Obj.getArticulos(Configuration.GetValue<string>("Settings:DefaultConnection"), inFiltro);
        }
        [HttpGet("getImagenArticulos")]
        public ActionResult<byte[]> getImagenArticulos(string artippro,string arclave1)
        {
            articuloBL Obj = new articuloBL();
            return Obj.getImagenArticulo(Configuration.GetValue<string>("Settings:DefaultConnection"), artippro,arclave1);
        }
        [HttpGet("getArticulosImagenes")]
        public ActionResult<IEnumerable<articuloimagenes>> getArticulosImagenes(string inFiltro)
        {
            articuloBL Obj = new articuloBL();
            return Obj.getArticulosImagenes(Configuration.GetValue<string>("Settings:DefaultConnection"), inFiltro);
        }

    }
}
