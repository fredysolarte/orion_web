using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.Models;
using WFC_ORION.Repository;

namespace WFC_ORION.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoHDController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public PedidoHDController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //[HttpGet]
        //public IActionResult GetPedido()
        //{
        //    RPPedido Obj = new RPPedido();
        //    return Ok(Obj.GetPedidoHD());
        //}
        [HttpGet("{PHPECPED}")]
        public IActionResult GetPedido(DateTime PHPECPED)
        {
            RPPedido Obj = new RPPedido();
            return Ok(Obj.GetPedidoHD(Configuration.GetValue<string>("Settings:DefaultConnection"), PHPECPED));
        }

        [HttpGet("getPedidos/{inFiltro}")]
        public ActionResult<IEnumerable<inPedidos>> GetPedido(string inFiltro)
        {
            RPPedido Obj = new RPPedido();
            return Ok(Obj.GetPedidoHD(Configuration.GetValue<string>("Settings:DefaultConnection"), inFiltro));
        }
        [HttpPost("InsertPedido")]
        public ActionResult<lst_resultado> InsertPedido(inPedidosFL inDatos) {
            RPPedido Obj = new RPPedido();
            lst_resultado itm = new lst_resultado();
            int ln_retorno = 0;
            try
            {
                ln_retorno = Obj.InsertPedido(Configuration.GetValue<string>("Settings:DefaultConnection"), inDatos);
                itm.id = ln_retorno;
                itm.msg = "";

                return itm;
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
