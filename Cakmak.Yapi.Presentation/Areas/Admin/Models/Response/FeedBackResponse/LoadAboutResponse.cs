using Cakmak.Yapi.Entity.Application;
using Cakmak.Yapi.Models.Base.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.FeedBackResponse
{
    public class LoadFeedBackResponse
    {

        public int TotalCount { get; set; }

        public List<FeedBack> Items { get; set; } = new List<FeedBack>();
    }
}
