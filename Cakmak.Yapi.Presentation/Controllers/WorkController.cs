using Cakmak.Yapi.Models.Base.Response;
using Cakmak.Yapi.Presentation.Models.Response;
using Cakmak.Yapi.Repository.Definition;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Controllers
{
    public class WorkController : Controller
    {

        private readonly WorkRepository repo;

        public WorkController(WorkRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IActionResult> Index()
        {
            var response = new BaseResponse<LoadWorkResponse>();
            response.Data = new LoadWorkResponse();
            var query = repo.GetBy(x => true);

            response.Data.TotalCount = await query.CountAsync();
            response.Data.Items = await query.OrderByDescending(x => x.CreateDate).ToListAsync();
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromRoute] string id)
        {

            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var response = new GetWorkResponse();

            response.Item = await repo.FirstOrDefaultByAsync(x => x.Slug == id);

            return View(response);
        }

    }
}
