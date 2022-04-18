using Cakmak.Yapi.Entity.Definition;
using System.Collections.Generic;

namespace Cakmak.Yapi.Presentation.Models.Response
{
    public class LoadCatalogResponse
    {
        public int TotalCount { get; set; }
        public List<Catalog> Items{ get; set; }
    }
}
