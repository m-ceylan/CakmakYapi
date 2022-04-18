using Cakmak.Yapi.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakmak.Yapi.Entity.Definition
{
  public  class Catalog:BaseEntity
    {
        public string Title { get; set; }

        public string Link { get; set; }
        public string Slug { get; set; }
        public string HeadImage { get; set; }

    }
}
