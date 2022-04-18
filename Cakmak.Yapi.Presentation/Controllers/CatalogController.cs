using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cakmak.Yapi.Models.Base.Response;
using Cakmak.Yapi.Presentation.Models.Response;
using Cakmak.Yapi.Repository.Definition;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Cakmak.Yapi.Presentation.Controllers
{
    public class CatalogController : Controller
    {

        private readonly CatalogRepository repo;
        public CatalogController(CatalogRepository _repo)
        {
            repo = _repo;
        }
        public async Task<IActionResult> Index()
        {
            var response = new BaseResponse<LoadCatalogResponse> { Data = new LoadCatalogResponse() };

            response.Data.Items = await repo.GetBy(x=>true).ToListAsync();
            response.Data.TotalCount=response.Data.Items.Count;
            return View(response);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var item = await repo.FirstOrDefaultByAsync(x=>x.Slug==id);

            if (item==null) return NotFound();

            string physicalPath = item.Link;
            byte[] pdfBytes = System.IO.File.ReadAllBytes(physicalPath);
            MemoryStream ms = new MemoryStream(pdfBytes);
            return new FileContentResult(pdfBytes, "application/pdf");
        }
    }
}
