using Cakmak.Yapi.Models.Base.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cakmak.Yapi.Helpers
{
    public class ValidateModel : ActionFilterAttribute
    {
        public  override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var modelStateResponse = new BaseResponse<bool?>();

            modelStateResponse.ValidationErrors = context.ModelState.Select(x => new ValidationError
            {
                Key = x.Key,
                Value = x.Value.Errors.FirstOrDefault().ErrorMessage
            }).ToList();

            if (context.ModelState.Any(x => x.Value.Errors.Any(y => y.ErrorMessage == "invalid-id")))
            {
                context.Result = new BadRequestResult();
                
                return;
            }
            else
            {
                if (modelStateResponse.ValidationErrors.Any())
                {
                    foreach (var item in modelStateResponse.ValidationErrors)
                    {
                        if (item.Value.Contains("to type 'System.Guid'"))
                            item.Value = "Lütfen listeden bir öğe seçin.";
                        else if (item.Value.Contains("is required"))
                            item.Value = "Lütfen bu alanı doldurun.";
                        else if (item.Value.Contains("not a valid e-mail address"))
                            item.Value = "Lütfen geçerli bir e-posta adresi girin.";
                        else if (item.Value.Contains("between"))
                            item.Value = "Lütfen geçerli bir değer girin.";
                        else if (item.Value.Contains("System.DateTime"))
                            item.Value = "Lütfen geçerli bir tarih seçin.";
                    }
                }

                modelStateResponse.SetErrorMessage("Lütfen hatalı alanları kontrol edin.");
                context.Result = new OkObjectResult(modelStateResponse);
            }
        }
    }
}
