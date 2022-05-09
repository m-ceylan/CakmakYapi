using Microsoft.AspNetCore.Mvc;

namespace Cakmak.Yapi.Presentation.Areas.Admin.ViewComponents
{
    public class FooterMViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
