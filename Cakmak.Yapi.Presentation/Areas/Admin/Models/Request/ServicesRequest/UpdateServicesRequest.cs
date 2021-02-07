using Cakmak.Yapi.Models.Base.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.ServicesRequest
{
    public class UpdateServicesRequest
    {
        [CustomRequiredID]
        public string Id { get; set; }

        [CustomRequired]
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
