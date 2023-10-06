using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GitHubPRSearch.Infrastracture
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException)
            {
                context.Result = new RedirectResult("~/Home/Error");
                context.ExceptionHandled = true;
            }
        }
    }
}
