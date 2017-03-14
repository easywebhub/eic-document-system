using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using eic.common.Enums;
using ew.common.Entities;
using System.Web.Http;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace eic.webapi.Controllers
{
    public class BaseApiController : Controller
    {
        protected StatusCodeResult NoContent()
        {
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        protected IActionResult BadRequest()
        {
            var y = new ObjectResult(null);
            y.StatusCode = 400;
            this.Response.Headers.Add(XHeaders.X_Status, GlobalStatus.InvalidData.ToString());
            return y;
        }

        protected IActionResult NotFound()
        {
            var y = new NotFoundResult();
            this.Response.Headers.Add(XHeaders.X_Status, GlobalStatus.NotFound.ToString());
            return y;
        }

        protected IActionResult Pagination<T>(T data, int totalItems = 0, int limit = 20, int page = 1)
        {
            var x = new OkObjectResult(data);

            int totalPage = totalItems / limit;
            totalPage = totalItems % limit == 0 ? totalPage : (totalPage + 1);

            this.Response.Headers.Add(XHeaders.X_Paging_Total_Count, totalItems.ToString());
            this.Response.Headers.Add(XHeaders.X_Paging_Limit, limit.ToString());
            this.Response.Headers.Add(XHeaders.X_Paging_Total_Pages, totalPage.ToString());
            this.Response.Headers.Add(XHeaders.X_Paging_Current_Page, page.ToString());
            this.Response.Headers.Add(XHeaders.X_Status, HttpStatusCode.OK.ToString());
            return x;
        }

        protected IActionResult NoOK(string statusCode)
        {
            var y = new ObjectResult(null);
            y.StatusCode = 422;

            this.Response.Headers.Add(XHeaders.X_Status, statusCode);
            return y;
        }

        protected IActionResult NoOK()
        {
            return NoOK("Something goes wrong");
        }

        protected IActionResult NoOK(GlobalStatus statusCode)
        {
            return NoOK(statusCode.ToString());
        }

        protected IActionResult ServerError(EwhEntityBase ewhEntityBase)
        {
            var x = NoOK(ewhEntityBase.XStatus.ToString()) as ResponseMessageResult;
            if (ewhEntityBase != null)
            {
                this.Response.Headers.Add(XHeaders.X_Error_Message, ewhEntityBase.XErrorMessage ?? "");
                //x.Response.Headers.Add(EwHeaders.X_Status, ewhEntityBase.EwhStatus.ToString());
            }
            return x;
        }
    }
}
