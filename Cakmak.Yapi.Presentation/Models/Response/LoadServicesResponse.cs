using Cakmak.Yapi.Entity.Application;
using Cakmak.Yapi.Entity.Definition;
using Cakmak.Yapi.Models.Base.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Response.ServicesResponse
{
    public class LoadServicesResponse 
    {

        public int TotalCount { get; set; }

        public List<Services> Items { get; set; } = new List<Services>();
    }
}
