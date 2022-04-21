using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakmak.Yapi.Core.Extensions;
using Cakmak.Yapi.Entity.Definition;
using Cakmak.Yapi.Models.Base.Response;
using Cakmak.Yapi.Presentation.Areas.Admin.Code;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.CatalogRequest;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.CatalogResponse;
using Cakmak.Yapi.Repository.Definition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Controllers
{
    public class CatalogMController : BaseMController
    {
        private readonly CatalogRepository repo;

        public CatalogMController(CatalogRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult<BaseResponse<LoadCatalogResponse>>> Get([FromBody] LoadCatalogRequest request)
        {

            var response = new BaseResponse<LoadCatalogResponse>();
            response.Data = new LoadCatalogResponse();
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
        public async Task<ActionResult<BaseResponse<AddCatalogResponse>>> Add([FromBody] AddCatalogRequest request)
        {
            var response = new BaseResponse<AddCatalogResponse>
            {
                Data = new AddCatalogResponse()
            };

            string slug = request.Title.ToUrlSlug();
            int sayac = 0;

            while (await repo.AnyAsync(x => x.Slug == slug))
            {
                sayac++;

                slug = $"{slug}-v{sayac}";
            }

            var item = new Catalog
            {
                Title = request.Title,
                Slug = slug,
                HeadImage = request.ImgUrl
            };

            await repo.AddAsync(item);
            response.SetMessage("Kayıt başarıyla eklendi");

            response.Data.Id = item.Id;

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<UpdateCatalogResponse>>> Update([FromBody] UpdateCatalogRequest request)
        {
            var response = new BaseResponse<UpdateCatalogResponse>();

            var item = await repo.GetByIdAsync(request.Id);
            if (item == null)
                return NotFound();


            item.Title = request.Title;
            item.Slug = request.Title.ToUrlSlug(); ;

            await repo.UpdateAsync(item);
            response.SetMessage("Kayıt başarıyla güncellendi.");

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<DeleteCatalogResponse>>> Delete([FromBody] DeleteCatalogRequest request)
        {
            var response = new BaseResponse<DeleteCatalogResponse>();
            response.Data = new DeleteCatalogResponse();

            var item = await repo.GetByIdAsync(request.Id);
            if (item == null)
                return NotFound();

            await repo.DeleteAsync(request.Id);
            response.SetMessage("Kayıt başarıyla silindi.");

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<DeleteCatalogResponse>>> BulkDelete([FromBody] BulkDeleteCatalogRequest request)
        {
            var response = new BaseResponse<DeleteCatalogResponse>();
            await repo.DeleteManyAsync(Builders<Catalog>.Filter.Where(x => request.SelectedIDs.Contains(x.Id)));
            response.SetMessage("Seçili öğeler başarıyla silindi");
            return Ok(response);
        }

        [HttpPost]
        public string AddHeaderPhoto()
        {
            var images = Request.Form?.Files;

            CustomFileUpload customFileUpload = new();
            var addImages = customFileUpload.UpLoadImage(
                 new Models.Request.FileUploadRequest.ImageUploadRequest()
                 {
                     Collection = images,
                     ContentCategory = Core.Enums.Enums.UploadFolder.Catalog,
                     ContentType = Core.Enums.Enums.UploadFolder.Head,
                 });

            return addImages.Items[0].Url;
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        public string AddFile()
        {
            var images = Request.Form?.Files;

            CustomFileUpload customFileUpload = new();
            var addImages = customFileUpload.UpLoadImage(
                 new Models.Request.FileUploadRequest.ImageUploadRequest()
                 {
                     Collection = images,
                     ContentCategory = Core.Enums.Enums.UploadFolder.Catalog,
                     ContentType = Core.Enums.Enums.UploadFolder.Body,
                 });

            return addImages.Items[0].Url;
        }

    }
}
