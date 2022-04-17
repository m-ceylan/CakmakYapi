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
       

    }
}
