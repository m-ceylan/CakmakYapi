using Cakmak.Yapi.Models.Base.Attributes;

namespace Cakmak.Yapi.Presentation.Models.Request.ServicesRequest
{
    public class GetServiceRequest
    {
        [CustomRequired]
        public string UrlSlug { get; set; }
    }
}
