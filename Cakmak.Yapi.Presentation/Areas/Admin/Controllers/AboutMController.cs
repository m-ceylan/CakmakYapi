﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cakmak.Yapi.Entity.Application;
using Cakmak.Yapi.Helpers;
using Cakmak.Yapi.Models.Base.Response;
using Cakmak.Yapi.Presentation.Areas.Admin.Code;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Request;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.AboutRequest;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Request.ServicesRequest;
using Cakmak.Yapi.Presentation.Areas.Admin.Models.Response.AboutResponse;
using Cakmak.Yapi.Repository.Application;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Cakmak.Yapi.Presentation.Areas.Admin.Controllers
{
    
    public class AboutMController : BaseMController
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
        public async Task<ActionResult<BaseResponse<LoadAboutResponse>>> Get([FromBody]LoadServicesRequest request)
        {
            var response = new BaseResponse<LoadAboutResponse>();
            response.Data = new LoadAboutResponse();
            var query = repo.GetBy(x => true);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(x=>x.Title.Contains(request.SearchTerm));
            }

            response.Data.TotalCount =await query.CountAsync();
            response.Data.Items = await query.OrderByDescending(x=>x.CreateDate).Skip(request.Skip).Take(request.Take).ToListAsync();



            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<AddAboutResponse>>> Add([FromBody]AddServicesRequest request)
        {
            var response = new BaseResponse<AddAboutResponse>();
            response.Data = new AddAboutResponse();
            var item = new About 
            {
             Title=request.Title
            };

            await repo.AddAsync(item);
            response.SetMessage("Kayıt başarıyla eklendi");

            response.Data.Id = item.Id;

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<UpdateAboutResponse>>> Update([FromBody]UpdateServicesRequest request)
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
        [HttpPost]
        public async Task<ActionResult<BaseResponse<DeleteAboutResponse>>> Delete([FromBody]DeleteServicesRequest request)
        {
            var response = new BaseResponse<DeleteAboutResponse>();
            response.Data = new DeleteAboutResponse();

            var item = await repo.GetByIdAsync(request.Id);
            if (item == null)
                return NotFound();

            await repo.DeleteAsync(request.Id);
            response.SetMessage("Kayıt başarıyla silindi.");

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<DeleteAboutResponse>>> BulkDelete([FromBody] BulkDeleteServicesRequest request)
        {
            var response = new BaseResponse<DeleteAboutResponse>();
            await repo.DeleteManyAsync(Builders<About>.Filter.Where(x => request.SelectedIDs.Contains(x.Id)));
            response.SetMessage("Seçili öğeler başarıyla silindi");
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<bool>>> UpdateActive([FromBody] UpdateIsActiveAboutRequest request)
        {
            var response = new BaseResponse<bool>();

            var item = await repo.GetByIdAsync(request.Id);
            if (item == null)
                return NotFound();
            item.IsActive = request.IsActive;
            await repo.UpdateAsync(item);
            response.SetMessage("Kayıt başarıyla güncellendi");
            return Ok(response);
        }

    }
}
