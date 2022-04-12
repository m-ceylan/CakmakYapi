using Cakmak.Yapi.Models.Base.Response;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.ServicesRequest;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.ServicesResponse;
using Cakmak.Yapi.Repository.Definition;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Controllers
{
    public class ServicesController : Controller
    {

        private readonly ServicesRepository repo;

        public ServicesController(ServicesRepository _repo)
        {
            repo=_repo;
        }

        public IActionResult Index()
        {
       
            return View();
        }

        public IActionResult Detail()
        {

            return View();
        }



        [HttpPost]
        public async Task<ActionResult<BaseResponse<LoadServicesResponse>>> Get([FromBody] LoadServicesRequest request)
        {
            var response = new BaseResponse<LoadServicesResponse>();
            response.Data = new LoadServicesResponse();
            var query = repo.GetBy(x => true);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(x => x.Title.Contains(request.SearchTerm));
            }

            response.Data.TotalCount = await query.CountAsync();
            response.Data.Items = await query.OrderByDescending(x => x.CreateDate).Skip(request.Skip).Take(request.Take).ToListAsync();

            return Ok(response);
        }

    }
}
