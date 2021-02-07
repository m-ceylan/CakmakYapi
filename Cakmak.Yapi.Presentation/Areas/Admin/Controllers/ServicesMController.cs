using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakmak.Yapi.Core.Extensions;
using Cakmak.Yapi.Entity.Definition;
using Cakmak.Yapi.Models.Base.Response;
using Cakmak.Yapi.Presentation.Areas.Admin.Code;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Request;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.ServicesRequest;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.ServicesResponse;
using Cakmak.Yapi.Repository.Definition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Controllers
{
    [Produces("application/json")]
    [Route("admin/[controller]/[action]")]
    public class ServicesMController : BaseMController
    {
        private readonly ServicesRepository repo;

        public ServicesMController(ServicesRepository servicesRepo)
        {
            this.repo = servicesRepo;
        }

        public IActionResult Index()
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
        [HttpPost]
        public async Task<ActionResult<BaseResponse<AddServicesResponse>>> Add()
        {
            try
            {
                var a = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }


            //var formCollection = await Request.ReadFormAsync();
            //var file = formCollection.Files.First();
           

            var response = new BaseResponse<AddServicesResponse>();
            //response.Data = new AddServicesResponse();
            //var item = new Services
            //{
            //    Title = request.Title,
            //    Description = request.Description,
            //    Slug = request.Title.ToUrlSlug()  
            //};

            //await repo.AddAsync(item);
            //response.SetMessage("Kayıt başarıyla eklendi");

            //response.Data.Id = item.Id;

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<UpdateServicesResponse>>> Update([FromBody] UpdateServicesRequest request)
        {
            var response = new BaseResponse<UpdateServicesResponse>();

            var item = await repo.GetByIdAsync(request.Id);
            if (item == null)
                return NotFound();


            item.Title = request.Title;
            item.Description = request.Description;
            item.Slug = request.Title.ToUrlSlug(); ;

            await repo.UpdateAsync(item);
            response.SetMessage("Kayıt başarıyla güncellendi.");

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<DeleteServicesResponse>>> Delete([FromBody] DeleteServicesRequest request)
        {
            var response = new BaseResponse<DeleteServicesResponse>();
            response.Data = new DeleteServicesResponse();

            var item = await repo.GetByIdAsync(request.Id);
            if (item == null)
                return NotFound();

            await repo.DeleteAsync(request.Id);
            response.SetMessage("Kayıt başarıyla silindi.");

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<DeleteServicesResponse>>> BulkDelete([FromBody]BulkDeleteServicesRequest request)
        {
            var response = new BaseResponse<DeleteServicesResponse>();
            await repo.DeleteManyAsync(Builders<Services>.Filter.Where(x => request.SelectedIDs.Contains(x.Id)));
            response.SetMessage("Seçili öğeler başarıyla silindi");
            return Ok(response);
        }
       
    }
}
