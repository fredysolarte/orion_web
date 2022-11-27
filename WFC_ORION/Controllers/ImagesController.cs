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
    public class ImagesController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public ImagesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //Dictionary<string, object> inDatos
        [HttpPost("UploadImageInspeccion")]
        public ActionResult<lst_resultado> UploadImageInspeccion(int inretorno,int tipo, int consecutivo, string nombre, string fechaHora,List<IFormFile> image)
        {
            RPInspeccion Obj = new RPInspeccion();
            lst_resultado itm = new lst_resultado();
            
            int ln_retorno = 0;
            try
            {
                ln_retorno = Obj.InsertEvidenciaInspeccion(Configuration.GetValue<string>("Settings:DefaultConnection"), consecutivo, tipo, Convert.ToDateTime(fechaHora.Replace("\"", "")), image);
                itm.id = inretorno;
                itm.error = fechaHora.Replace("\"", "");

                return itm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                itm = null;
            }
        }

        [HttpPost("UploadImagePedido")]
        public ActionResult<lst_resultado> UploadImagePedido(int consecutivo, string usuario, List<IFormFile> image) {
            RPPedido Obj = new RPPedido();
            lst_resultado itm = new lst_resultado();

            int ln_retorno = 0;
            try
            {
                ln_retorno = Obj.InsertEvidenciaPedido(Configuration.GetValue<string>("Settings:DefaultConnection"), "001", consecutivo, usuario, image);
                itm.id = 0;
                itm.error = "";

                return itm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                itm = null;
            }
        }

        //Dictionary<string, object> inDatos
        [HttpPost("UploadImageComercialPH")]
        public ActionResult<lst_resultado> UploadImageComercialPH(int inretorno, int tipo, int consecutivo, string nombre, string usuario,string fechaHora, List<IFormFile> image)
        {
            RPMultintegral Obj = new RPMultintegral();
            lst_resultado itm = new lst_resultado();

            int ln_retorno = 0;
            try
            {
                ln_retorno = Obj.InsertEvidenciaComercial(Configuration.GetValue<string>("Settings:DefaultConnection"), "001", consecutivo, tipo, usuario, image);
                itm.id = inretorno;
                itm.error = "";

                return itm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                itm = null;
            }
        }
    }
}
