using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.ViewComponents
{
    public class NavbarViewComponent:ViewComponent
    {

        public IViewComponentResult Invoke() { return View(); }
    }
}
