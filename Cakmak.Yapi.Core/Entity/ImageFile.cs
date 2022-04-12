using MongoDB.Bson.Serialization.Attributes;
using static Cakmak.Yapi.Core.Enums.Enums;

namespace Cakmak.Yapi.Core.Entity
{
    [BsonIgnoreExtraElements]
    public class ImageFile
    {
        public string Url { get; set; }
        public UploadFolder Type { get; set; }
        public int OrderNo { get; set; }
        public bool IsActive { get; set; }
    }
}
