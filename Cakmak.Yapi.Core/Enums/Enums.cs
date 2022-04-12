using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cakmak.Yapi.Core.Enums
{
    public class Enums
    {
        public enum RegistrationStatus
        {
            Active = 1,
            InActive = 2,
            Deleted = 404
        }

        public enum UploadFolder
        {
            Contents = 1,
            [Display(Name = "images")]
            Images = 2,

            [Display(Name = "services")]
            Services = 3,
            [Display(Name = "head")]
            Head = 4,
            [Display(Name = "body")]
            Body = 5
        }

    }
}
