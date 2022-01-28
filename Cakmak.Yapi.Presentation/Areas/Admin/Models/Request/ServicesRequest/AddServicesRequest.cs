using Microsoft.AspNetCore.Http;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.ServicesRequest
{
    public class AddServicesRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public object Collection { get; set; }
    }
}
