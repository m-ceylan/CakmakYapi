using Cakmak.Yapi.Models.Base.Response;
using Cakmak.Yapi.Presentation.Models.Request.ServicesRequest;
using Cakmak.Yapi.Presentation.Models.Response;
using Cakmak.Yapi.Presentation.Response.ServicesResponse;
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
            repo = _repo;
        }

        [HttpGet] 
        public async Task<IActionResult> Index()
        {
            var response = new BaseResponse<LoadServicesResponse>();
            response.Data = new LoadServicesResponse();
            var query = repo.GetBy(x => true);

            response.Data.TotalCount = await query.CountAsync();
            response.Data.Items = await query.OrderByDescending(x => x.CreateDate).ToListAsync();

            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromRoute]string id)
        {

            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var response = new GetServiceResponse();

            response.Item= await repo.FirstOrDefaultByAsync(x => x.Slug == id);

            return View(response);
        }


        [HttpPost]
        public async Task<ActionResult<BaseResponse<LoadServicesResponse>>> GetList([FromBody] LoadServicesRequest request)
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

        [HttpPost]
        public async Task<ActionResult<BaseResponse<GetServiceResponse>>> Get([FromBody] GetServiceRequest request)
        {
            var response = new BaseResponse<GetServiceResponse>();

             

            response.Data = new GetServiceResponse();


            response.Data.Item = await repo.FirstOrDefaultByAsync(x=>x.Slug==request.UrlSlug);

            return Ok(response);
        }

    }
}
