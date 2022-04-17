using System.Collections.Generic;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.WorkRequest
{
    public class BulkDeleteWorkRequest
    {

        public List<string> SelectedIDs { get; set; } = new List<string>();
    }
}
