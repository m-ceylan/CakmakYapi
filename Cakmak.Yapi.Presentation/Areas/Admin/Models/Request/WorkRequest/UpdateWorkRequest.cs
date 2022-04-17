using Cakmak.Yapi.Models.Base.Attributes;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.WorkRequest
{
    public class UpdateWorkRequest
    {
        [CustomRequiredID]
        public string Id { get; set; }
        [CustomRequired]
        public string Title { get; set; }
        public string Description { get; set; } 
    }
}
