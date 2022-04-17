using Cakmak.Yapi.Models.Base.Attributes;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.WorkRequest
{
    public class DeleteWorkRequest
    {
        [CustomRequired]
        [CustomRequiredID]
        public string Id { get; set; }
    }
}
