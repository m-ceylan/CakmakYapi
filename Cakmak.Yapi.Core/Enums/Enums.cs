using System;
using System.Collections.Generic;
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
            Contents=1,
            Images=2,
            Services=3,
            Head=4,
            Body=5
        }

    }
}
