using Cakmak.Yapi.Entity.Application;
using Cakmak.Yapi.Entity.Definition;
using Cakmak.Yapi.Models.Base.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.CatalogResponse
{
    public class LoadCatalogResponse 
    {

        public int TotalCount { get; set; }

        public List<Catalog> Items { get; set; } = new List<Catalog>();
    }
}
