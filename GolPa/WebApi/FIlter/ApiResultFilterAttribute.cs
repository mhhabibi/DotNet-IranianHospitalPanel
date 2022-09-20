using Domain.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace WebApi.FIlter
{
    public class ApiResultFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var myerrors = new List<Error>() {
                new Error("1001","این دوره برای این دپارتمان تعریف نشده است !"),
                new Error("1011","شخص انتخاب شده استاد این دوره نیست !"),
                new Error("0","مقادیر ارسال شده معتبر نیستند !"),
                new Error("-1","مقادیر ارسال شده معتبر نیستند !"),
                new Error("1021","فایلی ارسال نشده است !"),
                new Error("1100","مدیر دپارتمان ثبت نشده است  !"),
                new Error("1400","کد بیمار تکراری است  !"),
                //1600 for Personels
                new Error("1601","نام کاربری تکراریست !"),
                new Error("1602","چنین شغلی موجود نیست !"),
                new Error("1603","چنین دپارتمانی موجود نیست !"),
                new Error("1604","چنین تخصصی وجود ندارد !"),
                new Error("1605","چنین گروهی وجود ندارد !"),
                new Error("1701","چنین دوره ای وجود ندارد !"),
                new Error("1702","چنین استادی وجود ندارد !"),
                new Error("1703","این استاد دسترسی به این دوره ندارد !")
            };
            if (context.Result is OkObjectResult okObjectResult)
            {
                int res = (int.TryParse(okObjectResult.Value.ToString(), out int value)) ? Int32.Parse(okObjectResult.Value.ToString()) : 0;

                if (myerrors.Select(x => x.ErroreCode).Contains(okObjectResult.Value.ToString()) || res < 0)
                {
                    var apiResult = new ForReturn() { ResCode = Int32.Parse(okObjectResult.Value.ToString()), Message = myerrors.Where(x => x.ErroreCode == okObjectResult.Value.ToString()).Select(x => x.ErroreMessage).First(), Info = null };
                    context.Result = new JsonResult(apiResult) { StatusCode = 400 };
                }
                else
                {
                    var apiResult = new ForReturn() { ResCode = 1, Message = "عملیات با موفقیت انجام شد !", Info = (res == 0) ? okObjectResult.Value : null };
                    context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
                }
            }
            else if (context.Result is OkResult okResult)
            {
                var apiResult = new ForReturn() { ResCode = 1, Message = "خن" };
                context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
            }
            else if (context.Result is BadRequestResult badRequestResult)
            {
                var apiResult = new ForReturn() { ResCode = -1, Message = "مشکلی وجود دارد !" };
                context.Result = new JsonResult(apiResult) { StatusCode = badRequestResult.StatusCode };
            }
            //else if (context.Result is BadRequestObjectResult badRequestObjectResult)
            //{
            //    var message = badRequestObjectResult.Value.ToString();
            //    if (badRequestObjectResult.Value is SerializableError errors)
            //    {
            //        var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
            //        message = string.Join(" | ", errorMessages);
            //    }
            //    var apiResult = new ForReturn() { ResCode = -1, Message = message };
            //    context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode };
            //}
            else if (context.Result is ContentResult contentResult)
            {
                var apiResult = new ForReturn() { ResCode = -1, Message = "ریدی", Info = contentResult.Content };
                context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
            }
            else if (context.Result is NotFoundResult notFoundResult)
            {
                var apiResult = new ForReturn() { ResCode = -1, Message = "اه اه" };
                context.Result = new JsonResult(apiResult) { StatusCode = notFoundResult.StatusCode };
            }
            else if (context.Result is NotFoundObjectResult notFoundObjectResult)
            {
                var apiResult = new ForReturn() { ResCode = -1, Message = "مشکلی وجود دارد !" };
                context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
            }
            else if (context.Result is NoContentResult noContentResult)
            {
                var apiResult = new ForReturn() { ResCode = 204, Message = "چیزی یافت نشد !" };
                context.Result = new JsonResult(apiResult) { StatusCode = noContentResult.StatusCode };
            }
            //else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null
            //    && !(objectResult.Value is ApiResult))
            //{

            //    var apiResult = new ForReturn() { ResCode = -1, Message = "اه اه", Info = objectResult.Value };
            //    context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
            //}

            base.OnResultExecuting(context);
        }
    }
}
