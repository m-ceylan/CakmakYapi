﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakmak.Yapi.Presentation.Areas.Admin.Code;
using Microsoft.AspNetCore.Mvc;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Controllers
{
    
    public class HomeMController : BaseMController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {

            return View();
        }
    }
}
