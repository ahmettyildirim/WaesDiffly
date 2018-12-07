using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using WaesDiffly.Models;
/// <summary>
/// It is a generic exception filter, that catch all exceptions and returns it with customerrorresponse object
/// </summary>
namespace WaesDiffly.Filters
{
    public class CustomExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //TODO logging
            CustomErrorResponse result = new CustomErrorResponse();
            result.ErrorAction = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            result.ErrorController = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            result.ErrorMessage = actionExecutedContext.Exception.ToString();
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, result);
            base.OnException(actionExecutedContext);
        }
    }
}