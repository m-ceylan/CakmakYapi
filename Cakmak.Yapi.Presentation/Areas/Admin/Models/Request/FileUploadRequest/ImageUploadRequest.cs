using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Cakmak.Yapi.Core.Enums.Enums;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.FileUploadRequest
{
    public class ImageUploadRequest
    {
        public UploadFolder ContentCategory { get; set; }
        public UploadFolder ContentType { get; set; }
    }
}
