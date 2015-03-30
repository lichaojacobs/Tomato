using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;


namespace MVCAPP.Controllers
{
    public class CommonController : Controller
    {


        public JsonResult GetGoodsInfo()
        {
            var good = new
            {
                code = "1",
                Name = "电风扇",
                Price = "12"


            };
            return Json(good);
        }
        /// <summary>
        /// 验证图片
        /// </summary>
        /// <returns></returns>
        public FileContentResult GetImage()
        {
            MemoryStream My_Stream = Get_Images(12, 4, "#ffffff");
            return File(My_Stream.GetBuffer(), "image/Png");
        }

        private MemoryStream Get_Images(int Font_Size, int Char_Number, string BackgroundColor)
        {
            //把字符转换为图像，并且保存到内存流
            int image_w = Convert.ToInt32(Font_Size * 1.3) + Font_Size * Char_Number;
            //这个数字在调用页面需要,你要自己算出明确的数值 注意注意注意注意！！！！！
            int image_h = Convert.ToInt32(2.5 * Font_Size);
            //这个数字在调用页面需要,你要自己算出明确的数值    注意注意注意注意！！！！！

            Bitmap Temp_Bitmap = default(Bitmap);
            //封装GDI+位图 
            Graphics Temp_Graphics = default(Graphics);
            //封装GDI+绘图面
            Color Color_Back = ColorTranslator.FromHtml(BackgroundColor);
            //背景颜色 
            //Color_Back = Color.Transparent;

            Temp_Bitmap = new Bitmap(image_w, image_h, PixelFormat.Format32bppRgb);
            //注意注  确定背景大小

            Temp_Graphics = Graphics.FromImage(Temp_Bitmap);
            Temp_Graphics.FillRectangle(new SolidBrush(Color_Back), new Rectangle(0, 0, image_w, 5 * image_h));


            //注意注   绘制背景 

            //Dim Sesson_Company As String = "" '为了进行验证比较
            System.Random ran = new System.Random();
            int codeint = ran.Next(1000, 9999);
            string code = Convert.ToString(codeint);

            Session["code"] = code;   ///将结果放入session中
            Temp_Graphics.DrawString(code, new Font("Arial", 16), new SolidBrush(Color.Black), 0, 0);

            Temp_Bitmap.MakeTransparent(Color_Back);   //背景透明

            MemoryStream Temp_Stream = new MemoryStream();
            Temp_Bitmap.Save(Temp_Stream, ImageFormat.Png);
            Temp_Graphics.Dispose();
            //释放资源
            Temp_Bitmap.Dispose();
            //释放资源
            Temp_Stream.Close();
            //关闭打开的流文件
            return Temp_Stream;
            //返回流
        }

        /// <summary>
        /// 异步上传图片时候的处理函数
        /// </summary>
        /// <returns></returns>
        public ContentResult UpLoadImage()
        {
            //获得通过post方法得到的图片
          HttpPostedFileBase  image = HttpContext.Request.Files["image"];
            if (image != null)
            {
                string upLoadPath = Server.MapPath("~/Content/Images/");

                //Random rn = new Random();
                //string unique = rn.Next(10000).ToString();
                ///生成全局的唯一Id
                
                string fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.FileName);
                ///获取存储物理路径,并将图片存入指定物理路径
                string fileAdress = upLoadPath + fileName;

                image.SaveAs(fileAdress);
                ///获取要存入数据库的图片地址，并将其作为img标签的src值
                string photoAdress = "/content/Images/" + fileName;
                ///将图片url返回
                return Content(photoAdress, "text/html", System.Text.Encoding.UTF8);

            }
            else
            {
                return Content("传入错误！！");
            }

 
        }

    }
}
