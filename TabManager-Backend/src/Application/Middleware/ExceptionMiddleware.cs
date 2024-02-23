using Common.Exceptions;

namespace Application.Middleware
{
   public class ExceptionMiddleware
   {
      private readonly RequestDelegate _next;
      private readonly IWebHostEnvironment _env;
      public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
      {
         _next = next;
         _env = env;
      }
      public async Task Invoke(HttpContext context)
      {
         try
         {
            await _next(context);
         }
         catch(CustomException e)
         {
            context.Response.StatusCode = e.StatusCode;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(e.Message);
         }
         catch(Exception e)
         {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "text/plain";
            if(_env.IsDevelopment())
               await context.Response.WriteAsync(e.ToString());
            else
               await context.Response.WriteAsync("The error occured by server.");
         }
      }
   }

   public static class ExceptionMiddlewareExtensions
   {
      public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
      {
         return builder.UseMiddleware<ExceptionMiddleware>();
      }
   }
}