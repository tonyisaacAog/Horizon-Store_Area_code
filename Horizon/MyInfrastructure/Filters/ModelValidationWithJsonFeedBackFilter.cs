using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInfrastructure.Filters
{
    public class ModelValidationWithJsonFeedBackFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            List<string> errors = new List<string>();
            if (!context.ModelState.IsValid)
            {
                errors.AddRange(context.ModelState.Values
                                .SelectMany(x => x.Errors)
                                .Select(x => x.ErrorMessage));

                context.Result = new JsonResult(new { errors });
            }
        }
    }
}
