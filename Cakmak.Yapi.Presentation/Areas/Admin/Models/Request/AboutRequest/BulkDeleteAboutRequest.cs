using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.AboutRequest
{
    public class BulkDeleteAboutRequest
    {
        public List<string> SelectedIDs { get; set; } = new List<string>();
    }
}
