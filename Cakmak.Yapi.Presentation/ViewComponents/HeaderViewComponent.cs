using Microsoft.AspNetCore.Mvc;

namespace Cakmak.Yapi.Presentation.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
