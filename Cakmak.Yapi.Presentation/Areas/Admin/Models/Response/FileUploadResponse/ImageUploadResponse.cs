using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.FileUploadResponse
{
    public class ImageUploadResponse
    {
        public int TotalCount { get; set; }
        public List<string> Items { get; set; } = new List<string>();
    }
}
