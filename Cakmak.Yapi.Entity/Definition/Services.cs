using Cakmak.Yapi.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakmak.Yapi.Entity.Definition
{
   public class Services:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string HeadImageUrl { get; set; }
        public List<ImageFile> Images { get; set; } = new List<ImageFile>();
        public string Slug { get; set; }
    }
}
