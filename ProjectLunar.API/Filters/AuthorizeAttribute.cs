using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectLunar.API.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProjectLunar.API.Filters
{
    public class AuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var MessageContract = new MessageResponseContract();
            if (!context.HttpContext.Request.Headers.Keys.Contains("AccessToken") || (context.HttpContext.Request.Headers["AccessToken"].Count == 0))
            {
                MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                MessageContract.Message = "Unauthorized";
                return;
            }
            else
            {
                Guid result;
                Guid.TryParse(context.HttpContext.Request.Headers["1f02a256-b093-4081-a9f5-2d55030f90b9"], out result);
                var token = result;

                if (token == Guid.Empty)
                {
                    MessageContract.Code = HttpStatusCode.BadRequest.ToString();
                    MessageContract.Message = "Unauthorized";
                    return;
                }
                if (token != new Guid("1f02a256-b093-4081-a9f5-2d55030f90b9"))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
