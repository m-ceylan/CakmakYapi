using Cakmak.Yapi.Entity.Application;
using Cakmak.Yapi.Models.Base.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.AboutResponse
{
    public class LoadAboutResponse 
    {

        public int TotalCount { get; set; }

        public List<About> Items { get; set; } = new List<About>();
    }
}
