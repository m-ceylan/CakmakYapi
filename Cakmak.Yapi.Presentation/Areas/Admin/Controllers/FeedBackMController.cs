using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakmak.Yapi.Models.Base.Response;
using Cakmak.Yapi.Presentation.Areas.Admin.Code;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.FeedBackRequest;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.FeedBackResponse;
using Cakmak.Yapi.Repository.Application;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Controllers
{
    public class FeedBackMController : BaseMController
    {
        private readonly FeedBackRepository repo;

        public FeedBackMController(FeedBackRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<LoadFeedBackResponse>>> Get([FromBody] LoadFeedBackRequest request)
        {

            

            var response = new BaseResponse<LoadFeedBackResponse>();
            response.Data = new LoadFeedBackResponse();
            var query = repo.GetBy(x => true);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x => x.Email.Contains(request.SearchTerm));

            response.Data.TotalCount = await query.CountAsync();
            response.Data.Items = await query.OrderByDescending(x => x.CreateDate).Skip(request.Skip).Take(request.Take).ToListAsync();

            return Ok(response);
        }
    }
}
