
using Cakmak.Yapi.Core.Entity;
using Cakmak.Yapi.Core.Extensions;
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

        [HttpPost]
        public string FTPFileUpload(IFormFileCollection collection)
        {
            try
            {
                string FTPDosyaYolu = "ftp://94.73.170.187/ImageDeneme";
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
                    collection[0].CopyTo(ms);
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
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
            }


            return "";

        }
        public ImageUploadResponse UpLoadImage(ImageUploadRequest imageRequest)
        {

            if (!imageRequest.Collection.Any())
                return null;

            var response = new ImageUploadResponse();

            string rootPath = Cakmak.Yapi.Core.Constant.rootPath;
            string imagesPath = Cakmak.Yapi.Core.Constant.imagesPath;

            string imgUrl = $"/{imagesPath}/{imageRequest.ContentCategory.GetEnumDisplayName()}";

            ExistDirectory(rootPath+imgUrl);

            imgUrl += $"/{imageRequest.ContentType.GetEnumDisplayName()}";

            ExistDirectory(rootPath+imgUrl);

            imgUrl += $"/{imageRequest.ImageFolderName}";

            ExistDirectory(rootPath+imgUrl);

            var urls = ImageCollectionCopy(imageRequest, imgUrl);

            for (int i = 0; i < urls.Count; i++)
            {
                response.Items.Add(
                    new ImageFile
                    {
                        Url = urls[i],
                        OrderNo = i + 1,
                        Type = imageRequest.ContentType,
                        IsActive = true,
                    });
            }

            return response;
        }

        private List<string> ImageCollectionCopy(ImageUploadRequest imageRequest, string imgUrl)
        {
            List<string> imagesUrls = new();
            string path = string.Empty;
            foreach (var item in imageRequest.Collection)
            {
                string imageExtension = Path.GetExtension(item.FileName);

                //if (imageExtension != Cakmak.Yapi.Core.Constant.JPG_EXTENSION &&
                //    imageExtension != Cakmak.Yapi.Core.Constant.JPEG_EXTENSION &&
                //    imageExtension != Cakmak.Yapi.Core.Constant.PNG_EXTENSION)
                //{
                //    continue;
                //}

                string imageName = Guid.NewGuid() + imageExtension;
                path = $"{Cakmak.Yapi.Core.Constant.rootPath}{imgUrl}/{imageName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    item.CopyTo(stream);
                }
                imagesUrls.Add($"{imgUrl}/{imageName}");
            }

            return imagesUrls;
        }

        void ExistDirectory(string path)
        {
            if (Directory.Exists(path)) return;

            Directory.CreateDirectory(path);

        }
    }
}
