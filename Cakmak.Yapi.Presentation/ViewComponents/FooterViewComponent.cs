using Microsoft.AspNetCore.Mvc;

namespace Cakmak.Yapi.Presentation.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
