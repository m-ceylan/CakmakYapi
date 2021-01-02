using System;
using System.Collections.Generic;
using System.Text;

namespace Cakmak.Yapi.Models.Base.Request
{
   public class LoadRequest
    {
        public string SearchTerm { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
