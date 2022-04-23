using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
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

            response.Data.Items = await repo.GetBy(x => true).ToListAsync();
            response.Data.TotalCount = response.Data.Items.Count;
            return View(response);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var item = await repo.FirstOrDefaultByAsync(x => x.Slug == id);

            if (item == null) return NotFound();

            var request = HttpContext.Request;

            var physicalPath = string.Format("{0}://{1}{2}{3}", request.Scheme, request.Host, Url.Content("~"), item.Link);

            byte[] pdfBytes = null;
            
            using (WebClient client = new WebClient())
            {
                pdfBytes = client.DownloadData(physicalPath);
            }

            MemoryStream ms = new MemoryStream(pdfBytes);

            return new FileContentResult(pdfBytes, "application/pdf");
        }
    }
}
