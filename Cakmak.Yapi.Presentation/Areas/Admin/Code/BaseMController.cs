using Cakmak.Yapi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Code
{
    [Area("admin")]
    [ValidateModel]
    [Authorize]
    public class BaseMController : Controller 
    {
       
    }
}
