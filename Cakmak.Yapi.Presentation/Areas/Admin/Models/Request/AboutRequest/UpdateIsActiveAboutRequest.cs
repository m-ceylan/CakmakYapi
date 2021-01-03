﻿using Cakmak.Yapi.Models.Base.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.AboutRequest
{
    public class UpdateIsActiveAboutRequest
    {
        [CustomRequiredID]
        [CustomRequired]
        public string Id { get; set; }
        public bool IsActive { get; set; }
    }
}
