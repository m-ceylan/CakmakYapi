using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeMController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
