using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace 验证码.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpLoadController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(this.Request.Path);
        }
        [HttpPost]
        public ActionResult Post(IFormFile file) {
            if (file!=null)
            {
                var fileDir = "wwwroot/upload";// "~\\"+this.Request.Path; //AppContext.BaseDirectory+"\\upload";
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }
                string fileName = file.FileName;
                string filePath= fileDir + $@"\{fileName}";
                using (FileStream fs =System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
               return new  JsonResult(Newtonsoft.Json.JsonConvert.DeserializeObject("{\"code\":200,\"url\":\""+ this.Request.Scheme + "://" + this.Request.Host + "/upload/" + fileName+"\"}"));
            }
            return null;
        }
    }
}