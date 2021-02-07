using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Code
{
    public class FileUpload:BaseMController
    {
        public string FTPFileUpload()
        {

            var a = Request.Form.Files[0];

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://www.contoso.com/test.htm");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");

            // Copy the contents of the file to the request stream.
            byte[] fileContents;
            using (StreamReader sourceStream = new StreamReader("testfile.txt"))
            {
                fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            }

            request.ContentLength = fileContents.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }

            return "";

        }


        public static void UpLoadImage(Stream image, string target)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://www.site2.com/images/" + target);
            req.UseBinary = true;
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.Credentials = new NetworkCredential("myUser", "myPass");
            StreamReader rdr = new StreamReader(image);
            byte[] fileData = Encoding.UTF8.GetBytes(rdr.ReadToEnd());

            rdr.Close();
            req.ContentLength = fileData.Length;
            Stream reqStream = req.GetRequestStream();
            reqStream.Write(fileData, 0, fileData.Length);
            reqStream.Close();
        }
    }
}
