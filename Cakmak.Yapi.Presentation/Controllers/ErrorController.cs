using Microsoft.AspNetCore.Mvc;

namespace Cakmak.Yapi.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult PageNotFound()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            return View();
        }
    }
}
