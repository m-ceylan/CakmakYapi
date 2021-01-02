using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cakmak.Yapi.Entity.Application;
using Cakmak.Yapi.Helpers;
using Cakmak.Yapi.Models.Base.Response;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Request;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.AboutRequest;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.AboutResponse;
using Cakmak.Yapi.Repository.Application;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Controllers
{
    [Area("admin")]
    [ValidateModel]
    public class AboutMController : Controller
    {
        private readonly AboutRepository repo;

        public AboutMController(AboutRepository aboutRepo)
        {
            this.repo = aboutRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult<BaseResponse<LoadAboutResponse>>> GetAbouts([FromBody]LoadAboutRequest request)
        {
            var response = new BaseResponse<LoadAboutResponse>();
            response.Data = new LoadAboutResponse();
            var query = repo.GetBy(x => true);


            response.Data.TotalCount =await query.CountAsync();


            response.Data.Items = await query.OrderByDescending(x=>x.CreateDate).Skip(request.Skip).Take(request.Take).ToListAsync();



            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<AddAboutResponse>>> AddAbout([FromBody]AddAboutRequest request)
        {

            var response = new BaseResponse<AddAboutResponse>();
            var item = new About 
            {
             Title=request.Title
            };

            await repo.AddAsync(item);
            response.SetMessage("Kayıt başarıyla eklendi");

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<UpdateAboutResponse>>> UpdateAbout([FromBody]UpdateAboutRequest request)
        {
            var response = new BaseResponse<UpdateAboutResponse>();

            var item = await repo.GetByIdAsync(request.Id);
            if (item == null)
                return NotFound();


            item.Title = request.Title;

            await repo.UpdateAsync(item);
            response.SetMessage("Kayıt başarıyla güncellendi.");

            return Ok(response);
        }

    }
}
