using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WFC_ORION.Models;
using WFC_ORION.Repository;

namespace WFC_ORION.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspeccionController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public InspeccionController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        [HttpPost("InsertInspeccion")]        
        public ActionResult<lst_resultado> InsertInspeccion(inInspeccion inDatos)
        {
            RPInspeccion Obj = new RPInspeccion();
                                  
            lst_resultado itm = new lst_resultado();
            int ln_retorno = 0;
            try
            {                
                ln_retorno = Obj.InsertInspeccion(Configuration.GetValue<string>("Settings:DefaultConnection"), inDatos);
                itm.id = ln_retorno;
                itm.msg = inDatos.inspeccion[0].ip_codigo.ToString();

                return itm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                //lst = null;
                itm = null;
            }
        }      

        [HttpGet("getInspeccionAtencionCliente/{inFiltro}")]
        public ActionResult<IEnumerable<inAtencionClienteInspeccion>> getInpeccionAtencionCliente(string inFiltro)
        {
            RPInspeccion Obj = new RPInspeccion();
            try
            {
                return Obj.getInpeccionAtencionCliente(Configuration.GetValue<string>("Settings:DefaultConnection"), inFiltro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
    }
}
