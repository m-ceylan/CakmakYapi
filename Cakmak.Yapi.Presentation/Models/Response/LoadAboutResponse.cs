using Cakmak.Yapi.Entity.Application;
using System.Collections.Generic;

namespace Cakmak.Yapi.Presentation.Models.Response
{
    public class LoadAboutResponse
    {
        public int TotalCount { get; set; }

        public List<About> Items { get; set; }

    }
}
