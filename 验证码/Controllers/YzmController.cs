using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace 验证码.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YzmController : ControllerBase
    {
        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="len">生成的数量默认是4</param>
        /// <returns></returns>
        //ZKWeb.System.Drawing nuget
        [HttpGet("{len}")]
        public ActionResult Get(int len=4)
        {
            int codeW = 15*len;
            int codeH = 30;
            int fontSize = 16;
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //验证码内容
            string AllCode = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //随机数
            Random r = new Random();
            string code = "";
            for (int i = 0; i < len; i++)
            {
                code += AllCode.Substring(r.Next(AllCode.Length), 1);
            }
            // HttpContext.Session.SetString("VerificationCode", verificationCode);
            //创建画布
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //话背景，干扰画噪线 
            for (int i = 0; i < 30; i++)
            {
                Pen pen = new Pen(color[r.Next(color.Length)], r.Next(1, 5));

                Point p1 = new Point(r.Next(codeW), r.Next(codeH));
                int a = r.Next(-3,3);
                int b = r.Next(-3,3);
                Point p2 = new Point(p1.X + a, p1.Y + b);
                g.DrawLine(pen, p1, p2);
            }

            //画噪线

            for (int i = 0; i < 5; i++)

            {

                int x1 = r.Next(codeW);

                int y1 = r.Next(codeH);

                int x2 = r.Next(codeW);

                int y2 = r.Next(codeH);

                Color clr = color[r.Next(color.Length)];

                g.DrawLine(new Pen(clr), x1, y1, x2, y2);

            }
            SolidBrush brush = new SolidBrush(color[r.Next(color.Length)]);
            Font f = new Font("宋体", fontSize);
            Point p = new Point(3, 3);
            g.DrawString(code, f, brush, p);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bmp.Save(ms, ImageFormat.Png);


            g.Dispose();
            bmp.Dispose();

            return File(ms.ToArray(), "image/png");
        }
    }
}