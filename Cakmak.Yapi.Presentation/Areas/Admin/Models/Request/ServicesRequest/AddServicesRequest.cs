using Cakmak.Yapi.Models.Base.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.ServicesRequest
{
    public class AddServicesRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
