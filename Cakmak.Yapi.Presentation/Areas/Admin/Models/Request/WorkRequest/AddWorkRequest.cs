using Cakmak.Yapi.Models.Base.Attributes;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.WorkRequest
{
    public class AddWorkRequest
    {
        [CustomRequired]
        public string Title { get; set; }
        public string Description { get; set; }
        public string HeaderImageUrl { get; set; }
    }
}
