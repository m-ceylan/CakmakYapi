using Cakmak.Yapi.Models.Base.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.CatalogRequest
{
    public class DeleteCatalogRequest
    {
        [CustomRequiredID]
        [CustomRequired]
        public string Id { get; set; }

    }
}
