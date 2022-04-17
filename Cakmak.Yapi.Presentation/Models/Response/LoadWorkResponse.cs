using Cakmak.Yapi.Entity.Definition;
using System.Collections.Generic;

namespace Cakmak.Yapi.Presentation.Models.Response
{
    public class LoadWorkResponse
    {
        public int TotalCount { get; set; }

        public List<Work> Items { get; set; }
    }
}
