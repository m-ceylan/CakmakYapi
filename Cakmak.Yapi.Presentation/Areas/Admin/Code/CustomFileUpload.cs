
using Cakmak.Yapi.Models.Base.Response;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.FileUploadRequest;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.FileUploadResponse;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Code
{
    public class CustomFileUpload : BaseMController
    {
        private readonly IHostingEnvironment env;

        public CustomFileUpload(IHostingEnvironment env)
        {
            this.env = env;
        }
        [HttpPost]
        public string FTPFileUpload()
        {
            //UpLoadImage();
            var c = "";
            try
            {
                var a = Request.Form.Files;

                string FTPDosyaYolu = "ftp://94.73.170.187//httpdocs/wwwroot/files";
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPDosyaYolu);

                string username = "u9481094";
                string password = "tr1H9!5k";
                request.Credentials = new NetworkCredential(username, password);

                request.UsePassive = true; // pasif olarak kullanabilme
                request.UseBinary = true; // aktarım binary ile olacak
                request.KeepAlive = false; // sürekli açık tutma
                request.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] fileContents;

                using (var ms = new MemoryStream())
                {
                    a[0].CopyTo(ms);
                    fileContents = ms.ToArray();
                }


                using (Stream requestStream = request.GetRequestStream())
                {

                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                }




            }
            catch (Exception ex)
            {
                c = ex.Message;

            }


            return c;

        }


        public BaseResponse<ImageUploadResponse> UpLoadImage(ImageUploadRequest imageRequest )
        {
 
            var response = new BaseResponse<ImageUploadResponse>();
            response.Data = new ImageUploadResponse();

            string imageCategory = string.Empty;
            string imageType = string.Empty;
            switch (imageRequest.ContentCategory)
            {
                case Core.Enums.Enums.UploadFolder.Services:
                    imageCategory = "services";
                    break;
            }
            switch (imageRequest.ContentType)
            {
                case Core.Enums.Enums.UploadFolder.Head:
                    imageType = "header";
                    break;
                case Core.Enums.Enums.UploadFolder.Body:
                    imageType = "body";
                    break;
            }



            var a = Request.Form.Files[0];
            string imageExtension = Path.GetExtension(a.FileName);

            if (imageExtension!="jpg" || imageExtension != "jpeg" || imageExtension != "png")
            {

            }

            string imageName = Guid.NewGuid() + imageExtension;
           
            string imgUrl = $"content/images/{imageCategory}/{imageType}/{imageName}";
            string path = Path.Combine(env.WebRootPath, imgUrl);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                a.CopyTo(stream);
            }

            return null;
        }
    }
}
