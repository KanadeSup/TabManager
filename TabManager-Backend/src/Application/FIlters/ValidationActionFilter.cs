using Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Filters
{
   public class ValidationActionFilter : IActionFilter
   {
      public void OnActionExecuting(ActionExecutingContext context)
      {
         if(context.ModelState.IsValid) return;
         List<ValidateErrorResponse> errors = new List<ValidateErrorResponse>();
         for(int i = 0; i < context.ModelState.Keys.Count(); i++)
         {
            var key = context.ModelState.Keys.ElementAt(i);
            var value = context.ModelState[key]!;
            if(value.Errors.Count > 0)
            {
               var error = new ValidateErrorResponse
               {
                  Field = key,
                  Messages = value.Errors.Select(x => x.ErrorMessage).ToList()
               };
               errors.Add(error);
            }
         }
         context.Result = new BadRequestObjectResult(errors);
      }

      public void OnActionExecuted(ActionExecutedContext context)
      {
      }
   }
}