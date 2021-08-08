using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.v1.Requests;

namespace Tweetbook.Filters
{
    public class ValidationFilter :    IActionFilter // IAsyncActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //model valid not pass  
            if (!context.ModelState.IsValid)
            {
                var errosInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();
                var errorResponse = new ErrorResponse();
                foreach (var error in errosInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorModel = new ErrorModel
                        {
                            FiledName = error.Key,
                            Message = subError
                        };
                        errorResponse.Errors.Add(errorModel);
                    }
                }
                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }
        }

        //    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //    {
        //        //model valid not pass  
        //        if (!context.ModelState.IsValid)
        //        {
        //            var errosInModelState = context.ModelState
        //                .Where(x => x.Value.Errors.Count > 0)
        //                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();
        //            var errorResponse = new ErrorResponse();
        //            foreach (var error in errosInModelState)
        //            {
        //                foreach (var subError in error.Value)
        //                {
        //                    var errorModel = new ErrorModel
        //                    {
        //                        FiledName = error.Key+"-4545655544545",
        //                        Message = subError
        //                    };
        //                    errorResponse.Errors.Add(errorModel);
        //                }
        //            }
        //            context.Result = new BadRequestObjectResult(errorResponse);
        //            return;
        //        }
        //        await next();
        //    }
        }
    }
