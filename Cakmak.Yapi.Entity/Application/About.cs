using Cakmak.Yapi.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakmak.Yapi.Entity.Application
{
   public class About :BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
    }
}
