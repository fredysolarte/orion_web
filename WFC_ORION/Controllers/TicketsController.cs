using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Services;
using WFC_ORION.Repository;


namespace WFC_ORION.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    
    public class TicketsController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public TicketsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet("{usua_usuario}")]       
        public ActionResult<IEnumerable<consulta_ticket>> GetDatosTickets(string usua_usuario)
        {
            RPTickets Obj = new RPTickets();

            return Obj.GetDatosTickets(Configuration.GetValue<string>("Settings:DefaultConnection"), usua_usuario);
        }


    }
}
