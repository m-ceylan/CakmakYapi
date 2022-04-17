using Cakmak.Yapi.Core.Extensions;
using Cakmak.Yapi.Entity.Definition;
using Cakmak.Yapi.Models.Base.Response;
using Cakmak.Yapi.Presentation.Areas.Admin.Code;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.WorkRequest;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.WorkResponse;
using Cakmak.Yapi.Repository.Definition;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Controllers
{
    public class WorkMController : BaseMController
    {
        private readonly WorkRepository repo;

        public WorkMController(WorkRepository _repo)
        {
            repo = _repo;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult<BaseResponse<LoadWorkResponse>>> Get([FromBody] LoadWorkRequest request)
        {
            var response = new BaseResponse<LoadWorkResponse>();
            response.Data = new LoadWorkResponse();
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
        public async Task<ActionResult<BaseResponse<AddWorkResponse>>> Add()
        {
            var response = new BaseResponse<AddWorkResponse>();
            var request = new AddWorkRequest();

            request.Title = Request.Form.Keys.Any(x => x == "title") ?
                Request.Form["title"].ToString() : "";

            request.Description = Request.Form.Keys.Any(x => x == "description") ?
                Request.Form["description"].ToString() : "";

            request.HeaderImageUrl = Request.Form.Keys.Any(x => x == "headerImageUrl") ?
               Request.Form["headerImageUrl"].ToString() : "";

            var images = Request.Form?.Files;
            CustomFileUpload customFileUpload = new();
            var addImages = customFileUpload.UpLoadImage(
                 new Models.Request.FileUploadRequest.ImageUploadRequest()
                 {
                     Collection = images,
                     ContentCategory = Core.Enums.Enums.UploadFolder.Work,
                     ContentType = Core.Enums.Enums.UploadFolder.Body,
                     ImageFolderName = request.Title.ToUrlSlug()
                 });


            string slug = request.Title.ToUrlSlug();
            int sayac = 0;

            while (await repo.AnyAsync(x => x.Slug == slug))
            {
                sayac++;

                slug = $"{slug}-v{sayac}";
            }
             

            response.Data = new AddWorkResponse();
            var item = new Work
            {
                Title = request.Title,
                Description = request.Description,
                Slug = slug,
                HeadImageUrl = request.HeaderImageUrl,
                Images = addImages.Items,
            };

           
            await repo.AddAsync(item);
            response.SetMessage("Kayıt başarıyla eklendi");

            response.Data.Id = item.Id;

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<UpdateWorkResponse>>> Update([FromBody] UpdateWorkRequest request)
        {
            var response = new BaseResponse<UpdateWorkResponse>();

            var item = await repo.GetByIdAsync(request.Id);
            if (item == null)
                return NotFound();


            item.Title = request.Title;
            item.Description = request.Description;

            await repo.UpdateAsync(item);
            response.SetMessage("Kayıt başarıyla güncellendi.");

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<DeleteWorkResponse>>> Delete([FromBody] DeleteWorkRequest request)
        {
            var response = new BaseResponse<DeleteWorkResponse>();
            response.Data = new DeleteWorkResponse();

            var item = await repo.GetByIdAsync(request.Id);
            if (item == null)
                return NotFound();

            await repo.DeleteAsync(request.Id);
            response.SetMessage("Kayıt başarıyla silindi.");

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<DeleteWorkResponse>>> BulkDelete([FromBody] BulkDeleteWorkRequest request)
        {
            var response = new BaseResponse<DeleteWorkResponse>();
            await repo.DeleteManyAsync(Builders<Work>.Filter.Where(x => request.SelectedIDs.Contains(x.Id)));
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
                     ContentCategory = Core.Enums.Enums.UploadFolder.Work,
                     ContentType = Core.Enums.Enums.UploadFolder.Head,
                 });
            return addImages.Items[0].Url;
        }
    }
}
