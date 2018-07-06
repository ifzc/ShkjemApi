using KeJianApi.App_Start;
using System;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace KeJianApi.Controllers
{

    [RequestAuthorize]
    public class UpLoadController : ApiController
    {
        /// <summary>
        /// 上传图片 form-data
        /// </summary>
        /// <returns>返回上传图片的相对路径</returns>
        [HttpPost]
        public object UploadImage()
        {
            // 检查是否是 multipart/form-data
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return new { state = 0, msg = "不是有效的'form-data'类型" };
            }

            DateTime dt = DateTime.Now;
            string path = string.Format("/imagestore/{0}/{1}{2}", dt.Year, dt.Month.ToString().PadLeft(2, '0'), dt.Day.ToString().PadLeft(2, '0'));
            string abtPath = HttpContext.Current.Server.MapPath(path);

            //判断文件夹是否存在 
            if (!Directory.Exists(abtPath))
            {
                //不存在则创建文件夹 
                Directory.CreateDirectory(abtPath);
            }

            string fileName = "";
            string ext = "";
            string filePath = "";
            try
            {
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                HttpRequestBase request = context.Request;//定义传统request对象
                HttpFileCollectionBase imgFiles = request.Files;
                for (int i = 0; i < imgFiles.Count; i++)
                {
                    ext = imgFiles[i].FileName.Substring(imgFiles[i].FileName.LastIndexOf(".") + 1, (imgFiles[i].FileName.Length - imgFiles[i].FileName.LastIndexOf(".") - 1)); //扩展名
                    fileName = string.Format("{0}.{1}", Guid.NewGuid().ToString(), ext);
                    filePath = string.Format("{0}/{1}", path, fileName);
                    imgFiles[i].SaveAs(abtPath + "\\" + fileName);
                    imgFiles[i].InputStream.Position = 0;
                }
                return filePath;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
