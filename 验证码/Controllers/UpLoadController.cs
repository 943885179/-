using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace 验证码.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpLoadController : ControllerBase
    {
        private static string fileRoot="wwwroot/";//文件根路径
        /// <summary>
        /// 单张图片上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileTypeRoot">图片所在路径</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post(IFormFile file,string fileTypeRoot="upload") {
            if (file!=null)
            {
                //var fileDir = "wwwroot/upload";// "~\\"+this.Request.Path; //AppContext.BaseDirectory+"\\upload";
                var fileDir = fileRoot + fileTypeRoot+"/"+DateTime.Now.Year+"/"+DateTime.Now.Month+"/"+DateTime.Now.Day;
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }
                //string fileName = file.FileName;
                //string filePath= fileDir + $@"\{fileName}";
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                             Path.GetExtension(file.FileName);
                var filePath = Path.Combine(fileDir, fileName);
                using (FileStream fs =System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                return Ok(new { code=200,url=this.Request.Scheme+"://"+this.Request.Host+"/"+ fileTypeRoot + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/"+fileName});
               //return new  JsonResult(Newtonsoft.Json.JsonConvert.DeserializeObject("{\"code\":200,\"url\":\""+ this.Request.Scheme + "://" + this.Request.Host + "/upload/" + fileName+"\"}"));
            }
            return null;
        }
        /// <summary>
        /// 多张张图片上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileTypeRoot">图片所在路径</param>
        /// <returns></returns>
        [HttpPost("Uploads")]
        public ActionResult Uploads(List<IFormFile> files, string fileTypeRoot = "upload")
        {
            long size = files.Sum(f => f.Length);
            //var fileDir = "wwwroot/upload";// "~\\"+this.Request.Path; //AppContext.BaseDirectory+"\\upload";
            var fileDir = fileRoot + fileTypeRoot + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            var urlList = new List<string>();
            foreach (var file in files)
            {
                if (file.Length>0)
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                              Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(fileDir, fileName);
                    using (FileStream fs = System.IO.File.Create(filePath))//等同于new FileStream(filePath, FileMode.Create)
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    urlList.Add(this.Request.Scheme + "://" + this.Request.Host + "/"+ fileTypeRoot + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day +"/" + fileName);
                }
            }
            return Ok(new { count = files.Count, size,urlList });
        }
        /// <summary>
        /// 多张张图片上传_new
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileTypeRoot">图片所在路径</param>
        /// <returns></returns>
        [HttpPost("UploadsNew")]
        public ActionResult Uploads(IFormFileCollection files, string fileTypeRoot = "upload")
        {
            long size = files.Sum(f => f.Length);
            //var fileDir = "wwwroot/upload";// "~\\"+this.Request.Path; //AppContext.BaseDirectory+"\\upload";
            var fileDir = fileRoot + fileTypeRoot + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            var urlList = new List<string>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                              Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(fileDir, fileName);
                    using (FileStream fs = System.IO.File.Create(filePath))//等同于new FileStream(filePath, FileMode.Create)
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    urlList.Add(this.Request.Scheme + "://" + this.Request.Host + "/" + fileTypeRoot + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + fileName);
                }
            }
            return Ok(new { count = files.Count, size, urlList });
        }
        /// <summary>
        /// 多张张图片上传_ByRequest
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileTypeRoot">图片所在路径</param>
        /// <returns></returns>
        [HttpPost("UploadsByRequest")]
        public ActionResult Uploads(string fileTypeRoot = "upload")
        {
            var files = this.Request.Form.Files;
            long size = files.Sum(f => f.Length);
            //var fileDir = "wwwroot/upload";// "~\\"+this.Request.Path; //AppContext.BaseDirectory+"\\upload";
            var fileDir = fileRoot + fileTypeRoot + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            var urlList = new List<string>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                              Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(fileDir, fileName);
                    using (FileStream fs = System.IO.File.Create(filePath))//等同于new FileStream(filePath, FileMode.Create)
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    urlList.Add(this.Request.Scheme + "://" + this.Request.Host + "/" + fileTypeRoot + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + fileName);
                }
            }
            return Ok(new { count = files.Count, size, urlList });
        }
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileName">文件名称(加后缀)1.png</param>
        /// <param name="fileTypeRoot">图片所在路径</param>
        /// <returns></returns>
        [HttpGet("DownLoad/{fileTypeRoot=upload}/{fileName}")]
        public ActionResult DownLoad(string fileName, string fileTypeRoot = "upload") {
            try
            {
                var stream = System.IO.File.OpenRead(fileRoot + "/" + fileTypeRoot + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + fileName);
                //var memi = MimeMapping.GetMimeMapping(addrUrl);
                var memi = new FileExtensionContentTypeProvider().Mappings[Path.GetExtension(fileName)];
                return File(stream, memi, Path.GetFileName(fileName));
            }
            catch (Exception ex)
            {
                //return new HttpStatusCodeResult(500, "System Error");
                // return new NotFoundResult();
                 return new StatusCodeResult(404);
               // return new BadRequestObjectResult(ex.ToString());
            }
        }
        /// <summary>
        /// 添加文字水印
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileTypeRoot"></param>
        /// <returns></returns>
        [HttpPost("UploadAddText")]
        public IActionResult UploadAddText(IFormFile file,string fileTypeRoot="upload")
        {
            try
            {
                if (null == file)
                {
                    return BadRequest();
                }
                if (file.Length > 0)
                {
                    var fileDir = fileRoot + fileTypeRoot + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
                    if (!Directory.Exists(fileDir))
                    {
                        Directory.CreateDirectory(fileDir);
                    }
                    //string fileName = file.FileName;
                    //string filePath= fileDir + $@"\{fileName}";
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                                 Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(fileDir, fileName);
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        // Add watermark
                        var watermarkedStream = new MemoryStream();
                        using (var img = Image.FromStream(stream))
                        {
                            using (var graphic = Graphics.FromImage(img))
                            {
                                var font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold, GraphicsUnit.Pixel);
                                var color = Color.Red;//FromArgb(128, 255, 255, 255);
                                var brush = new SolidBrush(color);
                                var point = new Point(img.Width - 120, img.Height - 30);

                                graphic.DrawString("weixiao", font, brush, point);
                                img.Save(watermarkedStream, ImageFormat.Png);
                            }
                            img.Save(filePath);

                        }
                    }
                    return Ok(new { code = 200, url = this.Request.Scheme + "://" + this.Request.Host + "/" + fileTypeRoot + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + fileName });
                    // return StatusCode(StatusCodes.Status200OK);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}