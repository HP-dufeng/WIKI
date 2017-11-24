
using WIKI.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace WIKI.WebApi.Filter
{
    public class CatchExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Exception exception = actionExecutedContext.Exception;
            Logger.Instance.Error(exception, "");

            actionExecutedContext.Response = GetResponse(exception.Message);
        }

        private HttpResponseMessage GetResponse(string message)
        {
            var obj = new { message = message };

            return new HttpResponseMessage()
            {
                Content = new StringContent(message, Encoding.UTF8, "application/json"),
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }
}