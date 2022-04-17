using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakmak.Yapi.Models.Base.Response;
using Cakmak.Yapi.Presentation.Models.Response;
using Cakmak.Yapi.Repository.Application;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Cakmak.Yapi.Presentation.Controllers
{
    public class AboutController : Controller
    {
        private readonly AboutRepository repo;

        public AboutController(AboutRepository _repo)
        {
            repo=_repo;
        }
        public async Task<IActionResult> Index()
        {
           var response = await repo.FirstOrDefaultByAsync (x=>x.IsActive);

            return View(response);
        }
    }
}
