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
    public class AdministracionController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public AdministracionController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpGet("{usua_usuario}")]
        public IActionResult GetDatosUsuario(string usua_usuario)
        {
            RPAdministracion Obj = new RPAdministracion();
            return Ok(Obj.GetDatosUsuario(Configuration.GetValue<string>("Settings:DefaultConnection"), usua_usuario));
        }
    }
}
