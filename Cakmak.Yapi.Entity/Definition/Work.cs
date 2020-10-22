using Cakmak.Yapi.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakmak.Yapi.Entity.Definition
{
    public  class Work:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> ServicesIDs { get; set; } = new List<string>();
        public string Slug { get; set; }
        public string Url { get; set; }
        public List<string> Urls { get; set; } = new List<string>();
    }
}
