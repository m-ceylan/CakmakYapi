using Microsoft.AspNetCore.Http;
using static Cakmak.Yapi.Core.Enums.Enums;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.FileUploadRequest
{
    public class ImageUploadRequest
    {
        public UploadFolder ContentCategory { get; set; }
        public UploadFolder ContentType { get; set; }
        public IFormFileCollection Collection { get; set; }
        public string ImageFolderName { get; set; }
    }
}
