﻿using Cakmak.Yapi.Models.Base.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.AboutRequest
{
    public class AddAboutRequest
    {
        [CustomRequired]
        public string Title { get; set; }

    }
}
