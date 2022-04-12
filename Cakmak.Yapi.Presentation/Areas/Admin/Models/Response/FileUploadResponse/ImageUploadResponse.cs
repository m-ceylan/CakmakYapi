using Cakmak.Yapi.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Cakmak.Yapi.Core.Enums.Enums;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.FileUploadResponse
{
    public class ImageUploadResponse
    {
        public int TotalCount { get; set; }
        public List<ImageFile> Items { get; set; } = new List<ImageFile>();
    }
   
}
